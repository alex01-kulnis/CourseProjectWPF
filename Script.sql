use master; 

use Kulnis_Kp;

insert into  Users  (FirstName, Surname, Gender, Login, Password,Bday,IsAdmin )
	values	( 'admin', 'admin', '�������','admin', 'f6fdffe48c908deb0f4c3bd36c032e72',23-04-2020,1);
	--������ adminadmin

--
--Create table Doctors
--(
--	ID int primary key IDENTITY,
--	FIO nvarchar(MAX),
--	Post nvarchar(MAX),
--	Cabinet nvarchar(MAX),
--	Evenday_start time,
--	Evenday_end time,
--	Oddday_start time,
--	Oddday_end time,	
--);

insert into  Doctors (FIO, Post, Cabinet, Evenday_start, Evenday_end,Oddday_start,Oddday_end )
	values	( '������� �.�.', '����-��������', '102','08:00', '10:00','16:00','18:00'),
			( '����� �.�.', '����-��������', '104','11:00', '13:00','14:00','16:00'),
	    	( '������ �.�.', '����-��������', '108','14:00', '16:00','11:00','13:00');

	--insert into  Doctors (FIO, Post, Cabinet, Evenday_start )
	--values	( '������� �.�.', '����-��������', '102','08:00');
insert into  HistoryVisitings( FIO, Doctor, Cabinet, VisitDay,VisitTime,Info,UserId )
	values	( '������� �.�.', '����-��������', '102','13-04-2021', '10:00','info',1),
			( '������� �.�.', '����-��������', '102','15-12-2020', '10:00','info',1);

insert into  HistoryVisitings( FIO, Doctor, Cabinet, VisitDay,VisitTime,Info,UserId )
	values	( '������� �.�.', '����-��������', '102','13-04-2020', '10:00','info',1);
		
