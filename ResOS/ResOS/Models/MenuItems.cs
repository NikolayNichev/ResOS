using System;

namespace ResOS.Models
{
    public class MenuItems
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public bool IsAdded { get; set; }
    }
}