namespace Rainfall_Api.Models
{
    public class RainfallReadingResponse
    {
        public RainfallReading[] Readings { get; set; }
    }

    public class RainfallReading
    {
        public int DateMeasured { get; set; }

        public int AmountMeasured { get; set; }
    }
}
