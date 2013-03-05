Add-PSSnapin WDeploySnapin3.0

$0 = $MyInvocation.MyCommand.Definition
$dp0 = [System.IO.Path]::GetDirectoryName($0)

$params = Get-WDParameters -FilePath "$dp0\test.example.com.xml"

Restore-WDSite -Parameters $params -Package "$dp0\Example.Web.zip" -DestinationPublishSettings "$dp0\wfe1.publishsettings"

Write-Host "site deployed"