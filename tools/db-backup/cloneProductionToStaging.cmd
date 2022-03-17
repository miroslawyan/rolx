@echo off

set DB_HOST=rolx-database.mariadb.database.azure.com
set DB_USER=theadmin
set CA_FILE=BaltimoreCyberTrustRoot.crt.pem

echo Working on %DB_HOST% with user %DB_USER%
set "psCommand=powershell -Command "$pword = read-host 'Enter password' -AsSecureString ; ^
    $BSTR=[System.Runtime.InteropServices.Marshal]::SecureStringToBSTR($pword); ^
    [System.Runtime.InteropServices.Marshal]::PtrToStringAuto($BSTR)""
for /f "usebackq delims=" %%p in (`%psCommand%`) do set MYSQL_PWD=%%p

if not exist %CA_FILE% (
    echo - fetching certificate
    powershell -Command "wget https://www.digicert.com/CACerts/BaltimoreCyberTrustRoot.crt.pem -OutFile %CA_FILE%"
)

exit

echo - dumping production
mysqldump ^
    -u %DB_USER% ^
    --host=%DB_HOST% ^
    --ssl-ca=%CA_FILE% ^
    --single-transaction ^
    --extended-insert ^
    --result-file=rolx_production.sql ^
    rolx_production

echo - clearing staging
mysql ^
    -u %DB_USER% ^
    --host=%DB_HOST% ^
    --ssl-ca=%CA_FILE% ^
    -e "drop database rolx_staging; create database rolx_staging character set utf8mb4 collate utf8mb4_unicode_ci;"

echo - restoring staging
mysql ^
    -u %DB_USER% ^
    --host=%DB_HOST% ^
    --ssl-ca=%CA_FILE% ^
    rolx_staging < rolx_production.sql
