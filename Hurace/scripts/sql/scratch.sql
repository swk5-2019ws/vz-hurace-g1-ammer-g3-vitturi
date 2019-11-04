CREATE TABLE IF NOT EXISTS race
(
    id                INTEGER PRIMARY KEY,
    description       TEXT,
    end_data          TEXT    NOT NULL,
    name              TEXT    NOT NULL,
    number_of_sensors INTEGER NOT NULL,
    start_data        TEXT    NOT NULL,
    gender_id         TEXT,
    race_type_id      TEXT,
    location_id       INT,
    FOREIGN KEY (gender_id) REFERENCES gender (id),
    FOREIGN KEY (location_id) REFERENCES location (id),
    FOREIGN KEY (race_type_id) REFERENCES race_type (id),
    CHECK (number_of_sensors > 0)
);

CREATE TABLE IF NOT EXISTS skier
(
    id            INTEGER PRIMARY KEY,
    archived      INTEGER DEFAULT 0,
    birthdate     TEXT NOT NULL,
    first_name    TEXT NOT NULL,
    last_name     TEXT NOT NULL,
    profile_image BLOB,
    national_code TEXT,
    gender_id     TEXT,
    FOREIGN KEY (national_code) REFERENCES country (national_code),
    FOREIGN KEY (gender_id) REFERENCES gender (id),
    CHECK ( archived = 0 OR archived = 1 ),
    CHECK (LENGTH(first_name) = 50),
    CHECK (LENGTH(last_name) = 50)
);

CREATE TABLE IF NOT EXISTS start_list
(
    id       INTEGER PRIMARY KEY,
    number   INTEGER,
    skier_id INTEGER,
    race_id  INTEGER,
    run_id   INTEGER,
    FOREIGN KEY (skier_id) REFERENCES skier (id),
    FOREIGN KEY (race_id) REFERENCES race (id),
    FOREIGN KEY (run_id) REFERENCES run (id),
    CHECK (number > 0)
);

CREATE TABLE IF NOT EXISTS race_data
(
    id             INTEGER PRIMARY KEY,
    position       INTEGER,
    sensor_data_id INTEGER,
    run_id         INTEGER,
    FOREIGN KEY (sensor_data_id) REFERENCES sensor_data (id),
    FOREIGN KEY (run_id) REFERENCES run (id),
    CHECK (position > 0)
);

CREATE TABLE IF NOT EXISTS sensor_data
(
    id        INTEGER PRIMARY KEY,
    sensor_id INTEGER NOT NULL,
    time      TEXT    NOT NULL,
    CHECK (sensor_id >= 0)
);

CREATE TABLE IF NOT EXISTS location
(
    id            INTEGER PRIMARY KEY,
    name          TEXT NOT NULL,
    national_code INTEGER,
    FOREIGN KEY (national_code) REFERENCES country (national_code)
);

CREATE TABLE IF NOT EXISTS country
(
    national_code TEXT PRIMARY KEY NOT NULL,
    CHECK (LENGTH(national_code) = 3)
);

CREATE TABLE IF NOT EXISTS race_type
(
    id TEXT PRIMARY KEY NOT NULL
);

CREATE TABLE IF NOT EXISTS gender
(
    id TEXT PRIMARY KEY NOT NULL
);

CREATE TABLE IF NOT EXISTS run
(
    id INTEGER PRIMARY KEY NOT NULL
);

