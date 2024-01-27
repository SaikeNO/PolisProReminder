﻿using PolisProReminder.Entities;

namespace PolisProReminder.Models
{
    public class InsurancePolicyDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string PolicyNumber { get; set; } = null!;
        public string InsuranceCompany { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public bool IsPaid { get; set; }

        public InsurerDto Insurer { get; set; } = null!;
        public List<InsuranceTypeDto> InsuranceTypes { get; set; } = new();
    }
}
