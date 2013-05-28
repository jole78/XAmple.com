param (
	$NUnitConsolePath,
	$dll,
	$SpecFlowExePath,
	$csproj
)


#"C:\Program Files (x86)\NUnit 2.5.10\bin\net-2.0\nunit-console.exe" /labels /out=TestResult.txt /xml=TestResult.xml /framework=net-4.0 Bowling.Specflow\bin\Debug\Bowling.Specflow.dll
# 
#"C:\Program Files (x86)\TechTalk\SpecFlow\specflow.exe" nunitexecutionreport Bowling.Specflow\Bowling.SpecFlow.csproj
# 
#IF NOT EXIST TestResult.xml GOTO FAIL
#IF NOT EXIST TestResult.html GOTO FAIL
#EXIT
# 
#:FAIL
#echo ##teamcity[buildStatus status='FAILURE']
#EXIT /B 1

#specflow.exe nunitexecutionreport BookShop.AcceptanceTests.csproj /out:MyResult.html

function Execute-NUnit {
	$exe = $NUnitConsolePath
	$parameters = @()
	
	$parameters += "$dll"
	$parameters += "/labels"
	$parameters += "/out=TestResult.txt"
	$parameters += "/xml=TestResult.xml"
	
	&$exe $parameters
}

function Execute-SpecFlow {
	cls
	$exe = $SpecFlowExePath
	
	$parameters = @()
	
	$parameters += "nunitexecutionreport"
	$parameters += $csproj
	$parameters += "/out:SpecFlowResults.html"
	
	&$exe $parameters
}

cls
Execute-NUnit
Execute-SpecFlow