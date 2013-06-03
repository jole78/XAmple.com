

function Properties {
	param(
		[HashTable]$properties
	)
	
	foreach ($key in $properties.keys) {
		$cfg[$key] = $properties.$key
    }
}

function Invoke-TeamCitySpecFlowReport {
	param(
	 [string[]]$categories = @()
	)
	
	$args = Parse-Arguments
	
	Invoke-NUnitConsoleExe $args.PathToAssemblyOrProject $categories
	Invoke-SpecFlowExe $args.PathToProjectFile
}

function Parse-Arguments {

	$args = @{}
	
	$proj = Get-ChildItem -File "*.*proj"
	
	# Path to xxx.csproj
	if($cfg.PathToProjectFile) {
		# todo: test-path
		$args.PathToProjectFile = $cfg.PathToProjectFile
	} else {
		$args.PathToProjectFile = $proj.FullName
	}
	
	# Path to xxx.dll or xxx.csproj
	if($cfg.PathToAssemblyOrProject) {
		# todo: test-path
		$args.PathToAssemblyOrProject = $cfg.PathToAssemblyOrProject
	} else {
		# Finds the dll in the 'bin' folder under the specified configuration [default: Release]
		$args.PathToAssemblyOrProject = Get-ChildItem -File -Recurse "$($proj.BaseName).dll" | Where-Object {
			($_.Directory.Parent.Name -eq "bin") -and ($_.Directory.Name -eq $cfg.Configuration)
		} | Select-Object -First 1
	}	

	return $args	
}

function Invoke-NUnitConsoleExe {
	param (
		[Parameter(Position = 0, Mandatory = 1)][string] $assemblyOrProject,
		[Parameter(Position = 1, Mandatory = 0)][string[]] $categories
	)
	
	# Path to nunit-console.exe
	if($cfg.PathToNUnitConsoleExe) {
		$exe = $cfg.PathToNUnitConsoleExe
	} else {
		#TODO: need to find packages folder
		$exe = "..\packages\NUnit.Runners.2.6.2\tools\nunit-console.exe"
	}
	
	if(Test-Path $exe -PathType:Leaf) {
		try {
			$parameters = @()
			
			$parameters += "$assemblyOrProject"
			$parameters += "/labels"
			$parameters += "/out=TestResult.txt"
			$parameters += "/xml=TestResult.xml"
			if($categories) {
				$parameters += "/include:$($categories -join ',')"
			}
			
			&$exe $parameters | Out-Null
		} catch {
			throw "Error when executing nunit-console.exe: " + $_
		}
	} else {
		throw "Failed to find 'nunit-console.exe' at location '$exe'"
	}
}

function Invoke-SpecFlowExe {
	param (
		[Parameter(Position = 1, Mandatory = 1)][string] $projectFile
	)
	
	# Path to specflow.exe
	if($cfg.PathToSpecFlowExe) {
		$exe = $cfg.PathToSpecFlowExe
	} else {
		#TODO: need to find packages folder
		$exe = "..\packages\SpecFlow.1.9.0\tools\specflow.exe"
	}
	
	if(Test-Path $exe -PathType:Leaf) {
		try {
			
			#copy config file if it exists
			if(Test-Path .\specflow.exe.config -PathType:Leaf) {
				Copy-Item -Path .\specflow.exe.config -Destination (Get-Item $exe).Directory
			}
			# TODO: need to remove the config file after completion
		
			$parameters = @()
			
			$parameters += $cfg.SpecFlowReportType
			$parameters += $projectFile
			
			&$exe $parameters | Out-Host
		} catch {
			throw "Error when executing specflow.exe: " + $_
		} finally {
		 	$nn = "$((Get-Item $exe).Directory)\specflow.exe.config"
			Remove-Item $nn
		}
	} else {
		throw "Failed to find 'specflow.exe' at location '$exe'"
	}
}


# default values
$cfg = @{}
$cfg.Configuration = 'Release'
$cfg.SpecFlowReportType = 'nunitexecutionreport'

Export-ModuleMember -Function Invoke-TeamCitySpecFlowReport, Properties

