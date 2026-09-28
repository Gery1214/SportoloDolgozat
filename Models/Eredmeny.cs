namespace SportoloDolgozat.Models
{
    public class Eredmeny
    {
        public int id { get; set; }
        public string competition { get; set; }
        public string description { get; set; }
        public DateTime resultTime { get; set; }
        public DateTime updateTime { get; set; }
        public int sportoloId { get; set; }


    }
}
