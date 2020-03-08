import subprocess, urllib, urllib2

print ("Enter Your Desired Public Traceroute Server")
choice = input ("1. Viafacil (Argentina)\n\
2. SPT (Australia)\n3. Optus (Australia)\n\
4. Nemox.net (Austria)\n5. MegaLink (Bolivia)\n")

publicip = urllib2.urlopen("http://automation.whatismyip.com/n09230945.asp").read()

if   (choice == 1):
    host = '200.123.164.101'
    URL = 'http://baires02.com.ar/traceroute/index.php?host=myIP&submit=Traceroute'
    URL = URL.replace ('myIP', publicip)
elif (choice == 2):
    host = '203.220.0.5'
    URL = 'http://mirror.sptel.com.au/cgi-bin/trace?myIP+'
    URL = URL.replace ('myIP', publicip)
elif (choice == 3):
    host = '211.29.132.105'
    URL = 'http://traceroute.optusnet.com.au/?args=myIP+'
    URL = URL.replace ('myIP', publicip)
elif (choice == 4):
    host = '83.137.41.254'
    URL = 'http://nemox.net/traceroute/index.pl?t=myIP'
    URL = URL.replace ('myIP', publicip)
elif (choice == 5):
    host = '200.75.160.7'
    URL = 'http://trace.megalink.com/index.php?host=myIP&submit=Traceroute!'
    URL = URL.replace ('myIP', publicip)
else:
    print ("You have Entered An Invalid Choice")
    
f1 = open ('trace1.txt','w')
f2 = open ('rTr1.txt', 'w')
print ("Performing Reverse Traceroute Please Wait...")
f2.write(urllib2.urlopen(URL).read())
print ("Now Performing Forward Traceroute")
p = subprocess.Popen(["tracert", host],stdout=subprocess.PIPE, stderr=subprocess.STDOUT)

while True:
    line = p.stdout.readline()
    if not line: break
    print (line)
    f1.write(line)

p.wait()
f1.close()
f2.close()
