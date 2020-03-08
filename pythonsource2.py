import os
import re
import urllib

os.chdir ("RTr Probes") # Entering /FYP/Rtr Probes

i = 0
ip = open ('result.txt', 'r')
web = open ('tracert.txt', 'w+')

bytePattern = "([01]?\d\d?|2[0-4]\d|25[0-5])"
regObj = re.compile("\.".join([bytePattern]*4))

##    Extracting IPs & Parsing them to GEOIPTOOL.com  ####
logText = ip.read()
for match in regObj.finditer(logText):
    #print match.group(0)
    URL = 'http://www.geoiptool.com/en/?IP=IPADD'
    URL = URL.replace ('IPADD', match.group())
    web.write (urllib.urlopen(URL).read())
    web.write ("\n\n---------NEXT IP-----------\n\n")
    print URL
    #op.write(match.group())
    #op.write ("\n")
ip.close()
web.close()
#op.close()

web2 = open ('tracert.txt', 'r')
ll = open ('LongLattr.txt','w')
text = web2.read()
i = 0
bytePattern2 = '(>-?\d{1,3}<)|(>-?\d{1,3}[.]\d{0,4}<)'
regObj2 = re.compile(bytePattern2)
for same in regObj2.finditer(text):
   if i==4:
       print same.group()
       ll.write(same.group())
       ll.write('\n')
   else:
       i=i+1
web2.close()
ll.close()

os.chdir(os.pardir) # Going Back to /FYP

