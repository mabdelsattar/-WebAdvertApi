using System;

namespace AdvertAPI.Models
{
    //for support DB on status change for distriputed 
    public class ConfirmAdvertModel
    {
        public string Id { get; set; }
         public AdvertStatus Status { get; set; }
    }
}
