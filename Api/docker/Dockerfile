FROM postgres:15

ENV POSTGRES_USER=defaultuser
ENV POSTGRES_PASSWORD=root
ENV POSTGRES_DB=database

COPY init.sql /docker-entrypoint-initdb.d/

EXPOSE 5433
