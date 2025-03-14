﻿namespace CV.Models
{
    public class Education
    {
        public int Id { get; set; }
        public string EducationName { get; set; }
        public string Description { get; set; }
        public string EducationSite { get; set; }
        public string Degree { get; set; }
        public string Duration { get; set; }
        public Ibbi Ibbi { get; set; }
        public int IbbiId { get; set; }
    }
}
