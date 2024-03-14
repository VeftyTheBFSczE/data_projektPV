CREATE TABLE Customer (
    ID INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    RegistrationDate DATETIME NOT NULL
);

CREATE TABLE Product (
    ID INT PRIMARY KEY IDENTITY,
    Name VARCHAR(255) NOT NULL,
    Price FLOAT NOT NULL,
    InStock BIT NOT NULL
);

create table Orders (
    ID INT PRIMARY KEY IDENTITY,
    OrderDate DATETIME NOT NULL,
    CustomerID INT,
    FOREIGN KEY (CustomerID) REFERENCES Customer(ID)
);

CREATE TABLE OrderItem (
    ID INT PRIMARY KEY IDENTITY,
    OrdersID INT,
    ProductID INT,
    Quantity INT NOT NULL,
	Price int not null,
    FOREIGN KEY (OrdersID) REFERENCES Orders(ID),
    FOREIGN KEY (ProductID) REFERENCES Product(ID)
);

CREATE TABLE Supplier (
    ID INT PRIMARY KEY IDENTITY,
    Name VARCHAR(255) NOT NULL,
    Contact NVARCHAR(255) NOT NULL
);

CREATE TABLE ProductsSuppliers(
    ProductID INT,
    SupplierID INT,
    PRIMARY KEY (ProductID, SupplierID),
    FOREIGN KEY (ProductID) REFERENCES Product(ID),
    FOREIGN KEY (SupplierID) REFERENCES Supplier(ID)
);