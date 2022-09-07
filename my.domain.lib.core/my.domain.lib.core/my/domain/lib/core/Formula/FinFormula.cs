// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Formula.FinFormula
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TridentGoalSeek;

namespace my.domain.lib.core.Formula
{
    public static class FinFormula
    {
        public static double FutureValue(
          double PresentAmount,
          double MonthlyPayments = 0.0,
          double Interest = 0.0,
          double Inflation = 0.0,
          double Escalation = 0.0,
          int Years = 1,
          int Months = 0)
        {
            if (Years < 1)
                return 0.0;
            try
            {
                double Rate = Interest >= 0.0 ? Interest / 100.0 : Interest;
                double num1 = Inflation >= 0.0 ? Inflation / 100.0 : Inflation;
                double num2 = Escalation >= 0.0 ? Escalation / 100.0 : Escalation;
                if (num1 > 0.0)
                    Rate = (1.0 + Rate) / (1.0 + num1) - 1.0;
                return Financial.FV(Rate, (double)Years, 0.0, -PresentAmount, DueDate.BegOfPeriod) + MonthlyPayments * 12.0 * (double)FinFormula.SUMPRODUCT(FinFormula.OFFSET(Years, new double?(1.0 + num2)), FinFormula.SETOFF(Years, new double?(1.0 + Rate)));
            }
            catch (Exception ex)
            {
            }
            return 0.0;
        }

        public static double FValue(
          double PresentAmount,
          double Interest,
          int Years = 1,
          double MonthlyPayments = 0.0,
          double Inflation = 0.0)
        {
            if (Years < 1)
                return 0.0;
            try
            {
                double Rate = Interest >= 0.0 ? Interest / 100.0 : Interest;
                double num1 = Inflation >= 0.0 ? Inflation / 100.0 : Inflation;
                double num2 = Math.Pow(1.0 + Rate / 1.0, 1.0) - 1.0;
                double num3 = Math.Pow(1.0 + num1 / 1.0, 1.0) - 1.0;
                return Financial.FV(Rate, (double)Years, 0.0, -PresentAmount, DueDate.BegOfPeriod);
            }
            catch (Exception ex)
            {
            }
            return 0.0;
        }

        public static double PresentValue(
          double FutureAmount,
          double MonthlyPayments = 0.0,
          double Interest = 0.0,
          double Inflation = 0.0,
          double Escalation = 0.0,
          int Months = 1)
        {
            try
            {
                float num1 = Interest > 0.0 ? (float)(Interest / 100.0) : (float)Interest;
                float num2 = Inflation >= 0.0 ? (float)(Inflation / 100.0) : (float)Inflation;
                double NPer = (double)Months;
                if ((double)num2 > 0.0)
                    num1 = (float)((1.0 + (double)((float)Math.Pow(1.0 + (double)num1 / 12.0, 1.0) - 1f)) / (1.0 + (double)((float)Math.Pow(1.0 + (double)num2 / 12.0, 1.0) - 1f)) - 1.0);
                return Financial.PV((double)num1, NPer, -MonthlyPayments, -FutureAmount, DueDate.BegOfPeriod);
            }
            catch (Exception ex)
            {
            }
            return 0.0;
        }

        public static double RequiredValue(
          double Payment,
          double Interest = 0.0,
          double Inflation = 0.0,
          double Escalation = 0.0,
          int Years = 1,
          int PaymentPerYears = 12)
        {
            if (Years < 1)
                return 0.0;
            try
            {
                double num1 = Payment;
                double num2 = Math.Abs(Interest) >= 1.0 ? Interest / 100.0 / (double)PaymentPerYears : Interest / (double)PaymentPerYears;
                double num3 = Math.Abs(Escalation) >= 1.0 ? Escalation / 100.0 / (double)PaymentPerYears : Escalation / (double)PaymentPerYears;
                double y = (double)(Years * PaymentPerYears);
                if (num2 == num3)
                    num2 += 0.0001;
                return num1 / (num2 - num3) * (1.0 - Math.Pow((1.0 + num3) / (1.0 + num2), y));
            }
            catch (Exception ex)
            {
            }
            return 0.0;
        }

