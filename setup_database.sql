USE Video_Monitoring;

CREATE TABLE servers(
                        serverid BINARY(36) PRIMARY KEY,
                        servername VARCHAR(255) NOT NULL,
                        ipaddress VARCHAR(15) NOT NULL,
                        port INTEGER NOT NULL
);
CREATE TABLE videos(
                       videoid BINARY(36) PRIMARY KEY,
                       serverid BINARY(36) REFERENCES servers(serverid),
                       description TEXT NOT NULL,
                       timestamp BIGINT NOT NULL
);
