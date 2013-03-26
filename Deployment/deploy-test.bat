@echo off
setlocal 

@rem ---------------------------------------------------------------------------------
@rem Variables
@rem ---------------------------------------------------------------------------------

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
	set m_PathToPackage="_ArgValue"
	goto :ArgumentOK
)
if /I "%_ArgFlag%" == "/S:" (
	set m_PathToParamsFile="_ArgValue"
	goto :ArgumentOK
)
if /I "%_ArgFlag%" == "/U:" (
	set m_UserName="_ArgValue"
	goto :ArgumentOK
)
if /I "%_ArgFlag%" == "/P:" (
	set m_PassWord="_ArgValue"
	goto :ArgumentOK
)
if /I "%_ArgFlag%" == "/1:" (
	set m_PrimaryServer="_ArgValue"
	goto :ArgumentOK
)
if /I "%_ArgFlag%" == "/2:" (
	set m_SecondaryServer="_ArgValue"
	goto :ArgumentOK
)



:ArgumentOK
shift
goto :ParseArguments


:Start
if /I "%m_MSDeployPath%" == "msdeploy.exe" (
	set m_MSDeployCommandLine=%m_MSDeployPath%
) else (
	set m_MSDeployCommandLine="%m_MSDeployPath%msdeploy.exe"
)

@rem try to call msdeploy.exe just to be safe
call %m_MSDeployCommandLine% > NUL 2> NUL 
if errorlevel 1 (
	echo. msdeploy.exe is not found on this machine. Please install Web Deploy before execute the script. 
	echo. Please visit http://go.microsoft.com/?linkid=9278654
	goto :Usage
)
echo. Sync from package to server A
call %m_MSDeployCommandLine% -verb:sync -source:package="%m_PathToPackage%" -dest:auto,computerName=%m_PrimaryServer%,userName=%m_UserName%,password=%m_PassWord%,authType=Basic -disableLink:AppPoolExtension -disableLink:ContentExtension -disableLink:CertificateExtension -allowUntrusted -setParamFile:"%m_PathToParamsFile%"

echo. OK
goto :Finish

@rem ---------------------------------------------------------------------------------
@rem Usage
@rem ---------------------------------------------------------------------------------
:Usage
echo =========================================================
echo Usage:%~nx0 [/M:MSDeployPath] [/Z:WebDeployPackage] [/1:PrimaryServer] [/U:Username] [/P:Password] [/S:SetParamsFile]
echo Required flags:
echo /1:  MSDeploy destination name of remote computer or proxy-URL. (Primary Server)
echo /U:  MSDeploy destination user name. 
echo /P:  MSDeploy destination password.
echo /S:  Path to SetParamsFile
echo /Z:  Path to web deploy package (zip)
echo Optional flags:
echo /M:  Path to msdeploy.exe
echo =========================================================
goto :EOF



:Finish
goto :EOF

