CREATE TABLE [dbo].[TelemetryLogs]
(
	[TelemetryLogId] BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[ApplicationId] VARCHAR(50),
	[UtcDate] DATETIME NOT NULL,
	[TelemetryData] NVARCHAR(MAX)
)

GO

CREATE INDEX [IX_TelemetryLogs_ApplicationId] ON [dbo].[TelemetryLogs] ([ApplicationId]) INCLUDE ([TelemetryLogId],[UtcDate],[TelemetryData]);
GO

CREATE INDEX [IX_TelemetryLogs_UtcDate] ON [dbo].[TelemetryLogs] ([UtcDate]) INCLUDE ([TelemetryLogId],[ApplicationId],[TelemetryData]);
GO

CREATE INDEX [IX_TelemetryLogs_UtcDate_ApplicationId] ON [dbo].[TelemetryLogs] ([UtcDate],[ApplicationId]) INCLUDE ([TelemetryLogId],[TelemetryData]);
GO