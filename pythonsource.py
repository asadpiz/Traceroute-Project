import subprocess, urllib2, re, simplekml, os, time, whois

                ##      USER INPUT      ##

print ("Enter Your Desired Public Traceroute Server")
choice = input ("1. Viafacil (Argentina)\n\
2. SPT (Australia)\n3.  Nemox.net (Austria)\n\
4. MegaLink (Bolivia)\n")
            ## Making New Directory For Each Probe ##
os.mkdir('probe'+str(choice))
os.chdir ('probe'+str(choice))

####EXTRACTING PUBLIC IP####
start = time.time()
#publicip = urllib2.urlopen("http://automation.whatismyip.com/n09230945.asp").read()
publicip = '39.47.236.212'
print (publicip)

####CREATING PUBLIC SERVER TR REQUEST URL & SELECTING it'S IP####

if   (choice == 1):
    host = '200.123.164.101'
    URL = 'http://baires02.com.ar/traceroute/index.php?host=myIP&submit=Traceroute'
    URL = URL.replace ('myIP', publicip)
elif (choice == 2):
    host = '203.220.0.5'
    URL = 'http://mirror.sptel.com.au/cgi-bin/trace?myIP+'
    URL = URL.replace ('myIP', publicip)
elif (choice == 3):
    host = '83.137.41.254'
    URL = 'http://nemox.net/traceroute/index.pl?t=myIP'
    URL = URL.replace ('myIP', publicip)
elif (choice == 4):
    host = '200.75.160.7'
    URL = 'http://trace.megalink.com/index.php?host=myIP&submit=Traceroute!'
    URL = URL.replace ('myIP', publicip)
else:
    print ("You have Entered An Invalid Choice")

    
###########   CREATING FILES TO SAVE FORWARD & REVERSE TR ###############

f1 = open ('trace'+str(choice)+'.txt','w')
f2 = open ('rTr'+str(choice)+'.txt', 'w')


###########  PERFORMING REVERSER TRACEROUTE FROM PUBLIC SERVER   ##########

print ("Performing REVERSE TRACEROUTE Please Wait...")
f2.write(urllib2.urlopen(URL).read())


############# PERFROMING FORWARD TRACEROUTE FROM HOST MACHINE      ##########

print ("Now Performing Forward Traceroute")
                ## tracert for WINDOWS|||||traceroute for LINUX
p = subprocess.Popen(["traceroute",host],stdout=subprocess.PIPE, stderr=subprocess.STDOUT)
pa = 0
while True:
    line = p.stdout.readline()
    if not line: break
    print (line)
    f1.write(line)
p.wait()

f1.close()
f2.close()


################# Extracting IPs & RTTs From REVERSE TRACEROUTE   #############

RipS    = []
HopFlag = []
Rrttave = []
f3 = open ('rTr'+str(choice)+'.txt','r')
Rtrfile = f3.read()


##Rlinepattern = '([]\d+  .*)'
Rlinepattern = '([ \n]\d+  .*)'
RIPpattern   = '(\(([01]?\\d\\d?|2[0-4]\\d|25[0-5])\\.([01]?\\d\\d?|2[0-4]\\d|25[0-5])\\.([01]?\\d\\d?|2[0-4]\\d|25[0-5])\\.([01]?\\d\\d?|2[0-4]\\d|25[0-5])\))'
RStarpattern = '(\d)+  \* \* \*'
Rrttpattern  = '(\d*[.]\d* ms)|(\d+ ms)'
Rregobj1 =   re.compile (Rlinepattern)
Rregobj2 =   re.compile (RIPpattern)
Rregobj3 =   re.compile (RStarpattern)
Rregobj4 =   re.compile (Rrttpattern)

for line in Rregobj1.finditer (Rtrfile):
    first_ip = 0
    summ     = 0
    star     = 0
    length   = 0
    Rave     = 0
    #print (line.group())
    
    ######## Extract IPs Line-by-Line  #######
    for ip1 in Rregobj2.finditer (line.group()):
        IP1   = ip1.group()
        IP1   = IP1.strip('()')
        #print (IP1)
        if (first_ip == 0):
            RipS.append(IP1)
            first_ip = first_ip +1
        elif (first_ip > 0):           
            first_ip = first_ip+1
    HopFlag.append(first_ip)
    
    ########    CASE For '*'    ####
    for ip2 in Rregobj3.finditer (line.group()):
        RipS.append    ('0')
        Rrttave.append ('0 ms')
        star = 1
        
    ######## Extract RTTs & Calculate Average RTT Line-by-Line ######
    if (star!=1):
        for rtt1 in Rregobj4.finditer (line.group()):
            rtt11  = rtt1.group()
            #print (rtt1.group())
            rtt11  = rtt11.strip (' ms')
            rtt11  = float (rtt11)
            length = length + 1
            summ   = summ + rtt11
        Rave = summ/length
        Rave = str (Rave) + " ms"
        Rrttave.append (Rave)
        
f3.close()

print ("I did the reverse Part")
################# Extracting IPs & RTTs From FORWARD TRACEROUTE   #############

FipS    = []
FHopFlag = []
Frttave = []
f4 = open ('trace'+str(choice)+'.txt','r')
Ftrfile = f4.read()

