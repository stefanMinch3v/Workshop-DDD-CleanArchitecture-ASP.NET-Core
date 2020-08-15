namespace CarRentalSystem.Domain.Models.Dealers
{
    using Exceptions;
    using Common;

    using static ModelConstants;

    public class PhoneNumber : ValueObject
    {
        internal PhoneNumber(string value)
        {
            Guard.ForStringLength<InvalidPhoneNumberException>(
                value, 
                PhoneNumbers.MIN_LENGTH, 
                PhoneNumbers.MAX_LENGTH, 
                nameof(value));

            this.Value = value;
        }

        public static implicit operator string(PhoneNumber phoneNumber)
            => phoneNumber.Value;

        public static implicit operator PhoneNumber(string phoneNumber)
            => new PhoneNumber(phoneNumber);

        public string Value { get; }
    }
}
