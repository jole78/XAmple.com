param 
(
	[string]$ApplicationName = $(throw '- Need IIS site name'),
	[string]$PathToSourcePublishSettings,
	[string]$PathToBackupLocation
)

if($Env:TEAMCITY_DATA_PATH) {
	$Script:OnBeforeBackup = [System.Action] { 
		Remove-Item .\Backups -Force -Recurse
	}
} else {
	$Script:OnBeforeBackup = [System.Action] { }
}


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

function Build-BackupParameters {
	$parameters = @{
		Application = $ApplicationName
	}
	
	if($PathToSourcePublishSettings) {
		$parameters.SourcePublishSettings = $PathToSourcePublishSettings
	}
	
	if($PathToBackupLocation) {
		$parameters.Output = $PathToBackupLocation
	}
	
	return $parameters

}

function Backup {
	try {
		$OnBeforeBackup.Invoke()
		Write-Host " - Executing a backup of site '$ApplicationName'..." -NoNewline
		$Parameters = Build-BackupParameters
		$Result = Backup-WDApp @Parameters -ErrorAction:Stop
			
	} catch {
		$exception = $_.Exception
		Write-Host "ERROR" -ForegroundColor:Red
		throw " - Restore-WDPackage failed: $exception"
	}
	
	Write-Host "OK" -ForegroundColor Green
	Write-Host "Summary:"
	$Result | Out-String	
	
	# Backup Location = $Result.Package 
}

try {	
	Ensure-WDPowerShellMode
	Backup	
	
} catch {
	Write-Error $_.Exception
	exit 1
}