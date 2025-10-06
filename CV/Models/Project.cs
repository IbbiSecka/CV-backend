namespace CV.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PriorityView { get; set; }
        public string Img { get; set; }
        public string Description { get; set; }
        public string Date {  get; set; }
        public string Role { get; set; }
        public Ibbi Ibbi { get; set; }
        public int IbbiId { get; set; }
    }
}
