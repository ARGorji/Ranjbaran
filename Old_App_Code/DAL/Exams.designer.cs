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
	public partial class ExamsDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertExams(Exams instance);
    partial void UpdateExams(Exams instance);
    partial void DeleteExams(Exams instance);
    #endregion
		
		public ExamsDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["RanjbaranConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public ExamsDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ExamsDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ExamsDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ExamsDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Exams> Exams
		{
			get
			{
				return this.GetTable<Exams>();
			}
		}
		
		public System.Data.Linq.Table<vExams> vExams
		{
			get
			{
				return this.GetTable<vExams>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Exams")]
	public partial class Exams : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Code;
		
		private string _Title;
		
		private string _Description;
		
		private System.Nullable<int> _HCGradeCode;
		
		private System.Nullable<int> _HCStudyFieldCode;
		
		private string _PDFFile;
		
		private string _Lesson;
		
		private System.Nullable<int> _GroupCode;
		
		private System.Nullable<bool> _Free;
		
		private System.Nullable<int> _Price;
		
		private System.Nullable<int> _HCLessonCode;
		
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
    partial void OnHCGradeCodeChanging(System.Nullable<int> value);
    partial void OnHCGradeCodeChanged();
    partial void OnHCStudyFieldCodeChanging(System.Nullable<int> value);
    partial void OnHCStudyFieldCodeChanged();
    partial void OnPDFFileChanging(string value);
    partial void OnPDFFileChanged();
    partial void OnLessonChanging(string value);
    partial void OnLessonChanged();
    partial void OnGroupCodeChanging(System.Nullable<int> value);
    partial void OnGroupCodeChanged();
    partial void OnFreeChanging(System.Nullable<bool> value);
    partial void OnFreeChanged();
    partial void OnPriceChanging(System.Nullable<int> value);
    partial void OnPriceChanged();
    partial void OnHCLessonCodeChanging(System.Nullable<int> value);
    partial void OnHCLessonCodeChanged();
    #endregion
		
		public Exams()
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Description", DbType="NVarChar(500)")]
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_HCGradeCode", DbType="Int")]
		public System.Nullable<int> HCGradeCode
		{
			get
			{
				return this._HCGradeCode;
			}
			set
			{
				if ((this._HCGradeCode != value))
				{
					this.OnHCGradeCodeChanging(value);
					this.SendPropertyChanging();
					this._HCGradeCode = value;
					this.SendPropertyChanged("HCGradeCode");
					this.OnHCGradeCodeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_HCStudyFieldCode", DbType="Int")]
		public System.Nullable<int> HCStudyFieldCode
		{
			get
			{
				return this._HCStudyFieldCode;
			}
			set
			{
				if ((this._HCStudyFieldCode != value))
				{
					this.OnHCStudyFieldCodeChanging(value);
					this.SendPropertyChanging();
					this._HCStudyFieldCode = value;
					this.SendPropertyChanged("HCStudyFieldCode");
					this.OnHCStudyFieldCodeChanged();
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Lesson", DbType="NVarChar(50)")]
		public string Lesson
		{
			get
			{
				return this._Lesson;
			}
			set
			{
				if ((this._Lesson != value))
				{
					this.OnLessonChanging(value);
					this.SendPropertyChanging();
					this._Lesson = value;
					this.SendPropertyChanged("Lesson");
					this.OnLessonChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GroupCode", DbType="Int")]
		public System.Nullable<int> GroupCode
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Free", DbType="Bit")]
		public System.Nullable<bool> Free
		{
			get
			{
				return this._Free;
			}
			set
			{
				if ((this._Free != value))
				{
					this.OnFreeChanging(value);
					this.SendPropertyChanging();
					this._Free = value;
					this.SendPropertyChanged("Free");
					this.OnFreeChanged();
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_HCLessonCode", DbType="Int")]
		public System.Nullable<int> HCLessonCode
		{
			get
			{
				return this._HCLessonCode;
			}
			set
			{
				if ((this._HCLessonCode != value))
				{
					this.OnHCLessonCodeChanging(value);
					this.SendPropertyChanging();
					this._HCLessonCode = value;
					this.SendPropertyChanged("HCLessonCode");
					this.OnHCLessonCodeChanged();
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.vExams")]
	public partial class vExams
	{
		
		private int _Code;
		
		private string _Title;
		
		private string _GradeName;
		
		private string _StudyName;
		
		private string _GroupName;
		
		private System.Nullable<bool> _Free;
		
		private System.Nullable<int> _Price;
		
		private System.Nullable<int> _HCGradeCode;
		
		private System.Nullable<int> _HCStudyFieldCode;
		
		private System.Nullable<int> _HCLessonCode;
		
		private string _Lesson;
		
		public vExams()
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GradeName", DbType="NVarChar(500)")]
		public string GradeName
		{
			get
			{
				return this._GradeName;
			}
			set
			{
				if ((this._GradeName != value))
				{
					this._GradeName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_StudyName", DbType="NVarChar(500)")]
		public string StudyName
		{
			get
			{
				return this._StudyName;
			}
			set
			{
				if ((this._StudyName != value))
				{
					this._StudyName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GroupName", DbType="NVarChar(500)")]
		public string GroupName
		{
			get
			{
				return this._GroupName;
			}
			set
			{
				if ((this._GroupName != value))
				{
					this._GroupName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Free", DbType="Bit")]
		public System.Nullable<bool> Free
		{
			get
			{
				return this._Free;
			}
			set
			{
				if ((this._Free != value))
				{
					this._Free = value;
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_HCGradeCode", DbType="Int")]
		public System.Nullable<int> HCGradeCode
		{
			get
			{
				return this._HCGradeCode;
			}
			set
			{
				if ((this._HCGradeCode != value))
				{
					this._HCGradeCode = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_HCStudyFieldCode", DbType="Int")]
		public System.Nullable<int> HCStudyFieldCode
		{
			get
			{
				return this._HCStudyFieldCode;
			}
			set
			{
				if ((this._HCStudyFieldCode != value))
				{
					this._HCStudyFieldCode = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_HCLessonCode", DbType="Int")]
		public System.Nullable<int> HCLessonCode
		{
			get
			{
				return this._HCLessonCode;
			}
			set
			{
				if ((this._HCLessonCode != value))
				{
					this._HCLessonCode = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Lesson", DbType="NVarChar(500)")]
		public string Lesson
		{
			get
			{
				return this._Lesson;
			}
			set
			{
				if ((this._Lesson != value))
				{
					this._Lesson = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
