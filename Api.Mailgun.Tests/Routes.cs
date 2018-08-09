using Xunit;
using Api.Mailgun.Routes;

namespace Api.Mailgun.Tests
{
    public class Routes
    {
        [Fact]
        public void TestActionHelper()
        {
            Assert.Equal("forward(\"https://callback.com\")", RouteActions.Forward("https://callback.com"));
            Assert.Equal("store(notify=\"https://callback.com\")", RouteActions.Store("https://callback.com"));
            Assert.Equal("store()", RouteActions.Store());
            Assert.Equal("stop()", RouteActions.Stop());
        }

        [Fact]
        public void TestActionCollectionHelper()
        {
            Assert.Collection(RouteActions.AddForward("https://callback.com"),
                item =>
                {
                    Assert.Equal(RouteActions.Forward("https://callback.com"), item);
                });

            Assert.Collection(RouteActions.AddForward("https://callback.com").AddStore("https://callback.com"),
                item =>
                {
                    Assert.Equal(RouteActions.Forward("https://callback.com"), item);
                },
                item =>
                {
                    Assert.Equal(RouteActions.Store("https://callback.com"), item);
                });

            Assert.Collection(RouteActions.AddForward("https://callback1.com").AddForward("https://callback2.com").AddStore().AddStop(),
                item =>
                {
                    Assert.Equal(RouteActions.Forward("https://callback1.com"), item);
                },
                item =>
                {
                    Assert.Equal(RouteActions.Forward("https://callback2.com"), item);
                },
                item =>
                {
                    Assert.Equal(RouteActions.Store(), item);
                },
                item =>
                {
                    Assert.Equal(RouteActions.Stop(), item);
                });
        }

        [Fact]
        public void TestFilterHelper()
        {
            Assert.Equal("match_header(\"FROM\", \"*@mail.com\")", RouteFilters.MatchHeader("FROM", "*@mail.com"));
            Assert.Equal("match_recipient(\"*@mail.com\")", RouteFilters.MatchRecipient("*@mail.com"));
            Assert.Equal("catch_all()", RouteFilters.CatchAll());
        }
        
