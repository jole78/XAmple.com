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
set m_SitePath=""
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
	set m_MSDeployPath=""
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
if /I "%_ArgFlag%" == "/I:" (
	set m_SitePath="%_ArgValue%"
	goto :ArgumentOK
)


:ArgumentOK
shift
goto :ParseArguments

:Start
call :ValidateArguments
if /I "%m_ArgsValid%" NEQ "false" (
	set m_PrimaryServerArgs=computerName=%m_PrimaryServer%,userName=%m_UserName%,password=%m_PassWord%,authType=Basic
	set m_SecondaryServerArgs=computerName=%m_SecondaryServer%,userName=%m_UserName%,password=%m_PassWord%,authType=Basic

	echo Syncing from package to primary server...	
	call "%m_MSDeployPath%msdeploy.exe" -verb:sync -source:package="%m_PathToPackage%" -dest:auto,computerName=%m_PrimaryServer%,userName=%m_UserName%,password=%m_PassWord%,authType=Basic -disableLink:AppPoolExtension -disableLink:ContentExtension -disableLink:CertificateExtension -allowUntrusted -setParamFile:"%m_PathToParamsFile%"
	
	@rem echo Syncing from primary server to secondary server...
	@rem call "%m_MSDeployPath%msdeploy.exe" -verb:sync -source:contentPath=%m_SitePath%,%m_PrimaryServerArgs% -dest:auto,%m_SecondaryServerArgs% -allowUntrusted

	goto :Finish
) else (
	set m_ErrorMessage=required argument values for %m_InvalidArg%
	call :Usage
	goto :ERROR
)

:ERROR
echo ERROR - %m_ErrorMessage%
exit 1

:ValidateArguments
echo Validating arguments:
set m_ArgsValid=true
if %m_PathToPackage% == "" (
	set m_ArgsValid=false
	set m_InvalidArg=Z
)
if %m_PathToParamsFile% == "" (
	set m_ArgsValid=false
	set m_InvalidArg=%m_InvalidArg% S
)
if %m_UserName% == "" (
	set m_ArgsValid=false
	set m_InvalidArg=%m_InvalidArg% U
)
if %m_PassWord% == "" (
	set m_ArgsValid=false
	set m_InvalidArg=%m_InvalidArg% P
)
if %m_PrimaryServer% == "" (
	set m_ArgsValid=false
	set m_InvalidArg=%m_InvalidArg% 1
)
if %m_SecondaryServer% == "" (
	set m_ArgsValid=false
	set m_InvalidArg=%m_InvalidArg% 2
)
if %m_SitePath% == "" (
	set m_ArgsValid=false
	set m_InvalidArg=%m_InvalidArg% I
)
if /i "%m_ArgsValid%" == "true" (
 	echo OK 
)
goto :EOF

@rem ---------------------------------------------------------------------------------
@rem Usage
@rem ---------------------------------------------------------------------------------
:Usage
echo =========================================================
echo Usage:%~nx0 [/1:PrimaryServer] [/2:SecondaryServer] [/Z:WebDeployPackage] [/U:Username] [/P:Password] [/S:SetParamsFile] [/I:SitePath] [/M:MSDeployPath]
echo Required:
echo /1:  MSDeploy destination name of remote computer or proxy-URL. (Primary Server)
echo /2:  MSDeploy destination name of remote computer or proxy-URL. (Secondary Server)
echo /Z:  Path to web deploy package (zip)
echo /U:  MSDeploy destination user name. 
echo /P:  MSDeploy destination password.
echo /S:  Path to SetParamsFile (xml parameters file)
echo /I:  IIS site name (e.g. test.example.com)
echo Optional:
echo /M:  Path to msdeploy.exe
echo =========================================================
goto :EOF



:Finish
echo OK
goto :EOF

