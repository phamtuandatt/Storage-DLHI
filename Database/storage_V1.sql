﻿CREATE DATABASE STORAGE_DLHI
USE STORAGE_DLHI

GO
CREATE TABLE SUPPLIER (
	ID	UNIQUEIDENTIFIER NOT NULL,
	CODE CHAR(10),
	NAME_SUPPIER NVARCHAR(100),
	NAME_COMPANY_SUPPLIER NVARCHAR(MAX),
	ADDRESS NVARCHAR(MAX),
	PHONE CHAR(12),
	EMAIL NVARCHAR(MAX),
	NOTE NVARCHAR(MAX),

	CONSTRAINT PK_SUPPLIER PRIMARY KEY (ID),
)

GO
CREATE TABLE SUPPLIER_TYPE (
	ID UNIQUEIDENTIFIER NOT NULL,
	NAME NVARCHAR(20),

	CONSTRAINT PK_SUPPLIER_TYPES PRIMARY KEY (ID),
)

GO
CREATE TABLE UNIT (
	ID UNIQUEIDENTIFIER NOT NULL,
	NAME NVARCHAR(20),

	CONSTRAINT PK_UNIT PRIMARY KEY (ID),
)

GO
CREATE TABLE GROUPS (
	ID UNIQUEIDENTIFIER NOT NULL,
	NAME NVARCHAR(50),

	CONSTRAINT PK_GROUP_CONSUMABLE PRIMARY KEY (ID),
)

GO
CREATE TABLE TYPES (
	ID UNIQUEIDENTIFIER NOT NULL,
	NAME NVARCHAR(100),

	CONSTRAINT PK_TYPE_CONSUMABLE PRIMARY KEY (ID),
)

GO 
CREATE TABLE LOCATION_WAREHOUSE (
	ID UNIQUEIDENTIFIER NOT NULL,
	NAME NVARCHAR(MAX),

	CONSTRAINT PK_LOCATION_WAREHOUSE PRIMARY KEY (ID),
)


GO 
CREATE TABLE ITEM (
	ID UNIQUEIDENTIFIER NOT NULL,
	CODE CHAR(13),
	NAME NVARCHAR(100),
	PICTURE_LINK NVARCHAR(100),
	PICTURE VARBINARY(MAX),
	NOTE NVARCHAR(MAX),
	ENG_NAME NVARCHAR(MAX),
	
	UNIT_ID UNIQUEIDENTIFIER,
	GROUP_ID UNIQUEIDENTIFIER,
	TYPE_ID UNIQUEIDENTIFIER,
	SUPPLIER_ID UNIQUEIDENTIFIER,
	LOCATION_WAREHOUSE_ID UNIQUEIDENTIFIER,

	CONSTRAINT PK_CONSUMABLE PRIMARY KEY (ID),
	CONSTRAINT FK_COMSUMABLE_UNIT FOREIGN KEY (UNIT_ID) REFERENCES UNIT(ID),
	CONSTRAINT FK_COMSUMABLE_GROUP_CONSUMABLE FOREIGN KEY (GROUP_ID) REFERENCES GROUPS(ID),
	CONSTRAINT FK_COMSUMABLE_TYPE_CONSUMABLE FOREIGN KEY (TYPE_ID) REFERENCES TYPES(ID),
	CONSTRAINT FK_ITEM_SUPPLIER FOREIGN KEY (SUPPLIER_ID) REFERENCES SUPPLIER(ID),
	CONSTRAINT FK_ITEM_LOCATIONWAREHOUSE FOREIGN KEY (LOCATION_WAREHOUSE_ID) REFERENCES LOCATION_WAREHOUSE(ID),
)

GO
CREATE TABLE  MPR (
	ID UNIQUEIDENTIFIER NOT NULL,
	CREATED DATETIME,
	EXPECTED_DELIVERY DATETIME,
	NOTE NVARCHAR(MAX),
	ITEM_ID UNIQUEIDENTIFIER NOT NULL,
	MPR_NO NVARCHAR(100),
	USAGE NVARCHAR(MAX),
	QUANTITY INT,

	CONSTRAINT PK_MPR PRIMARY KEY (ID),
	CONSTRAINT FK_MPR_ITEM FOREIGN KEY (ITEM_ID) REFERENCES ITEM(ID),
)

