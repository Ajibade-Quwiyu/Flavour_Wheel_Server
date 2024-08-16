namespace Flavour_Wheel_Server.Model
{
    public class FlavourWheel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Spirit1Name { get; set; }
        public string Spirit2Name { get; set; }
        public string Spirit3Name { get; set; }
        public string Spirit4Name { get; set; }
        public string Spirit5Name { get; set; }
        public int Spirit1Flavours { get; set; }
        public int Spirit2Flavours { get; set; }
        public int Spirit3Flavours { get; set; }
        public int Spirit4Flavours { get; set; }
        public int Spirit5Flavours { get; set; }
        public int Spirit1Ratings { get; set; }
        public int Spirit2Ratings { get; set; }
        public int Spirit3Ratings { get; set; }
        public int Spirit4Ratings { get; set; }
        public int Spirit5Ratings { get; set; }
        public string Feedback { get; set; }
        public int OverallRating { get; set; }
    }
}
