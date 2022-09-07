using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel.ClassBased;
using my.domain.lib.core.Attributes;
using my.domain.lib.core.Domain;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace my.domain.lib.core.test.Models.Views
{
    public class ClientDetailsView : EntityTypedId<int>
    {
        //public virtual int Id { get; set; }
        public virtual string Fullname { get; set; }
        public virtual string LastName { get; set; }
        public virtual string MidName { get; set; }
        public virtual string FirstName { get; set; }
        public virtual DateTime DateOfBirth { get; set; }
        [Encrypt]
        public virtual string IdentificationNo { get; set; }
        [Encrypt]
        public virtual string RecipientAddress { get; set; }
        [Encrypt]
        public virtual string RecipientCell { get; set; }
        public virtual string Rating { get; set; }

        public virtual int ClientId { get; set; }
      
    }

    public class ClientDetailsViewMap : ClassMap<ClientDetailsView>
    {
        public ClientDetailsViewMap()
        {
            Table("clientdetailscontactview");
            ReadOnly();
            Id(x => x.Id);
            Map(x => x.Fullname);
            Map(x => x.LastName);
            Map(x => x.MidName);
            Map(x => x.FirstName);            
            Map(x => x.DateOfBirth).Column("DateOfBirth");
            Map(x => x.IdentificationNo).Column("IdentificationNo");
            Map(x => x.ClientId).Column("ClientId");
            Map(x => x.Status).Column("Status");
            Map(x => x.RecipientAddress).Column("EmailAddr");
            Map(x => x.RecipientCell).Column("CellNo");
        }
    }
    //public class ClientDetailsViewMap : ClassMapping<ClientDetailsView>
    //{
    //    public ClientDetailsViewMap()
    //    {
    //        string sql = @"SELECT CD.Id, CONCAT(LastName, ', ', ClientTitle,' ',Initials)  AS FullName,LastName,MidName,FirstName,DateOfBirth,IdentificationNo,CD.ClientId,CD.Status , CC.CellNo,CC.EMailAddr
    //                        FROM clientdetails CD LEFT JOIN clientcontacts CC ON 
    //                        CD.Id = CC.ClientDetailsId
    //                        where CD.ClientId>0;";
    //        this.Subselect(sql);
    //        this.Mutable(false);
    //        this.Synchronize("clientdetails", "clientcontacts");
    //        this.Id(x => x.Id, x => x.Column("Id"));
    //        this.Property(x => x.Fullname, x => x.Column("FullName"));
    //        this.Property(x => x.LastName, x => x.Column("LastName"));
    //        this.Property(x => x.IdentificationNo, x => x.Column("IdentificationNo"));
    //        this.Property(x => x.DateOfBirth, x => x.Column("DateOfBirth"));
    //        this.Property(x => x.ClientId, x => x.Column("ClientId"));
    //        //this.Property(x => x.FullName, x => x.Column("Status"));
    //        this.Property(x => x.RecipientCell, x => x.Column("CellNo"));
    //        this.Property(x => x.RecipientAddress, x => x.Column("EMailAddr"));


    //    }
    //}
}
