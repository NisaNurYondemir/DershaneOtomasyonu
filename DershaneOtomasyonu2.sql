

-- =============================================================
-- BÖLÜM 1: TEMİZLİK VE ŞEMA
-- =============================================================
DROP SCHEMA public CASCADE;
CREATE SCHEMA public;
GRANT ALL ON SCHEMA public TO postgres;
GRANT ALL ON SCHEMA public TO public;

-- =============================================================
-- BÖLÜM 2: TABLOLAR (DDL)
-- =============================================================

-- 1. Roller
CREATE TABLE Roller (
    RolID SERIAL PRIMARY KEY,
    RolAdi VARCHAR(20) NOT NULL
);

-- 2. Kullanicilar
CREATE TABLE Kullanicilar (
    KullaniciID SERIAL PRIMARY KEY,
    Ad VARCHAR(50) NOT NULL,
    Soyad VARCHAR(50) NOT NULL,
    TCNo CHAR(11) UNIQUE NOT NULL,
    Sifre VARCHAR(50) NOT NULL, 
    RolID INT REFERENCES Roller(RolID),
    AktifMi BOOLEAN DEFAULT TRUE
);

-- 3. Dersler
CREATE TABLE Dersler (
    DersID SERIAL PRIMARY KEY,
    DersAdi VARCHAR(50) NOT NULL,
    SinifSeviyesi INT 
);

-- 4. Derslikler
CREATE TABLE Derslikler (
    DerslikID SERIAL PRIMARY KEY,
    DerslikAdi VARCHAR(20), 
    Kapasite INT
);

-- 5. OgrenciDetay
CREATE TABLE OgrenciDetay (
    OgrenciID INT PRIMARY KEY REFERENCES Kullanicilar(KullaniciID),
    SinifSeviyesi INT NOT NULL DEFAULT 9,
    DanismanOgretmenID INT REFERENCES Kullanicilar(KullaniciID)
);

-- 6. PersonelDetay
CREATE TABLE PersonelDetay (
    PersonelID INT PRIMARY KEY REFERENCES Kullanicilar(KullaniciID),
    BransID INT, 
    Maas DECIMAL(10,2),
    HaftalikDersKotasi INT DEFAULT 20
);

-- 7. DersProgrami
CREATE TABLE DersProgrami (
    ProgramID SERIAL PRIMARY KEY,
    DersID INT REFERENCES Dersler(DersID),
    OgretmenID INT REFERENCES Kullanicilar(KullaniciID),
    DerslikID INT REFERENCES Derslikler(DerslikID),
    Gun VARCHAR(15), 
    BaslangicSaati TIME,
    BitisSaati TIME,
    OnaylandiMi VARCHAR(20) DEFAULT 'Beklemede', -- 'Onaylandi', 'Reddedildi', 'Beklemede'
    SonDegisiklik TIMESTAMP
);

-- 8. Odevler
CREATE TABLE Odevler (
    OdevID SERIAL PRIMARY KEY,
    DersID INT REFERENCES Dersler(DersID),
    OgretmenID INT REFERENCES Kullanicilar(KullaniciID),
    Aciklama TEXT,
    TeslimTarihi DATE,
    VerilisTarihi DATE DEFAULT CURRENT_DATE
);

