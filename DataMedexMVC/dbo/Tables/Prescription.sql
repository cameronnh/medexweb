CREATE TABLE [dbo].[prescription] (
    [prescriptionId]     INT           NOT NULL IDENTITY,
    [prescriptionName]   VARCHAR (250) NULL,
    [prescriptionDosage] VARCHAR (250) NULL,
    [classFID]           INT           NULL,
    PRIMARY KEY CLUSTERED ([prescriptionId] ASC), 
    CONSTRAINT [FK_prescription_ToTable] FOREIGN KEY ([classFID]) REFERENCES [prescriptionClasses]([classId])
);
