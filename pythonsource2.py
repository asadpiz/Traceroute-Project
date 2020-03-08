import os
import re
import urllib
import simplekml
import subprocess


os.chdir ("C:\\Users\\Asad\\Dropbox\\Final Year Project\\SourceCode\\Rtr Probes") # Entering /FYP/Rtr Probes


i = 0
ipS= []
ispS= []
lonlat= []
rttave = []
tag   =  []
ip = open ('3.txt', 'r')
web = open ('geoip.txt', 'w+')
isp = open ('isp.txt','w+')

bytePattern = "([01]?\d\d?|2[0-4]\d|25[0-5])"
regObj = re.compile("\.".join([bytePattern]*4))
##    Extracting IPs & Parsing them to GEOIPTOOL.com(Long/Lat)####
logText = ip.read()
for match in regObj.finditer(logText):
    if i == 2:
        print match.group()
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
    print (same3.group())
    isp11 = same3.group()
    isp11 = isp11.strip ('OrgName:        \n')
    ispS.append(isp11)
isp.close()
####                FINDING Method              ####
bytePattern5 = '(-?(dst\\n|sym\\n|tr\\n|rr\\n|ts\\n))'
regObj5 = re.compile(bytePattern5)
for same5 in regObj5.finditer(logText):
    print (same5.group())
    met = same5.group()
    tag.append(met)
####                FINDING RTT                 ####
rtttemp = []
bytePattern4 = '(\d*[.]\d* ms)'
regObj4 = re.compile(bytePattern4)
for same4 in regObj4.finditer(logText):
    #print (same4.group())
    rtt11 = same4.group()
    rtt11 = rtt11.strip (' ms')
    rtt11 = float (rtt11)
    rtttemp.append(rtt11)
count = 0
summ = 0
for value in range(0,len(rtttemp),3):
    summ = rtttemp[value] + rtttemp[value+1] +rtttemp[value+2]
    ave = summ/3.0
    ave = str (ave) + " ms"
    rttave.append (ave)
    
####                GENERATING KML File                 ####
kml = simplekml.Kml()
m = 0
k = 0
km = 0
no = 0
z=[]
repeat = 0
for item in lonlat:
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
n = kml.newlinestring(name="Reverse Traceroute", description="", coords=[(0,0)])
n.coords = z
for ind in range(len(z)):
    print (ind)
    if (ind>0):
        k= 0
        ## Checking For Repeatition ##
        while (k<ind):
            if (z[ind]==z[k]):
                descfile = open ('Desc'+str(k)+'.txt', 'a')
                descfile.write('<pre>'+str(ind)+'\t\t\t'+str(ipS[ind])+'\t\t\t'+str(ispS[ind])+'\t\t\t'+str(tag[ind])+'\t\t\t'+str(rttave[ind])+'\n</pre>')
                descfile.close()
                pnt = kml.newpoint(description="", coords=[(0,0)])
                descfile = open ('Desc'+str(k)+'.txt', 'r')
                pnt.description = descfile.read()
                pnt.coords = [z[ind]]
                descfile.close()
                repeat = repeat + 1
                break ## Break The Loop
            else:
                k = k+1
                repeat = 0
    if (repeat == 0):
        descfile = open ('Desc'+str(ind)+'.txt', 'w')
        descfile.write('<pre>\nHop No.\t\t\tIP Address\t\t\tISP\t\t\tMethod\t\t\tAverage RTT\n')
        descfile.write(str(ind)+'\t\t\t'+str(ipS[ind])+'\t\t\t'+str(ispS[ind])+'\t\t\t'+str(tag[ind])+'\t\t\t'+ str(rttave[ind])+'\n</pre>')
        descfile.close()
        pnt = kml.newpoint(name="R"+str(ind), description="", coords=[(0,0)])
        descfile = open ('Desc'+str(ind)+'.txt', 'r')
        pnt.description = descfile.read()
        pnt.coords = [z[ind]]
        descfile.close()

kml.save("route1.kml")
os.startfile('route1.kml')

os.chdir(os.pardir) # Going Back to /FYP

