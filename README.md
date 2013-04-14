MVC Area Navigation
======================================

This is my first attempt at starting to (finally) move to GITHUB. Sorry if I bugger anything.

MVC Navigation is a simple service that allows you to easily aggregate navigation items
across your asp.net mvc areas. 

Step One: Create a navigation node
======================================
In your area registration, add any routes you need managed.

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
          
Step Two: Add the node to the navigation map
=============================================
There is a default implementation as well as an IAuthorizationService that uses the built in
role provider.

	var service = {Your IOC}.GetService<INavigationService>();
	service.AddNavigationNode(menu);
	
Step Three: Get the navigation items for any user
==================================================
	var identity = this.User.Identity || new GenericIdentity("username");
	var userNav = service.GetNavigationForUser(identity);
	
Step Four: Create partial view or html helper to generate your html if necessary
=================================================================================
	
            
