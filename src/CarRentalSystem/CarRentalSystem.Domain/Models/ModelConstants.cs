namespace CarRentalSystem.Domain.Models
{
    public static class ModelConstants
    {
        public class Common
        {
            public const int MIN_NAME_LENGTH = 2;
            public const int MAX_NAME_LENGTH = 20;
            public const int MIN_EMAIL_LENGTH = 3;
            public const int MAX_EMAIL_LENGTH = 50;
            public const int MAX_URL_LENGTH = 2048;
            public const int ZERO = 0;
        }

        public class PhoneNumbers
        {
            public const int MIN_LENGTH = 5;
            public const int MAX_LENGTH = 15;
        }

        public class Dealers
        {
            public const int MIN_LENGTH = 2;
            public const int MAX_LENGTH = 150;
        }

        public class CarAds
        {
            public const decimal MIN_LENGTH_PRICE = 0;
            public const decimal MAX_LENGTH_PRICE = 100_000;
        }
    }
}
