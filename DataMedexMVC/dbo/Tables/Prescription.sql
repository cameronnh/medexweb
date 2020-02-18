CREATE TABLE [dbo].[prescription] (
    [prescriptionId]     INT           NOT NULL IDENTITY,
    [prescriptionName]   VARCHAR (250) NOT NULL,
    [prescriptionDosage] VARCHAR (250) NOT NULL,
    [classFID]           INT           NOT NULL,
    CONSTRAINT [FK_prescription_ToTable] FOREIGN KEY ([classFID]) REFERENCES [prescriptionClasses]([classId]), 
    CONSTRAINT [PK_prescription] PRIMARY KEY ([prescriptionId])
);
