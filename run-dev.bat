@ECHO OFF

@REM set variables
set DB_SERVER=localhost
set DB_PORT=5432
set DB_USER=postgres
set DB_PASSWORD=postgres
set DB_DATABASE=postgres

@ECHO BaseNet.Api starting...
cd BaseNet.Api

dotnet watch run --no-launch-profile

cd ..
