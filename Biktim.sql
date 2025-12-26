CREATE DATABASE DershaneOtomasyonu2;

DROP SCHEMA public CASCADE;
CREATE SCHEMA public;
GRANT ALL ON SCHEMA public TO postgres;
GRANT ALL ON SCHEMA public TO public;

-- Roller Tablosu (yetki yonetimi icin)
CREATE TABLE Roller (
    RolID SERIAL PRIMARY KEY,
    RolAdi VARCHAR(20) NOT NULL -- Ogrenci, Ogretmen, Mudur, Idare, Rehberlik
);

-- Kullanicilar (login islemleri icin ortak tablo)
CREATE TABLE Kullanicilar (
    KullaniciID SERIAL PRIMARY KEY,
    Ad VARCHAR(50) NOT NULL,
    Soyad VARCHAR(50) NOT NULL,
    TCNo CHAR(11) UNIQUE NOT NULL,
    Sifre VARCHAR(50) NOT NULL, 
    RolID INT REFERENCES Roller(RolID),
    AktifMi BOOLEAN DEFAULT TRUE
);

CREATE TABLE Dersler (
    DersID SERIAL PRIMARY KEY,
    DersAdi VARCHAR(50) NOT NULL,
    SinifSeviyesi INT -- 9, 10, 11, 12 
);

CREATE TABLE Derslikler (
    DerslikID SERIAL PRIMARY KEY,
    DerslikAdi VARCHAR(20), 
    Kapasite INT
);

CREATE TABLE OgrenciDetay (
    OgrenciID INT PRIMARY KEY REFERENCES Kullanicilar(KullaniciID),
	DanismanOgretmenID INT REFERENCES Kullanicilar(KullaniciID) -- rehberlik iliskisi
);

-- Personel Detayları (Oğretmen, Memur, Mudur)
CREATE TABLE PersonelDetay (
    PersonelID INT PRIMARY KEY REFERENCES Kullanicilar(KullaniciID),
    BransID INT, 
    Maas DECIMAL(10,2)
);

-- Ders Programi (memur olusturur, mudur onaylar)
CREATE TABLE DersProgrami (
    ProgramID SERIAL PRIMARY KEY,
    DersID INT REFERENCES Dersler(DersID),
    OgretmenID INT REFERENCES Kullanicilar(KullaniciID),
    DerslikID INT REFERENCES Derslikler(DerslikID),
    Gun VARCHAR(15), 
    BaslangicSaati TIME,
    BitisSaati TIME,
    OnaylandiMi VARCHAR(20) DEFAULT 'Beklemede' -- 'Onaylandi', 'Reddedildi'
);

-- Odevler (ogretmen verir)
CREATE TABLE Odevler (
    OdevID SERIAL PRIMARY KEY,
    DersID INT REFERENCES Dersler(DersID),
    OgretmenID INT REFERENCES Kullanicilar(KullaniciID),
    Aciklama TEXT,
    TeslimTarihi DATE,
    VerilisTarihi DATE DEFAULT CURRENT_DATE
);

