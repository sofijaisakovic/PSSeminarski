CREATE TABLE Grad (
    idGrad INT IDENTITY(1,1) NOT NULL,
    naziv  VARCHAR(50) NOT NULL,
    CONSTRAINT PK_Grad PRIMARY KEY (idGrad)
);
GO

-- 2. Teretana
CREATE TABLE Teretana (
    idTeretana INT IDENTITY(1,1) NOT NULL,
    naziv      VARCHAR(50)  NOT NULL,
    adresa     VARCHAR(100) NOT NULL,
    telefon    VARCHAR(20)  NULL,
    CONSTRAINT PK_Teretana PRIMARY KEY (idTeretana)
);
GO

-- 3. Vezba
CREATE TABLE Vezba (
    idVezba                      INT IDENTITY(1,1) NOT NULL,
    naziv                        VARCHAR(50)  NOT NULL,
    opis                         VARCHAR(255) NULL,
    misicnaGrupa                 VARCHAR(20)  NOT NULL,
    dozvoljenoDodatnoOpterecenje BIT          NOT NULL,
    CONSTRAINT PK_Vezba PRIMARY KEY (idVezba),
    CONSTRAINT CK_Vezba_misicnaGrupa
        CHECK (misicnaGrupa IN ('GRUDI','LEDJA','STOMAK','RAMENA','NOGE','RUKE','KARDIO'))
);
GO

-- 4. Trening
CREATE TABLE Trening (
    idTrening INT IDENTITY(1,1) NOT NULL,
    naziv     VARCHAR(50)  NOT NULL,
    opis      VARCHAR(255) NULL,
    CONSTRAINT PK_Trening PRIMARY KEY (idTrening)
);
GO

-- 5. Clan
CREATE TABLE Clan (
    idClan        INT IDENTITY(1,1) NOT NULL,
    ime           VARCHAR(50)  NOT NULL,
    prezime       VARCHAR(50)  NOT NULL,
    korisnickoIme VARCHAR(50)  NOT NULL,
    sifra         VARCHAR(50)  NOT NULL,
    email         VARCHAR(100) NULL,
    datumRodjenja DATE         NULL,
    idGrad        INT          NOT NULL,
    CONSTRAINT PK_Clan PRIMARY KEY (idClan),
    CONSTRAINT UQ_Clan_korisnickoIme UNIQUE (korisnickoIme),
    CONSTRAINT CK_Clan_sifra CHECK (LEN(sifra) >= 8),
    CONSTRAINT CK_Clan_datumRodjenja CHECK (datumRodjenja < CAST(GETDATE() AS DATE)),
    CONSTRAINT FK_Clan_Grad FOREIGN KEY (idGrad) REFERENCES Grad(idGrad)
);
GO

-- 6. Prijava
CREATE TABLE Prijava (
    idPrijava       INT IDENTITY(1,1) NOT NULL,
    datumPrijave    DATE         NOT NULL,
    datumTreninga   DATE         NOT NULL,
    napomena        VARCHAR(500) NULL,
    ukupanVolumenKg FLOAT        NOT NULL CONSTRAINT DF_Prijava_volumen DEFAULT 0,
    idTeretana      INT          NOT NULL,
    idClan          INT          NOT NULL,
    CONSTRAINT PK_Prijava PRIMARY KEY (idPrijava),
    CONSTRAINT CK_Prijava_volumen CHECK (ukupanVolumenKg >= 0),
    CONSTRAINT FK_Prijava_Teretana FOREIGN KEY (idTeretana) REFERENCES Teretana(idTeretana),
    CONSTRAINT FK_Prijava_Clan     FOREIGN KEY (idClan)     REFERENCES Clan(idClan)
);
GO

-- 7. StavkaPrijave  (DELETE CASCADE sa Prijavom)
CREATE TABLE StavkaPrijave (
    idPrijava         INT   NOT NULL,
    rb                INT   NOT NULL,
    brojSerija        INT   NOT NULL,
    brojPonavljanja   INT   NOT NULL,
    opterecenjeKg     FLOAT NOT NULL CONSTRAINT DF_Stavka_opterecenje DEFAULT 0,
    ukupnoPonavljanja INT   NOT NULL,
    ukupanVolumenKg   FLOAT NOT NULL,
    idVezba           INT   NOT NULL,
    CONSTRAINT PK_StavkaPrijave PRIMARY KEY (idPrijava, rb),
    CONSTRAINT CK_Stavka_rb              CHECK (rb > 0),
    CONSTRAINT CK_Stavka_brojSerija      CHECK (brojSerija > 0),
    CONSTRAINT CK_Stavka_brojPonavljanja CHECK (brojPonavljanja > 0),
    CONSTRAINT CK_Stavka_opterecenje     CHECK (opterecenjeKg >= 0),
    CONSTRAINT CK_Stavka_ukupnoPonav     CHECK (ukupnoPonavljanja > 0),
    CONSTRAINT CK_Stavka_ukupanVolumen   CHECK (ukupanVolumenKg >= 0),
    CONSTRAINT FK_Stavka_Prijava FOREIGN KEY (idPrijava)
        REFERENCES Prijava(idPrijava) ON DELETE CASCADE,
    CONSTRAINT FK_Stavka_Vezba   FOREIGN KEY (idVezba)
        REFERENCES Vezba(idVezba)
);
GO

-- 8. PonudaTreninga  (povezna tabela)
CREATE TABLE PonudaTreninga (
    idTeretana INT NOT NULL,
    idTrening  INT NOT NULL,
    CONSTRAINT PK_PonudaTreninga PRIMARY KEY (idTeretana, idTrening),
    CONSTRAINT FK_Ponuda_Teretana FOREIGN KEY (idTeretana) REFERENCES Teretana(idTeretana),
    CONSTRAINT FK_Ponuda_Trening  FOREIGN KEY (idTrening)  REFERENCES Trening(idTrening)
);