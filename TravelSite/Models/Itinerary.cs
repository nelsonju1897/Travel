﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TravelSite.Models
{
    public class Itinerary
    {
        public Itinerary()
        {
            Activities = new HashSet<Activity>();
            Travelers = new HashSet<Traveler>();
        }
        [Key]
        public Guid Id { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public TimeSpan TimeSpan { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/DD/YYYY}")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/DD/YYYY}")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public ICollection<Activity> Activities { get; set; }
        public ICollection<Traveler> Travelers { get; set; }
    }
}