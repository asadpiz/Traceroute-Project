﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;



[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="TraceRoute")]
public partial class TraceRouteDataClassesDataContext : System.Data.Linq.DataContext
{
	
	private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
	
  #region Extensibility Method Definitions
  partial void OnCreated();
  partial void InsertRoute(Route instance);
  partial void UpdateRoute(Route instance);
  partial void DeleteRoute(Route instance);
  partial void InsertWebsiteName(WebsiteName instance);
  partial void UpdateWebsiteName(WebsiteName instance);
  partial void DeleteWebsiteName(WebsiteName instance);
  #endregion
	
	public TraceRouteDataClassesDataContext() : 
			base(global::System.Configuration.ConfigurationManager.ConnectionStrings["TraceRouteConnectionString"].ConnectionString, mappingSource)
	{
		OnCreated();
	}
	
	public TraceRouteDataClassesDataContext(string connection) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public TraceRouteDataClassesDataContext(System.Data.IDbConnection connection) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public TraceRouteDataClassesDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public TraceRouteDataClassesDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public System.Data.Linq.Table<Route> Routes
	{
		get
		{
			return this.GetTable<Route>();
		}
	}
	
	public System.Data.Linq.Table<WebsiteName> WebsiteNames
	{
		get
		{
			return this.GetTable<WebsiteName>();
		}
	}
	
