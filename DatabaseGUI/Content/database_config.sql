CREATE TABLE address (
 postal_code CHAR(10) NOT NULL,
 country CHAR(50) NOT NULL,
 city CHAR(25),
 street CHAR(50),
 house_number INT,
 PRIMARY KEY (postal_code,country)
);


CREATE TABLE degree (
 course CHAR(255) NOT NULL,
 school CHAR(100),
 degree_level CHAR(50),
 PRIMARY KEY (course)
);


CREATE TABLE headquarters (
 building_name CHAR(25) NOT NULL,
 rooms INT,
 rent REAL,
 postal_code CHAR(10) NOT NULL,
 country CHAR(50) NOT NULL,
 PRIMARY KEY (building_name),
 FOREIGN KEY (postal_code,country) REFERENCES address (postal_code,country)
);


CREATE TABLE employee (
 bsn INT NOT NULL,
 name CHAR(50),
 surname CHAR(50),
 building_name CHAR(25) NOT NULL,
 PRIMARY KEY (bsn),
 FOREIGN KEY (building_name) REFERENCES headquarters (building_name)
);


CREATE TABLE position (
 position_name CHAR(50) NOT NULL,
 description CHAR(255),
 hourly_fee REAL,
 PRIMARY KEY (position_name)
);


CREATE TABLE project (
 project_id INT NOT NULL,
 budget REAL,
 total_hours INT,
 building_name CHAR(25) NOT NULL,
 PRIMARY KEY (project_id),
 FOREIGN KEY (building_name) REFERENCES headquarters (building_name)
);


CREATE TABLE employee_address (
 postal_code CHAR(10) NOT NULL,
 country CHAR(50) NOT NULL,
 bsn INT NOT NULL,
 is_residence BIT(1) NOT NULL,
 PRIMARY KEY (postal_code,country,bsn),
 FOREIGN KEY (postal_code,country) REFERENCES address (postal_code,country) ON DELETE CASCADE ON UPDATE CASCADE,
 FOREIGN KEY (bsn) REFERENCES employee (bsn) ON DELETE CASCADE ON UPDATE CASCADE
);


CREATE TABLE employee_degree (
 course CHAR(255) NOT NULL,
 bsn INT NOT NULL,
 PRIMARY KEY (course,bsn),
 FOREIGN KEY (course) REFERENCES degree (course) ON DELETE CASCADE ON UPDATE CASCADE,
 FOREIGN KEY (bsn) REFERENCES employee (bsn) ON DELETE CASCADE ON UPDATE CASCADE
);


CREATE TABLE employee_position (
 position_name CHAR(50) NOT NULL,
 bsn INT NOT NULL,
 PRIMARY KEY (position_name,bsn),
 FOREIGN KEY (position_name) REFERENCES position (position_name) ON DELETE CASCADE ON UPDATE CASCADE,
 FOREIGN KEY (bsn) REFERENCES employee (bsn) ON DELETE CASCADE ON UPDATE CASCADE
);


CREATE TABLE employee_project (
 position_name CHAR(50) NOT NULL,
 bsn INT NOT NULL,
 project_id INT NOT NULL,
 working_hours INT,
 PRIMARY KEY (position_name,bsn,project_id),
 FOREIGN KEY (position_name,bsn) REFERENCES employee_position (position_name,bsn) ON DELETE CASCADE ON UPDATE CASCADE,
 FOREIGN KEY (project_id) REFERENCES project (project_id) ON DELETE CASCADE ON UPDATE CASCADE
);
