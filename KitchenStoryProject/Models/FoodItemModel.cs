using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KitchenStoryProject.Models
{
    public class FoodItemModel
    {
        public int Id { get; set; }
        public string FoodName { get; set; }
        public float Price { get; set; }
    }
}