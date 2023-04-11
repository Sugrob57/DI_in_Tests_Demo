using Castle.MicroKernel.Registration;
using Castle.Windsor;
using MyService.AdminApi;
using MyService.Common.Services;
using MyService.PaymentsApi;
using MyService.TestServices.Services;
using MyService.UserApi;

namespace MyService.IntegrationTests.DI
{
	public class ContainerInitializer
	{
		public IWindsorContainer GetContainer()
		{
			var container = new WindsorContainer();
			Install(container);
			return container;
		}

		private void Install(IWindsorContainer container)
		{
			container
				.Register(
				Component
					.For<IApplicationConfiguration>()
					.ImplementedBy<ApplicationConfiguration>()
					.LifestyleSingleton(),
				Component
					.For<IRestHttpClient>()
					.ImplementedBy<RestHttpClient>()
					.LifestyleTransient(),
				Component
					.For<IAdminApi>()
					.ImplementedBy<MyServiceAdminApi>()
					.LifestyleSingleton(),
				Component
					.For<IUserApi>()
					.ImplementedBy<MyServiceUserApi>()
					.LifestyleScoped(),
				Component
					.For<IPaymentsApi>()
					.ImplementedBy<MyServicePaymentsApi>()
					.DependsOn(
						Dependency.OnValue("baseUrl", "http://localhost:9091")) // sample for set constructor dependencies
					.LifestyleTransient(),
				Component
					.For<TestContextService>()
					.ImplementedBy<TestContextService>()
					.LifestyleScoped(),
				Component
					.For<UsersTestService>()
					.ImplementedBy<UsersTestService>()
					.LifestyleScoped(),
				Component
					.For<PaymentsTestService>()
					.ImplementedBy<PaymentsTestService>()
					.LifestyleTransient());
		}
	}
}
