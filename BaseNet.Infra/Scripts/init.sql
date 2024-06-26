Build started...
Build succeeded.
CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    migration_id character varying(150) NOT NULL,
    product_version character varying(32) NOT NULL,
    CONSTRAINT pk___ef_migrations_history PRIMARY KEY (migration_id)
);

START TRANSACTION;

CREATE TABLE users (
    user_id uuid NOT NULL,
    created_user text NULL,
    updated_user text NULL,
    created_date timestamp with time zone NOT NULL,
    updated_date timestamp with time zone NOT NULL,
    CONSTRAINT pk_users PRIMARY KEY (user_id)
);

INSERT INTO "__EFMigrationsHistory" (migration_id, product_version)
VALUES ('20240612025731_Init', '6.0.0');

COMMIT;


