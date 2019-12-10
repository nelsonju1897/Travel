﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TravelSite.Models
{
    public class Traveler
    {
        public Traveler()
        {
            Interests = new HashSet<Interest>();
            Itineraries = new HashSet<Itinerary>();
        }
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser {get;set;}
        public ICollection<Interest> Interests { get; set; }
        public ICollection<Itinerary> Itineraries { get; set; }

    }
}