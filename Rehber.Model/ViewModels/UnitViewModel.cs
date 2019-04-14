using Rehber.Model.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rehber.Model.ViewModels
{
    public class UnitViewModel
    {
        public int UnitId { get; set; }
        public String UnitName { get; set; }
        public int? ParentId { get; set; }
        public string ParentName { get; set; }
    }

    public static class UnitViewExtensions
    {
        public static UnitViewModel ToViewModel(this Units item)
        {
            UnitViewModel view = new UnitViewModel
            {
                ParentId = item.ParentId,
                UnitId = item.UnitId,
                UnitName = item.UnitName
            };
            return view;
        }
    }
}
