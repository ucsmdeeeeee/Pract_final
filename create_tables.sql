CREATE TABLE logs (
ID_logs serial NOT NULL CONSTRAINT PK_logs PRIMARY KEY,
ip VARCHAR NOT NULL,
date_time_tz timestamp with time zone NOT NULL,
requests VARCHAR NOT NULL,
kod_status int NOT NULL
);
CREATE TABLE users (
ID_users serial NOT NULL CONSTRAINT PK_users PRIMARY KEY,
users_name VARCHAR NOT NULL,
users_password VARCHAR NOT NULL
);
