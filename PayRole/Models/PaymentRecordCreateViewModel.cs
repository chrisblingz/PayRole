using PayRole.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PayRole.Models
{
    public class PaymentRecordCreateViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Full Name")]
        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }
        
        public string FullName { get; set; }

        public string NiNo { get; set; }

        [DataType(DataType.Date), Display(Name = "Pay Date")]
        public DateTime PayDate { get; set; } = DateTime.UtcNow;

        public string PayMonth { get; set; } = DateTime.Today.Month.ToString();

        [Display(Name = "Tax Year")]
        public int TaxYearId { get; set; }

        public string TaxCode { get; set; } = "1205L";

        public decimal HourlyRate { get; set; }

        [Display(Name = "Hours Worked")]
        public decimal HoursWorked { get; set; }

        [Display(Name = "Contractual Hours")]
        public decimal ContractualHours { get; set; }

        public decimal OvertimeHours { get; set; }

        public decimal ContractualEarnings { get; set; }

        public decimal Overtimearnings { get; set; }

        public decimal Tax { get; set; }

        public decimal NIC { get; set; }

        public decimal? UnionFee { get; set; }

        public Nullable<decimal> SLC { get; set; }

        public decimal TotalEarnings { get; set; }

        public decimal TotalDeductions { get; set; }

        public decimal NetPayment { get; set; }

    }
}
