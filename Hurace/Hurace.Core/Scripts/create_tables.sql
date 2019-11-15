CREATE TABLE IF NOT EXISTS country (
    code TEXT PRIMARY KEY NOT NULL,
    CHECK (LENGTH(code) = 3)
);

CREATE TABLE IF NOT EXISTS location (
    id           INTEGER PRIMARY KEY,
    name         TEXT    NOT NULL,
    country_code INTEGER REFERENCES country (code)
);

CREATE TABLE IF NOT EXISTS race_type (
    name      TEXT    PRIMARY KEY NOT NULL,
    run_count INTEGER NOT NULL
);

CREATE TABLE IF NOT EXISTS race (
    id                INTEGER PRIMARY KEY,
    date              TEXT    NOT NULL,
    name              TEXT    NOT NULL,
    description       TEXT,
    gender            TEXT    NOT NULL,
    number_of_sensors INTEGER NOT NULL,
    race_type_name    TEXT    REFERENCES race_type (name),
    website           TEXT,
    location_id       INTEGER REFERENCES location (id),
    CHECK (gender IN ('Male', 'Female')),
    CHECK (number_of_sensors > 0)
);

CREATE TABLE IF NOT EXISTS skier (
    id           INTEGER PRIMARY KEY,
    first_name   TEXT    NOT NULL,
    last_name    TEXT    NOT NULL,
    birthdate    TEXT    NOT NULL,
    picture_url  TEXT,
    archived     INTEGER DEFAULT 0,
    country_code TEXT    REFERENCES country (code),
    gender       TEXT    NOT NULL,
    CHECK (gender IN ('Male', 'Female')),
    CHECK (archived IN (0, 1))
);

CREATE TABLE IF NOT EXISTS skier_run (
    skier_id   INTEGER REFERENCES skier (id),
    race_id    INTEGER REFERENCES race (id),
    run_number INTEGER,
    PRIMARY KEY (skier_id, race_id, run_number),
    CHECK (run_number IN (1, 2))
);

CREATE TABLE IF NOT EXISTS start_list (
    id         INTEGER PRIMARY KEY,
    skier_id   INTEGER,
    race_id    INTEGER,
    run_number INTEGER,
    number     INTEGER,
    FOREIGN KEY (skier_id, race_id, run_number) REFERENCES skier_run (skier_id, race_id, run_number),
    CHECK (number > 0)
);

CREATE TABLE IF NOT EXISTS race_data (
    id          INTEGER PRIMARY KEY,
    race_status TEXT,
    time        REAL,
    race_id     INTEGER,
    skier_id    INTEGER,
    run_number  INTEGER,
    FOREIGN KEY (skier_id, race_id, run_number) REFERENCES skier_run (skier_id, race_id, run_number),
    CHECK (race_status IN ('Completed', 'InProgress', 'Unfinished', 'Disqualified'))
);

CREATE TABLE IF NOT EXISTS sensor_measurement (
    id           INTEGER PRIMARY KEY,
    sensor_id    INTEGER NOT NULL,
    interim_time REAL    NOT NULL,
    race_data_id INTEGER REFERENCES race_data (id),
    CHECK (sensor_id >= 0),
    CHECK (interim_time >= 0)
);
