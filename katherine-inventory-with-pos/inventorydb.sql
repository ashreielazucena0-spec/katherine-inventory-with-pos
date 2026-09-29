/* ==========================================================================
   inventorydb - Database Creation Script
   Target: MySQL 8.0 (e.g. via XAMPP)
   ==========================================================================
   HOW TO RUN
   1. Open phpMyAdmin or the mysql CLI / MySQL Workbench.
   2. Run this whole file (Import in phpMyAdmin, or `source inventorydb.sql;`).
   3. Update App.config's connection string if your MySQL server/port differs
      from the default used below (127.0.0.1:3306).
   4. Safe to re-run: every table uses CREATE TABLE IF NOT EXISTS, so it
      won't wipe out data you already have.
   ========================================================================== */

CREATE DATABASE IF NOT EXISTS inventorydb;
USE inventorydb;

/* ---------------------------------------------------------------------- */
/* tbl_account - login / create account / system account management      */
/* (no role/type field - every account has the same access)              */
/* ---------------------------------------------------------------------- */
CREATE TABLE IF NOT EXISTS tbl_account (
  acc_id        VARCHAR(10)  NOT NULL,
  acc_name      VARCHAR(100) NOT NULL,
  user_name     VARCHAR(50)  NOT NULL,
  pass_word     VARCHAR(255) NOT NULL,
  gender        VARCHAR(10)  NOT NULL,
  phone         VARCHAR(20)  NULL DEFAULT NULL,
  email         VARCHAR(100) NULL DEFAULT NULL,
  user_status   VARCHAR(10)  NOT NULL DEFAULT 'Active',
  created_at    DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (acc_id),
  UNIQUE KEY uq_account_username (user_name)
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_general_ci;

/* ---------------------------------------------------------------------- */
/* tbl_supplier                                                            */
/* ---------------------------------------------------------------------- */
CREATE TABLE IF NOT EXISTS tbl_supplier (
  supplier_id     VARCHAR(10)  NOT NULL,
  supplier_name   VARCHAR(150) NOT NULL,
  contact_person  VARCHAR(100) NULL DEFAULT NULL,
  phone           VARCHAR(20)  NULL DEFAULT NULL,
  email           VARCHAR(100) NULL DEFAULT NULL,
  address         VARCHAR(255) NULL DEFAULT NULL,
  created_at      DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (supplier_id)
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_general_ci;

/* ---------------------------------------------------------------------- */
/* tbl_category                                                            */
/* ---------------------------------------------------------------------- */
CREATE TABLE IF NOT EXISTS tbl_category (
  category_id    VARCHAR(10)  NOT NULL,
  category_name  VARCHAR(100) NOT NULL,
  PRIMARY KEY (category_id),
  UNIQUE KEY uq_category_name (category_name)
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_general_ci;

/* ---------------------------------------------------------------------- */
/* tbl_product - manage product                                           */
/* ---------------------------------------------------------------------- */
CREATE TABLE IF NOT EXISTS tbl_product (
  product_id      VARCHAR(10)  NOT NULL,
  product_name    VARCHAR(150) NOT NULL,
  barcode         VARCHAR(50)  NULL DEFAULT NULL,
  category_id     VARCHAR(10)  NULL DEFAULT NULL,
  supplier_id     VARCHAR(10)  NULL DEFAULT NULL,
  cost_price      DECIMAL(12,2) NOT NULL DEFAULT 0.00,
  selling_price   DECIMAL(12,2) NOT NULL DEFAULT 0.00,
  product_status  VARCHAR(10)  NOT NULL DEFAULT 'Active',
  created_at      DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (product_id),
  UNIQUE KEY uq_product_barcode (barcode),
  KEY fk_product_category (category_id),
  KEY fk_product_supplier (supplier_id),
  CONSTRAINT fk_product_category FOREIGN KEY (category_id) REFERENCES tbl_category (category_id) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT fk_product_supplier FOREIGN KEY (supplier_id) REFERENCES tbl_supplier (supplier_id) ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_general_ci;

/* ---------------------------------------------------------------------- */
/* tbl_stock - current on-hand quantity per product                      */
/* ---------------------------------------------------------------------- */
CREATE TABLE IF NOT EXISTS tbl_stock (
  stock_id             INT UNSIGNED NOT NULL AUTO_INCREMENT,
  product_id           VARCHAR(10)  NOT NULL,
  quantity             INT          NOT NULL DEFAULT 0,
  low_stock_threshold  INT          NOT NULL DEFAULT 10,
  updated_at           DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (stock_id),
  UNIQUE KEY uq_stock_product (product_id),
  CONSTRAINT fk_stock_product FOREIGN KEY (product_id) REFERENCES tbl_product (product_id) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_general_ci;

/* ---------------------------------------------------------------------- */
/* tbl_stock_log - stock in / out / adjustment / sale / damage history   */
/* (expiry is tracked per incoming batch here, not on tbl_product)       */
/* ---------------------------------------------------------------------- */
CREATE TABLE IF NOT EXISTS tbl_stock_log (
  log_id          BIGINT UNSIGNED NOT NULL AUTO_INCREMENT,
  product_id      VARCHAR(10)  NOT NULL,
  movement_type   ENUM('IN','OUT') NOT NULL,
  reason          VARCHAR(30)  NOT NULL COMMENT 'PURCHASE, SALE, RETURN, DAMAGED, ADJUSTMENT, EXPIRED',
  quantity        INT          NOT NULL,
  batch_no        VARCHAR(50)  NULL DEFAULT NULL,
  expiry_date     DATE         NULL DEFAULT NULL,
  unit_cost       DECIMAL(12,2) NULL DEFAULT NULL,
  supplier_id     VARCHAR(10)  NULL DEFAULT NULL,
  reference_id    VARCHAR(20)  NULL DEFAULT NULL COMMENT 'e.g. sale_id or PO number',
  performed_by    VARCHAR(10)  NULL DEFAULT NULL,
  remarks         VARCHAR(255) NULL DEFAULT NULL,
  created_at      DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (log_id),
  KEY idx_stocklog_product (product_id),
  KEY idx_stocklog_expiry (expiry_date),
  KEY fk_stocklog_supplier (supplier_id),
  KEY fk_stocklog_account (performed_by),
  CONSTRAINT fk_stocklog_account FOREIGN KEY (performed_by) REFERENCES tbl_account (acc_id) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT fk_stocklog_product FOREIGN KEY (product_id) REFERENCES tbl_product (product_id) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT fk_stocklog_supplier FOREIGN KEY (supplier_id) REFERENCES tbl_supplier (supplier_id) ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_general_ci;

/* ---------------------------------------------------------------------- */
/* tbl_sale - point of sale                                                */
/* ---------------------------------------------------------------------- */
CREATE TABLE IF NOT EXISTS tbl_sale (
  sale_id               VARCHAR(20)  NOT NULL,
  cashier_id            VARCHAR(10)  NULL DEFAULT NULL,
  subtotal              DECIMAL(12,2) NOT NULL DEFAULT 0.00,
  discount_amount       DECIMAL(12,2) NOT NULL DEFAULT 0.00,
  discount_type         VARCHAR(30)  NULL DEFAULT NULL,
  discount_approved_by  VARCHAR(10)  NULL DEFAULT NULL,
  tax_amount            DECIMAL(12,2) NOT NULL DEFAULT 0.00,
  total_amount          DECIMAL(12,2) NOT NULL DEFAULT 0.00,
  amount_paid           DECIMAL(12,2) NOT NULL DEFAULT 0.00,
  change_due            DECIMAL(12,2) NOT NULL DEFAULT 0.00,
  payment_method        VARCHAR(20)  NOT NULL DEFAULT 'Cash',
  sale_status           VARCHAR(20)  NOT NULL DEFAULT 'Completed',
  created_at            DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (sale_id),
  KEY fk_sale_cashier (cashier_id),
  KEY idx_sale_created (created_at),
  KEY idx_sale_discount_approver (discount_approved_by),
  CONSTRAINT fk_sale_cashier FOREIGN KEY (cashier_id) REFERENCES tbl_account (acc_id) ON DELETE SET NULL ON UPDATE CASCADE,
  CONSTRAINT fk_sale_discount_approver FOREIGN KEY (discount_approved_by) REFERENCES tbl_account (acc_id) ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_general_ci;

/* ---------------------------------------------------------------------- */
/* tbl_sale_item                                                           */
/* ---------------------------------------------------------------------- */
CREATE TABLE IF NOT EXISTS tbl_sale_item (
  sale_item_id    BIGINT UNSIGNED NOT NULL AUTO_INCREMENT,
  sale_id         VARCHAR(20)  NOT NULL,
  product_id      VARCHAR(10)  NOT NULL,
  quantity        INT          NOT NULL,
  unit_price      DECIMAL(12,2) NOT NULL,
  line_discount   DECIMAL(12,2) NOT NULL DEFAULT 0.00,
  line_total      DECIMAL(12,2) NOT NULL,
  PRIMARY KEY (sale_item_id),
  KEY idx_saleitem_sale (sale_id),
  KEY idx_saleitem_product (product_id),
  CONSTRAINT fk_saleitem_product FOREIGN KEY (product_id) REFERENCES tbl_product (product_id) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT fk_saleitem_sale FOREIGN KEY (sale_id) REFERENCES tbl_sale (sale_id) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE = utf8mb4_general_ci;

/* ---------------------------------------------------------------------- */
/* Helpful views for the app's dashboard / reports                        */
/* ---------------------------------------------------------------------- */
CREATE OR REPLACE VIEW vw_product_stock AS
SELECT
    s.product_id       AS product_id,
    s.quantity         AS current_quantity
FROM tbl_stock s;

CREATE OR REPLACE VIEW vw_low_stock AS
SELECT p.product_id, p.product_name, c.category_name,
       s.quantity AS quantity_in_stock, s.low_stock_threshold
FROM tbl_product p
JOIN tbl_stock s ON s.product_id = p.product_id
LEFT JOIN tbl_category c ON c.category_id = p.category_id
WHERE p.product_status = 'Active' AND s.quantity <= s.low_stock_threshold;

-- Flags a product as "expired" based on the expiry date of its most recent
-- incoming ('IN') stock batch in tbl_stock_log, since expiry is tracked per
-- batch rather than as a single column on tbl_product.
CREATE OR REPLACE VIEW vw_expired_products AS
SELECT p.product_id, p.product_name, c.category_name,
       s.quantity AS quantity_in_stock, b.expiry_date,
       DATEDIFF(CURDATE(), b.expiry_date) AS days_expired
FROM tbl_product p
JOIN tbl_stock s ON s.product_id = p.product_id
JOIN (
    SELECT sl.product_id, sl.expiry_date,
           ROW_NUMBER() OVER (PARTITION BY sl.product_id ORDER BY sl.created_at DESC) AS rn
    FROM tbl_stock_log sl
    WHERE sl.movement_type = 'IN' AND sl.expiry_date IS NOT NULL
) b ON b.product_id = p.product_id AND b.rn = 1
LEFT JOIN tbl_category c ON c.category_id = p.category_id
WHERE p.product_status = 'Active' AND b.expiry_date <= CURDATE();

/* ---------------------------------------------------------------------- */
/* Seed data (safe to run on a fresh database only)                       */
/* ---------------------------------------------------------------------- */

-- Default administrator account: username "admin", password "admin123"
-- PasswordHash below is SHA-256 of "admin123" (lower-case hex, matches
-- SecurityHelper.HashPassword in the WinForms project).
INSERT INTO tbl_account (acc_id, acc_name, user_name, pass_word, gender, user_status)
SELECT 'ACC-001', 'System Administrator', 'admin',
       '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9',
       'Prefer not to say', 'Active'
WHERE NOT EXISTS (SELECT 1 FROM tbl_account WHERE user_name = 'admin');

INSERT INTO tbl_category (category_id, category_name)
SELECT * FROM (
    SELECT 'CAT-0001' AS category_id, 'General' AS category_name
    UNION ALL SELECT 'CAT-0002', 'Beverages'
    UNION ALL SELECT 'CAT-0003', 'Snacks'
    UNION ALL SELECT 'CAT-0004', 'Grocery'
    UNION ALL SELECT 'CAT-0005', 'Household'
) AS seed
WHERE NOT EXISTS (SELECT 1 FROM tbl_category);

INSERT INTO tbl_product (product_id, product_name, category_id, cost_price, selling_price, product_status)
SELECT * FROM (
    SELECT 'PRD-0001' AS product_id, 'Bottled Water 500ml' AS product_name, 'CAT-0002' AS category_id, 12.00 AS cost_price, 20.00 AS selling_price, 'Active' AS product_status
    UNION ALL SELECT 'PRD-0002', 'Instant Noodles', 'CAT-0004', 9.00, 15.00, 'Active'
    UNION ALL SELECT 'PRD-0003', 'Potato Chips', 'CAT-0003', 22.00, 35.00, 'Active'
    UNION ALL SELECT 'PRD-0004', 'Dish Soap', 'CAT-0005', 38.00, 55.00, 'Active'
) AS seed
WHERE NOT EXISTS (SELECT 1 FROM tbl_product);

INSERT INTO tbl_stock (product_id, quantity, low_stock_threshold)
SELECT * FROM (
    SELECT 'PRD-0001' AS product_id, 100 AS quantity, 20 AS low_stock_threshold
    UNION ALL SELECT 'PRD-0002', 150, 30
    UNION ALL SELECT 'PRD-0003', 80, 15
    UNION ALL SELECT 'PRD-0004', 40, 10
) AS seed
WHERE NOT EXISTS (SELECT 1 FROM tbl_stock);

SELECT 'inventorydb is ready. Default login -> username: admin / password: admin123' AS Message;
