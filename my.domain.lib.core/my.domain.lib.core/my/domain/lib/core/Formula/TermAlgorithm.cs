// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Formula.TermAlgorithm
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using System;
using TridentGoalSeek;

namespace my.domain.lib.core.Formula
{
    public class TermAlgorithm : IGoalSeekAlgorithm
    {
        public double PresentAmount { get; set; }

        public double FutureAmount { get; set; }

        public double InterestRate { get; set; }

        public double InflationRate { get; set; }

        public double EscalationRate { get; set; }

        public double MonthlyPayment { get; set; }

        public Decimal Calculate(Decimal Term)
        {
            return (Decimal)FinFormula.FutureValue(this.PresentAmount, this.MonthlyPayment, this.InterestRate, this.InflationRate, this.EscalationRate, (int)Term, 0);
        }
    }

    public class RequiredTermAlgorithm : IGoalSeekAlgorithm
    {
        private readonly double PresentValue;
        private readonly double FutureValue;
        private readonly double MonthlyPayment;
        private readonly double Interest;
        private readonly double Inflation;
        private readonly double Escalation;
        private readonly int PaymentPerYear;

        public RequiredTermAlgorithm(
         double PresentValue,
         double FutureValue,
         double MonthlyPayment,
         double Interest = 0.0,
         double Inflation = 0.0,
         double Escalation = 0.0,
         int PaymentPerYear = 12)
        {
            this.PresentValue = PresentValue;
            this.FutureValue = FutureValue;
            this.MonthlyPayment = MonthlyPayment;
            this.Interest = Interest;
            this.Inflation = Inflation;
            this.Escalation = Escalation;
            this.PaymentPerYear = PaymentPerYear;
        }

        public decimal Calculate(decimal term)
        {
            var result = (decimal)(FinFormula.RequiredValue(MonthlyPayment,Interest,Inflation,Escalation,(int)term ,PaymentPerYear));

            if (result < 0)
                return (decimal)FutureValue;

            return (decimal)result;
            
        }
    }
}