GO
CREATE TABLE MPR_EXPORT (
	ID UNIQUEIDENTIFIER NOT NULL,
	CREATED DATETIME,
	ITEM_COUNT INT,
	STATUS INT, 
	-- 0: Exported MPR -> When Export -> Update status : 0
	-- 1: Do not exported but create new MPR_Export -> When check create new MPR_Export -> Get MPR have status is 2 -> Update = 1
	-- 2: Do not exported and do not create MPR_Export -> MPR_Export new will have status 2

	CONSTRAINT PK_MPR_EXPORT PRIMARY KEY (ID),
)

GO
CREATE TABLE MPR_EXPORT_DETAIL (
	MPR_EXPORT_ID UNIQUEIDENTIFIER NOT NULL,
	MPR_ID UNIQUEIDENTIFIER NOT NULL,

	CONSTRAINT PK_MPR_EXPORT_DETAIL PRIMARY KEY (MPR_EXPORT_ID, MPR_ID),
	CONSTRAINT FK_MPR_EXPORT_DETAIL_MPR_EXPORT FOREIGN KEY (MPR_EXPORT_ID) REFERENCES MPR_EXPORT(ID),
	CONSTRAINT FK_MPR_EXPORT_DETAIL_MPR FOREIGN KEY (MPR_ID) REFERENCES MPR(ID),
)

GO
CREATE TABLE WAREHOUSE (
	ID UNIQUEIDENTIFIER NOT NULL,
	NAME NVARCHAR(MAX),
	ADDRESS NVARCHAR(MAX),
	TOTAL_ITEM INT,

	CONSTRAINT PK_WAREHOUSE PRIMARY KEY (ID),
)

GO 
CREATE TABLE WAREHOUSE_DETAIL (
	WAREHOUSE_ID UNIQUEIDENTIFIER NOT NULL,
	ITEM_ID UNIQUEIDENTIFIER NOT NULL,
	QUANTITY INT,

	CONSTRAINT PK_WAREHOUSE_DETAIL PRIMARY KEY (WAREHOUSE_ID, ITEM_ID),
	CONSTRAINT FK_WAREHOUSE_DETAIL_WAREHOUSE FOREIGN KEY (WAREHOUSE_ID) REFERENCES WAREHOUSE(ID),
	CONSTRAINT FK_WAREHOUSE_DETAIL_ITEM FOREIGN KEY (ITEM_ID) REFERENCES ITEM(ID)
)

GO
CREATE TABLE PAYMENT_METHOD (
	ID UNIQUEIDENTIFIER NOT NULL,
	NAME NVARCHAR(MAX),

	CONSTRAINT PK_PAYMENT_METHOD PRIMARY KEY (ID),
)

GO
CREATE TABLE PO (
	ID UNIQUEIDENTIFIER NOT NULL,
	CREATED DATETIME,
	EXPECTED_DELIVERY DATETIME,
	TOTAL INT,

	LOCATION_WAREHOUSE_ID UNIQUEIDENTIFIER,
	PAYMENT_METHOD_ID UNIQUEIDENTIFIER,

	CONSTRAINT PK_PO PRIMARY KEY (ID),
	CONSTRAINT FK_PO_LOCATION_WAREHOUSE FOREIGN KEY (LOCATION_WAREHOUSE_ID) REFERENCES WAREHOUSE(ID),
	CONSTRAINT FK_PO_PAYMENT_METHOD FOREIGN KEY (PAYMENT_METHOD_ID) REFERENCES PAYMENT_METHOD(ID),
)

