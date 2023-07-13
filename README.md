# BillardHazard

web.config structure example :

<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <connectionStrings>
    <add name="connection" connectionString="Data Source=[YOUR_SERVER_NAME];Initial Catalog=[YOUR_DB_NAME];User ID=[USER_ID];Password=[PWD];Persist Security Info=True;" providerName="System.Data.SqlClient" />
  </connectionStrings>
  <location path="." inheritInChildApplications="false">
    <system.webServer>
      <handlers>
        <add name="aspNetCore" path="*" verb="*" modules="AspNetCoreModuleV2" resourceType="Unspecified" />
      </handlers>
      <aspNetCore processPath="dotnet" arguments=".\BillardHazard.dll" stdoutLogEnabled="false" stdoutLogFile=".\logs\stdout" hostingModel="inprocess" />
    </system.webServer>
  </location>
</configuration>
