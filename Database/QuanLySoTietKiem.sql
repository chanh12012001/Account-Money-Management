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

CREATE TABLE KhachHang
(
	MaKhachHang NVARCHAR(20) NOT NULL PRIMARY KEY,
	HoTen NVARCHAR(100) NOT NULL,
	GioiTinh NVARCHAR(3) NOT NULL,
	NgaySinh DATETIME NOT NULL,
	DiaChi NVARCHAR(256) NOT NULL,
	SoDienThoai NVARCHAR(10) NOT NULL,
	CMND NVARCHAR(40) NOT NULL,
	NgayCap DATETIME NOT NULL,
	NoiCap NVARCHAR(256) NOT NULL,
)	

insert into KhachHang(MaKhachHang,HoTen,GioiTinh,NgaySinh,DiaChi,SoDienThoai,CMND,NgayCap,NoiCap) values ('KH001', 'Nguyen Van A', 'Nam', '23-1-2006', 'Quan 1, TP.Ho Chi Minh', '0364768956', '215564895', '26/8/2010', 'TP.Ho Chi Minh' )
insert into KhachHang(MaKhachHang,HoTen,GioiTinh,NgaySinh,DiaChi,SoDienThoai,CMND,NgayCap,NoiCap) values ('KH002','Le Thi B', 'Nu', '23-1-1996', 'Quan 9, TP. TP.Ho Chi Minh', '0364768956', '215564895', '26/8/2010', 'TP.Ho Chi Minh' )
insert into KhachHang(MaKhachHang,HoTen,GioiTinh,NgaySinh,DiaChi,SoDienThoai,CMND,NgayCap,NoiCap) values ('KH001', N'Lê Thị C', N'Nữ', '23-1-1996', N'Quận 9, TP. TP.Hồ Chí Minh', N'0364768956', N'215564895', '26/8/2010', N'TP.Hồ Chí Minh' )

CREATE TABLE LoaiTietKiem
(
	MaLoaiTietKiem VARCHAR(20) NOT NULL PRIMARY KEY,
	TenLoaiTietKiem NVARCHAR(100) NOT NULL,
	KyHan INT NOT NULL,
	LaiSuat FLOAT NOT NULL,
)	

insert into LoaiTietKiem(MaLoaiTietKiem,TenLoaiTietKiem,KyHan,LaiSuat) values ('LTK001', N'Không Kỳ Hạn', 0, 0.15)
insert into LoaiTietKiem(MaLoaiTietKiem,TenLoaiTietKiem,KyHan,LaiSuat) values ('LTK002', N'3 Tháng', 3, 0.5)
insert into LoaiTietKiem(MaLoaiTietKiem,TenLoaiTietKiem,KyHan,LaiSuat) values ('LTK003', N'6 Tháng', 6, 0.55)


select MaLoaiTietKiem from LoaiTietKiem where TenLoaiTietKiem = '3 Tháng'


CREATE TABLE SoTietKiem
(
	MaSoTietKiem VARCHAR(20) NOT NULL PRIMARY KEY,
	MaKhachHang VARCHAR(100) NOT NULL,
	MaLoaiTietKiem VARCHAR(100) NOT NULL,
	NgayMoSo DATETIME NOT NULL,
	SoTienGuiTietKiem INT NOT NULL,
	SoLanDaoHan INT NOT NULL,
	TienLai INT NOT NULL,
	SoDu INT NOT NULL,
	TrangThaiSo INT NOT NULL,
)

insert into SoTietKiem(MaSoTietKiem,MaKhachHang,MaLoaiTietKiem,NgayMoSo,SoTienGuiTietKiem,SoLanDaoHan,TienLai, SoDu,TrangThaiSo) values ('STK001', 'KH001', 'LTK002', '12-05-2020', 1000000, 0, 0, 1000000, 1)
delete from PhieuGuiTien where MaPhieuGui = 'PG002'


CREATE TABLE PhieuGuiTien
(
	MaPhieuGui VARCHAR(20) NOT NULL PRIMARY KEY,
	MaSoTietKiem VARCHAR(100) NOT NULL,
	SoTienGui VARCHAR(100) NOT NULL,
	NgayGui DATETIME NOT NULL,	
)

