namespace CarRentalSystem.Startup.Specs
{
    using Application.Features.Identity.Commands.LoginUser;
    using Application.Features.Identity.Commands.RegisterUser;
    using FluentAssertions;
    using Infrastructure.Identity;
    using MyTested.AspNetCore.Mvc;
    using Web.Features;
    using Xunit;

    public class IdentityControllerSpecs
    {
        [Theory]
        [InlineData(
            IdentityFakes.TestEmail,
            IdentityFakes.ValidPassword,
            JwtTokenGeneratorFakes.ValidToken)]
        public void LoginShouldReturnToken(string email, string password, string token)
            => MyPipeline
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("/Identity/Login")
                    .WithMethod(HttpMethod.Post)
                    .WithJsonBody(new
                    {
                        Email = email,
                        Password = password
                    }))

                .To<IdentityController>(c => c.Login(new LoginUserCommand(email, password)))

                .Which()
                .ShouldReturn()
                .ActionResult<LoginOutputModel>(result => result
                    .Passing(model => model.Token.Should().Be(token)));

        [Theory]
        [InlineData(
            IdentityFakes.TestEmail,
            IdentityFakes.ValidPassword,
            IdentityFakes.Name,
            IdentityFakes.PhoneNumber)]
        public void RegisterShouldReturnOkResult(string email, string password, string name, string phoneNumber)
            => MyPipeline
                .Configuration()
                .ShouldMap(request => request
                    .WithLocation("/Identity/Register")
                    .WithMethod(HttpMethod.Post)
                    .WithJsonBody(new
                    {
                        Email = email,
                        Password = password,
                        Name = name,
                        PhoneNumber = phoneNumber
                    }))

                .To<IdentityController>(c => c.Register(new RegisterUserCommand(email, password, name, phoneNumber)))

                .Which()
                .ShouldReturn()
                .ActionResult();

        [Fact]
        public void GetLoginShouldHaveCorrectAttributes()
            => MyController<IdentityController>
                .Calling(c => c.Login(new LoginUserCommand(IdentityFakes.TestEmail, IdentityFakes.ValidPassword)))

                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(HttpMethod.Post));

        [Fact]
        public void GetRegisterShouldHaveCorrectAttributes()
            => MyController<IdentityController>
                .Calling(c => c.Register(new RegisterUserCommand(
                    IdentityFakes.TestEmail, 
                    IdentityFakes.ValidPassword,
                    IdentityFakes.Name,
                    IdentityFakes.PhoneNumber)))

                .ShouldHave()
                .ActionAttributes(attr => attr
                    .RestrictingForHttpMethod(HttpMethod.Post));
    }
}