-- 9. Talepler
CREATE TABLE Talepler (
    TalepID SERIAL PRIMARY KEY,
    TalepEdenID INT REFERENCES Kullanicilar(KullaniciID),
    IlgiliOgretmenID INT REFERENCES Kullanicilar(KullaniciID),
    TalepTuru VARCHAR(50), 
    Aciklama TEXT,
    Durum VARCHAR(20) DEFAULT 'Beklemede', 
    OlusturmaTarihi TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- 10. Denemeler
CREATE TABLE Denemeler (
    DenemeID SERIAL PRIMARY KEY,
    DenemeAdi VARCHAR(100),
    Tarih DATE
);

-- 11. DenemeSonuclari
CREATE TABLE DenemeSonuclari (
    SonucID SERIAL PRIMARY KEY,
    DenemeID INT REFERENCES Denemeler(DenemeID),
    OgrenciID INT REFERENCES Kullanicilar(KullaniciID),
    DogruSayisi INT,
    YanlisSayisi INT,
    NetSayisi DECIMAL(5,2), 
    Puan DECIMAL(5,2)
);

-- 12. OzelDersler
CREATE TABLE OzelDersler (
    OzelDersID SERIAL PRIMARY KEY,
    OgretmenID INT REFERENCES Kullanicilar(KullaniciID),
    OgrenciID INT REFERENCES Kullanicilar(KullaniciID),
    DersID INT REFERENCES Dersler(DersID),
    Tarih DATE,
    Saat TIME,
    Durum VARCHAR(20) DEFAULT 'Beklemede'
);

-- 13. Duyurular
CREATE TABLE Duyurular (
    DuyuruID SERIAL PRIMARY KEY,
    Baslik VARCHAR(100) NOT NULL,
    Icerik TEXT NOT NULL,
    Tarih TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    HedefKitle VARCHAR(20)
);

-- 14. SilinenTaleplerLog
CREATE TABLE SilinenTaleplerLog (
    LogID SERIAL PRIMARY KEY,
    EskiTalepID INT,
    SilinmeTarihi TIMESTAMP DEFAULT NOW(),
    SilinenVeri TEXT
);

-- =============================================================
-- BÖLÜM 3: FONKSİYONLAR, PROSEDÜRLER VE TRIGGERLAR
-- =============================================================

-- A) Tarih Güncelleme
CREATE OR REPLACE FUNCTION tarih_guncelle()
RETURNS TRIGGER AS $$
BEGIN
    NEW.SonDegisiklik = NOW();
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_program_guncelle
BEFORE UPDATE ON DersProgrami
FOR EACH ROW
EXECUTE FUNCTION tarih_guncelle();

-- B) Sınıf Kapasite Kontrolü
CREATE OR REPLACE FUNCTION fn_sinif_kapasite_kontrol()
RETURNS TRIGGER AS $$
DECLARE
    mevcut_sayi INT;
BEGIN
    SELECT COUNT(*) INTO mevcut_sayi FROM OgrenciDetay WHERE SinifSeviyesi = NEW.SinifSeviyesi;
    IF mevcut_sayi >= 10 THEN
        RAISE EXCEPTION 'Bu sınıf seviyesi (%. Sınıf) dolu! Maksimum 10 öğrenci kayıt olabilir.', NEW.SinifSeviyesi;
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_sinif_kontrol
BEFORE INSERT ON OgrenciDetay
FOR EACH ROW
EXECUTE FUNCTION fn_sinif_kapasite_kontrol();

-- C) Ders Programı Çakışma (AYNI DERSLİK)
CREATE OR REPLACE FUNCTION fn_ders_cakisma_kontrol()
RETURNS TRIGGER AS $$
DECLARE
    cakisma_var_mi INT;
BEGIN
    SELECT COUNT(*) INTO cakisma_var_mi
    FROM DersProgrami
    WHERE DerslikID = NEW.DerslikID 
      AND Gun = NEW.Gun
      AND ProgramID <> NEW.ProgramID 
      AND (NEW.BaslangicSaati < BitisSaati AND NEW.BitisSaati > BaslangicSaati);

    IF cakisma_var_mi > 0 THEN
        RAISE EXCEPTION 'Çakışma Hatası (Derslik): % günü, belirtilen saatlerde bu derslik zaten dolu!', NEW.Gun;
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_ders_cakisma
BEFORE INSERT OR UPDATE ON DersProgrami
FOR EACH ROW
EXECUTE FUNCTION fn_ders_cakisma_kontrol();

-- D) Sınıf Programı Çakışma (AYNI SINIF SEVİYESİ)
CREATE OR REPLACE FUNCTION fn_sinif_ders_cakisma_kontrol()
RETURNS TRIGGER AS $$
DECLARE
    yeni_sinif_seviyesi INT;
    cakisma_var_mi INT;