	[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.GetRoutes")]
	public ISingleResult<GetRoutesResult> GetRoutes()
	{
		IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
		return ((ISingleResult<GetRoutesResult>)(result.ReturnValue));
	}
}

[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Routes")]
public partial class Route : INotifyPropertyChanging, INotifyPropertyChanged
{
	
	private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
	
	private long _id;
	
	private string _ip;
	
	private string _longlat;
	
	private string _RTT;
	
	private string _ASNumber;
	
	private string _ASName;
	
	private System.Nullable<long> _WebsiteID;
	
	private System.Nullable<System.DateTime> _Date_Time;
	
	private EntityRef<WebsiteName> _WebsiteName;
	
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnidChanging(long value);
    partial void OnidChanged();
    partial void OnipChanging(string value);
    partial void OnipChanged();
    partial void OnlonglatChanging(string value);
    partial void OnlonglatChanged();
    partial void OnRTTChanging(string value);
    partial void OnRTTChanged();
    partial void OnASNumberChanging(string value);
    partial void OnASNumberChanged();
    partial void OnASNameChanging(string value);
    partial void OnASNameChanged();
    partial void OnWebsiteIDChanging(System.Nullable<long> value);
    partial void OnWebsiteIDChanged();
    partial void OnDate_TimeChanging(System.Nullable<System.DateTime> value);
    partial void OnDate_TimeChanged();
    #endregion
	
	public Route()
	{
		this._WebsiteName = default(EntityRef<WebsiteName>);
		OnCreated();
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", AutoSync=AutoSync.OnInsert, DbType="BigInt NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
	public long id
	{
		get
		{
			return this._id;
		}
		set
		{
			if ((this._id != value))
			{
				this.OnidChanging(value);
				this.SendPropertyChanging();
				this._id = value;
				this.SendPropertyChanged("id");
				this.OnidChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ip", DbType="Text", UpdateCheck=UpdateCheck.Never)]
	public string ip
	{
		get
		{
			return this._ip;
		}
		set
		{
			if ((this._ip != value))
			{
				this.OnipChanging(value);
				this.SendPropertyChanging();
				this._ip = value;
				this.SendPropertyChanged("ip");
				this.OnipChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_longlat", DbType="Text", UpdateCheck=UpdateCheck.Never)]
	public string longlat
	{
		get
		{
			return this._longlat;
		}
		set
		{
			if ((this._longlat != value))
			{
				this.OnlonglatChanging(value);
				this.SendPropertyChanging();
				this._longlat = value;
				this.SendPropertyChanged("longlat");
				this.OnlonglatChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RTT", DbType="Text", UpdateCheck=UpdateCheck.Never)]
	public string RTT
	{
		get
		{
			return this._RTT;
		}
		set
		{
			if ((this._RTT != value))
			{
				this.OnRTTChanging(value);
				this.SendPropertyChanging();
				this._RTT = value;
				this.SendPropertyChanged("RTT");
				this.OnRTTChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ASNumber", DbType="Text", UpdateCheck=UpdateCheck.Never)]
	public string ASNumber
	{
		get
		{
			return this._ASNumber;
		}
		set
		{
			if ((this._ASNumber != value))
			{
				this.OnASNumberChanging(value);
				this.SendPropertyChanging();
				this._ASNumber = value;
				this.SendPropertyChanged("ASNumber");
				this.OnASNumberChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ASName", DbType="Text", UpdateCheck=UpdateCheck.Never)]
	public string ASName
	{
		get
		{
			return this._ASName;
		}
		set
		{
			if ((this._ASName != value))
			{
				this.OnASNameChanging(value);
				this.SendPropertyChanging();
				this._ASName = value;
				this.SendPropertyChanged("ASName");
				this.OnASNameChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_WebsiteID", DbType="BigInt")]
	public System.Nullable<long> WebsiteID
	{
		get
		{
			return this._WebsiteID;
		}
		set
		{
			if ((this._WebsiteID != value))
			{
				if (this._WebsiteName.HasLoadedOrAssignedValue)
				{
					throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
				}
				this.OnWebsiteIDChanging(value);
				this.SendPropertyChanging();
				this._WebsiteID = value;
				this.SendPropertyChanged("WebsiteID");
				this.OnWebsiteIDChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Date_Time", DbType="Date")]
	public System.Nullable<System.DateTime> Date_Time
	{
		get
		{
			return this._Date_Time;
		}
		set
		{
			if ((this._Date_Time != value))
			{
				this.OnDate_TimeChanging(value);
				this.SendPropertyChanging();
				this._Date_Time = value;
				this.SendPropertyChanged("Date_Time");
				this.OnDate_TimeChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.AssociationAttribute(Name="WebsiteName_Route", Storage="_WebsiteName", ThisKey="WebsiteID", OtherKey="id", IsForeignKey=true)]
	public WebsiteName WebsiteName
	{
		get
		{
			return this._WebsiteName.Entity;
		}
		set
		{
			WebsiteName previousValue = this._WebsiteName.Entity;
			if (((previousValue != value) 
						|| (this._WebsiteName.HasLoadedOrAssignedValue == false)))
			{
				this.SendPropertyChanging();
				if ((previousValue != null))
				{
					this._WebsiteName.Entity = null;
					previousValue.Routes.Remove(this);
				}
				this._WebsiteName.Entity = value;
				if ((value != null))
				{
					value.Routes.Add(this);
					this._WebsiteID = value.id;
				}
				else
				{
					this._WebsiteID = default(Nullable<long>);
				}
				this.SendPropertyChanged("WebsiteName");
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

[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.WebsiteName")]
public partial class WebsiteName : INotifyPropertyChanging, INotifyPropertyChanged
{
	
	private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
	
	private long _id;
	
	private string _Name;
	
	private EntitySet<Route> _Routes;
	
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnidChanging(long value);
    partial void OnidChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    #endregion
	
	public WebsiteName()
	{
		this._Routes = new EntitySet<Route>(new Action<Route>(this.attach_Routes), new Action<Route>(this.detach_Routes));
		OnCreated();
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", AutoSync=AutoSync.OnInsert, DbType="BigInt NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
	public long id
	{
		get
		{
			return this._id;
		}
		set
		{
			if ((this._id != value))
			{
				this.OnidChanging(value);
				this.SendPropertyChanging();
				this._id = value;
				this.SendPropertyChanged("id");
				this.OnidChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
	public string Name
	{
		get
		{
			return this._Name;
		}
		set
		{
			if ((this._Name != value))
			{
				this.OnNameChanging(value);
				this.SendPropertyChanging();
				this._Name = value;
				this.SendPropertyChanged("Name");
				this.OnNameChanged();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.AssociationAttribute(Name="WebsiteName_Route", Storage="_Routes", ThisKey="id", OtherKey="WebsiteID")]
	public EntitySet<Route> Routes
	{
		get
		{
			return this._Routes;
		}
		set
		{
			this._Routes.Assign(value);
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
	
	private void attach_Routes(Route entity)
	{
		this.SendPropertyChanging();
		entity.WebsiteName = this;
	}
	
	private void detach_Routes(Route entity)
	{
		this.SendPropertyChanging();
		entity.WebsiteName = null;
	}
}

public partial class GetRoutesResult
{
	
	private long _id;
	
	private string _ip;
	
	private string _longlat;
	
	private string _RTT;
	
	private string _ASNumber;
	
	private string _ASName;
	
	private System.Nullable<long> _WebsiteID;
	
	private System.Nullable<System.DateTime> _Date_Time;
	
	public GetRoutesResult()
	{
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", DbType="BigInt NOT NULL")]
	public long id
	{
		get
		{
			return this._id;
		}
		set
		{
			if ((this._id != value))
			{
				this._id = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ip", DbType="Text", UpdateCheck=UpdateCheck.Never)]
	public string ip
	{
		get
		{
			return this._ip;
		}
		set
		{
			if ((this._ip != value))
			{
				this._ip = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_longlat", DbType="Text", UpdateCheck=UpdateCheck.Never)]
	public string longlat
	{
		get
		{
			return this._longlat;
		}
		set
		{
			if ((this._longlat != value))
			{
				this._longlat = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RTT", DbType="Text", UpdateCheck=UpdateCheck.Never)]
	public string RTT
	{
		get
		{
			return this._RTT;
		}
		set
		{
			if ((this._RTT != value))
			{
				this._RTT = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ASNumber", DbType="Text", UpdateCheck=UpdateCheck.Never)]
	public string ASNumber
	{
		get
		{
			return this._ASNumber;
		}
		set
		{
			if ((this._ASNumber != value))
			{
				this._ASNumber = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ASName", DbType="Text", UpdateCheck=UpdateCheck.Never)]
	public string ASName
	{
		get
		{
			return this._ASName;
		}
		set
		{
			if ((this._ASName != value))
			{
				this._ASName = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_WebsiteID", DbType="BigInt")]
	public System.Nullable<long> WebsiteID
	{
		get
		{
			return this._WebsiteID;
		}
		set
		{
			if ((this._WebsiteID != value))
			{
				this._WebsiteID = value;
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Date_Time", DbType="Date")]
	public System.Nullable<System.DateTime> Date_Time
	{
		get
		{
			return this._Date_Time;
		}
		set
		{
			if ((this._Date_Time != value))
			{
				this._Date_Time = value;
			}
		}
	}
}
#pragma warning restore 1591
