namespace CarRentalSystem.Domain.Exceptions
{
    public sealed class InvalidCarAdException : BaseDomainException
    {
        public InvalidCarAdException()
        {
        }

        public InvalidCarAdException(string msg) 
            => base.Message = msg;
    }
}
