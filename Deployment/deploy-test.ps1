param 
(
    [string]$PackagePath = $(throw '- Need parameter package file path')
)
Add-PSSnapin WDeploySnapin3.0
$0 = $MyInvocation.MyCommand.Definition
$dp0 = [System.IO.Path]::GetDirectoryName($0)

$params = Get-WDParameters -FilePath "$dp0\test.example.com.xml"
Restore-WDPackage -Package $PackagePath -Parameters $params -DestinationPublishSettings "$dp0\build01_virjole-wfe1.publishsettings"

Write-Host "site deployed"