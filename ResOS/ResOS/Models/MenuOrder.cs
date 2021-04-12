using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ResOS.Models
{
    class MenuOrder
    {
        public ObservableCollection<MenuItems> MenuItems { get; set; }        
        public double Price { get; set; }


        public double getPriceOfOrder() 
        {
            double totalprice = 0.0;

            foreach (var item in MenuItems) 
            {
                totalprice += item.Price;
            }

            return totalprice;
        }
    }
}
