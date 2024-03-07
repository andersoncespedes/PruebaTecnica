
namespace Business.Entity;
    public class Flight : BaseEntity
    {
        public string Origin {get; set;}
        public string Destination {get; set;}
        public double Price {get; set;}
        public int IdTransportFK {get; set;}
        public Transport Transport {get; set;}
        public ICollection<Journey> Journies {get; set;}
        public ICollection<JourneyFlight> JourneyFlights {get; set;}
    }
