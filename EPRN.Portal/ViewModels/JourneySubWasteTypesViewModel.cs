using EPRN.Portal.Resources;
using System.ComponentModel.DataAnnotations;

namespace EPRN.Portal.ViewModels
{
    public class JourneySubWasteTypesViewModel
    {
        public int JourneyId { get; set; }

        public List<(int, string, bool)> SubWasteTypes { get; set; } = new List<(int, string, bool)>();

        [Required(ErrorMessageResourceName = "MissingSubWasteTypeSelectionMessage", ErrorMessageResourceType = typeof(SubWasteTypesResources))]
        public int? SelectedSubWasteTypeId { get; set; }
        
        public double? Adjustment { get; set; }
    }
}
