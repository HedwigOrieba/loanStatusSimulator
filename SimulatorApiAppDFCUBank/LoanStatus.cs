using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimulatorApiAppDFCUBank
{
    public class LoanStatus
    {
        public string  _id {get;set;}
        public string  __loanId {get;set;}
        public string  __disbursementDate {get;set;}
        public string  __outStandingAmount {get;set;}
        public string __v { get; set; }
    }
}