using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ObiletCase.Models.Api.Location;
using ObiletCase.Models.Api.Session;
using System.ComponentModel.DataAnnotations;

namespace ObiletCase.Models.ViewModels
{
    public class IndexViewModel
    {
        [ValidateNever]
        public SessionData Session { get; set; }

        [ValidateNever]
        public List<LocationData> Locations { get; set; }

        [Required(ErrorMessage = "Lütfen kalkış noktası seçin.")]
        public int? SelectedOriginId { get; set; }

        [Required(ErrorMessage = "Lütfen varış noktası seçin.")]
        public int? SelectedDestinationId { get; set; }

        [Required(ErrorMessage = "Lütfen bir tarih seçin.")]
        [DataType(DataType.Date)]
        public DateTime DepartureDate { get; set; }
    }
}
