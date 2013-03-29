param 
(
    [string]$PathToPackage = $(throw '- Need path to package'),
	[string]$PathToParamsFile = $(throw '- Need path to parameters file'),
	[string]$SitePath = $(throw '- Need IIS site name'),
	[string]$PathToPrimaryServerPublishSettingsFile = $(throw '- Need path to .PublishSettings file for primary WFE server'),
	[string]$PathToSecondaryServerPublishSettingsFile = $(throw '- Need path to .PublishSettings file for secondary WFE server')
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

function Load-Parameters {
	
	Write-Host " - Loading web deploy parameters '$PathToParamsFile'..." -NoNewline
	$WDParameters = Get-WDParameters -FilePath $PathToParamsFile -ErrorAction:SilentlyContinue -ErrorVariable e
	
	if($? -eq $false) {
		throw " - Get-WDParameters failed: $e"
	}
	Write-Host "OK" -ForegroundColor Green
	
	return $WDParameters
}

function Deploy-WebPackage {
	
	$WDParameters = Load-Parameters	

	try {
		Write-Host " - Syncing package '$PathToPackage' with parameter file '$PathToParamsFile'..." -NoNewline
		$Result = Restore-WDPackage -ErrorAction:Stop `
			-Package $PathToPackage `
			-Parameters $WDParameters `
			-DestinationPublishSettings $PathToPrimaryServerPublishSettingsFile
		} catch {
			$exception = $_.Exception
			Write-Host "ERROR" -ForegroundColor:Red
			throw " - Restore-WDPackage failed: $exception"
		}		
	
	Write-Host "OK" -ForegroundColor Green
	Write-Host "Summary:"
	$Result | Out-String
}

function Sync-Servers {
	try {
		Write-Host " - Syncing servers..." -NoNewline
		$Result = Sync-WDSite -ErrorAction:Stop `
			-SourcePublishSettings $PathToPrimaryServerPublishSettingsFile `
			-DestinationPublishSettings $PathToSecondaryServerPublishSettingsFile `
			-SourceSite $SitePath -DestinationSite $SitePath `
			-IncludeAppPool:$false
			
	} catch {
		$exception = $_.Exception
		Write-Host "ERROR" -ForegroundColor:Red
		throw " - Restore-WDPackage failed: $exception"
	}
	
	Write-Host "OK" -ForegroundColor Green
	Write-Host "Summary:"
	$Result | Out-String
}


try {
	Ensure-WDPowerShellMode
	Deploy-WebPackage
	Sync-Servers
} catch {
	Write-Error $_.Exception
	exit 1
}