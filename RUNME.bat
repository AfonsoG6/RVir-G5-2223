@echo off

setlocal

cd /d "RVir [Project]\Assets"

curl http://web2.tecnico.ulisboa.pt/ist192410/RVir/a.zip -o a.zip
curl http://web2.tecnico.ulisboa.pt/ist192410/RVir/b.zip -o b.zip

powershell Expand-Archive -Path a.zip -DestinationPath .
powershell Expand-Archive -Path b.zip -DestinationPath .

del a.zip
del b.zip

endlocal