GO 
CREATE TABLE PO_DETAIL (
	PO_ID UNIQUEIDENTIFIER NOT NULL,
	ITEM_ID UNIQUEIDENTIFIER NOT NULL,
	MPR_NO NVARCHAR(MAX),
	PO_NO NVARCHAR(MAX),
	PRICE INT,
	QUANTITY INT,

	CONSTRAINT PK_PO_DETAIL PRIMARY KEY (PO_ID, ITEM_ID),
	CONSTRAINT FK_PO_DETIAL_PO FOREIGN KEY (PO_ID) REFERENCES PO(ID),
	CONSTRAINT FK_PO_DETAIL_ITEM FOREIGN KEY (ITEM_ID) REFERENCES ITEM(ID),
)

GO
CREATE TABLE IMPORT_ITEM (
	ID UNIQUEIDENTIFIER NOT NULL,
	CREATED DATETIME,
	BILL_NO NVARCHAR(MAX),
	SUM_QUANTITY INT,
	SUM_PRICE INT,
	SUM_PAYMENT INT,

	CONSTRAINT PK_IMPORT_ITEM PRIMARY KEY (ID),
)

GO 
CREATE TABLE IMPORT_ITEM_DETAIL(
	IMPORT_ITEM_ID UNIQUEIDENTIFIER NOT NULL,
	ITEM_ID UNIQUEIDENTIFIER NOT NULL,
	QTY INT,
	PRICE INT,
	TOTAL INT,
	NOTE NVARCHAR(MAX),

	CONSTRAINT PK_IMPORT_ITEM_DETAIL PRIMARY KEY (IMPORT_ITEM_ID, ITEM_ID),
	CONSTRAINT FK_IMPORT_ITEM_DETAIL_IMPORT_ITEM FOREIGN KEY (IMPORT_ITEM_ID) REFERENCES IMPORT_ITEM(ID),
	CONSTRAINT FK_IMPORT_ITEM_DETAIL_ITEM FOREIGN KEY (ITEM_ID) REFERENCES ITEM(ID),
)

GO
CREATE TABLE EXPORT_ITEM (
	ID UNIQUEIDENTIFIER NOT NULL,
	CREATED DATETIME,
	BILL_NO NVARCHAR(MAX),
	SUM_QUANTITY INT,

	CONSTRAINT PK_EXPORT_ITEM PRIMARY KEY (ID),
)

GO
CREATE TABLE EXPORT_ITEM_DETAIL (
	EXPORT_ITEM_ID UNIQUEIDENTIFIER NOT NULL,
	ITEM_ID UNIQUEIDENTIFIER NOT NULL,
	QTY INT,
	NOTE NVARCHAR(MAX),

	CONSTRAINT PK_EXPORT_ITEM_DETAIL PRIMARY KEY (EXPORT_ITEM_ID, ITEM_ID),
	CONSTRAINT FK_EXPORT_ITEM_DETAIL_EXPORT_ITEM FOREIGN KEY (EXPORT_ITEM_ID) REFERENCES EXPORT_ITEM(ID),
	CONSTRAINT FK_EXPORT_ITEM_DETAIL_ITEM FOREIGN KEY (ITEM_ID) REFERENCES ITEM(ID)
)

GO 
CREATE TABLE INVENTORY (
	ID UNIQUEIDENTIFIER NOT NULL,
	ITEM_ID UNIQUEIDENTIFIER,
	INVENTORY_MONTH INT,
	AMOUNT INT

	CONSTRAINT PK_INVENTORY PRIMARY KEY (ID),
	CONSTRAINT FK_INVENTORY_ITEM FOREIGN KEY (ITEM_ID) REFERENCES ITEM(ID),
)


----------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------
-----------------------------PROCEDURE--------------------------------------------------------------
----------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------
GO
CREATE PROC GET_ITEMS
AS
BEGIN
	SELECT ITEM.ID, ITEM.CODE, ITEM.NAME, ITEM.PICTURE_LINK, ITEM.PICTURE,
			UNIT.NAME AS UNIT, GROUPS.NAME AS GROUPS, SUPPLIER.NAME_SUPPIER AS SUPPLIER, LOCATION_WAREHOUSE.NAME AS LOCATION_WAREHOUSE, ITEM.NOTE, ITEM.ENG_NAME
	FROM ITEM, UNIT, GROUPS, TYPES, SUPPLIER, LOCATION_WAREHOUSE
	WHERE ITEM.UNIT_ID = UNIT.ID 
			AND ITEM.GROUP_ID = GROUPS.ID 
			AND ITEM.SUPPLIER_ID = SUPPLIER.ID
			AND ITEM.LOCATION_WAREHOUSE_ID = LOCATION_WAREHOUSE.ID
