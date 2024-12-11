namespace TravelAssistant.Classes
{
    public class Connection
    {
        public string Destination { get; set; }
        public List<TravelOption> TravelOptions { get; set; }

        public Connection(string destination)
        {
            Destination = destination;
            TravelOptions = new List<TravelOption>();
        }
    }
}