        public static IList<KeyValuePair<int, double>> FutureValueList(
          double PresentAmount,
          double MonthlyPayments = 0.0,
          double Interest = 0.0,
          double Inflation = 0.0,
          double Escalation = 0.0,
          int Years = 1,
          int Months = 0)
        {
            IList<KeyValuePair<int, double>> keyValuePairList = (IList<KeyValuePair<int, double>>)new List<KeyValuePair<int, double>>();
            if (Years < 1)
                Years = 1;
            for (int key = 0; key <= Years; ++key)
            {
                PresentAmount = FinFormula.FutureValue(PresentAmount, MonthlyPayments, Interest, Inflation, Escalation, 1, 0);
                keyValuePairList.Add(new KeyValuePair<int, double>(key, PresentAmount));
            }
            return keyValuePairList;
        }

        public static double MonthlyPayment(
          double PresentValue,
          double FutureValue,
          double Interest = 0.0,
          double Inflation = 0.0,
          double Escalation = 0.0,
          int Years = 0,
          int PaymentPerYear = 12)
        {
            try
            {
                return (double)new GoalSeek((IGoalSeekAlgorithm)new MonthlyPaymentAlgorithm()
                {
                    PresentAmount = PresentValue,
                    FutureAmount = FutureValue,
                    InflationRate = Inflation,
                    InterestRate = Interest,
                    EscalationRate = Escalation,
                    Years = Years
                }).SeekResult((Decimal)FutureValue).InputVariable.Value;
            }
            catch (Exception ex)
            {
            }
            return 0.0;
        }

        public static int Term(
          double PresentValue,
          double FutureValue,
          double MonthlyPayment,
          double Interest = 0.0,
          double Inflation = 0.0,
          double Escalation = 0.0,
          int PaymentPerYear = 12)
        {
            try
            {
                double num1 = Interest >= 0.0 ? Interest / 100.0 : Interest;
                double num2 = Inflation >= 0.0 ? Inflation / 100.0 : Inflation;
                double num3 = Escalation >= 0.0 ? Escalation / 100.0 : Escalation;
                return (int)new GoalSeek((IGoalSeekAlgorithm)new TermAlgorithm()
                {
                    PresentAmount = PresentValue,
                    FutureAmount = FutureValue,
                    InflationRate = num2,
                    InterestRate = num1,
                    EscalationRate = num3,
                    MonthlyPayment = MonthlyPayment
                }).SeekResult((Decimal)FutureValue).InputVariable.Value;
            }
            catch (Exception ex)
            {
            }
            return 0;
        }

        public static int RequiredTerm(
          double PresentValue,
          double FutureValue,
          double MonthlyPayment,
          double Interest = 0.0,
          double Inflation = 0.0,
          double Escalation = 0.0,
          int PaymentPerYear = 12)
        {
            if (FutureValue < (MonthlyPayment* PaymentPerYear))
                return 1;

            var calc = new RequiredTermAlgorithm(PresentValue, FutureValue, MonthlyPayment, Interest, Inflation, Escalation, PaymentPerYear);
            var goalSeeker = new GoalSeek(calc);

            //var seekResult = goalSeeker.SeekResult((decimal)FutureValue);
            var seekResult = goalSeeker.SeekResult((decimal)FutureValue, new GoalSeekOptions(focusPercentage: 50,maximumAttempts: 1000)) ;

            var value = seekResult.InputVariable;

            return (int)value;
        }

        public static double FutureValue2(
          double PresentValue,
          double MonthlyPayment,
          double Interest = 0.0,
          double Inflation = 0.0,
          double Escalation = 0.0,
          int Years = 0,
          int PaymentPerYear = 12)
        {
            try
            {
                double num1 = Interest >= 0.0 ? Interest / 100.0 : Interest;
                double num2 = Inflation >= 0.0 ? Inflation / 100.0 : Inflation;
                double num3 = Escalation >= 0.0 ? Escalation / 100.0 : Escalation;
                double r1 = num1 / (double)PaymentPerYear;
                double r2 = num2 / (double)PaymentPerYear;
                double c = 12.0;
                double num4 = FinFormula.AnnualEffectiveYield(r1, c);
                double num5 = FinFormula.AnnualEffectiveYield(r2, c);
                double y = (double)(Years * PaymentPerYear);
                return (MonthlyPayment * Math.Pow(1.0 + num4, y) - Math.Pow(1.0 + num5, y)) / (num4 - num5);
            }
            catch (Exception ex)
            {
            }
            return 0.0;
        }