END
GO
EXEC GET_ITEMS

----------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------

GO
CREATE PROC GET_ITEMS_V2
AS
BEGIN
	SELECT ITEM.ID, ITEM.CODE, ITEM.NAME, ITEM.PICTURE_LINK, ITEM.PICTURE,
			UNIT.NAME AS UNIT, GROUPS.NAME AS GROUPS, SUPPLIER.NAME_SUPPIER AS SUPPLIER, ITEM.NOTE, ITEM.ENG_NAME
	FROM ITEM, UNIT, GROUPS, SUPPLIER
	WHERE ITEM.UNIT_ID = UNIT.ID 
			AND ITEM.GROUP_ID = GROUPS.ID 
			AND ITEM.SUPPLIER_ID = SUPPLIER.ID
END
GO
EXEC GET_ITEMS_V2

----------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------

GO
CREATE PROC GET_ITEMS_EXPORT @WAREHOUSE_ID UNIQUEIDENTIFIER
AS
BEGIN
	SELECT ITEM.ID, ITEM.CODE, ITEM.NAME, ITEM.PICTURE_LINK, ITEM.PICTURE,
			UNIT.NAME AS UNIT, GROUPS.NAME AS GROUPS, SUPPLIER.NAME_SUPPIER AS SUPPLIER, ITEM.NOTE, ITEM.ENG_NAME
	FROM ITEM, UNIT, GROUPS, TYPES, SUPPLIER
	WHERE ITEM.UNIT_ID = UNIT.ID 
			AND ITEM.GROUP_ID = GROUPS.ID 
			AND ITEM.SUPPLIER_ID = SUPPLIER.ID
			AND ITEM.ID IN (SELECT ITEM_ID FROM WAREHOUSE_DETAIL WHERE WAREHOUSE_ID = @WAREHOUSE_ID)
END
GO
EXEC GET_ITEMS_EXPORT '3141A894-0B11-48BE-9EA0-DCAC7B3CC7AD'

----------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------

GO
CREATE PROC GET_ITEMS_EXPORT_V2 @WAREHOUSE_ID UNIQUEIDENTIFIER
AS
BEGIN
	SELECT WAREHOUSE_DETAIL.WAREHOUSE_ID, WAREHOUSE_DETAIL.ITEM_ID, 
			ITEM.CODE, ITEM.NAME, ITEM.PICTURE, WAREHOUSE_DETAIL.QUANTITY,
			UNIT.NAME AS UNIT, GROUPS.NAME AS GROUPS, SUPPLIER.NAME_SUPPIER AS SUPPLIER
	FROM WAREHOUSE_DETAIL, ITEM, UNIT, GROUPS, SUPPLIER
	WHERE WAREHOUSE_DETAIL.ITEM_ID = ITEM.ID
		AND ITEM.GROUP_ID = GROUPS.ID
		AND ITEM.UNIT_ID = UNIT.ID
		AND ITEM.SUPPLIER_ID = SUPPLIER.ID
		AND WAREHOUSE_DETAIL.WAREHOUSE_ID = @WAREHOUSE_ID

END
GO
EXEC GET_ITEMS_EXPORT_V2 '3141A894-0B11-48BE-9EA0-DCAC7B3CC7AD'

----------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------
GO
CREATE PROC GET_CURRENT_CODE_SUPPLIER @KEY_CODE CHAR(3)
AS
BEGIN
	SELECT MAX(RIGHT(RTRIM(CODE), 7)) AS NUMBER FROM SUPPLIER 
	WHERE CODE LIKE @KEY_CODE + '%'
END
GO
EXEC GET_CURRENT_CODE_SUPPLIER 'CMA'

