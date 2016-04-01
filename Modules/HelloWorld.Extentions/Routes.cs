using Orchard.Mvc.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HelloWorld.Extentions.Routers
{
    public class Routes : IRouteProvider
    {
        public void GetRoutes(ICollection<RouteDescriptor> routes)
        {
            foreach (var routeDescriptor in GetRoutes())
                routes.Add(routeDescriptor);
        }

        public IEnumerable<RouteDescriptor> GetRoutes()
        {
            return new[] {
                new RouteDescriptor {
                    Priority = 5,
                    Route = new Route(
                        "UserMessages", // this is the name of the page url
                        new RouteValueDictionary {
                            {"area", "HelloWorld.Extentions"}, // this is the name of your module
                            {"controller", "Home"},
                            {"action", "Index"}
                        },
                        new RouteValueDictionary(),
                        new RouteValueDictionary {
                            {"area", "HelloWorld.Extentions"} // this is the name of your module
                        },
                        new MvcRouteHandler())
                },
				  new RouteDescriptor {
					Priority = 5,
					Route = new Route(
						"Manager/{action}", // this is the name of the page url
                        new RouteValueDictionary {
							{"area", "HelloWorld.Extentions"}, // this is the name of your module
                            {"controller", "Manager"},
							{"action", "Index"}
						},
						new RouteValueDictionary(),
						new RouteValueDictionary {
							{"area", "HelloWorld.Extentions"} // this is the name of your module
                        },
						new MvcRouteHandler())
				},
				new RouteDescriptor {
					Priority = 5,
					Route = new Route(
						"Contact/{action}", // this is the name of the page url
                        new RouteValueDictionary {
							{"area", "HelloWorld.Extentions"}, // this is the name of your module
                            {"controller", "Contact"},
							{"action", "Index"}
						},
						new RouteValueDictionary(),
						new RouteValueDictionary {
							{"area", "HelloWorld.Extentions"} // this is the name of your module
                        },
						new MvcRouteHandler())
				}
			};
        }
    }
}