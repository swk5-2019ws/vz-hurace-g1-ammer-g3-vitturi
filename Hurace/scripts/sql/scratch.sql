CREATE TABLE IF NOT EXISTS races
(
    id                INTEGER PRIMARY KEY,
    date              TEXT    NOT NULL,
    description       TEXT,
    name              TEXT    NOT NULL,
    number_of_sensors INTEGER NOT NULL,
    website           TEXT,
    gender_id         TEXT,
    competition_id    INTEGER,
    FOREIGN KEY (gender_id) REFERENCES genders (id),
    FOREIGN KEY (competition_id) REFERENCES competitions (id),
    CHECK (number_of_sensors > 0)
);

CREATE TABLE IF NOT EXISTS skiers
(
    id            INTEGER PRIMARY KEY,
    archived      INTEGER DEFAULT 0,
    birthdate     TEXT NOT NULL,
    first_name    TEXT NOT NULL,
    last_name     TEXT NOT NULL,
    profile_image BLOB,
    national_code TEXT,
    gender_id     TEXT,
    FOREIGN KEY (national_code) REFERENCES countries (national_code),
    FOREIGN KEY (gender_id) REFERENCES genders (id),
    CHECK ( archived = 0 OR archived = 1 )
);

CREATE TABLE IF NOT EXISTS start_lists
(
    id       INTEGER PRIMARY KEY,
    number   INTEGER,
    skier_id INTEGER,
    race_id  INTEGER,
    run_id   INTEGER,
    FOREIGN KEY (skier_id) REFERENCES skiers (id),
    FOREIGN KEY (race_id) REFERENCES races (id),
    FOREIGN KEY (run_id) REFERENCES runs (id),
    CHECK (number > 0)
);

CREATE TABLE IF NOT EXISTS race_data
(
    id             INTEGER PRIMARY KEY,
    position       INTEGER,
    run_id         INTEGER,
    race_status_id INTEGER,
    FOREIGN KEY (run_id) REFERENCES runs (id),
    FOREIGN KEY (race_status_id) REFERENCES race_status (id),
    CHECK (position > 0)
);

CREATE TABLE IF NOT EXISTS sensor_data
(
    id           INTEGER PRIMARY KEY,
    sensor_id    INTEGER NOT NULL,
    time         TEXT    NOT NULL,
    race_data_id INTEGER NOT NULL,
    FOREIGN KEY (race_data_id) REFERENCES race_data (id),
    CHECK (sensor_id >= 0)
);

CREATE TABLE IF NOT EXISTS locations
(
    id            INTEGER PRIMARY KEY,
    name          TEXT UNIQUE NOT NULL,
    national_code INTEGER,
    FOREIGN KEY (national_code) REFERENCES countries (national_code)
);

CREATE TABLE IF NOT EXISTS competitions
(
    id           INTEGER PRIMARY KEY,
    location_id  INTEGER,
    race_type_id INTEGER,
    FOREIGN KEY (location_id) REFERENCES locations (id),
    FOREIGN KEY (race_type_id) REFERENCES race_types (id)
);

CREATE TABLE IF NOT EXISTS countries
(
    national_code TEXT PRIMARY KEY NOT NULL,
    CHECK (LENGTH(national_code) = 3)
);

CREATE TABLE IF NOT EXISTS race_types
(
    id TEXT PRIMARY KEY NOT NULL
);

CREATE TABLE IF NOT EXISTS genders
(
    id TEXT PRIMARY KEY NOT NULL
);

CREATE TABLE IF NOT EXISTS runs
(
    id INTEGER PRIMARY KEY NOT NULL
);

CREATE TABLE IF NOT EXISTS race_status
(
    id TEXT PRIMARY KEY NOT NULL
);