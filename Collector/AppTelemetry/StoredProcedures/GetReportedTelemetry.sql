CREATE PROCEDURE [dbo].[GetReportedTelemetry]
	@startDate DATETIME,
	@endDate DATETIME
AS

SELECT TelemetryLogId,TelemetryData
FROM TelemetryLogs
CROSS APPLY OPENJSON(TelemetryData)

RETURN 0