----------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------
GO
CREATE PROC GET_CURRENT_BILLNO @KEY_CODE CHAR(8)
AS
BEGIN
	SELECT MAX(RIGHT(RTRIM(BILL_NO), 3)) AS NUMBER FROM IMPORT_ITEM 
	WHERE BILL_NO LIKE '%' + @KEY_CODE + '%'
END
GO
EXEC GET_CURRENT_BILLNO '14112023'

----------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------
GO
CREATE PROC GET_CURRENT_BILLNO_EXPORT @KEY_CODE CHAR(8)
AS
BEGIN
	SELECT MAX(RIGHT(RTRIM(BILL_NO), 3)) AS NUMBER FROM EXPORT_ITEM 
	WHERE BILL_NO LIKE '%' + @KEY_CODE + '%'
END
GO
EXEC GET_CURRENT_BILLNO_EXPORT '14112023'

----------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------
GO
CREATE PROC GET_CURRENT_CODE_ITEM @KEY_CODE CHAR(6)
AS
BEGIN
	SELECT MAX(RIGHT(RTRIM(CODE), 4)) AS NUMBER FROM ITEM 
	WHERE CODE LIKE @KEY_CODE + '%'
END
GO
EXEC GET_CURRENT_CODE_ITEM ''

----------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------
GO
CREATE PROC	GET_MPR_LIST 
AS
BEGIN
	SELECT MPR.ID AS MPR_ID, ITEM.ID AS ITEM_ID, ITEM.CODE, ITEM.NAME, UNIT.NAME AS UNIT, ITEM.PICTURE, 
			MPR.USAGE, MPR.CREATED, MPR.QUANTITY, MPR.NOTE, MPR.MPR_NO
	FROM MPR, ITEM, UNIT
	WHERE MPR.ITEM_ID = ITEM.ID AND ITEM.UNIT_ID = UNIT.ID
END
GO
EXEC GET_MPR_LIST

----------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------
GO
CREATE PROC GET_MPR_EXPORT_DETAIL @ID UNIQUEIDENTIFIER
AS
BEGIN
	SELECT MPR.ID AS MPR_ID, ITEM.ID AS ITEM_ID, ITEM.CODE, ITEM.NAME, UNIT.NAME AS UNIT, ITEM.PICTURE, 
			MPR.USAGE, MPR.CREATED, MPR.QUANTITY, MPR.NOTE, MPR.MPR_NO
	FROM MPR, ITEM, UNIT
	WHERE MPR.ITEM_ID = ITEM.ID AND ITEM.UNIT_ID = UNIT.ID 
		AND MPR.ID IN (SELECT MPR_ID FROM MPR_EXPORT_DETAIL WHERE MPR_EXPORT_ID = @ID)
END
GO
EXEC GET_MPR_EXPORT_DETAIL '0F921F32-88F2-48B0-A3EE-D09AED4372B0'

----------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------
GO
CREATE PROC GET_MPR_EXPORT_DETAILS
AS
BEGIN
SELECT MPR.ID AS MPR_ID, ITEM.ID AS ITEM_ID, ITEM.CODE, ITEM.NAME, UNIT.NAME AS UNIT, ITEM.PICTURE, 
			MPR.USAGE, MPR.CREATED, MPR.QUANTITY, MPR.NOTE, MPR.MPR_NO, MPR_EXPORT_DETAIL.MPR_EXPORT_ID
	FROM MPR, ITEM, UNIT, MPR_EXPORT_DETAIL
	WHERE MPR.ITEM_ID = ITEM.ID AND ITEM.UNIT_ID = UNIT.ID 
		AND MPR.ID = MPR_EXPORT_DETAIL.MPR_ID
END
GO
EXEC GET_MPR_EXPORT_DETAILS

----------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------
GO
CREATE PROC UPDATE_ITEM_COUNT_MPR_EXPORT @ID UNIQUEIDENTIFIER
AS
BEGIN
	UPDATE MPR_EXPORT 
	SET ITEM_COUNT = (SELECT COUNT(MPR_EXPORT_ID) AS TOTAL 
					FROM MPR_EXPORT_DETAIL WHERE MPR_EXPORT_ID = @ID)
	WHERE ID = @ID
