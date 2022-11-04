using System;
namespace _787DD97A_API.Models
{
    public class Apartment
    {
        public int Id { get; set; }
        public string Adress { get; set; }
        public string Undeground { get; set; }
        public uint Undeground_minutes { get; set; }
        public uint Rooms { get; set; }
        public uint Segment { get; set; }
        public uint House_floors { get; set; }
        public uint Material { get; set; }
        public uint Apartment_floor { get; set; }
        public uint Apatments_area { get; set; }
        public uint Kitchen_area { get; set; }
        public bool Balcony { get; set; }
        public string Condition { get; set; }
        public string Link { get; set; }

    }
}

