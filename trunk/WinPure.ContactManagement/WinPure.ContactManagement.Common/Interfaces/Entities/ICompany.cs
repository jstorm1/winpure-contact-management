using System;

namespace WinPure.ContactManagement.Common.Interfaces.Entities
{
    public interface ICompany
    {
        ///// <summary>
        ///// No Metadata Documentation available.
        ///// </summary>
        //[EdmScalarProperty(EntityKeyProperty = true, IsNullable = false)]
        //[DataMember()]
        Guid CompanyId { get; set; }

        ///// <summary>
        ///// No Metadata Documentation available.
        ///// </summary>
        //[EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        //[DataMemberAttribute()]
        String Name { get; set; }

        ///// <summary>
        ///// No Metadata Documentation available.
        ///// </summary>
        //[EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = true)]
        //[DataMemberAttribute()]
        String Industry { get; set; }

        ///// <summary>
        ///// No Metadata Documentation available.
        ///// </summary>
        //[EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = true)]
        //[DataMemberAttribute()]
        String Phone { get; set; }

        ///// <summary>
        ///// No Metadata Documentation available.
        ///// </summary>
        //[EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = true)]
        //[DataMemberAttribute()]
        String Fax { get; set; }

        ///// <summary>
        ///// No Metadata Documentation available.
        ///// </summary>
        //[EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = true)]
        //[DataMemberAttribute()]
        String Website { get; set; }

        ///// <summary>
        ///// No Metadata Documentation available.
        ///// </summary>
        //[EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = true)]
        //[DataMemberAttribute()]
        String Address1 { get; set; }

        ///// <summary>
        ///// No Metadata Documentation available.
        ///// </summary>
        //[EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = true)]
        //[DataMemberAttribute()]
        String Address2 { get; set; }

        ///// <summary>
        ///// No Metadata Documentation available.
        ///// </summary>
        //[EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = true)]
        //[DataMemberAttribute()]
        String Address3 { get; set; }

        ///// <summary>
        ///// No Metadata Documentation available.
        ///// </summary>
        //[EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = true)]
        //[DataMemberAttribute()]
        String Town { get; set; }

        ///// <summary>
        ///// No Metadata Documentation available.
        ///// </summary>
        //[EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        //[DataMemberAttribute()]
        String Country { get; set; }

        ///// <summary>
        ///// No Metadata Documentation available.
        ///// </summary>
        //[EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = true)]
        //[DataMemberAttribute()]
        String Postcode { get; set; }

        ///// <summary>
        ///// No Metadata Documentation available.
        ///// </summary>
        //[EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = true)]
        //[DataMemberAttribute()]
        String Description { get; set; }
    }
}