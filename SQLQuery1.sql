CREATE TABLE Books (
    Id UNIQUEIDENTIFIER PRIMARY KEY,  -- BaseEntity'den gelen Id (GUID) - birincil anahtar
    Author NVARCHAR(255) NOT NULL,  -- Yazar
    Description NVARCHAR(MAX) NULL,  -- Tanım (Varsayılan olarak boş olabilir)
    Price DECIMAL(18, 2) NOT NULL,  -- Fiyat (18 basamağa kadar, 2 ondalıklı)
    CoverPhoto UNIQUEIDENTIFIER NULL,  -- Kapak fotoğrafı (Varsayılan olarak NULL olabilir)
    UserId INT NOT NULL,  -- Kullanıcı Id (Users tablosundaki Id ile ilişkilendirilecek)
    ShowOnFirstScreen BIT NULL,  -- Ana ekranda gösterilsin mi? (Varsayılan olarak NULL olabilir)
    Language INT NOT NULL,  -- Dil (enum tipi, bu tip bir tamsayı olabilir)
    CreatedBy INT NULL,  -- Kitap oluşturulurken kim tarafından yaratıldığını belirten kullanıcı
    UpdatedBy INT NULL,  -- Kitap güncellenirken kim tarafından güncellendiğini belirten kullanıcı
    DeletedBy INT NULL,  -- Kitap silindiğinde kim tarafından silindiğini belirten kullanıcı
    CreatedDate DATETIME2(7) NULL,  -- Kitap oluşturulma tarihi
    UpdatedDate DATETIME2(7) NULL,  -- Kitap güncellenme tarihi
    DeletedDate DATETIME2(7) NULL,  -- Kitap silinme tarihi
    IsDeleted BIT NOT NULL DEFAULT 0,  -- Kitap silindi mi?
    CONSTRAINT FK_User FOREIGN KEY (UserId) REFERENCES Users(Id)  -- UserId'yi Users tablosuna bağlamak için foreign key
);

CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Surname NVARCHAR(100) NOT NULL,
    Email NVARCHAR(255) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    Username NVARCHAR(100) NOT NULL UNIQUE,
    FatherName NVARCHAR(100) NULL,
    Address NVARCHAR(255) NULL,
    MobilePhone NVARCHAR(20) NULL,
    CardNumber NVARCHAR(50) NULL,
    TableNumber NVARCHAR(50) NULL,
    Birthdate DATETIME NULL,
    DateOfEmployment DATETIME NULL,
    DateOfDismissal DATETIME NULL,
    Note NVARCHAR(MAX) NULL,
    Gender INT NOT NULL,  
    UserType INT NOT NULL,
    UpdatedBy   INT            NULL,
    CreatedBy   INT            NULL,
    DeletedBy   INT            NULL,
    CreatedDate DATETIME2 (7)  NULL,
    DeletedDate DATETIME2 (7)  NULL,
    UpdatedDate DATETIME2 (7)  NULL,
    IsDeleted   BIT            NOT NULL DEFAULT 0
);
