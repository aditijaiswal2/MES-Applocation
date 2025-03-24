using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Shared.Models
{
    public class Receiving
    {
        [Key]
        public int Id { get; set; } // Primary Key
        public DateTime Date { get; set; } = DateTime.Today; // Auto-set to today's date
        public string SerialNumber { get; set; } // Auto-generated serial number
        public string SelectedOption { get; set; } // Dropdown selection
    }
}
