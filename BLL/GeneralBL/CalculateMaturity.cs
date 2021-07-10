using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DAL;
using BusinessObject;

namespace BLL.GeneralBL
{
    public class CalculateMaturity
    {
        double MaturityAmt;
        DateTime MaturityDate;
        public double MaturityCalculation(String AcctTyp, Double DepositAmt, Int32 PeriodsinMonth, Int32 PeriodsInDays, Double ROI)
        {
            if (AcctTyp == "fd" || AcctTyp == "Fixed Deposit")
            {

                if (PeriodsinMonth != 0 && PeriodsInDays == 0)
                {
                    MaturityAmt = Convert.ToDouble(Math.Round(DepositAmt + (PeriodsinMonth * ROI * DepositAmt / 1200)));
                }
                if (PeriodsInDays != 0 && PeriodsinMonth == 0)
                {
                    MaturityAmt = Convert.ToDouble(Math.Round(DepositAmt + (PeriodsInDays * ROI * DepositAmt / 36500)));
                }

            }
            //else if (AcctTyp == "d")
            //{
            //    MaturityAmt = Convert.ToDouble(Math.Round((PeriodsinMonth * ROI * DepositAmt )/ 100));
            //}
            else if (AcctTyp == "mis")
            {
                MaturityAmt = Convert.ToDouble(Math.Round((DepositAmt * ROI) / (1200 + ROI)));
            }

            else if (AcctTyp == "cc" || AcctTyp == "Cash Cirtificate")
            {
                var totprd = PeriodsinMonth / 3;
                var remmom = PeriodsinMonth - (3 * totprd);
                var calcprd = remmom * 30 + PeriodsInDays;
                var totint1 = Convert.ToDouble(DepositAmt * Math.Pow((1 + (ROI / 400)), totprd));
                var totint2 = totint1 * ((ROI / 100) * (calcprd / 365));
                MaturityAmt = Math.Round(totint1 + totint2);
            }

            else if(AcctTyp == "Deposit Cirtificate")
            {
                var totprd = PeriodsinMonth / 3;
                var remmom = PeriodsinMonth - (3 * totprd);
                var calcprd = remmom * 30 + PeriodsInDays;
                var totint1 = DepositAmt * Math.Pow((1 + (ROI / 400)), totprd);
                var totint2 = totint1 * ((ROI / 100) * (calcprd / 365));
                MaturityAmt = Math.Round(totint1 + totint2);

            }

            else if (AcctTyp == "r" || AcctTyp == "Recurring Deposit")
            {

                Double x, y, z, i, p, c, d;
                i = ROI / 100;
                p = PeriodsinMonth;
                c = 3;
                d = DepositAmt;
                Double a = Math.Pow((1 + i * c / 12), (p / 3));
                x = a - 1;
                y = 1 + i * (c + 1) / 24;
                z = 12 * d / i;
                MaturityAmt = Math.Round(x * y * z);
            }
            return MaturityAmt;
        }

        public DateTime CalculateMaturityDate(DateTime dateopen, Int32 PeriodsinMonth, Int32 PeriodsInDays)
        {
            var date = dateopen.Date;

            if (dateopen == DateTime.MinValue)
            {
                dateopen = new DateTime();
                MaturityDate = dateopen.AddMonths(PeriodsinMonth);
                MaturityDate = MaturityDate.AddDays(PeriodsInDays);
            }
            else
            {
                MaturityDate = dateopen.AddMonths(PeriodsinMonth);
                //MaturityDate = MaturityDate.AddDays(PeriodsInDays);
            }


            return MaturityDate;

        }
    }
}
