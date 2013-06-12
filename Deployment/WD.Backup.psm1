function Invoke-Backup {
	param([string]$site = $(throw '- Need IIS site name'))
	
	try {
		EnsureWDPowerShellMode
		
		Write-Host " - Executing a backup of site '$site'..." -NoNewline
		$parameters = BuildParameters $site 
		$result = Backup-WDApp @parameters -ErrorAction:Stop
		AfterBackup -Path $result.Package
			
	} catch {
		Write-Error $_.Exception
		exit 1
	}
	
	Write-Host "OK"
	Write-Host "Summary:"
	$result | Out-String	
}

function Set-Properties {
	param(
		[HashTable]$properties
	)

	foreach ($key in $properties.keys) {
		$cfg[$key] = $properties.$key
    }
}

function AfterBackup([string] $path) {
	if($cfg.PublishArtifacts) {
		Write-Host "##teamcity[publishArtifacts '$path']"
	}
}

function EnsureWDPowerShellMode {
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

function BuildParameters {
	param([string]$name)
	
	$parameters = @{
		Application = $name
	}
	
	if($cfg.PathToSourcePublishSettingsFile) {
		$parameters.SourcePublishSettings = $cfg.PathToSourcePublishSettingsFile
	}
	
	if($cfg.PathToBackupLocation) {
		$parameters.Output = $cfg.PathToBackupLocation
	}
	
	return $parameters

}



# default values
# override by Set-Properties @{Key=Value} outside of this script
$cfg = @{
	PathToSourcePublishSettingsFile = $null
	PathToBackupLocation = (Get-Location).Path + "\Backups"
	PublishArtifacts = if($Env:TEAMCITY_DATA_PATH){$true} else {$false}
}

Export-ModuleMember -Function Invoke-Backup, Set-Properties
