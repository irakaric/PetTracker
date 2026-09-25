--Role
IF NOT EXISTS (SELECT TOP 1 * FROM Role WHERE id = 1)
BEGIN
	INSERT INTO Role (Naziv, Status) VALUES 
	('Korisnik', 1),
	('Administrator sustava', 1)
END

--SubjektiOsobe
IF NOT EXISTS (SELECT TOP 1 * FROM SubjektiOsobe WHERE id = 1)
BEGIN
	INSERT INTO SubjektiOsobe (Naziv, Status) VALUES 
	('Fizička osoba', 1),
	('Pravna osoba', 1)
END

--SubjektiVrste
IF NOT EXISTS (SELECT TOP 1 * FROM SubjektiVrste WHERE id = 1)
BEGIN
	INSERT INTO SubjektiVrste (Naziv, Status) VALUES 
	('Građanin', 1),
	('Obrt', 1),
	('Trgovačko društvo', 1)
	END

--PravniOblici
IF NOT EXISTS (SELECT TOP 1 * FROM PravniOblici WHERE id = 1)
BEGIN
	INSERT INTO PravniOblici (Naziv, Status) VALUES 
	('Društvo s ograničenom odgovornošću-kratica d.o.o – prema Zakonu o trgovačkim društvima', 1),
	('Zakon o obrtu', 1)
	END

--Djelatnosti
IF NOT EXISTS (SELECT TOP 1 * FROM Djelatnosti WHERE id = 1)
BEGIN
	INSERT INTO Djelatnosti (Oznaka, Naziv, Status) VALUES 
	('01.4', 'Uzgoj životinja', 1)
	END

--VrstaAdresa
IF NOT EXISTS (SELECT TOP 1 * FROM VrstaAdresa WHERE id = 1)
BEGIN
	INSERT INTO VrstaAdresa (Naziv, Status) VALUES 
	('Prebivalište', 1),
	('Sjedište', 1),
	('Uzgoj', 1)
END

--Zupanije
IF NOT EXISTS (SELECT TOP 1 * FROM Zupanije WHERE Id = 1)
BEGIN
    INSERT INTO Zupanije (Naziv, Status)
    VALUES
        (N'Bjelovarsko-bilogorska', 1),
        (N'Brodsko-posavska', 1),
        (N'Dubrovačko-neretvanska', 1),
        (N'Grad Zagreb', 1),
        (N'Istarska', 1),
        (N'Karlovačka', 1),
        (N'Koprivničko-križevačka', 1),
        (N'Krapinsko-zagorska', 1),
        (N'Ličko-senjska', 1),
        (N'Međimurska', 1),
        (N'Osječko-baranjska', 1),
        (N'Požeško-slavonska', 1),
        (N'Primorsko-goranska', 1),
        (N'Sisačko-moslavačka', 1),
        (N'Splitsko-dalmatinska', 1),
        (N'Šibensko-kninska', 1),
        (N'Varaždinska', 1),
        (N'Virovitičko-podravska', 1),
        (N'Vukovarsko-srijemska', 1),
        (N'Zadarska', 1),
        (N'Zagrebačka', 1);
END

--ZivotinjaVrste
IF NOT EXISTS (SELECT TOP 1 * FROM ZivotinjeVrste WHERE id = 1)
BEGIN
	INSERT INTO ZivotinjeVrste (Naziv, Status) VALUES 
	('Pas', 1),
	('Mačka', 1),
	('Ptica', 1),
	('Svinja', 1)
END

--Pasmina
IF NOT EXISTS (SELECT TOP 1 * FROM Pasmina WHERE id = 1)
BEGIN
	INSERT INTO Pasmina (Naziv, Status) VALUES 
	('Njemački ovčar', 1),
	('Pekinezer', 1),
	('Pudlica', 1),
	('Bengalska mačka', 1),
	('Britanska kratkodlaka mačka', 1),
	('Cymric mačka', 1),
	('Tigrica', 1),
	('Kanarinac', 1),
	('Nimfa', 1),
	('Crna slavonska svinja', 1),
	('Mangalica', 1),
	('Turopoljska svinja', 1)
END

--ZivotinjeVrstePasmina
IF NOT EXISTS (SELECT TOP 1 * FROM ZivotinjeVrstePasmina WHERE id = 1)
BEGIN
	INSERT INTO ZivotinjeVrstePasmina (VrstaZivotinjeId,PasminaId) VALUES 
	(1, 1),
	(1, 2),
	(1, 3),
	(2, 4),
	(2, 5),
	(2, 6),
	(3, 7),
	(3, 8),
	(3, 9),
	(4, 10),
	(4, 11),
	(4, 12)
END

--Spol
IF NOT EXISTS (SELECT TOP 1 * FROM Spol WHERE id = 1)
BEGIN
	INSERT INTO Spol (Naziv, Status) VALUES 
	('Muško', 1),
	('Žensko', 1)
END

--ZivotinjeStatusi
IF NOT EXISTS (SELECT TOP 1 * FROM ZivotinjeStatusi WHERE id = 1)
BEGIN
	INSERT INTO ZivotinjeStatusi (Naziv, Status) VALUES 
	('Aktivan', 1),
	('Uginuo', 1),
	('Nestao', 1)
END

--IdentifikacijskeOznakeVrste
IF NOT EXISTS (SELECT TOP 1 * FROM IdentifikacijskeOznakeVrste WHERE id = 1)
BEGIN
	INSERT INTO IdentifikacijskeOznakeVrste (Naziv, Status) VALUES 
	('Mikročip', 1),
	('Prsten', 1),
	('Tetovaža', 1),
	('Putovnica', 1)

END

--TipPrstena
IF NOT EXISTS (SELECT TOP 1 * FROM TipPrstena WHERE id = 1)
BEGIN
	INSERT INTO TipPrstena (Naziv, Status) VALUES 
	('Zatvoreni', 1),
	('Otvoreni', 1),
	('Elektronički', 1)

END

--ZdravstveniZapisiVrste
IF NOT EXISTS (SELECT TOP 1 * FROM ZdravstveniZapisiVrste WHERE id = 1)
BEGIN
	INSERT INTO ZdravstveniZapisiVrste (Naziv, Status) VALUES 
	('Veterinarski pregled', 1),
	('Cijepljenje', 1),
	('Vađenje krvi', 1),
	('Laboratorijska pretraga', 1),
	('Terapija', 1),
	('Operativni zahvat', 1),
	('Kontrolni pregled', 1),
	('Preventivni tretman', 1),
	('Ostalo', 1)

END
