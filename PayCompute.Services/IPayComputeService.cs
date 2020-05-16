﻿using Microsoft.AspNetCore.Mvc.Rendering;
using PayCompute.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayCompute.Services
{
    public interface IPayComputeService
    {
        PaymentRecord GetbyId(int id);
        TaxYear GetTaxYearById( int id);
        IEnumerable<PaymentRecord> GetAll();
        Task CreateAsync(PaymentRecord paymentRecord);
        IEnumerable<SelectListItem> GetAllTaxYear();
        decimal OvertimeHours(decimal hoursWorked, decimal contractualHours );
        decimal ContractualEarnings(decimal contractualHours, decimal hoursWorked, decimal hourlyRate);
        decimal OvertimeRate(decimal hourlyRate);
        decimal OvertimeEarnings(decimal overtimeRate, decimal overtimeHours);
        decimal TotalEarnings(decimal overtimeEarnings, decimal contractualEarnings);
        decimal TotalDeduction(decimal tax, decimal nic, decimal studentLoanRepayment, decimal unionFees);
        decimal NetPay  (decimal totalEarnings, decimal totalDeduction);
    }
}