BEGIN
    SELECT SinifSeviyesi INTO yeni_sinif_seviyesi FROM Dersler WHERE DersID = NEW.DersID;

    SELECT COUNT(*) INTO cakisma_var_mi
    FROM DersProgrami dp
    JOIN Dersler d ON dp.DersID = d.DersID
    WHERE d.SinifSeviyesi = yeni_sinif_seviyesi 
      AND dp.Gun = NEW.Gun
      AND dp.ProgramID <> NEW.ProgramID
      AND (NEW.BaslangicSaati < dp.BitisSaati AND NEW.BitisSaati > dp.BaslangicSaati);

    IF cakisma_var_mi > 0 THEN
        RAISE EXCEPTION 'Çakışma Hatası (Sınıf): % günü, belirtilen saatte %. Sınıfların zaten başka bir dersi var!', NEW.Gun, yeni_sinif_seviyesi;
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_sinif_ders_cakisma
BEFORE INSERT OR UPDATE ON DersProgrami
FOR EACH ROW
EXECUTE FUNCTION fn_sinif_ders_cakisma_kontrol();

-- E) Silme Loglaması
CREATE OR REPLACE FUNCTION fn_talep_silme_log()
RETURNS TRIGGER AS $$
BEGIN
    INSERT INTO SilinenTaleplerLog (EskiTalepID, SilinenVeri)
    VALUES (OLD.TalepID, 'Talep Türü: ' || OLD.TalepTuru || ' - Açıklama: ' || OLD.Aciklama);
    RETURN OLD;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_talep_sil_log
AFTER DELETE ON Talepler
FOR EACH ROW
EXECUTE FUNCTION fn_talep_silme_log();

-- F) Prosedürler (Talep ve Özel Ders Onay)
CREATE OR REPLACE PROCEDURE TalepOnayla(p_talep_id INT, p_durum VARCHAR)
LANGUAGE plpgsql AS $$
BEGIN
    UPDATE Talepler SET Durum = p_durum WHERE TalepID = p_talep_id;
END;
$$;

CREATE OR REPLACE PROCEDURE OzelDersOnayla(p_ozel_ders_id INT)
LANGUAGE plpgsql AS $$
DECLARE
    v_ogretmen_id INT;
    v_program_saati INT;
    v_ozel_ders_saati INT;
    v_kota INT;
BEGIN
    SELECT OgretmenID INTO v_ogretmen_id FROM OzelDersler WHERE OzelDersID = p_ozel_ders_id;
    SELECT HaftalikDersKotasi INTO v_kota FROM PersonelDetay WHERE PersonelID = v_ogretmen_id;
    
    SELECT COUNT(*) INTO v_program_saati FROM DersProgrami WHERE OgretmenID = v_ogretmen_id;
    SELECT COUNT(*) INTO v_ozel_ders_saati FROM OzelDersler WHERE OgretmenID = v_ogretmen_id AND Durum = 'Onaylandi';

    IF (v_program_saati + v_ozel_ders_saati + 1) > v_kota THEN
        RAISE EXCEPTION 'Öğretmenin haftalik ders kotası (%) dolu! Ders onaylanamaz.', v_kota;
    ELSE
        UPDATE OzelDersler SET Durum = 'Onaylandi' WHERE OzelDersID = p_ozel_ders_id;
    END IF;
END;
$$;

-- =============================================================
-- BÖLÜM 4: GÜVENLİK (ROLLER VE RLS)
-- =============================================================

-- 1. Rolleri Oluştur
DO $$ 
BEGIN
    IF NOT EXISTS (SELECT FROM pg_catalog.pg_roles WHERE rolname = 'ogrenci_rolu') THEN CREATE ROLE ogrenci_rolu; END IF;
    IF NOT EXISTS (SELECT FROM pg_catalog.pg_roles WHERE rolname = 'rehber_rolu') THEN CREATE ROLE rehber_rolu; END IF;
