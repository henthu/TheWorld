using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheWorld.ViewModels
{
    public class TripViewModel
    {
        [Required]
        [StringLength(255,MinimumLength = 5)]
        public string Name { get; set; }
        public DateTime Created { get; set; } =  DateTime.UtcNow;
        public int Id { get; set; }

        public IEnumerable<StopViewModel> Stops { get; set; }


    }
}