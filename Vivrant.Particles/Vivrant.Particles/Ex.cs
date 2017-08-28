using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vivrant.Particles
{
    static class Ex
    {
        public static bool IsNumeric(this string str)
        {
            double retNum;
            //return Double.TryParse(Convert.ToString(str), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            var success = Double.TryParse(Convert.ToString(str), out retNum);
            if (retNum.Equals(Double.NaN))
                return false;
            return success;
        }
    }
}