END $$;

-- 2. Şema ve Tablo İzinleri (EN ÖNEMLİ KISIM - Hata buradaydı)
GRANT USAGE ON SCHEMA public TO ogrenci_rolu;
GRANT USAGE ON SCHEMA public TO rehber_rolu;

GRANT SELECT ON Denemeler, DenemeSonuclari, Talepler, DersProgrami TO ogrenci_rolu;
GRANT SELECT ON Denemeler, DenemeSonuclari, Talepler, DersProgrami TO rehber_rolu;

-- RLS politikası için öğrencinin bu tabloları okuması lazım:
GRANT SELECT ON Dersler, Derslikler, OgrenciDetay TO ogrenci_rolu;

-- 3. RLS Politikaları
ALTER TABLE DenemeSonuclari ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS ogrenci_sadece_kendini_gorur ON DenemeSonuclari;
CREATE POLICY ogrenci_sadece_kendini_gorur ON DenemeSonuclari FOR SELECT TO ogrenci_rolu
USING (OgrenciID = current_setting('app.current_user_id', true)::INT);

ALTER TABLE Talepler ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS talep_sahibi_gorebilir ON Talepler;
CREATE POLICY talep_sahibi_gorebilir ON Talepler FOR SELECT TO ogrenci_rolu
USING (TalepEdenID = current_setting('app.current_user_id', true)::INT);

ALTER TABLE DersProgrami ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS ogrenci_kendi_programini_gorur ON DersProgrami;
CREATE POLICY ogrenci_kendi_programini_gorur ON DersProgrami FOR SELECT TO ogrenci_rolu
USING (
    DersID IN (
        SELECT d.DersID 
        FROM Dersler d
        JOIN OgrenciDetay o ON d.SinifSeviyesi = o.SinifSeviyesi
        WHERE o.OgrenciID = current_setting('app.current_user_id', true)::INT
    )
);

-- Rehber her şeyi görsün
CREATE POLICY rehber_sonuc_gor ON DenemeSonuclari FOR SELECT TO rehber_rolu USING (true);
CREATE POLICY rehber_program_gor ON DersProgrami FOR SELECT TO rehber_rolu USING (true);

-- =============================================================
-- BÖLÜM 5: VERİ GİRİŞİ (INSERT)
-- =============================================================

-- Roller
INSERT INTO Roller (RolAdi) VALUES ('Ogrenci'), ('Ogretmen'), ('Mudur'), ('Idare'), ('Rehberlik');

-- Kullanıcılar (Yönetim)
INSERT INTO Kullanicilar (Ad, Soyad, TCNo, Sifre, RolID) VALUES
('Hasan', 'Yılmaz',  '10000000000', '1234', 3), -- ID:1 Müdür
('Fatma', 'Demir',   '20000000000', '1234', 4), -- ID:2 Memur
('Mustafa', 'Çelik', '30000000000', '1234', 2), -- ID:3 Mat Öğrt
('Sevgi', 'Öztürk',  '40000000000', '1234', 2), -- ID:4 Fizik Öğrt
('Selin', 'Kaya',    '50000000000', '1234', 5); -- ID:5 Rehberlik

-- Personel Detay
INSERT INTO PersonelDetay (PersonelID, BransID, Maas, HaftalikDersKotasi) VALUES
(1, 0, 85000, 0),
(2, 0, 40000, 0),
(3, 1, 55000, 20),
(4, 2, 55000, 20),
(5, 0, 45000, 0);



-- Kullanıcılar (Öğrenci)
INSERT INTO Kullanicilar (Ad, Soyad, TCNo, Sifre, RolID) VALUES
('Nisa', 'Yıldız', '11111111111', '1234', 1), -- ID:6
('Ceren', 'Celik', '22222222222', '1234', 1); -- ID:7

-- Öğrenci Detay
INSERT INTO OgrenciDetay (OgrenciID, SinifSeviyesi, DanismanOgretmenID) VALUES
(6, 12, 5),
(7, 12, 5);

