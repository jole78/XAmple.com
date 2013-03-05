param 
(
    [string]$PackagePath = $(throw '- Need parameter package file path')
)
Add-PSSnapin WDeploySnapin3.0
$0 = $MyInvocation.MyCommand.Definition
$dp0 = [System.IO.Path]::GetDirectoryName($0)

$params = Get-WDParameters -FilePath "$dp0\test.example.com.xml"

Restore-WDSite -Parameters $params -Package $PackagePath -DestinationPublishSettings "$dp0\wfe1.publishsettings"

Write-Host "site deployed"