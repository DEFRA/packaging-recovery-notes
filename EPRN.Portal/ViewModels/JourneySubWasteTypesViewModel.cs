using EPRN.Portal.Resources;
using System.ComponentModel.DataAnnotations;

namespace EPRN.Portal.ViewModels
{
    public class JourneySubWasteTypesViewModel
    {      
        public int JourneyId { get; set; }

        public Dictionary<int, string> SubWasteTypes { get; set; } = new Dictionary<int, string>();

        [Required(ErrorMessageResourceName = "MissingSubWasteTypeSelectionMessage", ErrorMessageResourceType = typeof(SubWasteTypesResources))]
        public int? SelectedSubWasteTypeId { get; set; }
        
        public double? AgreedAmount { get; set; }
    }
}
