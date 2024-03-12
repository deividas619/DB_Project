USE [Project]
GO
/****** Object:  StoredProcedure [dbo].[DeleteBookById]    Script Date: 2024-03-06 6:54:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ClearData]
AS
BEGIN
	DELETE FROM Departments
	DELETE FROM DepartmentLecture
	DELETE FROM Lectures
	DELETE FROM LectureStudent
	DELETE FROM Students
END