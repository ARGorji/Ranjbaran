﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ranjbaran.Old_App_Code.DAL
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="Ranjbaran")]
	public partial class PublicationsDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertPublicationGroups(PublicationGroups instance);
    partial void UpdatePublicationGroups(PublicationGroups instance);
    partial void DeletePublicationGroups(PublicationGroups instance);
    partial void InsertPublications(Publications instance);
    partial void UpdatePublications(Publications instance);
    partial void DeletePublications(Publications instance);
    #endregion
		
		public PublicationsDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["RanjbaranConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public PublicationsDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public PublicationsDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public PublicationsDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public PublicationsDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<PublicationGroups> PublicationGroups
		{
			get
			{
				return this.GetTable<PublicationGroups>();
			}
		}
		
		public System.Data.Linq.Table<vPublicationGroups> vPublicationGroups
		{
			get
			{
				return this.GetTable<vPublicationGroups>();
			}
		}
		
		public System.Data.Linq.Table<Publications> Publications
		{
			get
			{
				return this.GetTable<Publications>();
			}
		}
		
		public System.Data.Linq.Table<vPublications> vPublications
		{
			get
			{
				return this.GetTable<vPublications>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.PublicationGroups")]
	public partial class PublicationGroups : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Code;
		
		private int _PublicationCode;
		
		private int _GroupCode;
		
		private EntityRef<Publications> _Publication;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnCodeChanging(int value);
    partial void OnCodeChanged();
    partial void OnPublicationCodeChanging(int value);
    partial void OnPublicationCodeChanged();
    partial void OnGroupCodeChanging(int value);
    partial void OnGroupCodeChanged();
    #endregion
		
		public PublicationGroups()
		{
			this._Publication = default(EntityRef<Publications>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Code", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Code
		{
			get
			{
				return this._Code;
			}
			set
			{
				if ((this._Code != value))
				{
					this.OnCodeChanging(value);
					this.SendPropertyChanging();
					this._Code = value;
					this.SendPropertyChanged("Code");
					this.OnCodeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PublicationCode", DbType="Int NOT NULL")]
		public int PublicationCode
		{
			get
			{
				return this._PublicationCode;
			}
			set
			{
				if ((this._PublicationCode != value))
				{
					if (this._Publication.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnPublicationCodeChanging(value);
					this.SendPropertyChanging();
					this._PublicationCode = value;
					this.SendPropertyChanged("PublicationCode");
					this.OnPublicationCodeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GroupCode", DbType="Int NOT NULL")]
		public int GroupCode
		{
			get
			{
				return this._GroupCode;
			}
			set
			{
				if ((this._GroupCode != value))
				{
					this.OnGroupCodeChanging(value);
					this.SendPropertyChanging();
					this._GroupCode = value;
					this.SendPropertyChanged("GroupCode");
					this.OnGroupCodeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Publications_PublicationGroups", Storage="_Publication", ThisKey="PublicationCode", OtherKey="Code", IsForeignKey=true)]
		public Publications Publications
		{
			get
			{
				return this._Publication.Entity;
			}
			set
			{
				Publications previousValue = this._Publication.Entity;
				if (((previousValue != value) 
							|| (this._Publication.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Publication.Entity = null;
						previousValue.PublicationGroups.Remove(this);
					}
					this._Publication.Entity = value;
					if ((value != null))
					{
						value.PublicationGroups.Add(this);
						this._PublicationCode = value.Code;
					}
					else
					{
						this._PublicationCode = default(int);
					}
					this.SendPropertyChanged("Publications");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.vPublicationGroups")]
	public partial class vPublicationGroups
	{
		
		private int _Code;
		
		private int _PublicationCode;
		
		private string _Title;
		
		public vPublicationGroups()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Code", DbType="Int NOT NULL")]
		public int Code
		{
			get
			{
				return this._Code;
			}
			set
			{
				if ((this._Code != value))
				{
					this._Code = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PublicationCode", DbType="Int NOT NULL")]
		public int PublicationCode
		{
			get
			{
				return this._PublicationCode;
			}
			set
			{
				if ((this._PublicationCode != value))
				{
					this._PublicationCode = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Title", DbType="NVarChar(500) NOT NULL", CanBeNull=false)]
		public string Title
		{
			get
			{
				return this._Title;
			}
			set
			{
				if ((this._Title != value))
				{
					this._Title = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Publications")]
	public partial class Publications : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Code;
		
		private string _Title;
		
		private string _Description;
		
		private string _SmallPic;
		
		private string _LargePic;
		
		private string _PDFFile;
		
		private string _Entesharat;
		
		private System.Nullable<int> _VisitCount;
		
		private System.Nullable<int> _Price;
		
		private string _PublicationTurn;
		
		private System.Nullable<int> _Ciculation;
		
		private System.Nullable<int> _ShowOrder;
		
		private EntitySet<PublicationGroups> _PublicationGroups;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnCodeChanging(int value);
    partial void OnCodeChanged();
    partial void OnTitleChanging(string value);
    partial void OnTitleChanged();
    partial void OnDescriptionChanging(string value);
    partial void OnDescriptionChanged();
    partial void OnSmallPicChanging(string value);
    partial void OnSmallPicChanged();
    partial void OnLargePicChanging(string value);
    partial void OnLargePicChanged();
    partial void OnPDFFileChanging(string value);
    partial void OnPDFFileChanged();
    partial void OnEntesharatChanging(string value);
    partial void OnEntesharatChanged();
    partial void OnVisitCountChanging(System.Nullable<int> value);
    partial void OnVisitCountChanged();
    partial void OnPriceChanging(System.Nullable<int> value);
    partial void OnPriceChanged();
    partial void OnPublicationTurnChanging(string value);
    partial void OnPublicationTurnChanged();
    partial void OnCiculationChanging(System.Nullable<int> value);
    partial void OnCiculationChanged();
    partial void OnShowOrderChanging(System.Nullable<int> value);
    partial void OnShowOrderChanged();
    #endregion
		
		public Publications()
		{
			this._PublicationGroups = new EntitySet<PublicationGroups>(new Action<PublicationGroups>(this.attach_PublicationGroups), new Action<PublicationGroups>(this.detach_PublicationGroups));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Code", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Code
		{
			get
			{
				return this._Code;
			}
			set
			{
				if ((this._Code != value))
				{
					this.OnCodeChanging(value);
					this.SendPropertyChanging();
					this._Code = value;
					this.SendPropertyChanged("Code");
					this.OnCodeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Title", DbType="NVarChar(500) NOT NULL", CanBeNull=false)]
		public string Title
		{
			get
			{
				return this._Title;
			}
			set
			{
				if ((this._Title != value))
				{
					this.OnTitleChanging(value);
					this.SendPropertyChanging();
					this._Title = value;
					this.SendPropertyChanged("Title");
					this.OnTitleChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Description", DbType="NVarChar(4000)")]
		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				if ((this._Description != value))
				{
					this.OnDescriptionChanging(value);
					this.SendPropertyChanging();
					this._Description = value;
					this.SendPropertyChanged("Description");
					this.OnDescriptionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SmallPic", DbType="NVarChar(200)")]
		public string SmallPic
		{
			get
			{
				return this._SmallPic;
			}
			set
			{
				if ((this._SmallPic != value))
				{
					this.OnSmallPicChanging(value);
					this.SendPropertyChanging();
					this._SmallPic = value;
					this.SendPropertyChanged("SmallPic");
					this.OnSmallPicChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LargePic", DbType="NVarChar(200)")]
		public string LargePic
		{
			get
			{
				return this._LargePic;
			}
			set
			{
				if ((this._LargePic != value))
				{
					this.OnLargePicChanging(value);
					this.SendPropertyChanging();
					this._LargePic = value;
					this.SendPropertyChanged("LargePic");
					this.OnLargePicChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PDFFile", DbType="NVarChar(200)")]
		public string PDFFile
		{
			get
			{
				return this._PDFFile;
			}
			set
			{
				if ((this._PDFFile != value))
				{
					this.OnPDFFileChanging(value);
					this.SendPropertyChanging();
					this._PDFFile = value;
					this.SendPropertyChanged("PDFFile");
					this.OnPDFFileChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Entesharat", DbType="NVarChar(200)")]
		public string Entesharat
		{
			get
			{
				return this._Entesharat;
			}
			set
			{
				if ((this._Entesharat != value))
				{
					this.OnEntesharatChanging(value);
					this.SendPropertyChanging();
					this._Entesharat = value;
					this.SendPropertyChanged("Entesharat");
					this.OnEntesharatChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_VisitCount", DbType="Int")]
		public System.Nullable<int> VisitCount
		{
			get
			{
				return this._VisitCount;
			}
			set
			{
				if ((this._VisitCount != value))
				{
					this.OnVisitCountChanging(value);
					this.SendPropertyChanging();
					this._VisitCount = value;
					this.SendPropertyChanged("VisitCount");
					this.OnVisitCountChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Price", DbType="Int")]
		public System.Nullable<int> Price
		{
			get
			{
				return this._Price;
			}
			set
			{
				if ((this._Price != value))
				{
					this.OnPriceChanging(value);
					this.SendPropertyChanging();
					this._Price = value;
					this.SendPropertyChanged("Price");
					this.OnPriceChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PublicationTurn", DbType="NVarChar(200)")]
		public string PublicationTurn
		{
			get
			{
				return this._PublicationTurn;
			}
			set
			{
				if ((this._PublicationTurn != value))
				{
					this.OnPublicationTurnChanging(value);
					this.SendPropertyChanging();
					this._PublicationTurn = value;
					this.SendPropertyChanged("PublicationTurn");
					this.OnPublicationTurnChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Ciculation", DbType="Int")]
		public System.Nullable<int> Ciculation
		{
			get
			{
				return this._Ciculation;
			}
			set
			{
				if ((this._Ciculation != value))
				{
					this.OnCiculationChanging(value);
					this.SendPropertyChanging();
					this._Ciculation = value;
					this.SendPropertyChanged("Ciculation");
					this.OnCiculationChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ShowOrder", DbType="Int")]
		public System.Nullable<int> ShowOrder
		{
			get
			{
				return this._ShowOrder;
			}
			set
			{
				if ((this._ShowOrder != value))
				{
					this.OnShowOrderChanging(value);
					this.SendPropertyChanging();
					this._ShowOrder = value;
					this.SendPropertyChanged("ShowOrder");
					this.OnShowOrderChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Publications_PublicationGroups", Storage="_PublicationGroups", ThisKey="Code", OtherKey="PublicationCode")]
		public EntitySet<PublicationGroups> PublicationGroups
		{
			get
			{
				return this._PublicationGroups;
			}
			set
			{
				this._PublicationGroups.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_PublicationGroups(PublicationGroups entity)
		{
			this.SendPropertyChanging();
			entity.Publications = this;
		}
		
		private void detach_PublicationGroups(PublicationGroups entity)
		{
			this.SendPropertyChanging();
			entity.Publications = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.vPublications")]
	public partial class vPublications
	{
		
		private int _Code;
		
		private string _Title;
		
		private System.Nullable<int> _Price;
		
		private string _PublicationTurn;
		
		private System.Nullable<int> _ShowOrder;
		
		private string _Description;
		
		private string _SmallPic;
		
		private string _LargePic;
		
		private string _PDFFile;
		
		private string _Entesharat;
		
		private System.Nullable<int> _VisitCount;
		
		private System.Nullable<int> _Ciculation;
		
		public vPublications()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Code", AutoSync=AutoSync.Always, DbType="Int NOT NULL IDENTITY", IsDbGenerated=true)]
		public int Code
		{
			get
			{
				return this._Code;
			}
			set
			{
				if ((this._Code != value))
				{
					this._Code = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Title", DbType="NVarChar(500) NOT NULL", CanBeNull=false)]
		public string Title
		{
			get
			{
				return this._Title;
			}
			set
			{
				if ((this._Title != value))
				{
					this._Title = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Price", DbType="Int")]
		public System.Nullable<int> Price
		{
			get
			{
				return this._Price;
			}
			set
			{
				if ((this._Price != value))
				{
					this._Price = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PublicationTurn", DbType="NVarChar(200)")]
		public string PublicationTurn
		{
			get
			{
				return this._PublicationTurn;
			}
			set
			{
				if ((this._PublicationTurn != value))
				{
					this._PublicationTurn = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ShowOrder", DbType="Int")]
		public System.Nullable<int> ShowOrder
		{
			get
			{
				return this._ShowOrder;
			}
			set
			{
				if ((this._ShowOrder != value))
				{
					this._ShowOrder = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Description", DbType="NVarChar(4000)")]
		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				if ((this._Description != value))
				{
					this._Description = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SmallPic", DbType="NVarChar(200)")]
		public string SmallPic
		{
			get
			{
				return this._SmallPic;
			}
			set
			{
				if ((this._SmallPic != value))
				{
					this._SmallPic = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LargePic", DbType="NVarChar(200)")]
		public string LargePic
		{
			get
			{
				return this._LargePic;
			}
			set
			{
				if ((this._LargePic != value))
				{
					this._LargePic = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PDFFile", DbType="NVarChar(200)")]
		public string PDFFile
		{
			get
			{
				return this._PDFFile;
			}
			set
			{
				if ((this._PDFFile != value))
				{
					this._PDFFile = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Entesharat", DbType="NVarChar(200)")]
		public string Entesharat
		{
			get
			{
				return this._Entesharat;
			}
			set
			{
				if ((this._Entesharat != value))
				{
					this._Entesharat = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_VisitCount", DbType="Int")]
		public System.Nullable<int> VisitCount
		{
			get
			{
				return this._VisitCount;
			}
			set
			{
				if ((this._VisitCount != value))
				{
					this._VisitCount = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Ciculation", DbType="Int")]
		public System.Nullable<int> Ciculation
		{
			get
			{
				return this._Ciculation;
			}
			set
			{
				if ((this._Ciculation != value))
				{
					this._Ciculation = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
