CREATE TABLE IF NOT EXISTS race
(
    id                INTEGER PRIMARY KEY,
    date              TEXT    NOT NULL,
    description       TEXT,
    name              TEXT    NOT NULL,
    number_of_sensors INTEGER NOT NULL,
    website           TEXT,
    gender_name       TEXT,
    location_id    INTEGER,
    race_type_name TEXT,
    FOREIGN KEY (gender_name) REFERENCES gender (name),
    FOREIGN KEY (race_type_name) REFERENCES race_type (name)
    FOREIGN KEY (location_id) REFERENCES location (id),
    CHECK (number_of_sensors > 0)
);

CREATE TABLE IF NOT EXISTS skier
(
    id            INTEGER PRIMARY KEY,
    archived      INTEGER DEFAULT 0,
    birthdate     TEXT NOT NULL,
    first_name    TEXT NOT NULL,
    last_name     TEXT NOT NULL,
    picture_url   TEXT,
    national_code TEXT,
    gender_name     TEXT,
    FOREIGN KEY (national_code) REFERENCES country(national_code),
    FOREIGN KEY (gender_name) REFERENCES gender (name),
    CHECK ( archived = 0 OR archived = 1 )
);

CREATE TABLE IF NOT EXISTS skier_run
(
    skier_id   INTEGER,
    race_id    INTEGER,
    run_number INTEGER,
    PRIMARY KEY (skier_id, race_id, run_number),
    FOREIGN KEY (skier_id) REFERENCES skier (id),
    FOREIGN KEY (race_id) REFERENCES race (id),
    CHECK ( run_number = 1 OR run_number = 2 )
);

CREATE TABLE IF NOT EXISTS start_list
(
    id         INTEGER PRIMARY KEY,
    number     INTEGER,
    skier_id   INTEGER,
    race_id    INTEGER,
    run_number INTEGER,
    FOREIGN KEY (skier_id) REFERENCES skier (id),
    FOREIGN KEY (race_id) REFERENCES race (id),
    FOREIGN KEY (run_number) REFERENCES skier_run (run_number),
    CHECK (number > 0)
);

CREATE TABLE IF NOT EXISTS race_data
(
    id          INTEGER PRIMARY KEY,
    time        REAL,
    race_status TEXT,
    skier_id    INTEGER,
    race_id     INTEGER,
    run_number  INTEGER,
    FOREIGN KEY (skier_id) REFERENCES skier (id),
    FOREIGN KEY (race_id) REFERENCES race (id),
    FOREIGN KEY (run_number) REFERENCES skier_run (run_number),
    CHECK (
        race_status IS NULL OR
        race_status = 'DNF' OR
        race_status = 'DNQ'
    )
);

CREATE TABLE IF NOT EXISTS sensor_measurement
(
    id           INTEGER PRIMARY KEY,
    sensor_id    INTEGER NOT NULL,
    interim_time TEXT    NOT NULL,
    race_data_id INTEGER NOT NULL,
    FOREIGN KEY (race_data_id) REFERENCES race_data (id),
    CHECK (sensor_id >= 0)
);

CREATE TABLE IF NOT EXISTS location
(
    id            INTEGER PRIMARY KEY,
    name          TEXT UNIQUE NOT NULL,
    code INTEGER,
    FOREIGN KEY (code) REFERENCES country (code)
);

CREATE TABLE IF NOT EXISTS country
(
    code TEXT PRIMARY KEY NOT NULL,
    CHECK (LENGTH(code) = 3)
);

CREATE TABLE IF NOT EXISTS race_type
(
    name TEXT PRIMARY KEY NOT NULL,
    run_count INTEGER NOT NULL
);

CREATE TABLE IF NOT EXISTS gender
(
    name TEXT PRIMARY KEY NOT NULL
);