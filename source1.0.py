import subprocess, urllib, urllib2, re, simplekml

                ##      USER INPUT      ##

print ("Enter Your Desired Public Traceroute Server")
choice = input ("1. Viafacil (Argentina)\n\
2. SPT (Australia)\n3. Optus (Australia)\n\
4. Nemox.net (Austria)\n5. MegaLink (Bolivia)\n")

publicip = urllib2.urlopen("http://www.whatismyip.org/").read()

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

##  PERFORMING REVERSER TRACEROUTE FROM PUBLIC SERVER   ##
print ("Performing Reverse Traceroute Please Wait...")
f2.write(urllib2.urlopen(URL).read())


## PERFROMING FORWARD TRACEROUTE FROM HOST MACHINE      ##
print ("Now Performing Forward Traceroute")
## tracert for WINDOWS|||||traceroute for LINUX
p = subprocess.Popen(["traceroute",host],stdout=subprocess.PIPE, stderr=subprocess.STDOUT)

while True:
    line = p.stdout.readline()
    if not line: break
    print (line)
   # f1.write(line)

p.wait()
f1.close()
f2.close()

## EXTRACTING IPs FROM REVERSE TRACEROUTE       ##
RipS = []
f3 = open ('rTr1.txt', 'r')
trfile = f3.read()
ippattern = '(\(([01]?\\d\\d?|2[0-4]\\d|25[0-5])\\.([01]?\\d\\d?|2[0-4]\\d|25[0-5])\\.([01]?\\d\\d?|2[0-4]\\d|25[0-5])\\.([01]?\\d\\d?|2[0-4]\\d|25[0-5])\))'
regularobj = re.compile (ippattern)
for ip in regularobj.finditer (trfile):
    #print (ip.group())
    temp1 = ip.group()
    temp1 = temp1.strip ('()')
    RipS.append(temp1)
f3.close()

## EXTRACTING IPs FROM FORWARD TRACEROUTE       ##
skip = 0
FipS = []
f4 = open ('trace1.txt', 'r')
trfile = f4.read()
ippattern = '([01]?\\d\\d?|2[0-4]\\d|25[0-5])\\.([01]?\\d\\d?|2[0-4]\\d|25[0-5])\\.([01]?\\d\\d?|2[0-4]\\d|25[0-5])\\.([01]?\\d\\d?|2[0-4]\\d|25[0-5])'
regularobj = re.compile (ippattern)
for ip in regularobj.finditer (trfile):
    #print (ip.group())
    if (skip == 0):
        skip = skip +1
    else:
        FipS.append(ip.group())
f4.close()

geo1 = open ('Fgeoip.txt', 'w')
### Parsing Forward IPs to GEOIPtool
for item in FipS:
        URL1 = 'http://www.geoiptool.com/en/?IP=IPADD'
        URL1 = URL1.replace ('IPADD', item)
        geo1.write (urllib2.urlopen(URL1).read())
        geo1.write ("\n\n---------NEXT IP-----------\n\n")
geo2 = open ('Rgeoip.txt', 'w')
### Parsing Reverse IPs to GEOIPtool
for item in RipS:
        URL2 = 'http://www.geoiptool.com/en/?IP=IPADD'
        URL2 = URL2.replace ('IPADD', item)
        geo2.write (urllib2.urlopen(URL2).read())
        geo2.write ("\n\n---------NEXT IP-----------\n\n")    

geo1.close()
geo2.close()


Flonlat = []
####                FINDING Forward LONGITUDE/LATITUDE              ####
web2 = open ('Fgeoip.txt', 'r')
text = web2.read()
bytePattern2 = '(>-?\d{1,3}<)|(>-?\d{1,3}[.]\d{0,4}<)'
regObj2 = re.compile(bytePattern2)
for same in regObj2.finditer(text):
    llat = same.group()
    llat = llat.strip ('><')
    print (llat)
    Flonlat.append(llat)
web2.close()

Rlonlat = []
####                FINDING Reverse LONGITUDE/LATITUDE              ####
web3 = open ('Rgeoip.txt', 'r')
text = web3.read()
bytePattern3 = '(>-?\d{1,3}<)|(>-?\d{1,3}[.]\d{0,4}<)'
regObj3 = re.compile(bytePattern3)
for same in regObj3.finditer(text):
    llat = same.group()
    llat = llat.strip ('><')
    print (llat)
    Rlonlat.append(llat)
web3.close()



####                GENERATING Forward KML File                 ####
kml = simplekml.Kml()
m = 0
k = 0
km = 0
no = 0
z=[]
repeat = 0
for item in Flonlat:
    if m==0:
        longg= item
        m = m+1
    elif m==1:
        lat= item
        m=m-1
        km =km+1
    if km ==1:
        y = (longg,lat)
        z.append(y)
        km =km-1
n = kml.newlinestring(name="Forward Traceroute", description="", coords=[(0,0)])
n.coords = z
kml.save("Froute1.kml")
####                GENERATING Reverse KML File                 ####
kml2 = simplekml.Kml()
m = 0
k = 0
km = 0
no = 0
z=[]
repeat = 0
for item in Rlonlat:
    if m==0:
        longg= item
        m = m+1
    elif m==1:
        lat= item
        m=m-1
        km =km+1
    if km ==1:
        y = (longg,lat)
        z.append(y)
        km =km-1
n = kml2.newlinestring(name="Reverse Traceroute", description="", coords=[(0,0)])
n.coords = z
kml2.save("Rroute1.kml")