ALTER TABLE PhieuRutTien ALTER COLUMN SoTienRut INT
insert into PhieuGuiTien(MaPhieuGui,MaSoTietKiem,SoTienGui,NgayGui) values ('PG001', 'STK001', 1000000, '12-5-2015')

select HoTen
from KhachHang KH, SoTietKiem STK
where KH.MaKhachHang = STK.MaKhachHang AND STK.MaSoTietKiem = 'STK001'

update SoTietKiem 
set SoDu = STK.SoDu + PGT.SoTienGui
from SoTietKiem STK, PhieuGuiTien PGT
WHERE STK.MaSoTietKiem = PGT.MaSoTietKiem

select MaLoaiTietKiem, sum(SoTienGuiTietKiem) as TongThu
from SoTietKiem
where NgayMoSo = '6-7-2021'
group by MaLoaiTietKiem

select sum(SoTienGui)
from PhieuGuiTien
where NgayGui = '6-7-2021'


select sum(SoTienRut)
from PhieuRutTien
where NgayRut = '6-7-2021'



CREATE TABLE PhieuRutTien
(
	MaPhieuRut VARCHAR(20) NOT NULL PRIMARY KEY,
	MaSoTietKiem VARCHAR(100) NOT NULL,
	SoTienRut VARCHAR(100) NOT NULL,
	NgayRut DATETIME NOT NULL,	
)
insert into PhieuRutTien(MaPhieuRut,MaSoTietKiem,SoTienRut,NgayRut) values ('PR001', 'STK002', 1000000, '12-5-2020')
select * from SoTietKiem
update SoTietKiem 
set TrangThaiSo = 0
from SoTietKiem
WHERE MaSoTietKiem = 'STK001'


CREATE TABLE ChiTietBaoCaoNgay
(
	MaChiTietBaoCao VARCHAR(20) NOT NULL PRIMARY KEY,
	MaLoaiTietKiem VARCHAR(100) NOT NULL,
	SoTienThu INT NOT NULL,
	SoTienChi INT NOT NULL,
	ChenhLech INT NOT NULL,
	NgayLap DATETIME NOT NULL,	
)

select * from ChiTietBaoCaoNgay
delete from ChiTietBaoCaoNgay where MaChiTietBaoCao = 'PR0081'



select NgayLap, MaLoaiTietKiem, Sum(SoTienThu) as TongThu, sum(SoTienChi) as TongChi, ABS(Sum(SoTienThu)- sum(SoTienChi)) as ChenhLech
from ChiTietBaoCaoNgay
where NgayLap = '6-7-2021'
GROUP BY NgayLap, MaLoaiTietKiem

CREATE TABLE ChiTietBaoCaoThang
(
	MaChiTietBaoCaoThang VARCHAR(20) NOT NULL PRIMARY KEY,
	NgayMoSo DATETIME NOT NULL,	
	MaSoTietKiem VARCHAR(100) NOT NULL,
	MaLoaiTietKiem VARCHAR(100) NOT NULL,
	SoMo INT NOT NULL,
	SoDong INT NOT NULL,
	ChenhLech INT NOT NULL,
)
select * from ChiTietBaoCaoThang

update ChiTietBaoCaoThang
set SoDong = 0, SoMo = 1
where MaSoTietKiem = 'STK008'

select NgayMoSo, sum(SoMo) as SoMo, sum(SoDong) as SoDong, ABS(sum(SoMo) - sum(SoDong)) AS ChenhLech
from ChiTietBaoCaoThang
where MONTH(NgayMoSo) = 7 and YEAR(NgayMoSo) = 2021 AND MaLoaiTietKiem = 'LTK001'
group by NgayMoSo


CREATE TABLE ThamSo
(
	MaThamSo VARCHAR(20) NOT NULL PRIMARY KEY,
	SoTienGuiToiThieu INT NOT NULL,
	SoTienGuiThemToiThieu INT NOT NULL,
	ThoiGianGuiToiThieu INT NOT NULL,
)

insert into ThamSo(MaThamSo,SoTienGuiToiThieu,SoTienGuiThemToiThieu,ThoiGianGuiToiThieu) values ('TH001', 100000, 100000, 15)

select * from ThamSo

update ThamSo
set SoTienGuiToiThieu = 100000
