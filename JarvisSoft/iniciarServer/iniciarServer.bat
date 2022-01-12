start cmd /c init.vbs
ping 127.0.0.1 -n 3 > nul
start startServ.py
ping 127.0.0.1 -n 5 > nul
start clickRefreesh.py