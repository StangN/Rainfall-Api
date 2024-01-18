namespace Rainfall_Api.Models
{
    public class RainfallReadingResponse
    {
        public List<RainfallReading> Readings { get; set; }
    }

    public class RainfallReading
    {
        public DateTime DateMeasured { get; set; }

        public int AmountMeasured { get; set; }
    }
}
