using FluentNHibernate.Mapping;
using my.domain.lib.core.Attributes;
using System;

namespace easiplan.domain.Views
{
    [IgnoreAutoMap]
    public class ClientRetirementPortfolio_View : BaseEntity<int>
    {
        public virtual int ClientId { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string IdentificationNo { get; set; }
        public virtual DateTime DateOfBirth { get; set; }
        public virtual string PassportNo { get; set; }
        public virtual int RetirementId { get; set; }
        public virtual int ClientPortfolioId { get; set; }
        public virtual string RetirementDescription { get; set; }
        public virtual string PolicyNo { get; set; }
        public virtual int FundId { get; set; }
        public virtual string FundName { get; set; }
        public virtual double CurrentAmount { get; set; }

    }


    public class ClientRetirementPortfolioViewMap : ClassMap<ClientRetirementPortfolio_View>
    {
        public ClientRetirementPortfolioViewMap()
        {
            Table("ClientRetirementPortfolio_View");
            ReadOnly();

            Id(x => x.ClientId);
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.IdentificationNo);
            Map(x => x.DateOfBirth);
            Map(x => x.PassportNo);
            Map(x => x.RetirementId);
            Map(x => x.ClientPortfolioId);
            Map(x => x.RetirementDescription);
            Map(x => x.PolicyNo);
            Map(x => x.FundId);
            Map(x => x.FundName);
            Map(x => x.CurrentAmount);

        }
    }
}
