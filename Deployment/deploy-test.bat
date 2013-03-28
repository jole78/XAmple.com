@echo off
setlocal 

@rem ---------------------------------------------------------------------------------
@rem Variables
@rem ---------------------------------------------------------------------------------
set m_PathToPackage=""
set m_PathToParamsFile=""
set m_UserName=""
set m_PassWord=""
set m_PrimaryServer=""
set m_SecondaryServer=""
set m_ArgsValid=true

@rem ---------------------------------------------------------------------------------
@rem Locating MSDeploy
@rem Test if msdeploy.exe was added to PATH or find it by looking in the registry
@rem ---------------------------------------------------------------------------------
call msdeploy.exe > NUL 2> NUL 
if errorlevel 1 (
	for /F "usebackq tokens=1,2,*" %%h  in (`reg query "HKLM\SOFTWARE\Microsoft\IIS Extensions\MSDeploy" /s  ^| findstr -i "InstallPath"`) do (
		if /I "%%h" == "InstallPath" ( 
			if /I "%%i" == "REG_SZ" ( 
				if not "%%j" == "" ( 
					if "%%~dpj" == "%%j" (
						set m_MSDeployPath=%%j
)))))
) else (
	set m_MSDeployPath=msdeploy.exe
)

@rem ---------------------------------------------------------------------------------
@rem Parse Arguments
@rem ---------------------------------------------------------------------------------

:ParseArguments
set _ArgCurrent=%~1
set _ArgFlagFirst=%_ArgCurrent:~0,1%
set _ArgFlag=%_ArgCurrent:~0,3%
set _ArgValue=%_ArgCurrent:~3%

if /I "%_ArgFlag%" == "" goto :Start
if /I "%_ArgFlag%" == "~0,3" goto :Start
if /I "%_ArgFlag%" == "/M:" (
	set m_MSDeployPath="%_ArgValue%"
	goto :ArgumentOK
)
if /I "%_ArgFlag%" == "/Z:" (
	set m_PathToPackage="%_ArgValue%"
	goto :ArgumentOK
)
if /I "%_ArgFlag%" == "/S:" (
	set m_PathToParamsFile="%_ArgValue%"
	goto :ArgumentOK
)
if /I "%_ArgFlag%" == "/U:" (
	set m_UserName="%_ArgValue%"
	goto :ArgumentOK
)
if /I "%_ArgFlag%" == "/P:" (
	set m_PassWord="%_ArgValue%"
	goto :ArgumentOK
)
if /I "%_ArgFlag%" == "/1:" (
	set m_PrimaryServer="%_ArgValue%"
	goto :ArgumentOK
)
if /I "%_ArgFlag%" == "/2:" (
	set m_SecondaryServer="%_ArgValue%"
	goto :ArgumentOK
)

:ArgumentOK
shift
goto :ParseArguments

:Start
call :ValidateMSDeployPath
call :ValidateArguments
echo %m_MSDeployPath%
if /I "%m_ArgsValid%" NEQ "false" (
	echo Syncing from package to primary server
	call %m_MSDeployCommandLine% -verb:sync -source:package="%m_PathToPackage%" -dest:auto,computerName=%m_PrimaryServer%,userName=%m_UserName%,password=%m_PassWord%,authType=Basic -disableLink:AppPoolExtension -disableLink:ContentExtension -disableLink:CertificateExtension -allowUntrusted -setParamFile:"%m_PathToParamsFile%"
	goto :Finish
) else (
	set m_ErrorMessage=required argument values for %m_InvalidArg%
	call :ERROR
	goto :Usage
)

:ERROR
echo ERROR - %m_ErrorMessage%
goto :EOF

:ValidateMSDeployPath
echo Validating path to msdeploy.exe
if /I "%m_MSDeployPath%" == "msdeploy.exe" (
	set m_MSDeployCommandLine=%m_MSDeployPath%
) else (
	echo 100
	echo "%m_MSDeployPath%msdeploy.exe"
	set m_MSDeployCommandLine="%m_MSDeployPath%msdeploy.exe"
)
goto :EOF

:ValidateArguments
echo Validating arguments:
set m_ArgsValid=true
if %m_PathToPackage% == "" (
	set m_ArgsValid=false
	set m_InvalidArg=Z
)
if %m_PathToParamsFile% == "" (
	set m_ArgsValid=false
	set m_InvalidArg=%m_InvalidArg%,S
)
if %m_UserName% == "" (
	set m_ArgsValid=false
	set m_InvalidArg=%m_InvalidArg%,U
)
if %m_PassWord% == "" (
	set m_ArgsValid=false
	set m_InvalidArg=%m_InvalidArg%,P
)
if %m_PrimaryServer% == "" (
	set m_ArgsValid=false
	set m_InvalidArg=%m_InvalidArg%,1
)
if %m_SecondaryServer% == "" (
	set m_ArgsValid=false
	set m_InvalidArg=%m_InvalidArg%,2
)
if m_ArgsValid == "true" (
 	echo OK 
)
goto :EOF

@rem ---------------------------------------------------------------------------------
@rem Usage
@rem ---------------------------------------------------------------------------------
:Usage
echo =========================================================
echo Usage:%~nx0 [/1:PrimaryServer] [/2:SecondaryServer] [/Z:WebDeployPackage] [/U:Username] [/P:Password] [/S:SetParamsFile] [/M:MSDeployPath]
echo Required:
echo /1:  MSDeploy destination name of remote computer or proxy-URL. (Primary Server)
echo /2:  MSDeploy destination name of remote computer or proxy-URL. (Secondary Server)
echo /Z:  Path to web deploy package (zip)
echo /U:  MSDeploy destination user name. 
echo /P:  MSDeploy destination password.
echo /S:  Path to SetParamsFile
echo Optional:
echo /M:  Path to msdeploy.exe
echo =========================================================
goto :EOF



:Finish
echo. OK
goto :EOF