#linepattern = '((\d)+  .*)'
linepattern = '([ \n]\d+  .*)'
ippattern   = '(\(([01]?\\d\\d?|2[0-4]\\d|25[0-5])\\.([01]?\\d\\d?|2[0-4]\\d|25[0-5])\\.([01]?\\d\\d?|2[0-4]\\d|25[0-5])\\.([01]?\\d\\d?|2[0-4]\\d|25[0-5])\))'
starpattern = '(\d)+  \* \* \*'
rttpattern  = '(\d*[.]\d* ms)'
regularobj  = re.compile  (linepattern)
regularobj2 = re.compile (ippattern)
regularobj3 = re.compile (starpattern)
regularobj4 = re.compile (rttpattern)

for line in regularobj.finditer (Ftrfile):
    first_Fip = 0
    Fsumm     = 0
    Fstar     = 0
    Flength   = 0
    Fave      = 0
    print (line.group())
    
############# Extract IPs Line-by-Line  ################
    
    for ip3 in regularobj2.finditer (line.group()):
        FIP = ip3.group()
        FIP = FIP.strip('()')
        if (first_Fip == 0):
            FipS.append(FIP)
            first_Fip = first_Fip +1
        elif (first_Fip > 0):           
            first_Fip= first_Fip+1
    FHopFlag.append(first_Fip)

##########    CASE For '*'    #########################
    
    for ip4 in regularobj3.finditer (line.group()):
        FipS.append ('0')
        Frttave.append ('0 ms')
        Fstar = 1
        
######## Extract RTTs & Calculate Average RTT Line-by-Line ###########
        
    if (Fstar!=1):
        for Frtt in regularobj4.finditer (line.group()):
            Frtt11  = Frtt.group()
            Frtt11  = Frtt11.strip (' ms')
            Frtt11  = float (Frtt11)
            Flength = Flength +1
            Fsumm   = Fsumm + Frtt11
        Fave = Fsumm/Flength
        Fave = str (Fave) + " ms"
        Frttave.append (Fave)    
f4.close()


Fgeo = open ('Fgeoip'+str(choice)+'.txt', 'w')
Rgeo = open ('Rgeoip'+str(choice)+'.txt', 'w')

### Parsing FORWARD IPs to GEOIPtool  ##########

for item in FipS:
    if (item !=0):
        URL1 = 'http://www.geoiptool.com/en/?IP=IPADD'
        URL1 = URL1.replace ('IPADD', item)
        Fgeo.write (urllib2.urlopen(URL1).read())
        Fgeo.write ("\n\n---------NEXT IP-----------\n\n")


### Parsing REVERSE IPs to GEOIPtool   ##########

for item in RipS:
    if (item!=0):
        URL2 = 'http://www.geoiptool.com/en/?IP=IPADD'
        URL2 = URL2.replace ('IPADD', item)
        Rgeo.write (urllib2.urlopen(URL2).read())
        Rgeo.write ("\n\n---------NEXT IP-----------\n\n")    

Fgeo.close()
Rgeo.close()


##                FINDING Forward LONGITUDE/LATITUDE              ####

Flonlat = []
FLL = open ('Fgeoip'+str(choice)+'.txt', 'r')
FLLtext = FLL.read()
FLLpattern = '(>-?\d{1,3}<)|(>-?\d{1,3}[.]\d{0,4}<)'
FLLregObj = re.compile(FLLpattern)
for same in FLLregObj.finditer(FLLtext):
    Fllat = same.group()
    Fllat = Fllat.strip ('><')
    print (Fllat)
    Flonlat.append(Fllat)
FLL.close()

##                FINDING Reverse LONGITUDE/LATITUDE              ####
Rlonlat = []
RLL = open ('Rgeoip'+str(choice)+'.txt', 'r')
RLLtext = RLL.read()
RLLpattern = '(>-?\d{1,3}<)|(>-?\d{1,3}[.]\d{0,4}<)'
RLLregObj = re.compile(RLLpattern)
for same in RLLregObj.finditer(RLLtext):
    Rllat = same.group()
    Rllat = Rllat.strip ('><')
    print (Rllat)
    Rlonlat.append(Rllat)
RLL.close()
print "it took", time.time() - start, "seconds."
##
##
##
######                GENERATING Forward KML File                 ####
##kml = simplekml.Kml()
##m = 0
##k = 0
##km = 0
##no = 0
##z=[]
##repeat = 0
##for item in Flonlat:
##    if m==0:
##        longg= item
##        m = m+1
##    elif m==1:
##        lat= item
##        m=m-1
##        km =km+1
##    if km ==1:
##        y = (longg,lat)
##        z.append(y)
##        km =km-1
##n = kml.newlinestring(name="Forward Traceroute", description="", coords=[(0,0)])
##n.coords = z
##kml.save("Froute1.kml")
######                GENERATING Reverse KML File                 ####
##kml2 = simplekml.Kml()
##m = 0
##k = 0
##km = 0
##no = 0
##z=[]
##repeat = 0
##for item in Rlonlat:
##    if m==0:
##        longg= item
##        m = m+1
##    elif m==1:
##        lat= item
##        m=m-1
##        km =km+1
##    if km ==1:
##        y = (longg,lat)
##        z.append(y)
##        km =km-1
##n = kml2.newlinestring(name="Reverse Traceroute", description="", coords=[(0,0)])
##n.coords = z
##kml2.save("Rroute1.kml")
##
os.chdir(os.pardir)
