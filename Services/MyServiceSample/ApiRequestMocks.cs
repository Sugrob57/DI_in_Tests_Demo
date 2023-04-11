using WireMock.Matchers;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace MyService.Sample
{
	public class ApiRequestMocks
	{
		public ApiRequestMocks(WireMockServer mock)
		{
			Mock = mock;
		}

		public void MockAllAdminApiRequests()
		{
			Mock
				.Given(
					Request
						.Create()
						.WithPath(new WildcardMatcher($"/admin/api/*", true))
						.UsingGet())
				.RespondWith(
					Response
						.Create()
						.WithStatusCode(200)
						.WithBody("So good"));

			Mock
				.Given(
					Request
						.Create()
						.WithPath(new WildcardMatcher($"/admin/api/users", true))
						.UsingPost())
				.RespondWith(
					Response
						.Create()
						.WithStatusCode(200)
						.WithBodyAsJson(new
						{
							Login = "{{Random Type=\"EmailAddress\"}}",
							Password = "{{Random Type=\"Guid\"}}"
						})
						.WithTransformer(true));

			Mock
				.Given(
					Request
						.Create()
						.WithPath(new WildcardMatcher($"/admin/api/users", true))
						.UsingDelete())
				.RespondWith(
					Response
						.Create()
						.WithStatusCode(200));
		}

		public void MockAllUserApiRequests()
		{
			Mock
				.Given(
					Request
						.Create()
						.WithPath(new WildcardMatcher($"/user/login", true))
						.UsingPost())
				.RespondWith(
					Response
						.Create()
						.WithStatusCode(200)
						.WithBodyAsJson(new
						{
							BearerToken = "{{Random Type=\"Guid\"}}"
						})
						.WithTransformer(true));

			Mock
				.Given(ValidOrderRequest.UsingPost())
				.RespondWith(
					Response
						.Create()
						.WithStatusCode(201)
						.WithBodyAsJson(new
						{
							Id = "{{Random Type=\"Integer\"}}",
							Name = TextWords(1, 7)
						})
						.WithTransformer(true));

			Mock
				.Given(ValidOrderRequest.UsingGet())
				.RespondWith(
					Response
						.Create()
						.WithStatusCode(200)
						.WithBodyAsJson(new
						{
							Id = "{{Random Type=\"Integer\"}}",
							Name = TextWords(1, 7),
							Products = new List<object>
									{
										new { Id = "{{Random Type=\"Integer\"}}", Count = "{{Random Type=\"Integer\"}}" },
										new { Id = "{{Random Type=\"Integer\"}}", Count = "{{Random Type=\"Integer\"}}" }
									}
						})
						.WithTransformer(true));

			Mock
				.Given(ValidOrderRequest.UsingDelete())
				.RespondWith(
					Response
						.Create()
						.WithStatusCode(200));

			Mock
				.Given(
					Request
						.Create()
						.WithPath(new WildcardMatcher($"/user/logout", true))
						.UsingDelete())
				.RespondWith(
					Response
						.Create()
						.WithStatusCode(200));
		}

		public void MockAllPaymentApiRequests()
		{
			Mock
				.Given(
					Request
						.Create()
						.WithPath(new WildcardMatcher($"/pay/*", true))
						.UsingPost()
						.WithHeader("Cookie", true, new WildcardMatcher("NotFirstRequest=true", true)))
				.AtPriority(1)
				.RespondWith(
					Response
						.Create()
						.WithStatusCode(400)
						.WithBody("Session expired. Please reconect"));

			Mock
				.Given(
					Request
						.Create()
						.WithPath(new WildcardMatcher($"/pay/*", true))
						.WithHeader("Authorization", true, new WildcardMatcher("Bearer*", true))
						.UsingPost())
				.AtPriority(2)
				.RespondWith(
					Response
						.Create()
						.WithStatusCode(200)
						.WithBody("Session is active. It is first request.")
						.WithHeader("Set-Cookie", "NotFirstRequest=true")
						.WithBodyAsJson(new
						{
							IsSuccess = true
						}));
		}

		private IRequestBuilder ValidOrderRequest => Request
						.Create()
						.WithPath(new WildcardMatcher($"/user/orders", true))
						.WithHeader("Authorization", true, new WildcardMatcher("Bearer*", true));

		private static string TextWords(int min, int max) => "{{Random Type=\"TextWords\" Min=" + min + " Max=" + max + "}}";

		protected WireMockServer Mock { get; private set; }
	}
}