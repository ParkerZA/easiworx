using FluentNHibernate.Mapping;
using my.domain.lib.core.Attributes;
using my.domain.lib.core.Domain;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easiplan.domain.Views
{
    [IgnoreAutoMap]
    public class ClientDetailsView : BaseEntity<int>
    {
        public virtual int AgentId { get; set; }
        public virtual string AgentName { get; set; }
        public virtual string Fullname { get; set; }
        public virtual string LastName { get; set; }
        public virtual string MidName { get; set; }
        public virtual string FirstName { get; set; }
        public virtual DateTime DateOfBirth { get; set; }

        public virtual string IdentificationNo { get; set; }
        [Encrypt]
        public virtual string RecipientAddress { get; set; }
        [Encrypt]
        public virtual string RecipientCell { get; set; }
        public virtual string Rating { get; set; }

        public virtual int ClientId { get; set; }

        public virtual bool IsSelected { get; set; } = true;

        public virtual int Age
        {
            get
            {
                if (DateOfBirth > base.MinDateTime)
                {
                    return (int)((double)DateTime.Now.Subtract(DateOfBirth).Days / 365.242199);
                }
                return 0;
            }
            set
            {

            }
        }

        [IgnoreAutoMap]
        public virtual string _AgeDescription
        {
            get { return $"{Age}{getAgeSuffix(Age)}"; }
        }

        public virtual string getAgeSuffix(int Age)
        {
            switch (Age)
            {
                case 1:
                case 21:
                case 31:
                case 41:
                case 51:
                case 61:
                case 71:
                case 81:
                case 91:
                case 101:
                    return "st";
                case 2:
                case 22:
                case 32:
                case 42:
                case 52:
                case 62:
                case 72:
                case 82:
                case 92:
                case 102:
                    return "nd";
                case 3:
                case 23:
                case 33:
                case 43:
                case 53:
                case 63:
                case 73:
                case 83:
                case 93:
                case 103:
                    return "rd";
                default:
                    return "th";
            }
        }

        public virtual string ClientTitle { get; set; }
    }

    public class ClientDetailsViewMap : ClassMap<ClientDetailsView>
    {
        public ClientDetailsViewMap()
        {
            Table("clientdetailscontactview");
            ReadOnly();
            SchemaAction.None();

            Id(x => x.Id);
            Map(x => x.AgentId);
            Map(x => x.AgentName);
            Map(x => x.Fullname);
            Map(x => x.ClientTitle);
            Map(x => x.LastName);
            Map(x => x.MidName);
            Map(x => x.FirstName);
            Map(x => x.DateOfBirth);
            Map(x => x.IdentificationNo);
            Map(x => x.ClientId);
            Map(x => x.Rating);
            Map(x => x.RecipientAddress).Column("EmailAddr");
            Map(x => x.RecipientCell).Column("CellNo");
            
        }
    }

}
