PRAGMA foreign_keys = ON;

CREATE TABLE IF NOT EXISTS country (
    id   INTEGER PRIMARY KEY,
    code TEXT    NOT NULL UNIQUE,
    CHECK (LENGTH(code) = 2)
);

CREATE TABLE IF NOT EXISTS location (
    id         INTEGER PRIMARY KEY,
    name       TEXT    NOT NULL,
    country_id INTEGER REFERENCES country (id)
);

CREATE TABLE IF NOT EXISTS race (
    id                INTEGER PRIMARY KEY,
    name              TEXT    NOT NULL,
    date              TEXT    NOT NULL,
    location_id       INTEGER REFERENCES location (id),
    gender            TEXT    NOT NULL,
    race_type         TEXT    NOT NULL,
    description       TEXT,
    website           TEXT,
	picture_url       TEXT,
    number_of_sensors INTEGER NOT NULL,
    status            TEXT,
    CHECK (gender IN ('Male', 'Female')),
    CHECK (race_type IN ('Slalom', 'SuperSlalom')),
    CHECK (status IN ('Ready', 'InProgress', 'Finished'))
);

CREATE TABLE IF NOT EXISTS run (
    id             INTEGER PRIMARY KEY,
    skier_id       INTEGER REFERENCES skier (id) ON DELETE CASCADE,
    race_id        INTEGER REFERENCES race  (id) ON DELETE CASCADE,
    run_number     INTEGER NOT NULL,
    start_position INTEGER,
    status         TEXT,
    total_time     REAL,
    UNIQUE (skier_id, race_id, run_number),
    CHECK (status IN ('Ready', 'Completed', 'InProgress', 'Unfinished', 'NotStarted', 'Disqualified')),
    CHECK (run_number IN (1, 2))
);

CREATE TABLE IF NOT EXISTS sensor_measurement (
    id           INTEGER PRIMARY KEY,
    sensor_id    INTEGER NOT NULL,
    interim_time REAL    NOT NULL,
    run_id       INTEGER REFERENCES run (id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS skier (
    id           INTEGER PRIMARY KEY,
    first_name   TEXT    NOT NULL,
    last_name    TEXT    NOT NULL,
    birthdate    TEXT    NOT NULL,
    gender       TEXT    NOT NULL,
    country_id   INTEGER REFERENCES country (id),
    picture_url  TEXT,
    archived     INTEGER DEFAULT 0,
    CHECK (gender IN ('Male', 'Female')),
    CHECK (archived IN (0, 1))
);
