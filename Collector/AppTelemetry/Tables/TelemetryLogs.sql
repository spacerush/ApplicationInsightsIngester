CREATE TABLE [dbo].[TelemetryLogs]
(
	[TelemetryLogId] BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[UtcDate] DATETIME NOT NULL,
	[TelemetryData] NVARCHAR(MAX)
)
