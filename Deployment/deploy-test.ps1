param 
(
    [string]$PathToPackage = $(throw '- Need path to package'),
	[string]$PathToParamsFile = $(throw '- Need path to parameters file'),
	[string]$PathToPublishSettingsFile = $(throw '- Need path to PublishSettings file')
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
	
	Write-Host " - Loading web deploy parameters '$PathToParamsFile..." -NoNewline
	$ParamsFile = Get-WDParameters -FilePath $PathToParamsFile -ErrorAction:SilentlyContinue -ErrorVariable e | Out-Null
	
	if($? -eq $false) {
		throw " - Get-WDParameters failed: $e"
	}
	Write-Host "OK" -ForegroundColor Green
	
	return $ParamsFile
}

function Load-PublishSettings {
	
	Write-Host " - Loading web deploy parameters '$PathToPublishSettingsFile..." -NoNewline
	$PublishSettings = Get-WDPublishSettings -FileName $PathToPublishSettingsFile -ErrorAction:SilentlyContinue -ErrorVariable e | Out-Null
	
	if($? -eq $false) {
		throw " - Get-WDPublishSettings failed: $e"
	}
	Write-Host "OK" -ForegroundColor Green
	
	return $PublishSettings
}

function Deploy-WebPackage {
	#$ParamsFile = Get-WDParameters -FilePath "$dp0\test.example.com.xml"
	#$PublishSettingsFile = Get-WDPublishSettings -FileName "$dp0\build01_virjole-wfe1.publishsettings"
	
	Write-Host " - Syncing package to server..." -NoNewline
	Restore-WDPackage -ErrorAction:SilentlyContinue -ErrorVariable e `
		-Package $PathToPackage `
		-Parameters Load-Parameters `
		-DestinationPublishSettings Load-PublishSettings | Out-Null
	
	if($? -eq $false) {
		throw " - Restore-WDPackage failed: $e"
	}
	
	Write-Host "OK" -ForegroundColor Green
}

Ensure-WDPowerShellMode
Deploy-WebPackage