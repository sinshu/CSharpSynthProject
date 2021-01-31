echo off
cls

REM Delete Binaries
del /q Bin\*
for /d %%x in (Bin\*) do @rd /s /q %%x

REM Delete Object Data
del /q vs2012\obj\*
for /d %%x in (vs2012\obj\*) do @rd /s /q %%x

del /q XNA4\obj\*
for /d %%x in (XNA4\obj\*) do @rd /s /q %%x

del /q Tools\BankUtil\obj\*
for /d %%x in (Tools\BankUtil\obj\*) do @rd /s /q %%x

del /q Tools\SfztoMulti\obj\*
for /d %%x in (Tools\SfztoMulti\obj\*) do @rd /s /q %%x

REM Delete IDE Data
del /q /s /f *.suo
del /q /s /f /ah *.suo

ECHO DONE!