using Rehber.Model.MessageContracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rehber.Model.DataModels
{
    public class Units
    {
        public Units()
        {
            InverseParent = new HashSet<Units>();
        }

        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public int? ParentId { get; set; }

        public Units Parent { get; set; }
        public ICollection<Units> InverseParent { get; set; }
    }
}