--  Talepler (ogrenci ve ogretmenlerin istekleri)
-- Tur: 'DersDegisimi', 'OzelDers', 'DanismanGorusmesi', 'EkDers'
CREATE TABLE Talepler (
    TalepID SERIAL PRIMARY KEY,
    TalepEdenID INT REFERENCES Kullanicilar(KullaniciID),
    IlgiliOgretmenID INT REFERENCES Kullanicilar(KullaniciID),
	TalepTuru VARCHAR(50),
    Aciklama TEXT,
    Durum VARCHAR(20) DEFAULT 'Beklemede', -- 'Onaylandi', 'Reddedildi'
    OlusturmaTarihi TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE Denemeler (
    DenemeID SERIAL PRIMARY KEY,
    DenemeAdi VARCHAR(100),
    Tarih DATE,
    DuyuruMetni TEXT
);

CREATE TABLE Duyurular (
    DuyuruID SERIAL PRIMARY KEY,
    Baslik VARCHAR(100) NOT NULL,
    Icerik TEXT NOT NULL,
    Tarih TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    HedefKitle VARCHAR(20) -- 'Tumu', 'Ogretmen', 'Ogrenci'
);

-- Denemeler tablosunu duzenleme 
ALTER TABLE Denemeler DROP COLUMN DuyuruMetni;

-- DenemeSonuclari Tablosu (Yeni - ogrenci netleri icin) -raporda bahsedilen yer
CREATE TABLE DenemeSonuclari (
    SonucID SERIAL PRIMARY KEY,
    DenemeID INT REFERENCES Denemeler(DenemeID),
    OgrenciID INT REFERENCES Kullanicilar(KullaniciID),
    DogruSayisi INT,
    YanlisSayisi INT,
    NetSayisi DECIMAL(5,2), 
    Puan DECIMAL(5,2)
);

CREATE TABLE OzelDersler (
    OzelDersID SERIAL PRIMARY KEY,
    OgretmenID INT REFERENCES Kullanicilar(KullaniciID),
    OgrenciID INT REFERENCES Kullanicilar(KullaniciID),
    DersID INT REFERENCES Dersler(DersID),
    Tarih DATE,
    Saat TIME,
    Durum VARCHAR(20) DEFAULT 'Beklemede' -- 'Beklemede', 'Onaylandi', 'Reddedildi'
);

CREATE TABLE SilinenTaleplerLog (
    LogID SERIAL PRIMARY KEY,
    EskiTalepID INT,
    SilinmeTarihi TIMESTAMP DEFAULT NOW(),
    SilinenVeri TEXT -- JSON veya Text olarak eski veriyi tutar
);

ALTER TABLE PersonelDetay ADD COLUMN HaftalikDersKotasi INT DEFAULT 20;

ALTER TABLE OgrenciDetay ADD COLUMN SinifSeviyesi INT NOT NULL DEFAULT 9;

CREATE OR REPLACE PROCEDURE TalepOnayla(
    p_talep_id INT, 
    p_durum VARCHAR
)
LANGUAGE plpgsql
AS $$
BEGIN
    -- Talebin durumunu guncelle ('Beklemede' -> 'Onaylandi')
    UPDATE Talepler
    SET Durum = p_durum
    WHERE TalepID = p_talep_id;
END;
$$;

-- tarih_guncelle() triggeri icin
ALTER TABLE DersProgrami ADD COLUMN SonDegisiklik TIMESTAMP;

CREATE OR REPLACE FUNCTION tarih_guncelle()
RETURNS TRIGGER AS $$
BEGIN
    NEW.SonDegisiklik = NOW();
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

-- trigger tanimi
CREATE TRIGGER trg_program_guncelle
BEFORE UPDATE ON DersProgrami
FOR EACH ROW
EXECUTE FUNCTION tarih_guncelle();


CREATE OR REPLACE FUNCTION fn_sinif_kapasite_kontrol()
RETURNS TRIGGER AS $$
DECLARE
    mevcut_sayi INT;
BEGIN
    -- Eklenmek istenen sinif seviyesindeki mevcut ogrenci sayisini bul
    SELECT COUNT(*) INTO mevcut_sayi 
    FROM OgrenciDetay 
    WHERE SinifSeviyesi = NEW.SinifSeviyesi;

    -- Eger 10 veya daha fazla ise hata ver
    IF mevcut_sayi >= 10 THEN
        RAISE EXCEPTION 'Bu sınıf seviyesi (%. Sınıf) dolu! Maksimum 10 öğrenci kayıt olabilir.', NEW.SinifSeviyesi;
    END IF;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

-- trigger'i bagla
CREATE TRIGGER trg_sinif_kontrol
BEFORE INSERT ON OgrenciDetay
FOR EACH ROW
EXECUTE FUNCTION fn_sinif_kapasite_kontrol();

CREATE OR REPLACE PROCEDURE OzelDersOnayla(p_ozel_ders_id INT)
LANGUAGE plpgsql
AS $$
DECLARE
    v_ogretmen_id INT;
    v_program_saati INT;
    v_ozel_ders_saati INT;
    v_kota INT;
BEGIN
    -- Bu ozel ders kime ait
    SELECT OgretmenID INTO v_ogretmen_id FROM OzelDersler WHERE OzelDersID = p_ozel_ders_id;

    -- Ogretmenin kotasini al
    SELECT HaftalikDersKotasi INTO v_kota FROM PersonelDetay WHERE PersonelID = v_ogretmen_id;

    -- Ders programindaki yukunu hesapla (her kayit bir saat)
    SELECT COUNT(*) INTO v_program_saati 
    FROM DersProgrami 
    WHERE OgretmenID = v_ogretmen_id;

    -- Onaylanmis diger ozel derslerini hesapla
    SELECT COUNT(*) INTO v_ozel_ders_saati 
    FROM OzelDersler 
    WHERE OgretmenID = v_ogretmen_id AND Durum = 'Onaylandi';

    -- Mevcut yuk + 1  > kota ise hata
    IF (v_program_saati + v_ozel_ders_saati + 1) > v_kota THEN
        RAISE EXCEPTION 'Öğretmenin haftalik ders kotası (%) dolu! Ders onaylanamaz.', v_kota;
    ELSE
        -- Sorun yoksa onayla
        UPDATE OzelDersler SET Durum = 'Onaylandi' WHERE OzelDersID = p_ozel_ders_id;
    END IF;
END;
$$;


CREATE OR REPLACE FUNCTION fn_ders_cakisma_kontrol()
RETURNS TRIGGER AS $$
DECLARE
    cakisma_var_mi INT;
BEGIN
    -- Aynı gun, ayni derslikte, saatleri cakisan kayit var mi
    SELECT COUNT(*) INTO cakisma_var_mi
    FROM DersProgrami
    WHERE DerslikID = NEW.DerslikID 
      AND Gun = NEW.Gun
      AND ProgramID <> NEW.ProgramID -- Kendi kendisiyle çakışmasın (Update için)
      AND (NEW.BaslangicSaati < BitisSaati AND NEW.BitisSaati > BaslangicSaati);

    IF cakisma_var_mi > 0 THEN
        RAISE EXCEPTION 'Çakışma Hatası: % günü, belirtilen saatlerde bu derslik zaten dolu!', NEW.Gun;
    END IF;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

-- trigger'i baglama (hem ekleme hem guncelleme icin)
CREATE TRIGGER trg_ders_cakisma
BEFORE INSERT OR UPDATE ON DersProgrami
FOR EACH ROW
EXECUTE FUNCTION fn_ders_cakisma_kontrol();


-- RLS'yi Aktif Et
ALTER TABLE DersProgrami ENABLE ROW LEVEL SECURITY;

-- rolleri olustur
DO $$ 
BEGIN
    IF NOT EXISTS (SELECT FROM pg_catalog.pg_roles WHERE rolname = 'ogrenci_rolu') THEN 
        CREATE ROLE ogrenci_rolu; 
    END IF;
    
    IF NOT EXISTS (SELECT FROM pg_catalog.pg_roles WHERE rolname = 'rehber_rolu') THEN 
        CREATE ROLE rehber_rolu; 
    END IF;
END $$;

GRANT USAGE ON SCHEMA public TO ogrenci_rolu;
GRANT USAGE ON SCHEMA public TO rehber_rolu;

-- "Eger giren kisi 'ogrenci_rolu' ise; 
-- Sadece kendi sinif seviyesindeki dersleri gorebilsin."
CREATE POLICY ogrenci_kendi_programini_gorur ON DersProgrami
FOR SELECT
TO ogrenci_rolu
USING (
    DersID IN (
        SELECT d.DersID 
        FROM Dersler d
        JOIN OgrenciDetay o ON d.SinifSeviyesi = o.SinifSeviyesi
        WHERE o.OgrenciID = current_setting('app.current_user_id', true)::INT
    )
);

-- Diger roller icin (Rehber, İdare vb. her şeyi görsün)
CREATE POLICY idare_her_seyi_gorur_program ON DersProgrami
FOR SELECT
TO rehber_rolu, postgres
USING (true);


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


-- Sinif/Ogrenci programi cakisma kontrolu 
CREATE OR REPLACE FUNCTION fn_sinif_ders_cakisma_kontrol()
RETURNS TRIGGER AS $$
DECLARE
    yeni_sinif_seviyesi INT;
    cakisma_var_mi INT;
BEGIN
    -- Eklenmek istenen dersin sinif seviyesini bul (Matematik -> 12)
    SELECT SinifSeviyesi INTO yeni_sinif_seviyesi 
    FROM Dersler 
    WHERE DersID = NEW.DersID;

    -- Kontrol: Ayni gun, ayni saatte, AYNI SINIF SEVİYESİNE ait baska ders var mi?
    SELECT COUNT(*) INTO cakisma_var_mi
    FROM DersProgrami dp
    JOIN Dersler d ON dp.DersID = d.DersID
    WHERE d.SinifSeviyesi = yeni_sinif_seviyesi -- Ayni sinifa hitap eden dersler
      AND dp.Gun = NEW.Gun                    -- Aynı gun
      AND dp.ProgramID <> NEW.ProgramID       -- (Guncelleme yapiyorsak kendisini sayma)
      AND (NEW.BaslangicSaati < dp.BitisSaati AND NEW.BitisSaati > dp.BaslangicSaati); -- Zaman cakismasi

    -- Hata
    IF cakisma_var_mi > 0 THEN
        RAISE EXCEPTION 'Çakışma Hatası: % günü, belirtilen saatte %. Sınıfların zaten başka bir dersi var! Öğrenciler ikiye bölünemez.', 
        NEW.Gun, yeni_sinif_seviyesi;
    END IF;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

-- trigger'i tabloya bagla
CREATE TRIGGER trg_sinif_ders_cakisma
BEFORE INSERT OR UPDATE ON DersProgrami
FOR EACH ROW
EXECUTE FUNCTION fn_sinif_ders_cakisma_kontrol();


DO $$ 
BEGIN
    IF NOT EXISTS (SELECT FROM pg_catalog.pg_roles WHERE rolname = 'ogrenci_rolu') THEN CREATE ROLE ogrenci_rolu; END IF;
    IF NOT EXISTS (SELECT FROM pg_catalog.pg_roles WHERE rolname = 'rehber_rolu') THEN CREATE ROLE rehber_rolu; END IF;
END $$;


GRANT SELECT ON Denemeler, DenemeSonuclari TO rehber_rolu;
GRANT SELECT ON Denemeler, DenemeSonuclari TO ogrenci_rolu;
GRANT SELECT ON Talepler TO ogrenci_rolu;

ALTER TABLE DenemeSonuclari ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS ogrenci_sadece_kendini_gorur ON DenemeSonuclari;
CREATE POLICY ogrenci_sadece_kendini_gorur ON DenemeSonuclari FOR SELECT TO ogrenci_rolu
USING (OgrenciID = current_setting('app.current_user_id', true)::INT);

ALTER TABLE Talepler ENABLE ROW LEVEL SECURITY;
DROP POLICY IF EXISTS talep_sahibi_gorebilir ON Talepler;
CREATE POLICY talep_sahibi_gorebilir ON Talepler FOR SELECT TO ogrenci_rolu
USING (TalepEdenID = current_setting('app.current_user_id', true)::INT);

CREATE POLICY rehber_her_seyi_gorur ON DenemeSonuclari FOR SELECT TO rehber_rolu USING (true);


-- Roller ID sirasi onemli -SERIAL
INSERT INTO Roller (RolAdi) VALUES 
('Ogrenci'), ('Ogretmen'), ('Mudur'), ('Idare'), ('Rehberlik');

-- Kullanıcılar (Yonetim Kadrosu ve ogretmenler)
INSERT INTO Kullanicilar (Ad, Soyad, TCNo, Sifre, RolID) VALUES
('Hasan', 'Yılmaz',  '10000000000', '1234', 3), -- ID:1 Müdür
('Fatma', 'Demir',   '20000000000', '1234', 4), -- ID:2 Memur
('Mustafa', 'Çelik', '30000000000', '1234', 2), -- ID:3 Mat Öğrt
('Sevgi', 'Öztürk',  '40000000000', '1234', 2), -- ID:4 Fizik Öğrt
('Selin', 'Kaya',   '50000000000', '1234', 5); -- ID:5 Rehberlik

-- Personel Detayları
INSERT INTO PersonelDetay (PersonelID, BransID, Maas, HaftalikDersKotasi) VALUES
(1, 0, 85000, 0),  -- Mudur
(2, 0, 40000, 0),  -- Memur
(3, 1, 55000, 20), -- Mat
(4, 2, 55000, 20), -- Fizik
(5, 0, 45000, 0);  -- Rehberlik

-- Kullanıcılar (Ogrenciler)
INSERT INTO Kullanicilar (Ad, Soyad, TCNo, Sifre, RolID) VALUES
('Nisa', 'Yıldız', '11111111111', '1234', 1), -- ID:6
('Ceren', 'Celik',     '22222222222', '1234', 1); -- ID:7

-- Öğrenci Detayları
INSERT INTO OgrenciDetay (OgrenciID, SinifSeviyesi, DanismanOgretmenID) VALUES
(6, 12, 5), -- Danisman Selin Hoca
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

-- Ders Programı
INSERT INTO DersProgrami (OgretmenID, DersID, DerslikID, Gun, BaslangicSaati, BitisSaati, OnaylandiMi) VALUES
(3, 1, 1, 'Pazartesi', '09:00', '10:30', TRUE), -- Mustafa Hoca Mat
(4, 2, 2, 'Salı',      '11:00', '12:30', TRUE); -- Sevgi Hoca Fizik

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

SELECT * FROM Kullanicilar;
SELECT * FROM OgrenciDetay;
SELECT * FROM DersProgrami;

UPDATE DersProgrami 
SET OnaylandiMi = 'Onaylandi' 
WHERE OnaylandiMi::text = 'true';


SELECT 
    ProgramID,
    OnaylandiMi,
    pg_typeof(OnaylandiMi) as veri_tipi
FROM DersProgrami;

SELECT 
    OnaylandiMi,
    length(OnaylandiMi) as uzunluk,
    ascii(OnaylandiMi) as ascii_degeri
FROM DersProgrami;





-- Eğer "O" ile başlıyorsa tamamını "Onaylandi" yap
UPDATE DersProgrami 
SET OnaylandiMi = 'Onaylandi'
WHERE OnaylandiMi LIKE 'O%' OR OnaylandiMi LIKE 'o%';

-- NULL veya boş olanları "Beklemede" yap
UPDATE DersProgrami 
SET OnaylandiMi = 'Beklemede'
WHERE OnaylandiMi IS NULL OR OnaylandiMi = '' OR LENGTH(TRIM(OnaylandiMi)) = 0;