END
GO
SELECT *FROM MPR_EXPORT
EXEC UPDATE_ITEM_COUNT_MPR_EXPORT '9D70F742-CA22-4ACB-89B8-8CD8209475BF'

----------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------
GO
CREATE PROC GET_POs
AS
BEGIN
	SELECT PO.ID, PO.CREATED, PO.EXPECTED_DELIVERY, PO.TOTAL, WAREHOUSE.NAME AS WAREHOUSE, PAYMENT_METHOD.NAME AS PAYMENT
	FROM PO, WAREHOUSE, PAYMENT_METHOD
	WHERE PO.LOCATION_WAREHOUSE_ID = WAREHOUSE.ID
		AND PO.PAYMENT_METHOD_ID = PAYMENT_METHOD.ID
END
GO
EXEC GET_POs

----------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------
GO
CREATE PROC GET_PO_DETAIL
AS
BEGIN
	SELECT PO_DETAIL.PO_ID, ITEM.CODE, ITEM.NAME, ITEM.PICTURE, PO_DETAIL.MPR_NO, PO_DETAIL.PO_NO, PO_DETAIL.QUANTITY, PO_DETAIL.PRICE
	FROM PO_DETAIL, ITEM
	WHERE PO_DETAIL.ITEM_ID = ITEM.ID
END
GO
EXEC GET_PO_DETAIL

----------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------
GO
CREATE PROC GET_IMPORT_ITEMS
AS
BEGIN
	SELECT IMPORT_ITEM_DETAIL.IMPORT_ITEM_ID, IMPORT_ITEM_DETAIL.ITEM_ID, 
		ITEM.CODE, ITEM.NAME, ITEM.PICTURE, IMPORT_ITEM_DETAIL.QTY, IMPORT_ITEM_DETAIL.PRICE, 
		(IMPORT_ITEM_DETAIL.QTY * IMPORT_ITEM_DETAIL.PRICE) AS TOTAL, IMPORT_ITEM_DETAIL.NOTE,
		SUPPLIER.CODE AS SUPPLIER_CODE, SUPPLIER.NAME_SUPPIER
	FROM IMPORT_ITEM_DETAIL, ITEM, SUPPLIER
	WHERE IMPORT_ITEM_DETAIL.ITEM_ID = ITEM.ID
	AND ITEM.SUPPLIER_ID = SUPPLIER.ID
END
GO
EXEC GET_IMPORT_ITEMS

----------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------
GO
CREATE PROC GET_EMPORT_ITEMS
AS
BEGIN
	SELECT EXPORT_ITEM_DETAIL.EXPORT_ITEM_ID, EXPORT_ITEM_DETAIL.ITEM_ID, 
		ITEM.CODE, ITEM.NAME, ITEM.PICTURE, EXPORT_ITEM_DETAIL.QTY, EXPORT_ITEM_DETAIL.NOTE,
		SUPPLIER.CODE AS SUPPLIER_CODE, SUPPLIER.NAME_SUPPIER
	FROM EXPORT_ITEM_DETAIL, ITEM, SUPPLIER
	WHERE EXPORT_ITEM_DETAIL.ITEM_ID = ITEM.ID
	AND ITEM.SUPPLIER_ID = SUPPLIER.ID
END
GO
EXEC GET_EMPORT_ITEMS

