﻿using EPRN.Common.Enums;
using EPRN.Portal.Resources;
using System.ComponentModel.DataAnnotations;

namespace EPRN.Portal.ViewModels.Waste
{
    public class DuringWhichMonthRequestViewModel
    {
        public int JourneyId { get; set; }

        public Dictionary<int, string> Quarter { get; set; } = new Dictionary<int, string>();

        public string WasteType { get; set; }

        public DoneWaste WhatHaveYouDone { get; set; }

        public virtual int? SelectedMonth { get; set; }
    }

    // Class to be returned to the view if the DoneWaste enum returns ReprocessedIt and handles the error message correctly
    public class DuringWhichMonthSentOnRequestViewModel : DuringWhichMonthRequestViewModel
    {
        [Required(ErrorMessageResourceName = "ErrorMessageSent", ErrorMessageResourceType = typeof(WhichQuarterResources))]
        public override int? SelectedMonth { get; set; }
    }

    // Class to be returned to the view if the DoneWaste enum returns SentItOn and handles the error message correctly
    public class DuringWhichMonthReceivedRequestViewModel : DuringWhichMonthRequestViewModel
    {
        [Required(ErrorMessageResourceName = "ErrorMessageReceived", ErrorMessageResourceType = typeof(WhichQuarterResources))]
        public override int? SelectedMonth { get; set; }
    }
}