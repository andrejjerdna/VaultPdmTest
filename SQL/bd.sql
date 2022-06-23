create table users
(
    id bigserial primary key,
    Name varchar not null,
    Password varchar not null,
    Email varchar not null
);