        private static float[] OFFSET(int Length, double? value)
        {
            float[] numArray = new float[Length];
            for (int index = 1; index <= Length; ++index)
                numArray[index - 1] = (float)Math.Pow(value.Value, (double)(index - 1));
            return numArray;
        }

        private static float[] SETOFF(int Length, double? value)
        {
            float[] numArray = new float[Length];
            int num = Length;
            for (int index = 1; index <= Length; ++index)
                numArray[index - 1] = (float)Math.Pow(value.Value, (double)(num - index + 1));
            return numArray;
        }

        private static float SUMPRODUCT(float[] arr1, float[] arr2)
        {
            float num = 0.0f;
            for (int index = 0; index < ((IEnumerable<float>)arr1).Count<float>(); ++index)
                num += arr1[index] * arr2[index];
            return num;
        }

        private static double AnnualEffectiveYield(double r, double c)
        {
            return Math.Pow(1.0 + r / c, c) - 1.0;
        }

        public static double IRR(
          double InitialAmount,
          double MonthlyPayments = 0.0,
          double FinalAmount = 0.0,
          double Interest = 0.0,
          double Escalation = 0.0,
          int NoOfPeriods = 0)
        {
            try
            {
                if (FinalAmount == 0.0)
                    return 0.0;
                double[] ValueArray = new double[NoOfPeriods + 1];
                ValueArray[0] = -InitialAmount;
                for (int index = 1; index < NoOfPeriods; ++index)
                    ValueArray[index] = -MonthlyPayments;
                if (NoOfPeriods == 0 || Interest == 0.0)
                    return 0.0;
                ValueArray[NoOfPeriods] = FinalAmount;
                return Financial.IRR(ref ValueArray, Interest / 100.0);
            }
            catch (Exception ex)
            {
            }
            return 0.0;
        }

        private static double tadEFFECT(double rate, double compounding)
        {
            if (compounding == 0.0)
                return Math.Exp(rate) - 1.0;
            return Math.Pow(1.0 + rate * compounding, 1.0 / compounding - 1.0);
        }

        private static double tadFVIF(double rate, double n, double compounding)
        {
            return Math.Pow(1.0 + FinFormula.tadEFFECT(rate, compounding), n);
        }

        private static double tadPVIF(double rate, double n, double compounding)
        {
            return Math.Pow(1.0 + FinFormula.tadEFFECT(rate, compounding), -n);
        }

        private static double tadPVIF2(double r, double n, double c, double p, double d)
        {
            double n1 = (n - 1.0) * p + d * p;
            if (r == 0.0)
                return 1.0;
            return FinFormula.tadPVIF(r, n1, c);
        }

