using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Security.Principal;
using FakeItEasy;

namespace Hx.MvcNavigation.Tests
{
    [TestClass]
    public class NavigationTests
    {
        [TestMethod]
        public void DeclarativeConstructionTest()
        {
            var menu = GenerateMenu();

            Assert.IsNotNull(menu);
            Assert.IsNotNull(menu.Children);
            Assert.AreEqual<int>(2, menu.Children.Count);
            Assert.AreEqual<int>(1, menu.Children[0].Children.Count);
            Assert.IsNotNull(menu.Children[0].Children[0].Children);
            Assert.AreEqual<int>(0, menu.Children[0].Children[0].Children.Count);
            
        }

        [TestMethod]
        public void GetFullMenuTest()
        {
            IAuthorizationService authService = A.Fake<IAuthorizationService>();
            MvcNavigationService service = new MvcNavigationService(authService);
            service.AddNavigationNode(this.GenerateMenu());
            var full = service.GetNavigationMap();
            var menu = full.First();
            Assert.IsNotNull(menu);
            Assert.IsNotNull(menu.Children);
            Assert.AreEqual<int>(2, menu.Children.Count);
            Assert.AreEqual<int>(1, menu.Children[0].Children.Count);
            Assert.IsNotNull(menu.Children[0].Children[0].Children);
            Assert.AreEqual<int>(0, menu.Children[0].Children[0].Children.Count);
        }

        [TestMethod]
        public void GetMenuWithNoAuthorizedItemsTest()
        {
            IAuthorizationService authService = A.Fake<IAuthorizationService>();
            A.CallTo(() => authService.GetRolesForUser(A<string>.Ignored)).Returns(new string[] { "no match" });
            MvcNavigationService service = new MvcNavigationService(authService);
            service.AddNavigationNode(this.GenerateMenu());

            // A.CallTo(() => foo.Bar(A<string>.Ignored, "second argument")).Throws(new Exception());
            var userNav = service.GetNavigationForUser(new GenericIdentity("testuser"));

            Assert.IsNotNull(userNav);
            Assert.AreEqual<int>(0, userNav.Length);
        }

        [TestMethod]
        public void GetMenuAuthorizedForRootItemWithGrantTest()
        {
            IAuthorizationService authService = A.Fake<IAuthorizationService>();
            A.CallTo(() => authService.GetRolesForUser(A<string>.Ignored)).Returns(new string[] { "RootPermission" });
            MvcNavigationService service = new MvcNavigationService(authService);
            service.AddNavigationNode(this.GenerateMenu());

            // A.CallTo(() => foo.Bar(A<string>.Ignored, "second argument")).Throws(new Exception());
            var userNav = service.GetNavigationForUser(new GenericIdentity(""));

            Assert.IsNotNull(userNav);
            Assert.AreEqual<int>(1, userNav.Length);
        }


        [TestMethod]
        public void GetMenuAuthorizedForRootItemWithDenyTest()
        {
            IAuthorizationService authService = A.Fake<IAuthorizationService>();
            A.CallTo(() => authService.GetRolesForUser(A<string>.Ignored)).Returns(new string[] { "RootPermission" });
            MvcNavigationService service = new MvcNavigationService(authService);
            var menu = this.GenerateMenu();

            // Should deny the user 
            menu.Meta.AccessType = AuthorizationType.Deny;

            service.AddNavigationNode(menu);

            // A.CallTo(() => foo.Bar(A<string>.Ignored, "second argument")).Throws(new Exception());
            var userNav = service.GetNavigationForUser(new GenericIdentity(""));

            Assert.IsNotNull(userNav);
            Assert.AreEqual<int>(0, userNav.Length);
        }

        [TestMethod]
        public void GetMenuAuthorizedForChildItemWithGrantTest()
        {
            IAuthorizationService authService = A.Fake<IAuthorizationService>();
            A.CallTo(() => authService.GetRolesForUser(A<string>.Ignored)).Returns(new string[] { "RootPermission", "Secondary", "All" });
            MvcNavigationService service = new MvcNavigationService(authService);
            service.AddNavigationNode(this.GenerateMenu());

            // A.CallTo(() => foo.Bar(A<string>.Ignored, "second argument")).Throws(new Exception());
            var userNav = service.GetNavigationForUser(new GenericIdentity(""));

            Assert.IsNotNull(userNav);
            Assert.AreEqual<int>(1, userNav.Length);
            Assert.AreEqual<int>(1, userNav[0].Children.Count);
        }
  
        private NavigationItem GenerateMenu()
        {
            var menu = new NavigationItem
            {
                Meta = new MvcNavigationMeta
                {
                    Text = "Root",
                    Roles = new[] { "RootPermission" }
                },
                ItemIdentifier = "root-item",
                Children = new List<NavigationItem>
                {
                    {
                        new NavigationItem
                        {
                            Meta = new MvcNavigationMeta
                            {
                                PreferredOrder = 100,
                                Text = "Sub-Item1",
                                Roles = new[] { "Secondary" }
                            },
                            ItemIdentifier = "sub-item1",
                            Children = new List<NavigationItem>
                            {
                                {
                                        new NavigationItem
                                        {
                                            Meta = new MvcNavigationMeta
                                            {
                                                Text = "Sub-Sub-Item1",
                                                Roles = new[] { "All" }
                                            },
                                            ItemIdentifier = "sub-sub-item1"
                                        }
                                }
                            }
                        }
                    },
                    {
                        new NavigationItem
                        {
                            Meta = new MvcNavigationMeta
                            {
                                PreferredOrder = 10,
                                Text = "Sub-Item0",
                                Roles = new[] { "All" }
                            },
                            ItemIdentifier = Guid.NewGuid().ToString()
                        }
                    }
                }
            };
            return menu;
        }
    }
}
