
SET NAMES 'utf8mb4';
SET CHARACTER SET utf8mb4;

CREATE TABLE person (
    id INT AUTO_INCREMENT PRIMARY KEY,
    first_name VARCHAR(100) NOT NULL,
    last_name VARCHAR(100) NOT NULL,
    secret_code VARCHAR(100) NOT NULL UNIQUE,
    type ENUM('Reporter', 'Target', 'Both', 'PotentialAgent') NOT NULL,
    num_reports INT DEFAULT 0,
    num_mentions INT DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE report (
    id INT AUTO_INCREMENT PRIMARY KEY,
    reporter_id INT NOT NULL,
    target_id INT NOT NULL,
    text TEXT NOT NULL,
    timestamp DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (reporter_id) REFERENCES person(id) ON DELETE CASCADE,
    FOREIGN KEY (target_id) REFERENCES person(id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE alerts (
    id INT AUTO_INCREMENT PRIMARY KEY,
    target_id INT NOT NULL,
    start_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    alert_reason VARCHAR(255) NOT NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    end_time DATETIME DEFAULT NULL,
    close_reason VARCHAR(255) DEFAULT NULL,
    FOREIGN KEY (target_id) REFERENCES person(id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
