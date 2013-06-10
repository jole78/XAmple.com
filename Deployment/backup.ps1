param 
(
	[string]$ApplicationName = $(throw '- Need IIS site name'),
	[string]$PathToSourcePublishSettings,
	[string]$PathToBackupLocation
)

function AfterBackup([string] $Path) {
	if($Env:TEAMCITY_DATA_PATH) {
		Write-Host "##teamcity[publishArtifacts '$Path']"
	}
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
		Write-Host " - Executing a backup of site '$ApplicationName'..." -NoNewline
		$Parameters = Build-BackupParameters
		$Result = Backup-WDApp @Parameters -ErrorAction:Stop
		AfterBackup -Path $Result.Package
			
	} catch {
		$exception = $_.Exception
		Write-Host "ERROR" -ForegroundColor:Red
		throw " - Backup failed: $exception"
	}
	
	Write-Host "OK" -ForegroundColor Green
	Write-Host "Summary:"
	$Result | Out-String	
}

try {	
	Ensure-WDPowerShellMode
	Backup	
	
} catch {
	Write-Error $_.Exception
	exit 1
}
