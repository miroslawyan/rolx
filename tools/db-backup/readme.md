# Producation

For connections to the production database, a certificate is required. To fetch it, run:

```powershell
> wget https://www.digicert.com/CACerts/BaltimoreCyberTrustRoot.crt.pem -OutFile BaltimoreCyberTrustRoot.crt.pem
```

Dump the database:

```powershell
> mysqldump `
    -u theadmin `
    -p `
    --host=rolx-database.mariadb.database.azure.com `
    --ssl-ca=BaltimoreCyberTrustRoot.crt.pem `
    --single-transaction `
    --extended-insert `
    --result-file=rolx_production.sql `
    rolx_production
```

# Development

## Backup

```powershell
> mysqldump `
    -u root `
    -p `
    --single-transaction `
    --extended-insert `
    --result-file=db_rolx.sql `
    db_rolx
```

## Drop and recreate database

```powershell
> mysql -u root -p -e "drop database db_rolx; create database db_rolx character set utf8mb4 collate utf8mb4_unicode_ci;"
```

## Restore

When restoring dumps from production, they have to be sanitized before:

```powershell
> python sanitize4dev.py rolx_production.sql rolx_production_4_dev.sql
```


```cmd
> mysql -u root -p db_rolx < rolx_production_4_dev.sql
```
