param 
(
    [string]$PackagePath = $(throw '- Need parameter package file path')
)

$0 = $MyInvocation.MyCommand.Definition
$dp0 = [System.IO.Path]::GetDirectoryName($0)


function Ensure-WDPowerShellMode {
	$WDPowerShellSnapin = Get-PSSnapin -Name WDeploySnapin3.0 -ErrorAction:SilentlyContinue
	
	if( $WDPowerShellSnapin -eq $null) {
		
		Write-Host " - Adding 'Web Deploy 3.0' to console..." -NoNewline
		Add-PsSnapin -Name WDeploySnapin3.0 -ErrorAction:SilentlyContinue -ErrorVariable e | Out-Null
		
		if($? -eq $false) {
			throw " - failed to load the Web Deploy 3.0 PowerShell snap-in: $e"
		} else {
			Write-Host "OK" -ForegroundColor Green
		}
	} else {
		
		Write-Host " - 'Web Deploy 3.0' already added to console"
	}
}

function Deploy-WebPackage {
	$params = Get-WDParameters -FilePath "$dp0\test.example.com.xml"
	Restore-WDPackage -Package $PackagePath -Parameters $params -DestinationPublishSettings "$dp0\build01_virjole-wfe1.publishsettings"
}

Ensure-WDPowerShellMode
Deploy-WebPackage