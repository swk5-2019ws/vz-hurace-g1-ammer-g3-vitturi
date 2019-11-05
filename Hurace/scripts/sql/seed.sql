INSERT OR
REPLACE
INTO countries (national_code)
VALUES ('AND'),
       ('AUT'),
       ('BEL'),
       ('BLR'),
       ('BUL'),
       ('CAN'),
       ('CHI'),
       ('CHN'),
       ('CRO'),
       ('CZE'),
       ('DEN'),
       ('ESP'),
       ('FIN'),
       ('FRA'),
       ('GBR'),
       ('GER'),
       ('ITA'),
       ('JPN'),
       ('KOR'),
       ('LIE'),
       ('MON'),
       ('NED'),
       ('NOR'),
       ('NZL'),
       ('POL'),
       ('RUS'),
       ('SLO'),
       ('SRB'),
       ('SUI'),
       ('SVK'),
       ('SWE'),
       ('USA');

INSERT OR
REPLACE
INTO runs (id)
VALUES (1),
       (2);

INSERT OR
REPLACE
INTO genders (id)
VALUES ('m'),
       ('f');

INSERT OR
REPLACE
INTO race_types (id)
VALUES ('Downhill'),
       ('GiantSlalom'),
       ('Slalom'),
       ('SuperG');

INSERT OR
REPLACE
INTO locations (id, name, national_code)
VALUES (1, 'Adelboden', 'SUI'),
       (2, 'Alta Badia', 'ITA'),
       (3, 'Bansko', 'BUL'),
       (4, 'Beaver Creek', 'USA'),
       (5, 'Bormio', 'ITA'),
       (6, 'Chamonix', 'FRA'),
       (7, 'Cortina d''Ampezzo', 'ITA'),
       (8, 'Courchevel', 'FRA'),
       (9, 'Crans-Montana', 'SUI'),
       (10, 'Flachau', 'AUT'),
       (11, 'Garmisch-Partenkirchen', 'GER'),
       (12, 'Hinterstoder', 'AUT'),
       (13, 'Killington', 'USA'),
       (14, 'Kitzbühel', 'AUT'),
       (15, 'Kranjska Gora', 'SLO'),
       (16, 'Kvitfjell', 'NOR'),
       (17, 'La Thuile', 'ITA'),
       (18, 'Lake Louise', 'CAN'),
       (19, 'Levi', 'FIN'),
       (20, 'Lienz', 'AUT'),
       (21, 'Madonna di Campiglio', 'ITA'),
       (22, 'Maribor', 'SLO'),
       (23, 'Ofterschwang', 'GER'),
       (24, 'Rosa Khutor', 'RUS'),
       (25, 'Schladming', 'AUT'),
       (26, 'St. Moritz', 'SUI'),
       (27, 'Sölden', 'AUT'),
       (28, 'Val d''Isère', 'FRA'),
       (29, 'Val Gardena/Groeden', 'ITA'),
       (30, 'Wengen', 'SUI'),
       (31, 'Yanqing', 'CHN'),
       (32, 'Yuzawa Naeba', 'JPN'),
       (33, 'Zagreb', 'CRO'),
       (34, 'Zauchensee', 'AUT'),
       (35, 'Åre', 'SWE');

INSERT OR
REPLACE
INTO competitions (id, location_id, race_type_id)
VALUES (1, 25, 'Slalom'), /* Schladming Slalom */
       (2, 14, 'SuperG'), /* Kitzbühel Super G */
       (3, 14, 'Downhill'), /* Kitzbbühel Downhill */
       (4, 14, 'Slalom'), /* Kitzbühel Slalom */
       (5, 30, 'Downhill'), /* Wengen Downhill */
       (6, 30, 'Slalom'), /* Wengen Slalom */
       (7, 1, 'GiantSlalom'), /* Adelboden Giant Slalom */
       (8, 1, 'Slalom'), /* Adelboden Slalom */
       (9, 10, 'Slalom'), /* Flachau Slalom */
       (10, 33, 'Slalom'), /* Zagreb Slalom */
       (11, 8, 'Slalom'), /* Courchevel Slalom */
       (12, 8, 'GiantSlalom'); /* Courchevel Giant Slalom */

INSERT OR
REPLACE
INTO races (id, date, description, name, number_of_sensors, website, gender_id, competition_id)
VALUES (1, '2019-01-26 00:00:00.000', 'The Kitzbühel Slalom', 'Kitzbühel Slalom', 5, 'hahnenkamm.com', 'm', 4),
       (2, '2019-01-29 00:00:00.000', 'The Schladming Slalom', 'Schladming Slalom', 5, 'thenightrace.at', 'm', 1),
       (3, '2019-01-20 00:00:00.000', 'The Wengen Slalom', 'Wengen Slalom', 5, NULL, 'm', 6),
       (4, '2019-01-12 00:00:00.000', 'The Adelboden Giant Slalom', 'Adelboden Giant Slalom', 5, 'weltcup-adelboden.ch', 'm', 7),
       (5, '2019-01-13 00:00:00.000', 'The Adelboden Slalom', 'Adelboden Slalom', 5, 'weltcup-adelboden.ch', 'm', 8),
       (6, '2019-01-06 00:00:00.000', 'The Zagreb Slalom', 'Zagreb Slalom', 5, 'snowqueentrophy.com', 'm', 10),
       (7, '2019-01-05 00:00:00.000', 'The Zagreb Slalom', 'Zagreb Slalom', 5, 'snowqueentrophy.com', 'w', 10),
       (8, '2019-01-08 00:00:00.000', 'The Flachau Slalom', 'Flachau Slalom', 5, 'skiweltcup-flachau.at', 'w', 9),
       (9, '2018-12-21 00:00:00.000', 'The Courchevel Giant Slalom', 'Courchevel Giant Slalom', 5, 'sportcourchevel.com', 'w', 12),
       (10, '2018-12-22 00:00:00.000', 'The Courchevel Slalom', 'Courchevel Slalom', 5, 'sportcourchevel.com', 'w', 11);