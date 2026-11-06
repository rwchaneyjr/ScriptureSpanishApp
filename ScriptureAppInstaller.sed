[Version]
Class=IEXPRESS
SEDVersion=3

[Options]
PackagePurpose=InstallApp
InstallPrompt=This will install Scripture App.
DisplayLicense=
FinishMessage=Scripture App is installed.
ShowInstallProgramWindow=1
HideExtractAnimation=1
UseLongFileName=1
InsideCompressed=1
Cabinet=1
Compress=1
TargetName=C:\Users\rober\ScriptureApp\ScriptureAppInstaller.exe
FriendlyName=Scripture App Installer
AppLaunched=installer.cmd
PostInstallCmd=<None>
AdminQuietInstCmd=
UserQuietInstCmd=
RebootMode=N
SourceFiles=SourceFiles0

[SourceFiles]
SourceFiles0=C:\Users\rober\ScriptureApp

[SourceFiles0]
%FILE0%=C:\Users\rober\ScriptureApp\ScriptureApp_Build.zip
%FILE1%=C:\Users\rober\ScriptureApp\installer.cmd
