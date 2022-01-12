' CreateObject("Wscript.Shell").Run "startNgrok.bat",0,True

Set WshShell = WScript.CreateObject("WScript.Shell")
obj = WshShell.Run("""startNgrok.bat""", 0)
set WshShell = Nothing