        [Fact]
        public async void TestRoutes()
        {
            var manager = new RouteManager(Settings.ApiKey);

            #region create routes
            var createRoute1 = await manager.CreateRouteAsync(RouteFilters.CatchAll(), RouteActions.AddStore());
            Assert.True(createRoute1.Successful, createRoute1.ErrorMessage);
            Assert.Equal(0, createRoute1.Response.Route.Priority);
            Assert.Equal("false", createRoute1.Response.Route.Description);
            Assert.Equal(RouteFilters.CatchAll(), createRoute1.Response.Route.Filter);
            Assert.Collection(createRoute1.Response.Route.Actions,
                item => {
                    Assert.Equal(RouteActions.Store(), item);
                });

            var createRoute2 = await manager.CreateRouteAsync(RouteFilters.MatchRecipient(".*@bar.com"), RouteActions.AddForward("http://callback.com").AddStore(), 1);
            Assert.True(createRoute2.Successful, createRoute2.ErrorMessage);
            Assert.Equal(1, createRoute2.Response.Route.Priority);
            Assert.Equal("false", createRoute2.Response.Route.Description);
            Assert.Equal(RouteFilters.MatchRecipient(".*@bar.com"), createRoute2.Response.Route.Filter);
            Assert.Collection(createRoute2.Response.Route.Actions,
                item =>
                {
                    Assert.Equal(RouteActions.Forward("http://callback.com"), item);
                },
                item => {
                    Assert.Equal(RouteActions.Store(), item);
                });

            var createRoute3 = await manager.CreateRouteAsync(RouteFilters.MatchHeader("FROM", ".*"), RouteActions.AddForward("http://callback.com").AddStore("http://callback.com").AddStop(), 2, "test_description");
            Assert.True(createRoute3.Successful, createRoute3.ErrorMessage);
            Assert.Equal(2, createRoute3.Response.Route.Priority);
            Assert.Equal("test_description", createRoute3.Response.Route.Description);
            Assert.Equal(RouteFilters.MatchHeader("FROM", ".*"), createRoute3.Response.Route.Filter);
            Assert.Collection(createRoute3.Response.Route.Actions,
                item =>
                {
                    Assert.Equal(RouteActions.Forward("http://callback.com"), item);
                },
                item =>
                {
                    Assert.Equal(RouteActions.Store("http://callback.com"), item);
                },
                item => {
                    Assert.Equal(RouteActions.Stop(), item);
                });
            #endregion

            #region get routes
            var getRoute1 = await manager.GetRouteAsync(createRoute1.Response.Route.Id);
            Assert.True(getRoute1.Successful, getRoute1.ErrorMessage);
            Assert.Equal(0, getRoute1.Response.Route.Priority);
            Assert.Equal("false", getRoute1.Response.Route.Description);
            Assert.Equal(RouteFilters.CatchAll(), getRoute1.Response.Route.Filter);
            Assert.Collection(getRoute1.Response.Route.Actions,
                item => {
                    Assert.Equal(RouteActions.Store(), item);
                });

            var getRoutes = await manager.GetRoutesAsync(666);
            Assert.True(getRoutes.Successful, getRoutes.ErrorMessage);
            Assert.True(getRoutes.Response.TotalCount >= 3);
            Assert.Contains(getRoutes.Response.Routes, item =>
            {                    
                return item.Id == createRoute1.Response.Route.Id;
            });
            Assert.Contains(getRoutes.Response.Routes, item =>
            {
                return item.Id == createRoute2.Response.Route.Id;
            });
            Assert.Contains(getRoutes.Response.Routes, item =>
            {
                return item.Id == createRoute3.Response.Route.Id;
            });
            #endregion

            #region update routes
            var route1 = createRoute1.Response.Route;

            var updateRoute1 = await manager.UpdateRoutePriorityAsync(route1.Id, 10);
            Assert.True(updateRoute1.Successful, updateRoute1.ErrorMessage);
            Assert.Equal(10, updateRoute1.Response.Priority);
            Assert.Equal("false", updateRoute1.Response.Description);
            Assert.Equal(RouteFilters.CatchAll(), updateRoute1.Response.Filter);
            Assert.Collection(updateRoute1.Response.Actions,
                item => {
                    Assert.Equal(RouteActions.Store(), item);
                });
            
            updateRoute1 = await manager.UpdateRouteDescriptionAsync(route1.Id, "new_description");
            Assert.True(updateRoute1.Successful, updateRoute1.ErrorMessage);
            Assert.Equal(10, updateRoute1.Response.Priority);
            Assert.Equal("new_description", updateRoute1.Response.Description);
            Assert.Equal(RouteFilters.CatchAll(), updateRoute1.Response.Filter);
            Assert.Collection(updateRoute1.Response.Actions,
                item => {
                    Assert.Equal(RouteActions.Store(), item);
                });

            updateRoute1 = await manager.UpdateRouteFilterAsync(route1.Id, RouteFilters.MatchRecipient(".*"));
            Assert.True(updateRoute1.Successful, updateRoute1.ErrorMessage);
            Assert.Equal(10, updateRoute1.Response.Priority);
            Assert.Equal("new_description", updateRoute1.Response.Description);
            Assert.Equal(RouteFilters.MatchRecipient(".*"), updateRoute1.Response.Filter);
            Assert.Collection(updateRoute1.Response.Actions,
                item => {
                    Assert.Equal(RouteActions.Store(), item);
                });

            updateRoute1 = await manager.UpdateRouteActionsAsync(route1.Id, RouteActions.AddStore().AddStop());
            Assert.True(updateRoute1.Successful, updateRoute1.ErrorMessage);
            Assert.Equal(10, updateRoute1.Response.Priority);
            Assert.Equal("new_description", updateRoute1.Response.Description);
            Assert.Equal(RouteFilters.MatchRecipient(".*"), updateRoute1.Response.Filter);
            Assert.Collection(updateRoute1.Response.Actions,
                item => {
                    Assert.Equal(RouteActions.Store(), item);
                },
                item => {
                    Assert.Equal(RouteActions.Stop(), item);
                });

            updateRoute1 = await manager.UpdateRouteAsync(route1);
            Assert.True(updateRoute1.Successful, updateRoute1.ErrorMessage);
            Assert.Equal(0, updateRoute1.Response.Priority);
            Assert.Equal("false", updateRoute1.Response.Description);
            Assert.Equal(RouteFilters.CatchAll(), updateRoute1.Response.Filter);
            Assert.Collection(updateRoute1.Response.Actions,
                item => {
                    Assert.Equal(RouteActions.Store(), item);
                });
            #endregion

            #region delete routes
            var removeRoute1 = await manager.DeleteRouteAsync(createRoute1.Response.Route.Id);
            Assert.True(removeRoute1.Successful, removeRoute1.ErrorMessage);
            Assert.Equal(createRoute1.Response.Route.Id, removeRoute1.Response.RouteId);

            var removeRoute2 = await manager.DeleteRouteAsync(createRoute2.Response.Route.Id);
            Assert.True(removeRoute2.Successful, removeRoute2.ErrorMessage);
            Assert.Equal(createRoute2.Response.Route.Id, removeRoute2.Response.RouteId);

            var removeRoute3 = await manager.DeleteRouteAsync(createRoute3.Response.Route.Id);
            Assert.True(removeRoute3.Successful, removeRoute3.ErrorMessage);
            Assert.Equal(createRoute3.Response.Route.Id, removeRoute3.Response.RouteId);
            #endregion
        }
    }
}
