CREATE TABLE [dbo].[Diaryes]
(
	[Id] BIGINT NOT NULL PRIMARY KEY, 
    [Type] NVARCHAR(10) NOT NULL, 
    [Theme] NVARCHAR(50) NOT NULL, 
    [BeginDate] DATETIME NOT NULL, 
    [EndDate] DATETIME NULL, 
    [Place] NVARCHAR(50) NULL, 
    [DoneStatus] NVARCHAR(100) NOT NULL DEFAULT 0, 
    CONSTRAINT [CK_Diaryes_Type] CHECK (Type in ('Встреча','Дело','Памятка')), 
    CONSTRAINT [DF_Diaryes_DoneStatus] CHECK (DoneStatus = 1 or DoneStatus =0)
)
