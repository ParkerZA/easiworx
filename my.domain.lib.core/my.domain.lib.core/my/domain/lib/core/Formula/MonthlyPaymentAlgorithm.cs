// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Formula.MonthlyPaymentAlgorithm
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using System;
using TridentGoalSeek;

namespace my.domain.lib.core.Formula
{
    public class MonthlyPaymentAlgorithm : IGoalSeekAlgorithm
    {
        public double PresentAmount { get; set; }

        public double FutureAmount { get; set; }

        public double InterestRate { get; set; }

        public double InflationRate { get; set; }

        public double EscalationRate { get; set; }

        public int Years { get; set; }

        public int Months { get; set; }

        public Decimal Calculate(Decimal MonthlyPayment)
        {
            if (MonthlyPayment == Decimal.Zero)
                return Decimal.Zero;
            return (Decimal)FinFormula.FutureValue(this.PresentAmount, (double)MonthlyPayment, this.InterestRate, this.InflationRate, this.EscalationRate, this.Years, 0);
        }
    }
}
