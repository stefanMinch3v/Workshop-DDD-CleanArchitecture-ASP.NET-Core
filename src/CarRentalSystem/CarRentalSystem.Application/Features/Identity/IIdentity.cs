namespace CarRentalSystem.Application.Features.Identity
{
    using Commands.RegisterUser;
    using Features.Identity.Commands.LoginUser;
    using System.Threading.Tasks;

    public interface IIdentity
    {
        Task<Result<IUser>> Register(RegisterUserCommand input);

        Task<Result<LoginOutputModel>> Login(LoginUserCommand input);
    }
}
