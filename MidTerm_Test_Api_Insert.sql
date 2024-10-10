USE [MidTermTestApi]
GO

INSERT dbo.Publishers
    (id, pub_name, phone, email, address, status)
VALUES
    (1, N'NXB Đồng Kim', '0123456789', 'nxb@dongkim.com', 'Unknown', 1),
    (2, N'Hoyoversa', '0987654321', 'cs@hoyoversa.com', 'Unknown', 1),
    (3, N'Microphoft', '0123987654', 'cs@microphoft.com', 'Unknown', 1),
    (4, N'Mendel', '02391398293', 'helper@mendel.com', 'Unknown', 1),
    (5, N'Amazol', '05920139023', 'helper@amazol.com', 'Unknown', 1)

INSERT dbo.Titles
    (id, title, code, type, pub_id, number, price, notes, status)
VALUES
    (1, N'Thám tử lừng danh Locan', '101', 'Unknown', 1, 101, '40000', '', 1),
    (2, N'Bé học tiếng Anh', '102', 'Unknown', 1, 102, '12000', '', 1),
    (3, N'How to teach otaku save the world', '2020', 'Unknown', 2, 2020, '150000', '', 1),
    (4, N'Gesshin Guide', '2032', 'Unknown', 2, 2032, '100000', '', 1)

INSERT dbo.Authors
    (id, au_lname, au_fname, phone, email, address, status)
VALUES
    (1, N'Nguyễn Hoàng', N'Anh', '09238392018', 'hoanganh2401@gmail.com', 'Unknown', 1),
    (2, N'Trần Hoàng', N'Hải', '0985201293', 'thhai591@gmail.com', 'Unknown', 1),
    (3, N'Chungon', N'Sei', '0123940102', 'seichungon29@gmail.com', 'Unknown', 1),
    (4, N'Chingcheng Han', N'Ji', '0210302034', 'chingchenghanji@gmail.com', 'Unknown', 1)

INSERT dbo.TitleAuthor
    (id, author_id, title_id, pub_date, royaltyper, sort, created_at, status)
VALUES
    (1, 1, 1, CAST(N'2021-03-22T14:42:40.457' AS DateTime), 1, 0, CAST(N'2021-03-22T14:42:40.457' AS DateTime), 1),
    (2, 2, 2, CAST(N'2021-04-22T15:22:20.293' AS DateTime), 1, 1, CAST(N'2021-04-22T15:22:20.293' AS DateTime), 1),
    (3, 3, 3, CAST(N'2021-03-19T02:14:42.193' AS DateTime), 0, 2, CAST(N'2021-03-19T02:14:42.193' AS DateTime), 1),
    (4, 4, 4, CAST(N'2021-04-05T21:51:09.231' AS DateTime), 0, 3, CAST(N'2021-04-05T21:51:09.231' AS DateTime), 1)
