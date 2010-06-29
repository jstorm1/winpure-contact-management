﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.EntityClient;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;

[assembly: EdmSchemaAttribute()]
#region EDM Relationship Metadata

[assembly: EdmRelationshipAttribute("WinPure.ContactManagement.Client.Data.Model", "FK_Company_Contact", "Company", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(WinPure.ContactManagement.Client.Data.Model.Company), "Contact", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(WinPure.ContactManagement.Client.Data.Model.Contact), true)]

#endregion

namespace WinPure.ContactManagement.Client.Data.Model
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class EntitiesDataContext : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new EntitiesDataContext object using the connection string found in the 'EntitiesDataContext' section of the application configuration file.
        /// </summary>
        public EntitiesDataContext() : base("name=EntitiesDataContext", "EntitiesDataContext")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new EntitiesDataContext object.
        /// </summary>
        public EntitiesDataContext(string connectionString) : base(connectionString, "EntitiesDataContext")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new EntitiesDataContext object.
        /// </summary>
        public EntitiesDataContext(EntityConnection connection) : base(connection, "EntitiesDataContext")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Company> Companies
        {
            get
            {
                if ((_Companies == null))
                {
                    _Companies = base.CreateObjectSet<Company>("Companies");
                }
                return _Companies;
            }
        }
        private ObjectSet<Company> _Companies;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Contact> Contacts
        {
            get
            {
                if ((_Contacts == null))
                {
                    _Contacts = base.CreateObjectSet<Contact>("Contacts");
                }
                return _Contacts;
            }
        }
        private ObjectSet<Contact> _Contacts;

        #endregion
        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Companies EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToCompanies(Company company)
        {
            base.AddObject("Companies", company);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Contacts EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToContacts(Contact contact)
        {
            base.AddObject("Contacts", contact);
        }

        #endregion
    }
    

    #endregion
    
    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="WinPure.ContactManagement.Client.Data.Model", Name="Company")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Company : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Company object.
        /// </summary>
        /// <param name="companyId">Initial value of the CompanyId property.</param>
        /// <param name="name">Initial value of the Name property.</param>
        /// <param name="country">Initial value of the Country property.</param>
        public static Company CreateCompany(global::System.Guid companyId, global::System.String name, global::System.String country)
        {
            Company company = new Company();
            company.CompanyId = companyId;
            company.Name = name;
            company.Country = country;
            return company;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Guid CompanyId
        {
            get
            {
                return _CompanyId;
            }
            set
            {
                if (_CompanyId != value)
                {
                    OnCompanyIdChanging(value);
                    ReportPropertyChanging("CompanyId");
                    _CompanyId = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("CompanyId");
                    OnCompanyIdChanged();
                }
            }
        }
        private global::System.Guid _CompanyId;
        partial void OnCompanyIdChanging(global::System.Guid value);
        partial void OnCompanyIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                OnNameChanging(value);
                ReportPropertyChanging("Name");
                _Name = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Name");
                OnNameChanged();
            }
        }
        private global::System.String _Name;
        partial void OnNameChanging(global::System.String value);
        partial void OnNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Industry
        {
            get
            {
                return _Industry;
            }
            set
            {
                OnIndustryChanging(value);
                ReportPropertyChanging("Industry");
                _Industry = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Industry");
                OnIndustryChanged();
            }
        }
        private global::System.String _Industry;
        partial void OnIndustryChanging(global::System.String value);
        partial void OnIndustryChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Phone
        {
            get
            {
                return _Phone;
            }
            set
            {
                OnPhoneChanging(value);
                ReportPropertyChanging("Phone");
                _Phone = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Phone");
                OnPhoneChanged();
            }
        }
        private global::System.String _Phone;
        partial void OnPhoneChanging(global::System.String value);
        partial void OnPhoneChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Fax
        {
            get
            {
                return _Fax;
            }
            set
            {
                OnFaxChanging(value);
                ReportPropertyChanging("Fax");
                _Fax = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Fax");
                OnFaxChanged();
            }
        }
        private global::System.String _Fax;
        partial void OnFaxChanging(global::System.String value);
        partial void OnFaxChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Website
        {
            get
            {
                return _Website;
            }
            set
            {
                OnWebsiteChanging(value);
                ReportPropertyChanging("Website");
                _Website = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Website");
                OnWebsiteChanged();
            }
        }
        private global::System.String _Website;
        partial void OnWebsiteChanging(global::System.String value);
        partial void OnWebsiteChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Address1
        {
            get
            {
                return _Address1;
            }
            set
            {
                OnAddress1Changing(value);
                ReportPropertyChanging("Address1");
                _Address1 = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Address1");
                OnAddress1Changed();
            }
        }
        private global::System.String _Address1;
        partial void OnAddress1Changing(global::System.String value);
        partial void OnAddress1Changed();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Address2
        {
            get
            {
                return _Address2;
            }
            set
            {
                OnAddress2Changing(value);
                ReportPropertyChanging("Address2");
                _Address2 = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Address2");
                OnAddress2Changed();
            }
        }
        private global::System.String _Address2;
        partial void OnAddress2Changing(global::System.String value);
        partial void OnAddress2Changed();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Address3
        {
            get
            {
                return _Address3;
            }
            set
            {
                OnAddress3Changing(value);
                ReportPropertyChanging("Address3");
                _Address3 = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Address3");
                OnAddress3Changed();
            }
        }
        private global::System.String _Address3;
        partial void OnAddress3Changing(global::System.String value);
        partial void OnAddress3Changed();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Town
        {
            get
            {
                return _Town;
            }
            set
            {
                OnTownChanging(value);
                ReportPropertyChanging("Town");
                _Town = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Town");
                OnTownChanged();
            }
        }
        private global::System.String _Town;
        partial void OnTownChanging(global::System.String value);
        partial void OnTownChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Country
        {
            get
            {
                return _Country;
            }
            set
            {
                OnCountryChanging(value);
                ReportPropertyChanging("Country");
                _Country = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Country");
                OnCountryChanged();
            }
        }
        private global::System.String _Country;
        partial void OnCountryChanging(global::System.String value);
        partial void OnCountryChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Postcode
        {
            get
            {
                return _Postcode;
            }
            set
            {
                OnPostcodeChanging(value);
                ReportPropertyChanging("Postcode");
                _Postcode = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Postcode");
                OnPostcodeChanged();
            }
        }
        private global::System.String _Postcode;
        partial void OnPostcodeChanging(global::System.String value);
        partial void OnPostcodeChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Description
        {
            get
            {
                return _Description;
            }
            set
            {
                OnDescriptionChanging(value);
                ReportPropertyChanging("Description");
                _Description = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Description");
                OnDescriptionChanged();
            }
        }
        private global::System.String _Description;
        partial void OnDescriptionChanging(global::System.String value);
        partial void OnDescriptionChanged();

        #endregion
    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("WinPure.ContactManagement.Client.Data.Model", "FK_Company_Contact", "Contact")]
        public EntityCollection<Contact> Contacts
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Contact>("WinPure.ContactManagement.Client.Data.Model.FK_Company_Contact", "Contact");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Contact>("WinPure.ContactManagement.Client.Data.Model.FK_Company_Contact", "Contact", value);
                }
            }
        }

        #endregion
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="WinPure.ContactManagement.Client.Data.Model", Name="Contact")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Contact : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Contact object.
        /// </summary>
        /// <param name="contactID">Initial value of the ContactID property.</param>
        public static Contact CreateContact(global::System.Guid contactID)
        {
            Contact contact = new Contact();
            contact.ContactID = contactID;
            return contact;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Guid ContactID
        {
            get
            {
                return _ContactID;
            }
            set
            {
                if (_ContactID != value)
                {
                    OnContactIDChanging(value);
                    ReportPropertyChanging("ContactID");
                    _ContactID = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("ContactID");
                    OnContactIDChanged();
                }
            }
        }
        private global::System.Guid _ContactID;
        partial void OnContactIDChanging(global::System.Guid value);
        partial void OnContactIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Title
        {
            get
            {
                return _Title;
            }
            set
            {
                OnTitleChanging(value);
                ReportPropertyChanging("Title");
                _Title = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Title");
                OnTitleChanged();
            }
        }
        private global::System.String _Title;
        partial void OnTitleChanging(global::System.String value);
        partial void OnTitleChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String FirstName
        {
            get
            {
                return _FirstName;
            }
            set
            {
                OnFirstNameChanging(value);
                ReportPropertyChanging("FirstName");
                _FirstName = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("FirstName");
                OnFirstNameChanged();
            }
        }
        private global::System.String _FirstName;
        partial void OnFirstNameChanging(global::System.String value);
        partial void OnFirstNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String MiddleName
        {
            get
            {
                return _MiddleName;
            }
            set
            {
                OnMiddleNameChanging(value);
                ReportPropertyChanging("MiddleName");
                _MiddleName = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("MiddleName");
                OnMiddleNameChanged();
            }
        }
        private global::System.String _MiddleName;
        partial void OnMiddleNameChanging(global::System.String value);
        partial void OnMiddleNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String LastName
        {
            get
            {
                return _LastName;
            }
            set
            {
                OnLastNameChanging(value);
                ReportPropertyChanging("LastName");
                _LastName = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("LastName");
                OnLastNameChanged();
            }
        }
        private global::System.String _LastName;
        partial void OnLastNameChanging(global::System.String value);
        partial void OnLastNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Suffix
        {
            get
            {
                return _Suffix;
            }
            set
            {
                OnSuffixChanging(value);
                ReportPropertyChanging("Suffix");
                _Suffix = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Suffix");
                OnSuffixChanged();
            }
        }
        private global::System.String _Suffix;
        partial void OnSuffixChanging(global::System.String value);
        partial void OnSuffixChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String JobTitle
        {
            get
            {
                return _JobTitle;
            }
            set
            {
                OnJobTitleChanging(value);
                ReportPropertyChanging("JobTitle");
                _JobTitle = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("JobTitle");
                OnJobTitleChanged();
            }
        }
        private global::System.String _JobTitle;
        partial void OnJobTitleChanging(global::System.String value);
        partial void OnJobTitleChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Guid> CompanyId
        {
            get
            {
                return _CompanyId;
            }
            set
            {
                OnCompanyIdChanging(value);
                ReportPropertyChanging("CompanyId");
                _CompanyId = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("CompanyId");
                OnCompanyIdChanged();
            }
        }
        private Nullable<global::System.Guid> _CompanyId;
        partial void OnCompanyIdChanging(Nullable<global::System.Guid> value);
        partial void OnCompanyIdChanged();

        #endregion
    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("WinPure.ContactManagement.Client.Data.Model", "FK_Company_Contact", "Company")]
        public Company Company
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Company>("WinPure.ContactManagement.Client.Data.Model.FK_Company_Contact", "Company").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Company>("WinPure.ContactManagement.Client.Data.Model.FK_Company_Contact", "Company").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<Company> CompanyReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Company>("WinPure.ContactManagement.Client.Data.Model.FK_Company_Contact", "Company");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Company>("WinPure.ContactManagement.Client.Data.Model.FK_Company_Contact", "Company", value);
                }
            }
        }

        #endregion
    }

    #endregion
    
}
