--CREATE DATABASE gym;

DROP TABLE IF EXISTS Customer CASCADE;

CREATE TABLE Customer (
    id BIGSERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    phone_number VARCHAR(20),
    registeredAt TIMESTAMP WITHOUT TIME ZONE DEFAULT now()
);

INSERT INTO Customer (name, phone_number) VALUES ('Bob', '+314 242 4123'),
                                                   ('Michael', '+314 512 1324'),
                                                   ('Alice', '+12 4123 5123');

DROP TABLE IF EXISTS Payment CASCADE;

CREATE TABLE Payment (
    id BIGSERIAL PRIMARY KEY,
    amount DECIMAL(10, 2) NOT NULL ,
    type VARCHAR(100) NOT NULL ,
    bank_name VARCHAR(100) DEFAULT 'not the case',
    id_customer BIGINT,
    CONSTRAINT fk_customer FOREIGN KEY (id_customer) REFERENCES Customer (id) ON DELETE  CASCADE 
);

INSERT INTO Payment (amount, type, id_customer) VALUES ('195', 'cash', 1),
                                                       ('45', 'cash', 2),
                                                       ('120', 'cash', 3);

INSERT INTO Payment (amount, type, bank_name, id_customer) VALUES ('450', 'bank transfer', 'ING', 1),
                                                                  ('750', 'card', 'BRD', 3),
                                                                  ('2150', 'card', 'BCR', 2);

DROP TABLE IF EXISTS Membership CASCADE;

CREATE TABLE Membership (
    id BIGSERIAL PRIMARY KEY,
    name VARCHAR(20) NOT NULL,
    length INT NOT NULL,
    price INT NOT NULL
);

INSERT INTO Membership (name, length, price) VALUES ('Monthly Pro', 31, 195),
                                                    ('Monthly Basic', 31, 120),
                                                    ('Trial', 7, 45),
                                                    ('Annual', 365, 2150),
                                                    ('Trimestrial Pro', 100, 750),
                                                    ('Trimestrial Basic', 100, 450);

DROP TABLE IF EXISTS Customer_Membership CASCADE;

CREATE TABLE Customer_Membership (
    id_customer BIGINT,
    id_membership BIGINT,
    CONSTRAINT pk PRIMARY KEY (id_customer, id_membership),
    CONSTRAINT fk_customer FOREIGN KEY (id_customer) REFERENCES Customer (id) ON DELETE CASCADE,
    CONSTRAINT fk_membership FOREIGN KEY (id_membership) REFERENCES Membership (id) ON DELETE CASCADE 
);

INSERT INTO Customer_Membership (id_customer, id_membership)  VALUES (1, 1), (1, 6),
                                                                     (2, 3), (3, 2),
                                                                     (3, 5), (2, 4);