-- Dersler
INSERT INTO Dersler (DersAdi, SinifSeviyesi) VALUES
('Matematik', 12),('Matematik', 11), ('Matematik', 10), ('Matematik', 9),
('Fizik', 12), ('Fizik', 11),('Fizik', 10),('Fizik', 9),
('Türkçe', 12), ('Türkçe', 11), ('Türkçe', 10), ('Türkçe', 9),
('Geometri', 12),('Geometri', 11),('Geometri', 10),('Geometri', 9);

-- Derslikler
INSERT INTO Derslikler (DerslikAdi, Kapasite) VALUES
('101-A', 20), ('102-B', 25), ('Zemin Lab', 15);

-- Ders Programı (String olarak 'Beklemede' veya 'Onaylandi')
INSERT INTO DersProgrami (OgretmenID, DersID, DerslikID, Gun, BaslangicSaati, BitisSaati, OnaylandiMi) VALUES
(3, 1, 1, 'Pazartesi', '09:00', '10:30', 'Beklemede'), -- Mustafa Hoca Mat
(4, 2, 2, 'Salı',      '11:00', '12:30', 'Onaylandi'); -- Sevgi Hoca Fizik

-- Denemeler
INSERT INTO Denemeler (DenemeAdi, Tarih) VALUES 
('YKS-1 Türkiye Geneli', '2025-01-15'),
('TYT Kurumsal Deneme', '2025-02-01');

-- Deneme Sonuçları
INSERT INTO DenemeSonuclari (DenemeID, OgrenciID, DogruSayisi, YanlisSayisi, NetSayisi, Puan) VALUES
(1, 6, 90, 10, 87.50, 450.00), -- Nisa
(1, 7, 75, 25, 68.75, 380.00); -- Ceren

-- Ödevler
INSERT INTO Odevler (DersID, OgretmenID, Aciklama, TeslimTarihi) VALUES
(1, 3, 'İntegral fasikülü ilk 3 test bitecek', '2025-01-10');

-- Talepler
INSERT INTO Talepler (TalepEdenID, IlgiliOgretmenID, TalepTuru, Aciklama) VALUES
(6, 3, 'OzelDers', 'Matematik ek ders istiyorum.');

-- Duyurular
INSERT INTO Duyurular (Baslik, Icerik, HedefKitle) VALUES
('Yarıyıl Tatili', 'Dershanemiz 1 hafta kapalı olacaktır.', 'Tumu');



-- Nisa Yıldız (12. Sınıf) için ek ders programı verileri
-- Not: Bu dersler 12. sınıf olduğu için Nisa sisteme girdiğinde otomatik görecektir.

-- 1. ÇARŞAMBA: 09:00 - 10:30 (Mustafa Hoca - Matematik)
INSERT INTO DersProgrami (OgretmenID, DersID, DerslikID, Gun, BaslangicSaati, BitisSaati, OnaylandiMi) 
VALUES (3, 1, 1, 'Çarşamba', '09:00', '10:30', 'Onaylandi');

-- 2. ÇARŞAMBA: 11:00 - 12:30 (Sevgi Hoca - Fizik)
INSERT INTO DersProgrami (OgretmenID, DersID, DerslikID, Gun, BaslangicSaati, BitisSaati, OnaylandiMi) 
VALUES (4, 5, 2, 'Çarşamba', '11:00', '12:30', 'Onaylandi');

-- 3. PERŞEMBE: 13:00 - 14:30 (Mustafa Hoca - Matematik Tekrar)
INSERT INTO DersProgrami (OgretmenID, DersID, DerslikID, Gun, BaslangicSaati, BitisSaati, OnaylandiMi) 
VALUES (3, 1, 1, 'Perşembe', '13:00', '14:30', 'Onaylandi');


SELECT* FROM Kullanicilar;

SELECT * FROM DersProgrami;

SELECT* FROM Derslikler;

SELECT * FROM Talepler;

