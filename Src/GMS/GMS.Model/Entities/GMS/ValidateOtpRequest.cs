using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Model.Entities.GMS
{
    public class ValidateOtpRequest
    {
        public string PhoneNumber { get; set; }
        public string Otp { get; set; }
    }
}
