create database superpase;
use superpase;
create table login (user_rfid varchar(12),user_name varchar(100),user_ap varchar(100),user_am varchar(100),user_boleta varchar(10));
create table asistencia(user_rfid varchar(12),fecha timestamp);

delimiter $$
create procedure sp_registrar(user_rfid_in nvarchar(12),name_in nvarchar(100),ap_in nvarchar(100),am_in nvarchar(100),boleta_in nvarchar(10))
begin
declare existe int(2);

set existe = (select count(*) from login where user_rfid = user_rfid_in);

if existe = 0 then

INSERT INTO login VALUES(user_rfid_in,name_in,ap_in,am_in,boleta_in);
select concat('Registrado con exito el usuario con RFID: ',user_rfid_in) as result;
else
select 'Usuario ya Registrado' as result;
end if;
end$$
delimiter ;

delimiter $$
create procedure sp_pasarlista(user_rfid_in nvarchar(12))
begin
declare existe int(2);

set existe = (select count(*) from login where user_rfid = user_rfid_in);

if existe = 1 then
INSERT INTO asistencia VALUES(user_rfid_in,now());
select concat('Bienvenido(a) ',user_name,' ',user_ap,' ',user_am) from login where user_rfid = user_rfid_in;
else
select 'Alumno no registrado' as result;
end if ;
end$$
delimiter ;


delimiter $$
create procedure sp_get_lista()
begin
select concat(login.user_name,' ',login.user_ap,' ',login.user_am),login.user_boleta ,asistencia.fecha from login INNER JOIN asistencia ON asistencia.user_rfid = login.user_rfid;
end$$
delimiter ;


call sp_registrar(" F7 FB 08 3B","Gerardo Misael","Rico","Carlos","2016601438");
call sp_pasarlista(" F7 FB 08 3B");

select * from asistencia;