INSERT INTO Talepler (TalepEdenID, IlgiliOgretmenID, TalepTuru, Aciklama) VALUES
(11, 3, 'OzelDers', 'Matematik ek ders istiyorum.'),
(11, 3, 'OzelDers', 'Matematik'),
(6, 3, 'OzelDers', 'Matematik ek ders istiyorum.')
;

UPDATE PersonelDetay 
SET HaftalikDersKotasi = 8 
WHERE PersonelID = 3;

SELECT* FROM OgrenciDetay;
SELECT* FROM Kullanicilar;
SELECT* FROM DenemeSonuclari;

INSERT INTO Kullanicilar (KullaniciID, Ad, Soyad, TCNo, Sifre, RolID) VALUES
(9,  'irem',  'c',      '16161616161', '1234', 1),
(11, 'Ahmet', 'H',      '18181818181', '1234', 1),
(12, 'a',     'b',      '13131313131', '1234', 1),
(13, 'p',     'c',      '12121212121', '1234', 1),
(14, 'd',     'k',      '45454545454', '1234', 1),
(15, 'k',     'o',      '98989898989', '1234', 1),
(16, 'p',     'l',      '65656565455', '1234', 1),
(17, 'pn',    'h',      '52525252525', '1234', 1),
(18, 'li',    'mn',     '47474747474', '1234', 1),
(19, 'kk',    'aa',     '65858475955', '1234', 1),
(20, 'mmb',   'vb',     '41785178922', '1234', 1)
ON CONFLICT (KullaniciID) DO NOTHING;


INSERT INTO Denemeler (DenemeAdi, Tarih) 
VALUES ('YKS Yıl Sonu Değerlendirme Sınavı', CURRENT_DATE);

SELECT * FROM Denemeler;


-- ID: 6 (Nisa Yıldız) 
INSERT INTO DenemeSonuclari (DenemeID, OgrenciID, DogruSayisi, YanlisSayisi, NetSayisi, Puan)
VALUES ((SELECT MAX(DenemeID) FROM Denemeler), 6, 110, 5, 108.75, 490.00);

SELECT * FROM DenemeSonuclari;
-- ID: 9 (İrem C) 
INSERT INTO DenemeSonuclari (DenemeID, OgrenciID, DogruSayisi, YanlisSayisi, NetSayisi, Puan)
VALUES ((SELECT MAX(DenemeID) FROM Denemeler), 9, 95, 15, 91.25, 430.50);

-- ID: 11 (Ahmet H)
INSERT INTO DenemeSonuclari (DenemeID, OgrenciID, DogruSayisi, YanlisSayisi, NetSayisi, Puan)
VALUES ((SELECT MAX(DenemeID) FROM Denemeler), 11, 80, 25, 73.75, 380.00);


-- ID: 14 (d k)
INSERT INTO DenemeSonuclari (DenemeID, OgrenciID, DogruSayisi, YanlisSayisi, NetSayisi, Puan)
VALUES ((SELECT MAX(DenemeID) FROM Denemeler), 14, 55, 35, 46.25, 290.00);


-- ID: 16 (p l)
INSERT INTO DenemeSonuclari (DenemeID, OgrenciID, DogruSayisi, YanlisSayisi, NetSayisi, Puan)
VALUES ((SELECT MAX(DenemeID) FROM Denemeler), 16, 85, 10, 82.50, 410.00);

-- ID: 17 (pn h)
INSERT INTO DenemeSonuclari (DenemeID, OgrenciID, DogruSayisi, YanlisSayisi, NetSayisi, Puan)
VALUES ((SELECT MAX(DenemeID) FROM Denemeler), 17, 75, 20, 70.00, 370.00);



-- ID: 20 (mmb vb)
INSERT INTO DenemeSonuclari (DenemeID, OgrenciID, DogruSayisi, YanlisSayisi, NetSayisi, Puan)
VALUES ((SELECT MAX(DenemeID) FROM Denemeler), 20, 40, 60, 25.00, 210.00);
