-- Inserimento dei record nella tabella Products
INSERT INTO Products (NameProd, DescriptionProd, Price, Category)
VALUES 
('IPA', 'A hoppy beer style within the broader category of pale ale.', 3.99, 'Ale'),
('Lager', 'A type of beer conditioned at low temperature.', 2.99, 'Lager'),
('Stout', 'A dark, top-fermented beer with a number of variations.', 4.49, 'Stout'),
('Pilsner', 'A type of pale lager that takes its name from the Czech city of Pilsen.', 3.49, 'Lager'),
('Wheat Beer', 'A beer, usually top-fermented, which is brewed with a large proportion of wheat.', 3.79, 'Wheat');

-- Inserimento dei record nella tabella Cart
INSERT INTO Cart (CreatedDate)
VALUES 
('2024-07-01 10:00:00'),
('2024-07-02 11:30:00');

-- Inserimento dei record nella tabella CartItems
INSERT INTO CartItems (CartID, ProductID, Quantity)
VALUES 
(1, 1, 2),  -- 2 IPA nel carrello 1
(1, 3, 1),  -- 1 Stout nel carrello 1
(2, 2, 4),  -- 4 Lager nel carrello 2
(2, 5, 3);  -- 3 Wheat Beer nel carrello 2
