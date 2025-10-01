-- Создание базы данных
IF DB_ID('RamenInstrumentPlant') IS NULL
BEGIN
    CREATE DATABASE RamenInstrumentPlant;
END
GO

USE RamenInstrumentPlant;
GO

-- Таблица отделов
CREATE TABLE Departments (
    DepartmentID INT IDENTITY(1,1) PRIMARY KEY,
    DepartmentName NVARCHAR(255) NOT NULL,
    Location NVARCHAR(255)
);

-- Таблица сотрудников
CREATE TABLE Employees (
    EmployeeID INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(500) NOT NULL, -- Фамилия Имя Отчество в одном поле
    Position NVARCHAR(255),
    HireDate DATE,
    DepartmentID INT NOT NULL,
    RoleID INT NOT NULL DEFAULT 1 -- 1 = сотрудник, 2 = менеджер, 3 = администратор и т.д.
);

-- Таблица оборудования
CREATE TABLE Equipment (
    EquipmentID INT IDENTITY(1,1) PRIMARY KEY,
    EquipmentName NVARCHAR(255) NOT NULL,
    SerialNumber NVARCHAR(255),
    InstallationDate DATE,
    DepartmentID INT NOT NULL
);

-- Таблица продуктов
CREATE TABLE Products (
    ProductID INT IDENTITY(1,1) PRIMARY KEY,
    ProductName NVARCHAR(255) NOT NULL,
    ProductCode NVARCHAR(50) UNIQUE NOT NULL,
    ProductionStartDate DATE,
    Description NVARCHAR(MAX)
);

-- Таблица производственных заказов
CREATE TABLE ProductionOrders (
    OrderID INT IDENTITY(1,1) PRIMARY KEY,
    ProductID INT NOT NULL,
    OrderDate DATE NOT NULL,
    PlannedCompletionDate DATE,
    ActualCompletionDate DATE,
    Quantity INT NOT NULL CHECK (Quantity > 0),
    Status NVARCHAR(50) NOT NULL DEFAULT N'Новый' -- Текстовый статус
);

-- Таблица производственных операций
CREATE TABLE ProductionOperations (
    OperationID INT IDENTITY(1,1) PRIMARY KEY,
    OrderID INT NOT NULL,
    OperationName NVARCHAR(255) NOT NULL,
    EmployeeID INT NOT NULL,
    DepartmentID INT NOT NULL,
    PlannedStartDate DATE,
    ActualStartDate DATE,
    ActualEndDate DATE,
    Status NVARCHAR(50) NOT NULL DEFAULT N'Запланировано' -- Текстовый статус
);

-- Таблица авторизации
CREATE TABLE Auth (
    AuthID INT IDENTITY(1,1) PRIMARY KEY,
    Login NVARCHAR(255) UNIQUE NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL, -- хэш пароля (никогда не открытый текст!)
    EmployeeID INT NOT NULL UNIQUE,
    CreatedAt DATETIME2 DEFAULT GETDATE()
);

-- Внешние ключи

ALTER TABLE Employees
ADD CONSTRAINT FK_Employees_Departments 
FOREIGN KEY (DepartmentID) REFERENCES Departments(DepartmentID);

ALTER TABLE Equipment
ADD CONSTRAINT FK_Equipment_Departments 
FOREIGN KEY (DepartmentID) REFERENCES Departments(DepartmentID);

ALTER TABLE ProductionOrders
ADD CONSTRAINT FK_ProductionOrders_Products 
FOREIGN KEY (ProductID) REFERENCES Products(ProductID);

ALTER TABLE ProductionOperations
ADD CONSTRAINT FK_ProductionOperations_ProductionOrders 
FOREIGN KEY (OrderID) REFERENCES ProductionOrders(OrderID);

ALTER TABLE ProductionOperations
ADD CONSTRAINT FK_ProductionOperations_Employees 
FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID);

ALTER TABLE ProductionOperations
ADD CONSTRAINT FK_ProductionOperations_Departments 
FOREIGN KEY (DepartmentID) REFERENCES Departments(DepartmentID);

ALTER TABLE Auth
ADD CONSTRAINT FK_Auth_Employees 
FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID);

-- Индексы

CREATE INDEX IX_Employees_DepartmentID ON Employees(DepartmentID);
CREATE INDEX IX_Equipment_DepartmentID ON Equipment(DepartmentID);
CREATE INDEX IX_ProductionOrders_ProductID ON ProductionOrders(ProductID);
CREATE INDEX IX_ProductionOperations_OrderID ON ProductionOperations(OrderID);
CREATE INDEX IX_ProductionOperations_EmployeeID ON ProductionOperations(EmployeeID);
CREATE INDEX IX_ProductionOperations_DepartmentID ON ProductionOperations(DepartmentID);
CREATE INDEX IX_Auth_EmployeeID ON Auth(EmployeeID);

-- Готово!
PRINT 'База данных "RamenInstrumentPlant" успешно создана с текстовыми статусами!';