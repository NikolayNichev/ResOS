using System;
using SQLite;

namespace ResOS.Models
{
    public class MenuItems
    {
        [PrimaryKey, AutoIncrement, Column("Id")]
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public bool IsAdded { get; set; }
    }
}