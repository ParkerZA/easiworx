using easiplan.domain.Entities;
using my.domain.lib.core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easiplan.app.Models
{
    internal static class ModelExt
    {
        internal static Investment CloneAsInvestment(this Retirement obj)
        {
            Investment need = new Investment()
            {
                Status = obj.Status
            };

            need.IsLoading = true;
            try
            {
                // need.Id = obj.Id;
                need.Type = obj.Type;
                need.Description = obj.Description;

                need.MonthlyContribution = obj.MonthlyContribution;
               
                need.CurrentAmount = obj.CurrentAmount;
                need.InitialAmount = obj.InitialAmount;

                need.CurrentAge = obj.CurrentAge;
                need.InvestmentAge = obj.InvestmentAge;

                need.InflationPercentage = obj.InflationPercentage;
                need.EscalationPercentage = (double)obj.EscalationPercentage;
                need.GrowthPercentage = obj.GrowthPercentage;

                need.ReferenceNo = obj.ReferenceNo;
                need.ReferenceId = obj.Id;

                need.Insured = obj.Insured; //YJ 10/02/2020

                need.Funds.Clear();
                foreach (var fund in obj.Funds)
                {
                    Fund f = fund.ToObject<Fund>();
                    f.Id = 0;
                    f.StartDate = DateTime.Now;
                    f.EndDate = f.MinDateTime;
                    f.InitialAmount = 0;
                    f.WithdrawalAmount = 0;
                    need.Funds.Add(f);
                }

                need.Beneficiaries.Clear();
                foreach (var beneficiary in obj.Beneficiaries)
                {
                    ClientDependent f = beneficiary.ToObject<ClientDependent>();
                    f.Id = 0;
                    need.Beneficiaries.Add(f);
                }

                need.Amendments.Clear();
                foreach (var note in obj.Amendments)
                {

                    need.Amendments.Add(note);
                }
               
                need.BequethTo = obj.BequethTo;

                need.Notes.Clear();
                foreach (var note in obj.Notes)
                {
                    
                    need.Notes.Add(note);
                }

                need.Type = obj.Type;

                
            }
            catch (Exception x)
            {

            }
            finally
            {
                need.Calculate();
                need.IsLoading = false;
            }
            return need;

        }

        internal static Retirement CloneAsRetirement(this Investment obj)
        {
            Retirement need = new Retirement()
            {
                Status = obj.Status
            };

            need.IsLoading = true;
            try
            {
                // need.Id = obj.Id;
                need.Type = obj.Type;
                need.Description = obj.Description;

                need.MonthlyContribution = obj.MonthlyContribution;

                need.CurrentAmount = obj.CurrentAmount;
                need.InitialAmount = obj.InitialAmount;

                need.CurrentAge = obj.CurrentAge;
                need.InvestmentAge = obj.InvestmentAge;

                need.InflationPercentage = obj.InflationPercentage;
                need.EscalationPercentage = (double)obj.EscalationPercentage;
                need.GrowthPercentage = obj.GrowthPercentage;

                need.ReferenceNo = obj.ReferenceNo;
                need.ReferenceId = obj.Id;

                need.Insured = obj.Insured; //YJ 10/02/2020

                need.Funds.Clear();
                foreach (var fund in obj.Funds)
                {
                    Fund f = fund.ToObject<Fund>();
                    f.Id = 0;
                    f.StartDate = DateTime.Now;
                    f.EndDate = f.MinDateTime;
                    f.InitialAmount = 0;
                    f.WithdrawalAmount = 0;
                    need.Funds.Add(f);
                }

                need.Beneficiaries.Clear();
                foreach (var beneficiary in obj.Beneficiaries)
                {
                    ClientDependent f = beneficiary.ToObject<ClientDependent>();
                    f.Id = 0;
                    need.Beneficiaries.Add(f);
                }


                need.Amendments.Clear();
                foreach (var note in obj.Amendments)
                {

                    need.Amendments.Add(note);
                }

                need.BequethTo = obj.BequethTo;

                need.Notes.Clear();
                foreach (var note in obj.Notes)
                {

                    need.Notes.Add(note);
                }

                need.Type = obj.Type;


            }
            catch (Exception x)
            {

            }
            finally
            {
                need.Calculate();
                need.IsLoading = false;
            }
            return need;

        }
    }
}