        private static double pvifga(
          double r,
          double g,
          double n,
          int type1,
          double c,
          double p,
          double d)
        {
            double num1 = 0.0;
            double num2 = 0.0;
            double num3 = n - (double)(int)n;
            for (long index = 0; index <= (long)((int)n - 1); ++index)
            {
                double n1 = type1 != 0 ? (index != 0L ? (double)(index - 1L) * p + d * p : 0.0) : (double)index * p + d * p;
                double n2 = index != 0L ? (double)(index - 1L) * p + d * p : 0.0;
                num1 += FinFormula.tadFVIF(g, n2, c) * FinFormula.tadPVIF(r, n1, c);
            }
            double n3 = (n - 1.0) * p + p * d;
            double n4 = (double)((int)n - 1) * p + p * d;
            if (num3 != 0.0)
            {
                if (r == g)
                {
                    num2 = n * (1.0 + Math.Pow(FinFormula.tadEFFECT(r, c) * (double)type1, p * d)) / (1.0 + Math.Pow(FinFormula.tadEFFECT(g, c), p * d)) - (double)(int)n * (1.0 + Math.Pow(FinFormula.tadEFFECT(r, c) * (double)type1, p * d)) / (1.0 + Math.Pow(FinFormula.tadEFFECT(g, c), p * d));
                }
                else
                {
                    double num4 = 1.0 + Math.Pow(FinFormula.tadEFFECT(r, c) * (double)type1, p * d);
                    double num5 = FinFormula.tadFVIF(g, n4, c) * FinFormula.tadPVIF(r, n4, c);
                    double num6 = FinFormula.tadEFFECT(r, c) - FinFormula.tadEFFECT(g, c);
                    double num7 = FinFormula.tadFVIF(g, n3, c) * FinFormula.tadPVIF(r, n3, c);
                    double num8 = FinFormula.tadEFFECT(r, c) - FinFormula.tadEFFECT(g, c);
                    num2 = num4 * num5 / num6 - num4 * num7 / num8;
                }
            }
            return num1 + num2;
        }

        private static double tadPV(
          double rate,
          double gradient,
          double nper,
          double pmt,
          double fv,
          int type1 = 0,
          double compounding = 1.0,
          double period = 1.0,
          double distribution = 1.0)
        {
            return -fv * FinFormula.tadPVIF2(rate, nper, compounding, period, distribution) * FinFormula.tadPVIF2(gradient, nper - 1.0, compounding, period, distribution) - pmt * FinFormula.pvifga(rate, gradient, nper, type1, compounding, period, distribution);
        }

        private static double tadPMT(
          double rate,
          double gradient,
          double nper,
          double pv,
          double fv,
          int type1 = 0,
          double compounding = 1.0,
          double period = 1.0,
          double distribution = 1.0)
        {
            return (-pv - fv * FinFormula.tadPVIF2(rate, nper, compounding, period, distribution) * FinFormula.tadPVIF2(gradient, nper - 1.0, compounding, period, distribution)) / FinFormula.pvifga(rate, gradient, nper, type1, compounding, period, distribution);
        }

        private static double GoalSeek(
          double InitialGuess,
          double PresentAmount,
          double FutureAmount,
          double InterestRate,
          double InflationRate = 0.0,
          double EscalationRate = 0.0,
          int Years = 1,
          int Months = 0)
        {
            double num1 = 1E-09;
            double MonthlyPayments1 = InitialGuess - 1.0;
            double MonthlyPayments2;
            double num2;
            double num3;
            for (MonthlyPayments2 = InitialGuess; Math.Abs(MonthlyPayments2 - MonthlyPayments1) > num1; MonthlyPayments2 = MonthlyPayments1 + (num2 - FutureAmount) / num3)
            {
                num2 = FinFormula.FutureValue(PresentAmount, MonthlyPayments1, InterestRate, InflationRate, EscalationRate, Years, Months);
                num3 = FinFormula.FutureValue(PresentAmount, MonthlyPayments2, InterestRate, InflationRate, EscalationRate, Years, Months);
                MonthlyPayments1 = MonthlyPayments2;
            }
            return MonthlyPayments2;
        }

        private static double GoalSeek2(double InitialGuess, FinFormula.Function f)
        {
            double num1 = 0.001;
            int num2 = 100;
            double x1 = f(InitialGuess);
            double x2 = f(InitialGuess + 1.0);
            double num3 = x2 - f(x2) * (x2 - x1) / (f(x2) - f(x1));
            int num4;
            for (num4 = 0; Math.Abs(num3 - x2) > num1 && num4 < num2; ++num4)
            {
                double x3 = x2;
                x2 = num3;
                num3 = x2 - f(x2) * (x2 - x3) / (f(x2) - f(x3));
            }
            if (num4 < num2)
                return num3;
            Debug.WriteLine("{0}.The method did not converge", (object)num3);
            return double.NaN;
        }

        public delegate double Function(double x);

    }
}
