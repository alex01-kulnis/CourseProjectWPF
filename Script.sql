use master; 

use Kulnis_Kp;

insert into  Users  (FirstName, Surname, Gender, Login, Password,Bday,IsAdmin )
	values	( 'admin', 'admin', 'Мужчина','admin', 'f6fdffe48c908deb0f4c3bd36c032e72',23-04-2020,1);
	--пароль adminadmin

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
	values	( 'Иванова В.К.', 'Врач-терапевт', '102','08:00', '10:00','16:00','18:00'),
			( 'Рачко О.Л.', 'Врач-терапевт', '104','11:00', '13:00','14:00','16:00'),
	    	( 'Жигало А.М.', 'Врач-терапевт', '108','14:00', '16:00','11:00','13:00');

	--insert into  Doctors (FIO, Post, Cabinet, Evenday_start )
	--values	( 'Иванова В.К.', 'Врач-терапевт', '102','08:00');
insert into  HistoryVisitings( FIO, Doctor, Cabinet, VisitDay,VisitTime,Info,UserId )
	values	( 'Иванова В.К.', 'Врач-терапевт', '102','13-04-2021', '10:00','info',1),
			( 'Иванова В.К.', 'Врач-терапевт', '102','15-12-2020', '10:00','info',1);

insert into  HistoryVisitings( FIO, Doctor, Cabinet, VisitDay,VisitTime,Info,UserId )
	values	( 'Иванова В.К.', 'Врач-терапевт', '102','13-04-2020', '10:00','info',1);
		