----------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------
GO
CREATE PROC GET_IMPORT_EXPORT_ITEM_BY_LAST_MONTH @MONTH INT
AS
BEGIN
	SELECT IMPORT_INFO.ITEM_ID, IMPORT_INFO.SUM_QTY_IMPORT, 
			IMPORT_INFO.SUM_PRICE_IMPORT, EXPORT_INFO.SUM_QTY_EXPORT, INVENTORY_INFO.INVENTORY AS INVENTORY
	FROM (SELECT ITEM_ID, SUM(QTY) AS SUM_QTY_IMPORT, SUM(PRICE) AS SUM_PRICE_IMPORT 
			FROM IMPORT_ITEM_DETAIL 
			WHERE IMPORT_ITEM_ID IN (SELECT ID FROM IMPORT_ITEM WHERE MONTH(CREATED) = @MONTH)
			GROUP BY ITEM_ID) IMPORT_INFO,
		 (SELECT ITEM_ID, SUM(QTY) AS SUM_QTY_EXPORT 
			FROM EXPORT_ITEM_DETAIL 
			WHERE EXPORT_ITEM_ID IN (SELECT ID FROM EXPORT_ITEM WHERE MONTH(CREATED) = @MONTH)
			GROUP BY ITEM_ID) EXPORT_INFO,
		 (SELECT ITEM_ID, SUM(QUANTITY) AS INVENTORY 
				FROM WAREHOUSE_DETAIL
				GROUP BY ITEM_ID) AS INVENTORY_INFO
	WHERE IMPORT_INFO.ITEM_ID = EXPORT_INFO.ITEM_ID
		AND INVENTORY_INFO.ITEM_ID = IMPORT_INFO.ITEM_ID
END
GO
EXEC GET_IMPORT_EXPORT_ITEM_BY_LAST_MONTH 11

----------------------------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------------------------
SELECT *FROM WAREHOUSE
SELECT *FROM WAREHOUSE_DETAIL

SELECT IMPORT_INFO.ITEM_ID, INVENTORY_INFO.INVENTORY, IMPORT_INFO.SUM_QTY_IMPORT, 
		IMPORT_INFO.SUM_PRICE_IMPORT, EXPORT_INFO.SUM_QTY_EXPORT,  
		(INVENTORY_INFO.INVENTORY + IMPORT_INFO.SUM_QTY_IMPORT - EXPORT_INFO.SUM_QTY_EXPORT) AS LAST_INVENTORY
		FROM (SELECT ITEM_ID, SUM(QTY) AS SUM_QTY_IMPORT, SUM(PRICE) AS SUM_PRICE_IMPORT 
				FROM IMPORT_ITEM_DETAIL 
				WHERE IMPORT_ITEM_ID IN (SELECT ID FROM IMPORT_ITEM WHERE MONTH(CREATED) = 11)
				GROUP BY ITEM_ID) IMPORT_INFO,
			 (SELECT ITEM_ID, SUM(QTY) AS SUM_QTY_EXPORT 
				FROM EXPORT_ITEM_DETAIL 
				WHERE EXPORT_ITEM_ID IN (SELECT ID FROM EXPORT_ITEM WHERE MONTH(CREATED) = 11)
				GROUP BY ITEM_ID) EXPORT_INFO,
			 (SELECT ITEM_ID, SUM(QUANTITY) AS INVENTORY 
				FROM WAREHOUSE_DETAIL 
				GROUP BY ITEM_ID) AS INVENTORY_INFO
		WHERE IMPORT_INFO.ITEM_ID = EXPORT_INFO.ITEM_ID
			AND IMPORT_INFO.ITEM_ID = INVENTORY_INFO.ITEM_ID


SELECT ITEM.ID, ITEM.CODE, ITEM.NAME, UNIT.NAME, WAREHOUSE_INFO.INVENTORY, WAREHOUSE_INFO.SUM_QTY_IMPORT, 
		WAREHOUSE_INFO.SUM_PRICE_IMPORT, WAREHOUSE_INFO.SUM_QTY_EXPORT, WAREHOUSE_INFO.LAST_INVENTORY,
		GROUPS.NAME, SUPPLIER.NAME_SUPPIER
