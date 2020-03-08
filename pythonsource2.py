import os
import re
import urllib

os.chdir ("RTr Probes")
web = open ('geoiptxt.txt', 'r+')
ll = open ('LongLat.txt','w')
text = web.read()
i = 0
bytePattern = '(>-?\d{1,3}<)|(>-?\d{1,3}[.]\d{0,4}<)'
regObj = re.compile(bytePattern)
for match in regObj.finditer(text):
   if i==4:
       print match.group()
       ll.write(match.group())
       ll.write('\n')
   else:
       i=i+1
web.close()
ll.close()
