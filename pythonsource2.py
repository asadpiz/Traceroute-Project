import os
import re
import urllib
import simplekml

os.chdir ("RTr Probes") # Entering /FYP/Rtr Probes

i = 0
ipS= []
ispS= []
lonlat= []
rttave = []

ip = open ('1.txt', 'r')
web = open ('geoip.txt', 'w+')
isp = open ('isp.txt','w+')

bytePattern = "([01]?\d\d?|2[0-4]\d|25[0-5])"
regObj = re.compile("\.".join([bytePattern]*4))
##    Extracting IPs & Parsing them to GEOIPTOOL.com(Long/Lat)####
logText = ip.read()
for match in regObj.finditer(logText):
    if i == 2:
        #print match.group()
        ipS.append(match.group())
        URL = 'http://www.geoiptool.com/en/?IP=IPADD'
        URL = URL.replace ('IPADD', match.group())
        web.write (urllib.urlopen(URL).read())
        web.write ("\n\n---------NEXT IP-----------\n\n")
##      whois for ISP info
        URL = 'http://www.topwebhosts.org/whois/index.php?query=IPADD'
        URL = URL.replace ('IPADD', match.group())
        isp.write (urllib.urlopen(URL).read())
        isp.write ("\n\n---------NEXT IP-----------\n\n")
    else:
        i = i+1
ip.close()
web.close()
isp.close()

####                FINDING LONGITUDE/LATITUDE              ####
web2 = open ('geoip.txt', 'r')
text = web2.read()
bytePattern2 = '(>-?\d{1,3}<)|(>-?\d{1,3}[.]\d{0,4}<)'
regObj2 = re.compile(bytePattern2)
for same in regObj2.finditer(text):
    llat = same.group()
    llat = llat.strip ('><')
    #print (llat)
    lonlat.append(llat)
web2.close()
####                FINDING ISP                 ####
isp = open ('isp.txt', 'r')
text3 = isp.read()
bytePattern3 = '(OrgName:        .*\n)'
regObj3 = re.compile(bytePattern3)
for same3 in regObj3.finditer(text3):
    #print (same3.group())
    isp11 = same3.group()
    isp11 = isp11.strip ('OrgName:        \n')
    ispS.append(isp11)
isp.close()
####                Method              ####
method = open ('1.txt', 'r')
tag = []
text5 = method.read()
bytePattern5 = '(-?(dst\\n|sym\\n|tr\\n|rr\\n|t\\ns))'
regObj5 = re.compile(bytePattern5)
for same5 in regObj5.finditer(text5):
    print (same5.group())
    met = same5.group()
    tag.append(met)
method.close()
####                Finding RTT                 ####
rtt = open ('1.txt', 'r')
text4 = rtt.read()
rtttemp = []
bytePattern4 = '(\d*[.]\d* ms)'
regObj4 = re.compile(bytePattern4)
for same4 in regObj4.finditer(text4):
    #print (same4.group())
    rtt11 = same4.group()
    rtt11 = rtt11.strip (' ms')
    rtttemp.append(rtt11)
rtt.close()

####                GENERATING KML File                 ####
kml = simplekml.Kml()
m = 0
km = 0
no = 0
z=[]
rep = []
for item in lonlat:
    if m==0:
        longg= item
        m = m+1
    elif m==1:
        lat= item
        m=m-1
        km =km+1
    if km ==1:
        n = kml.newlinestring(name="Pathway", description="", coords=[(0,0)])
        y = (longg,lat)
        z.append(y)
        n.coords = z
        #rep = 
        pnt = kml.newpoint(name="R"+str(no), description="Point", coords=[(longg,lat)])
        kml.save("1.kml")
        no =no+1
        km =km-1


##z = 0
##for z:(len(rtttemp)-3):
##    average = (rtttemp[z]+rtttemp[z+1]+rtttemp[z+2])/5
##    rttave.append(average)
##    z =z+3

os.chdir(os.pardir) # Going Back to /FYP