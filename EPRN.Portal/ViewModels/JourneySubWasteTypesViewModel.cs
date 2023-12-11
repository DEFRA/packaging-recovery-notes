using EPRN.Portal.Resources;
using System.ComponentModel.DataAnnotations;

namespace EPRN.Portal.ViewModels
{
    public class JourneySubWasteTypesViewModel
    {
        public JourneySubWasteTypesViewModel()
        {
            SubWasteTypes = new List<JourneySubWasteTypeViewModel>();
        }

        public int JourneyId { get; set; }

        public List<JourneySubWasteTypeViewModel> SubWasteTypes { get; set; }

        [Required(ErrorMessageResourceName = "MissingSubWasteTypeSelectionMessage", ErrorMessageResourceType = typeof(SubWasteTypesResources))]
        public int? SelectedSubWasteTypeId { get; set; }
        
        public double? Adjustment { get; set; }
    }

}
