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
	public partial class GalleriesDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertGalleries(Galleries instance);
    partial void UpdateGalleries(Galleries instance);
    partial void DeleteGalleries(Galleries instance);
    #endregion
		
		public GalleriesDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["RanjbaranConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public GalleriesDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public GalleriesDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public GalleriesDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public GalleriesDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Galleries> Galleries
		{
			get
			{
				return this.GetTable<Galleries>();
			}
		}
		
		public System.Data.Linq.Table<vGalleries> vGalleries
		{
			get
			{
				return this.GetTable<vGalleries>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Galleries")]
	public partial class Galleries : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Code;
		
		private string _Title;
		
		private string _PicFile;
		
		private string _SmallPicFile;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnCodeChanging(int value);
    partial void OnCodeChanged();
    partial void OnTitleChanging(string value);
    partial void OnTitleChanged();
    partial void OnPicFileChanging(string value);
    partial void OnPicFileChanged();
    partial void OnSmallPicFileChanging(string value);
    partial void OnSmallPicFileChanged();
    #endregion
		
		public Galleries()
		{
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Title", DbType="NVarChar(500)")]
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PicFile", DbType="NVarChar(200)")]
		public string PicFile
		{
			get
			{
				return this._PicFile;
			}
			set
			{
				if ((this._PicFile != value))
				{
					this.OnPicFileChanging(value);
					this.SendPropertyChanging();
					this._PicFile = value;
					this.SendPropertyChanged("PicFile");
					this.OnPicFileChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SmallPicFile", DbType="NVarChar(200)")]
		public string SmallPicFile
		{
			get
			{
				return this._SmallPicFile;
			}
			set
			{
				if ((this._SmallPicFile != value))
				{
					this.OnSmallPicFileChanging(value);
					this.SendPropertyChanging();
					this._SmallPicFile = value;
					this.SendPropertyChanged("SmallPicFile");
					this.OnSmallPicFileChanged();
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.vGalleries")]
	public partial class vGalleries
	{
		
		private int _Code;
		
		private string _Title;
		
		private string _PicFile;
		
		private string _SmallPicFile;
		
		public vGalleries()
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Title", DbType="NVarChar(500)")]
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PicFile", DbType="NVarChar(200)")]
		public string PicFile
		{
			get
			{
				return this._PicFile;
			}
			set
			{
				if ((this._PicFile != value))
				{
					this._PicFile = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SmallPicFile", DbType="NVarChar(200)")]
		public string SmallPicFile
		{
			get
			{
				return this._SmallPicFile;
			}
			set
			{
				if ((this._SmallPicFile != value))
				{
					this._SmallPicFile = value;
				}
			}
		}
	}
}
#pragma warning restore 1591