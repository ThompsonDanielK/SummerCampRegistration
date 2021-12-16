USE master
GO

-- Drop database if it exists
IF DB_ID('final_capstone') IS NOT NULL
BEGIN
	ALTER DATABASE final_capstone SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE final_capstone;
END

CREATE DATABASE final_capstone
GO

USE final_capstone
GO

-- Create Tables
CREATE TABLE users (
	user_id int IDENTITY(100001,1) NOT NULL,
	username varchar(50) NOT NULL,
	password_hash varchar(200) NOT NULL,
	salt varchar(200) NOT NULL,
	user_role varchar(50) NOT NULL
	CONSTRAINT PK_user PRIMARY KEY (user_id)
)

-- Populate default data for testing: user and admin with password of 'password'
-- These values should not be kept when going to Production
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('user','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('admin','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin');
GO

CREATE TABLE family (
	family_id int IDENTITY(100001, 1) PRIMARY KEY,
	parent_guardian_name nvarchar(100) NOT NULL,
	address nvarchar(200),
	city nvarchar(100),
	state nvarchar(2),
	zip nvarchar(10),
	phone nvarchar(12),
	email_address nvarchar(50)
)

CREATE TABLE campers (
	camper_code int IDENTITY(200001, 1) PRIMARY KEY,
	family_id int NOT NULL,
	first_name nvarchar(50),
	last_name nvarchar(50),
	dob date NOT NULL,
	medications nvarchar(200) DEFAULT '',
	allergies nvarchar(200) DEFAULT '', 
	special_needs nvarchar(1000) DEFAULT '',
	registrar nvarchar(100),
	payment_status bit,
	active_status bit DEFAULT 'true',
	date_added date
)

CREATE TABLE camper_updates (
	request_id int IDENTITY(400001, 1) NOT NULL PRIMARY KEY,
	field_to_be_changed nvarchar(50),
	camper_code int NOT NULL,
	action nvarchar(20) NOT NULL,
	new_data nvarchar(1000) NOT NULL,
	old_data nvarchar(1000),
	requestor nvarchar(100) NOT NULL,
	status nvarchar(20) NOT NULL,
	request_date date NOT NULL,
	finalize_date date
);

CREATE TABLE family_updates (
	request_id int IDENTITY(300001, 1) NOT NULL PRIMARY KEY,
	field_to_be_changed nvarchar(50),
	family_id int NOT NULL,
	action nvarchar(20) NOT NULL,
	new_data nvarchar(1000) NOT NULL,
	old_data nvarchar(1000),
	requestor nvarchar(100) NOT NULL,
	status nvarchar(20) NOT NULL,
	request_date date NOT NULL,
	finalize_date date
);

CREATE TABLE ad_hoc_notes(
	note_id int IDENTITY(500001, 1) NOT NULL PRIMARY KEY,
	camper_code int NOT NULL,
	parameter nvarchar(1000) NOT NULL,
	value nvarchar(1000) NOT NULL,
);

GO
SET IDENTITY_INSERT ad_hoc_notes ON;
INSERT INTO ad_hoc_notes (note_id, camper_code, parameter, value)
	VALUES(500001, 200006, 'Members of Team Oscar', 'Caleb, Dan, & Eric');
SET IDENTITY_INSERT ad_hoc_notes OFF;

SET IDENTITY_INSERT camper_updates ON;
INSERT INTO camper_updates (request_id, field_to_be_changed, camper_code, action, new_data, old_data, requestor, status, request_date)
	VALUES(400001, 'last_name', '200003', 'Update', 'Bowersmith', 'Bowers', 'user', 'Pending', '2021-10-09');
INSERT INTO camper_updates (request_id, field_to_be_changed, camper_code, action, new_data, old_data, requestor, status, request_date, finalize_date)
	VALUES(400002, 'first_name', '200005', 'Update', 'Erica', 'Eric', 'user', 'Approved', '2021-11-09', '2021-11-10');
INSERT INTO camper_updates (request_id, field_to_be_changed, camper_code, action, new_data, old_data, requestor, status, request_date, finalize_date)
	VALUES(400003, 'payment_status', '200003', 'Update', 'true', 'false', 'user', 'Rejected', '2021-11-23', '2021-11-24');
INSERT INTO camper_updates (request_id, field_to_be_changed, camper_code, action, new_data, old_data, requestor, status, request_date, finalize_date)
	VALUES(400004, 'medications', '200004', 'Update', 'Aspirin', 'Asparan', 'admin', 'Approved', '2021-12-16', '2021-12-16');
INSERT INTO camper_updates (request_id, field_to_be_changed, camper_code, action, new_data, old_data, requestor, status, request_date, finalize_date)
	VALUES(400005, 'active_status', '200005', 'Update', 'true', 'false', 'user', 'Rejected', '2021-12-16', '2021-12-17');
INSERT INTO camper_updates (request_id, field_to_be_changed, camper_code, action, new_data, old_data, requestor, status, request_date)
	VALUES(400006, 'active_status', '200006', 'Update', 'false', 'true', 'user', 'Pending', '2021-12-09');
SET IDENTITY_INSERT camper_updates OFF;

SET IDENTITY_INSERT family_updates ON;
INSERT INTO family_updates (request_id, field_to_be_changed, family_id, action, new_data, old_data, requestor, status, request_date)
	VALUES(300001, 'address', '100002', 'Update', '200 Secondary Road', '200 Second Ave.', 'user', 'Pending', '2021-10-09');
INSERT INTO family_updates (request_id, field_to_be_changed, family_id, action, new_data, old_data, requestor, status, request_date)
	VALUES(300002, 'parent_guardian_name', '100004', 'Update', 'Cohort 15 Team Mega-Awesome', 'Cohort 15 Team Oscar', 'user', 'Rejected', '2021-11-24');
INSERT INTO family_updates (request_id, field_to_be_changed, family_id, action, new_data, old_data, requestor, status, request_date)
	VALUES(300003, 'parent_guardian_name', '100002', 'Update', 'Mike Bowers', 'Mike Flowers', 'admin', 'Approved', '2021-12-05');
INSERT INTO family_updates (request_id, field_to_be_changed, family_id, action, new_data, old_data, requestor, status, request_date)
	VALUES(300004, 'email_address', '100003', 'Update', 'mollygolly@molly.com', 'molly@molly.com', 'user', 'Pending', '2021-11-27');
INSERT INTO family_updates (request_id, field_to_be_changed, family_id, action, new_data, old_data, requestor, status, request_date)
	VALUES(300005, 'city', '100001', 'Update', 'Flavortown', 'Columbus', 'user', 'Rejected', '2021-12-05');
INSERT INTO family_updates (request_id, field_to_be_changed, family_id, action, new_data, old_data, requestor, status, request_date)
	VALUES(300006, 'zip', '100004', 'Update', '43430', '43333', 'user', 'Pending', '2021-12-02');
SET IDENTITY_INSERT family_updates OFF;

SET IDENTITY_INSERT family ON;
INSERT INTO family (family_id, parent_guardian_name, address, city, state, zip, phone, email_address)
	VALUES (100001, 'Mary Andrews', '100 First St.', 'Columbus', 'OH', '43111', '614-222-3333', 'mary@mary.com');
INSERT INTO family (family_id, parent_guardian_name, address, city, state, zip, phone, email_address)
	VALUES (100002, 'Mike Bowers', '200 Second Ave.', 'Dublin', 'OH', '43222', '614-444-5555', 'mike@mike.com');
INSERT INTO family (family_id, parent_guardian_name, address, city, state, zip, phone, email_address)
	VALUES (100003, 'Molly Carter', '300 Third St.', 'Hilliard', 'OH', '43333', '614-666-7777', 'molly@molly.com');
INSERT INTO family (family_id, parent_guardian_name, address, city, state, zip, phone, email_address)
	VALUES (100004, 'Cohort 15 Team Oscar', '100 Wow Way', 'Columbus', 'OH', '43333', '614-888-9999', 'ioicaleb@hotmail.com');
SET IDENTITY_INSERT family OFF;

GO

SET IDENTITY_INSERT campers ON;
INSERT INTO campers (camper_code, family_id, first_name, last_name, dob, registrar, payment_status, active_status, date_added)
	VALUES(200001, 100001, 'Alex', 'Andrews', '2010-01-01', 'user', 'false', 'true', '2021-12-10');
INSERT INTO campers (camper_code, family_id, first_name, last_name, dob, registrar, payment_status, active_status, date_added)
	VALUES(200002, 100001, 'Brandy', '', '2009-02-03', 'user', 'true', 'true', '2021-12-10');
INSERT INTO campers (camper_code, family_id, first_name, last_name, dob, registrar, payment_status, active_status, date_added)
	VALUES(200003, 100002, 'Cathy', 'Bowers', '2011-03-03', 'user', 'false', 'true', '2021-12-10');
INSERT INTO campers (camper_code, family_id, first_name, last_name, dob, registrar, payment_status, active_status, medications, allergies, special_needs, date_added)
	VALUES(200004, 100003, 'David', 'Carter', '2010-04-04', 'user', 'false', 'true', 'Asprin', 'Bees, Nuts, Grass', 'Likes Bedtime Stories, Nightlight', '2021-12-10');
INSERT INTO campers (camper_code, family_id, first_name, last_name, dob, registrar, payment_status, active_status, date_added)
	VALUES(200005, 100003, 'Erica', '', '2009-05-05', 'user', 'false', 'false', '2021-12-10');
INSERT INTO campers (camper_code, family_id, first_name, last_name, dob, medications, allergies, special_needs, registrar, payment_status, active_status, date_added)
	VALUES(200006, 100004, 'Team Oscar', 'Cohort 15', '2020-12-03', 'Coffee', 'Bright Light, Early Mornings, Bad Reviews', 'Hugs, Appreciation', 'admin', 'true', 'true', '2021-12-10');
SET IDENTITY_INSERT campers OFF;



ALTER TABLE campers ADD FOREIGN KEY (family_id) REFERENCES family(family_id);
ALTER TABLE ad_hoc_notes ADD FOREIGN KEY (camper_code) REFERENCES campers(camper_code);

ALTER TABLE camper_updates ADD FOREIGN KEY (camper_code) REFERENCES campers(camper_code);
ALTER TABLE camper_updates ADD CONSTRAINT Chk_camper_updates_action CHECK (action IN ('ADD', 'Update', 'Delete'));
ALTER TABLE camper_updates ADD CONSTRAINT Chk_camper_updates_status CHECK (status IN ('Pending', 'Approved', 'Updated', 'Rejected'));

ALTER TABLE family_updates ADD FOREIGN KEY (family_id) REFERENCES family(family_id);
ALTER TABLE family_updates ADD CONSTRAINT Chk_family_updates_action CHECK (action IN ('ADD', 'Update', 'Delete'));
ALTER TABLE family_updates ADD CONSTRAINT Chk_family_updates_status CHECK (status IN ('Pending', 'Approved', 'Updated', 'Rejected'));
