@echo off
setlocal
set "INSTALL_DIR=%LOCALAPPDATA%\ScriptureApp"
if not exist "%INSTALL_DIR%" mkdir "%INSTALL_DIR%"
powershell.exe -NoProfile -ExecutionPolicy Bypass -Command "Expand-Archive -LiteralPath '%~dp0ScriptureApp_Build.zip' -DestinationPath '%INSTALL_DIR%' -Force"
powershell.exe -NoProfile -ExecutionPolicy Bypass -Command "$s=(New-Object -ComObject WScript.Shell).CreateShortcut([Environment]::GetFolderPath('Desktop') + '\Scripture App.lnk'); $s.TargetPath='%INSTALL_DIR%\WinIOSLinux\ScriptureApp.exe'; $s.WorkingDirectory='%INSTALL_DIR%\WinIOSLinux'; $s.Save()"
start "" "%INSTALL_DIR%\WinIOSLinux\ScriptureApp.exe"
exit /b 0
