using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class BaseEntity
    {
        public int Id { get; set; } // PK
        public int CreateBy { get; set; } // UserID
        public DateTime CreateOn { get; set; }
        public int LastModifiedBy { get; set; }  // UserID
        public DateTime LastModifiedOn { get; set; } // Automatic Calculated
        public bool IsDeleted { get; set; }  // UserID

    }
}
