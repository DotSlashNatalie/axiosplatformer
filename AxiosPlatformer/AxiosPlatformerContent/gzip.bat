@echo off
REM http://stackoverflow.com/questions/9252980/how-to-split-the-filename-from-a-full-path-in-batch
For %%A in ("%1") do (
    Set Folder=%%~dpA
    Set Name=%%~nxA
)
set Name=%Name:"=%
gzip -f -k %Name%
copy %1.gz "../AxiosPlatformer/Levels/%Name%.gz"
