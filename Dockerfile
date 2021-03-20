# MySQL image
FROM mysql:latest
# Adding SQL scripts
COPY setup_database.sql /docker-entrypoint-initdb.d/