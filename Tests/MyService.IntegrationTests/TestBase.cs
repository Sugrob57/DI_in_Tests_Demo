using Castle.MicroKernel.Lifestyle;
using Castle.Windsor;
using MyService.AdminApi;
using MyService.Common.Models;
using MyService.IntegrationTests.DI;
using MyService.PaymentsApi;
using MyService.TestServices.Services;
using MyService.UserApi;

namespace MyService.IntegrationTests
{
    public abstract class TestBase
	{
		[SetUp]
		public void SetUp()
		{
			TestScope = Container.BeginScope();
		}

		[TearDown]
		public void TearDown()
		{
			TestScope.Dispose();
		}

		protected IAdminApi AdminApi => Container.Resolve<IAdminApi>();

		protected IUserApi UserApi => Container.Resolve<IUserApi>();

		protected IPaymentsApi PaymentsApi => Container.Resolve<IPaymentsApi>();

		protected UsersTestService UsersTestService => Container.Resolve<UsersTestService>();

		protected PaymentsTestService PaymentsTestService => Container.Resolve<PaymentsTestService>();

		protected TestContextService TestContext => Container.Resolve<TestContextService>();

		protected UserInfo User
		{
			get => TestContext.GetProperty<UserInfo>(nameof(User));
			set => TestContext.SetProperty(nameof(User), value);
		}

		protected IDisposable TestScope
		{
			get => TestContext.GetProperty<IDisposable>(nameof(TestScope));
			set => TestContext.SetProperty(nameof(TestScope), value);
		}

		protected IWindsorContainer Container => _container ??= new ContainerInitializer().GetContainer();

		private static IWindsorContainer _container;
	}
}