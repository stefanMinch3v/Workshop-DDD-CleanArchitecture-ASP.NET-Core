namespace CarRentalSystem.Domain.Models.CarAds
{
    using Common;

    public class Options : ValueObject
    {
        private Options(bool hasClimateControl, int numberOfSeats)
        {
            this.HasClimateControl = hasClimateControl;
            this.NumberOfSeats = numberOfSeats;

            this.TransmissionType = null!;
        }

        internal Options(bool hasClimateControl, int numberOfSeats, TransmissionType type)
        {
            this.HasClimateControl = hasClimateControl;
            this.NumberOfSeats = numberOfSeats;
            this.TransmissionType = type;
        }

        public bool HasClimateControl { get; }
        public int NumberOfSeats { get; }
        public TransmissionType TransmissionType { get; }
    }
}
