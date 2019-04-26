create table [dbo].[Countries] (
	[Id] uniqueidentifier not null primary key,
	[CreationDate] datetime2(7) not null,
	[DeletedDate] datetime2(7),
	[Name] nvarchar(50) not null unique
)

create table [dbo].[Cities] (
	[Id] uniqueidentifier not null primary key,
	[CreationDate] datetime2(7) not null,
	[DeletedDate] datetime2(7),
	[Name] nvarchar(50) not null unique,
	[CountryId] uniqueidentifier not null
)

create table [dbo].[Streets] (
	[Id] uniqueidentifier not null primary key,
	[CreationDate] datetime2(7) not null,
	[DeletedDate] datetime2(7),
	[Name] nvarchar(50) not null unique,
	[CityId] uniqueidentifier not null
)