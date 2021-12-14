DELETE FROM ad_hoc_notes
DELETE FROM family_updates
DELETE FROM camper_updates
DELETE FROM campers
DELETE FROM family
DELETE FROM users

SET IDENTITY_INSERT users ON

-- Username: user, Password: password
INSERT INTO users
	(user_id, username, password_hash, salt, user_role)
VALUES
	(1, 'user', 'Jg45HuwT7PZkfuKTz6IB90CtWY4=', 'LHxP4Xh7bN0=', 'user')

	-- Username: admin, Password: password
INSERT INTO users
	(user_id, username, password_hash, salt, user_role)
VALUES
	(2, 'admin', 'YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=', 'admin')

-- Username: Jimbo, Password: DoggoLover69
INSERT INTO users
	(user_id, username, password_hash, salt, user_role)
VALUES
	(3, 'Jimbo', 'ae3bb8945ae942b224a12cb4cebaf2bdbef4dfee20d0ea4b3250df5648a2f9c8', 'lkasjpfsaondslfn', 'user')

SET IDENTITY_INSERT users OFF


SET IDENTITY_INSERT family ON

INSERT INTO family 
	(family_id, parent_guardian_name, address, city, state, zip, phone, email_address)
VALUES 
	(100001, 'Mary Andrews', '100 First St.', 'Columbus', 'OH', '43111', '614-222-3333', 'mary@mary.com')

INSERT INTO family 
	(family_id, parent_guardian_name, address, city, state, zip, email_address)
VALUES 
	(100002, 'Mike Bowers', '200 Second Ave.', 'Dublin', 'OH', '43222', 'mike@mike.com')

INSERT INTO family 
	(family_id, parent_guardian_name, address, city, state, zip, phone)
VALUES 
	(100003, 'Molly Carter', '300 Third St.', 'Hilliard', 'OH', '43333', '614-666-7777')

INSERT INTO family 
	(family_id, parent_guardian_name, address, city, state, zip, email_address)
VALUES 
	(100004, 'Antony Gonzales', '400 Fourth St.', 'Bexley',  'OH', '43209', 'tony@tony.com')

SET IDENTITY_INSERT family OFF;


SET IDENTITY_INSERT campers ON;

INSERT INTO campers 
	(camper_code, family_id, first_name, last_name, dob, registrar, payment_status, active_status, date_added)
VALUES
	(200001, 100001, 'Alex', 'Andrews', '2010-01-01', 'user', 'false', 'true', '2021-01-20')

INSERT INTO campers 
	(camper_code, family_id, first_name, last_name, dob, registrar, payment_status, active_status, date_added)
VALUES
	(200002, 100001, 'Brandy', 'Andrews', '2009-02-03', 'user', 'false', 'true', '2021-01-21')

INSERT INTO campers 
	(camper_code, family_id, first_name, last_name, dob, registrar, payment_status, active_status, date_added)
VALUES
	(200003, 100002, 'Cathy', 'Bowers', '2011-03-03', 'user', 'false', 'true', '2021-02-14')

INSERT INTO campers 
	(camper_code, family_id, first_name, last_name, dob, registrar, payment_status, active_status, medications, allergies, special_needs, date_added)
VALUES
	(200004, 100003, 'David', 'Carter', '2010-04-04', 'user', 'false', 'true', 'Asprin', 'Bees, Nuts, Grass', 'Likes Bedtime Stories, Nightlight', '2020-11-25')

INSERT INTO campers 
	(camper_code, family_id, first_name, last_name, dob, registrar, payment_status, active_status)
VALUES
	(200005, 100003, 'Erica', 'Carter', '2009-05-05', 'user', 'true', 'false')

INSERT INTO campers 
	(camper_code, family_id, last_name, dob, registrar, payment_status, active_status)
VALUES
	(200006, 100004, 'Gonzales', '2009-05-05', 'user', 'false', 'false')

SET IDENTITY_INSERT campers OFF;

SET IDENTITY_INSERT camper_updates ON;

INSERT INTO camper_updates 
	(request_id, field_to_be_changed, camper_code, action, new_data, old_data, requestor, status, request_date)
VALUES
	(400001, 'last_name', '200003', 'Update', 'Bowersmith', 'Bowers', 'user', 'Pending', '2021-10-09')

INSERT INTO camper_updates 
	(request_id, field_to_be_changed, camper_code, action, new_data, old_data, requestor, status, request_date, finalize_date)
VALUES
	(400002, 'first_name', '200005', 'Update', 'Erica', 'Eric', 'user', 'Approved', '2021-10-08', '2021-10-10')

INSERT INTO camper_updates 
	(request_id, field_to_be_changed, camper_code, action, new_data, requestor, status, request_date)
VALUES
	(400003, 'first_name', '200006', 'ADD', 'Jimmy', 'user', 'Pending', '2021-09-08')

INSERT INTO camper_updates 
	(request_id, field_to_be_changed, camper_code, action, new_data, old_data, requestor, status, request_date)
VALUES
	(400004, 'payment_status', '200005', 'Update', 'True', 'False', 'user', 'Pending', '2021-07-08')

SET IDENTITY_INSERT camper_updates OFF;


SET IDENTITY_INSERT family_updates ON;

INSERT INTO family_updates 
	(request_id, field_to_be_changed, family_id, action, new_data, old_data, requestor, status, request_date)
VALUES
	(300001, 'address', '100002', 'Update', '200 Secondary Road', '200 Second Ave.', 'user', 'Pending', '2021-10-09')

INSERT INTO family_updates 
	(request_id, field_to_be_changed, family_id, action, new_data, requestor, status, request_date)
VALUES
	(300002, 'email_address', '100004', 'Update', 'gonzales@microsoft.com', 'user', 'Pending', '2021-10-10')

INSERT INTO family_updates 
	(request_id, field_to_be_changed, family_id, action, new_data, requestor, status, request_date)
VALUES
	(300003, 'city', '100004', 'Update', 'Columbus', 'user', 'Pending', '2021-10-10')

INSERT INTO family_updates 
	(request_id, field_to_be_changed, family_id, action, new_data, old_data, requestor, status, request_date, finalize_date)
VALUES
	(300004, 'address', '100002', 'Update', '200 Secondary Road', '200 Secondry Rda', 'user', 'Approved', '2021-10-08', '2021-10-10')

SET IDENTITY_INSERT family_updates OFF;

