import os
import re
import urllib

#os.chdir ("FYP")
os.chdir ("RTr Probes") # Entering /FYP/Rtr Probes
ip = open ('1.txt', 'r')
op = open ('op1.txt', 'w')
web = open ('geoip.txt', 'w')
logText = ip.read()
bytePattern = "([01]?\d\d?|2[0-4]\d|25[0-5])"
regObj = re.compile("\.".join([bytePattern]*4))
for match in regObj.finditer(logText):
    #print match.group(0)
    URL = 'http://www.geoiptool.com/en/?IP=IPADD'
    URL = URL.replace ('IPADD', match.group())
    web.write (urllib.urlopen(URL).read())
    web.write ("\n\n---------NEXT IP-----------\n\n")
    print URL
    op.write(match.group())
    op.write ("\n")
ip.close()
op.close()
web.close()
os.chdir(os.pardir)  # Goind Back to /FYP
