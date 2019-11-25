# EarcutAndTimezone
Arma 3 timezone and earcut extension

[earcut.js by Mapbox](https://github.com/mapbox/earcut) and [earcut.net](https://github.com/oberbichler/earcut.net) implementation for Arma 3.
Also contains player timezone collector. 

Usage: 

```sqf
"EarcutAndTimezone" callExtension "hello";
"EarcutAndTimezone" callExtension "GetTimezoneOffset";
"EarcutAndTimezone" callExtension "GetTimezoneName";
"EarcutAndTimezone" callExtension ["GetEarcut", ["[[[10,0],[0,50],[60,60],[70,10]]]"]]
```
