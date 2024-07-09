CREATE TABLE Products (
    ProductID INT PRIMARY KEY IDENTITY,
    NameProd NVARCHAR(50) NOT NULL,
    DescriptionProd NVARCHAR(100),
    Price DECIMAL(18, 2) NOT NULL CHECK (Price > 0),
    Category NVARCHAR (100) NULL
);
CREATE TABLE Cart (
    CartID INT PRIMARY KEY IDENTITY,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE()
);
CREATE TABLE CartItems (
    CartItemID INT PRIMARY KEY IDENTITY,
    CartID INT,
    ProductID INT,
    Quantity INT NOT NULL CHECK (Quantity > 0),
    FOREIGN KEY (CartID) REFERENCES Cart(CartID),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);