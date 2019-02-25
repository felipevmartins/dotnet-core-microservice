\connect auth

CREATE TABLE users (
    id SERIAL PRIMARY KEY NOT NULL,
    name VARCHAR(100) NOT NULL,
    email VARCHAR(100) NOT NULL,
    password VARCHAR(250) NOT NULL
);

ALTER TABLE "users" OWNER TO docker;

Insert into users(name,email,password) values( 'docker','docker@docker.com','docker');