namespace CarRentalSystem.Infrastructure.Identity
{
    using Application.Features.Identity;
    using Domain.Models.Dealers;
    using Microsoft.AspNetCore.Identity;
    using System;

    public class User : IdentityUser, IUser
    {
        internal User(string email, string userName, string phoneNumber)
            : base(email)
        {
            base.Email = email;
            base.UserName = userName;
            base.PhoneNumber = phoneNumber;
        }

        public Dealer? Dealer { get; private set; }

        public void BecomeDealer(Dealer dealer)
        {
            if (this.Dealer != null)
            {
                throw new InvalidOperationException($"User {base.UserName} is already a dealer.");
            }

            this.Dealer = dealer;
        }
    }
}