FROM (SELECT IMPORT_INFO.ITEM_ID, INVENTORY_INFO.INVENTORY, IMPORT_INFO.SUM_QTY_IMPORT, 
		IMPORT_INFO.SUM_PRICE_IMPORT, EXPORT_INFO.SUM_QTY_EXPORT,  
		(INVENTORY_INFO.INVENTORY + IMPORT_INFO.SUM_QTY_IMPORT - EXPORT_INFO.SUM_QTY_EXPORT) AS LAST_INVENTORY
		FROM (SELECT ITEM_ID, SUM(QTY) AS SUM_QTY_IMPORT, SUM(PRICE) AS SUM_PRICE_IMPORT 
				FROM IMPORT_ITEM_DETAIL 
				WHERE IMPORT_ITEM_ID IN (SELECT ID FROM IMPORT_ITEM WHERE MONTH(CREATED) = 11)
				GROUP BY ITEM_ID) IMPORT_INFO,
			 (SELECT ITEM_ID, SUM(QTY) AS SUM_QTY_EXPORT 
				FROM EXPORT_ITEM_DETAIL 
				WHERE EXPORT_ITEM_ID IN (SELECT ID FROM EXPORT_ITEM WHERE MONTH(CREATED) = 11)
				GROUP BY ITEM_ID) EXPORT_INFO,
			 (SELECT ITEM_ID, SUM(QUANTITY) AS INVENTORY 
				FROM WAREHOUSE_DETAIL
				GROUP BY ITEM_ID) AS INVENTORY_INFO
		WHERE IMPORT_INFO.ITEM_ID = EXPORT_INFO.ITEM_ID
			AND IMPORT_INFO.ITEM_ID = INVENTORY_INFO.ITEM_ID) WAREHOUSE_INFO,
		ITEM, SUPPLIER, UNIT, GROUPS
WHERE WAREHOUSE_INFO.ITEM_ID = ITEM.ID
	AND ITEM.SUPPLIER_ID = SUPPLIER.ID
	AND ITEM.UNIT_ID = UNIT.ID
	AND ITEM.GROUP_ID = GROUPS.ID


SELECT ITEM.ID, ITEM.CODE, ITEM.NAME, UNIT.NAME, INVENTORY.AMOUNT, WAREHOUSE_INFO.SUM_QTY_IMPORT, 
		WAREHOUSE_INFO.SUM_PRICE_IMPORT, WAREHOUSE_INFO.SUM_QTY_EXPORT,
		(INVENTORY.AMOUNT + WAREHOUSE_INFO.SUM_QTY_IMPORT - WAREHOUSE_INFO.SUM_QTY_EXPORT) AS LAST_INVENTORY,
		GROUPS.NAME, SUPPLIER.NAME_SUPPIER
FROM (SELECT IMPORT_INFO.ITEM_ID, IMPORT_INFO.SUM_QTY_IMPORT, 
		IMPORT_INFO.SUM_PRICE_IMPORT, EXPORT_INFO.SUM_QTY_EXPORT
		FROM (SELECT ITEM_ID, SUM(QTY) AS SUM_QTY_IMPORT, SUM(PRICE) AS SUM_PRICE_IMPORT 
				FROM IMPORT_ITEM_DETAIL 
				WHERE IMPORT_ITEM_ID IN (SELECT ID FROM IMPORT_ITEM WHERE MONTH(CREATED) = 12)
				GROUP BY ITEM_ID) IMPORT_INFO,
			 (SELECT ITEM_ID, SUM(QTY) AS SUM_QTY_EXPORT 
				FROM EXPORT_ITEM_DETAIL 
				WHERE EXPORT_ITEM_ID IN (SELECT ID FROM EXPORT_ITEM WHERE MONTH(CREATED) = 12)
				GROUP BY ITEM_ID) EXPORT_INFO
		WHERE IMPORT_INFO.ITEM_ID = EXPORT_INFO.ITEM_ID) WAREHOUSE_INFO,
		ITEM, SUPPLIER, UNIT, GROUPS, INVENTORY
WHERE WAREHOUSE_INFO.ITEM_ID = ITEM.ID
	AND ITEM.SUPPLIER_ID = SUPPLIER.ID
	AND ITEM.UNIT_ID = UNIT.ID
	AND ITEM.GROUP_ID = GROUPS.ID
	AND WAREHOUSE_INFO.ITEM_ID = INVENTORY.ITEM_ID
	AND INVENTORY.INVENTORY_MONTH = 11


----------------------------------------------------------------------------------------------------
-------------------------------FUNCTION-------------------------------------------------------------
----------------------------------------------------------------------------------------------------


SELECT *FROM INVENTORY
SELECT *FROM MPR_EXPORT
EXEC GET_IMPORT_EXPORT_ITEM_BY_LAST_MONTH 11

