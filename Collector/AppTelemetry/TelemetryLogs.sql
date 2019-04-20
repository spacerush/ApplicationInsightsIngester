CREATE TABLE [dbo].[TelemetryLogs]
(
	[TelemetryLogId] BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[TelemetryData] NVARCHAR(MAX)
)
