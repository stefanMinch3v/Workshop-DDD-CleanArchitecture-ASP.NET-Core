namespace CarRentalSystem.Domain.Exceptions
{
    public sealed class InvalidDealerException : BaseDomainException
    {
        public InvalidDealerException()
        {
        }

        public InvalidDealerException(string msg)
            => base.Message = msg;
    }
}
