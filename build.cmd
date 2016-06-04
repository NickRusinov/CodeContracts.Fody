@echo off
cls

"tools\nuget.exe" "install" "FAKE" "-OutputDirectory" "packages" "-ExcludeVersion"
"packages\Fake\tools\FAKE.exe" "build.fsx" %*

pause
