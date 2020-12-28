START TRANSACTION;


DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201228174137_ClientDateOfBirthWasAdded') THEN
    ALTER TABLE "Clients" ADD "DateOfBirth" timestamp without time zone NOT NULL DEFAULT TIMESTAMP '0001-01-01 00:00:00';
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20201228174137_ClientDateOfBirthWasAdded') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20201228174137_ClientDateOfBirthWasAdded', '5.0.1');
    END IF;
END $$;
COMMIT;

