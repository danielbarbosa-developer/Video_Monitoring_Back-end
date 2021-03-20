CREATE DATABASE Video_Monitoring;

\c Video_Monitoring;
CREATE TABLE server (
                        server_id varchar(36) not null,
                        server_name varchar(100) not null,
                        ip_address varchar(15) not null,
                        port integer not null
);
CREATE TABLE video (
                       video_id varchar(36) not null,
                       video_description varchar(500) not null,
                       video_content bytea not null
);