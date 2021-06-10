create database QuanLySoTietKiem
use QuanLySoTietKiem
set dateformat dmy

CREATE TABLE TaiKhoan
(
	TenDangnhap VARCHAR(100) NOT NULL,
	MatKhau VARCHAR(100) NOT NULL
)

insert into TaiKhoan(TenDangnhap,MatKhau) values ('admin1', '123456')
insert into TaiKhoan(TenDangnhap,MatKhau) values ('admin2', '123456')