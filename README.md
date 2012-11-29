# Ball Control #
#### A BlueTooth/Accelerometer/Camera/VoiceControl app for fun and for Developer Competition ####

Ball Control is an open source Sphero controller.

To use Ball Control, you first need a Ball - get your Sphero from http://gosphero.com - and in the UK from http://www.firebox.com/product/5367/Sphero

----------

###WHAT?###

Ball Control's first target platform is *Windows Phone 8* where it makes use of *Portable Class Libraries*, *Xamarin.Mobile library*, and *MvvmCross*

The app allows you to control your ball - currently you can control:

- heading
- rolling
- colo(u)r
- tail light on/off

Using the WP8 APIs - abstracted inside PCL MvvmCross plugins, you can control these things using:

- touch
- tilt
- voice

The current repo includes a first version of Ball Control for *Xaramin Mono for Android*. this is in draft at present - it works but is not styled yet. Ball Control will also soon provide *WinRT* and *Xamarin MonoTouch for iOS* version :)

More will follow - especially if you join this project and help make it even more awesome.

**IMPORTANT:** Before you attempt to compile and use the code, please read the ***GETTING STARTED*** section first!!!
 
![Ball Control Banner](https://raw.github.com/slodge/BallControl/master/wide.png)

#[Watch an intro video about the project!](http://www.youtube.com/watch?v=TODO&feature=youtube_gdata "Watch an Intro Video about the Project!")#

----------


###PROJECT DETAILS###
The goal of this application was to show off C# Mobile coding, specifically showing off the use of C# native solutions including the use of Xamarin.Mobile products. It was also a chance for me to write some fun code after being on 'business logic' for a few months. This application uses MvvmCross and Portable Class Libraries throughout - there are no #if statements allowed - it's **AmazeBalls**.

This project includes:

- Xamarin.Mobile
	- Media Picker
        - Really easy way to take a photo - took seconds to add to my app
<br/><br/>					
- MvvmCross
    - Mvvm Platform
	     - Plugins via IoC and PCL for code sharing
<br/><br/>					
- Windows Phone 8 SDK
	- Voice Control
	- BlueTooth
	- Accelerometer
<br/><br/>					
- Android SDK with Xamarin's Mono for Android
	- BlueTooth
	- Accelerometer
	- more coming soon....

	
Other platforms and features will be added soon....

There are some NUnit unit tests included - I ran out of time a bit - so coverage is not huge. More will be added!

----------

###GETTING STARTED###
At the time of creating this project, Ball Control makes heavy use of the latest version of MvvmCross which in turn uses Portable Class libraries (PCL’s) extensively.  At this time, there are a few tweaks you must make to your system(s) before you may be able to compile the project.  The main issue is that the Mono for Android and MonoTouch profiles do not recognize Portable Class Libraries (PCL’s) as valid profile types to reference.  We need to ‘trick’ visual studio into allowing us to reference these PCL’s.

#####Windows Setup:#####
In order to get Visual Studio Mono for Android projects to be able to reference Portable Class Libraries, we need to trick it.

1. Open the folder: *C:\Program Files (x86)\Referenced Assemblies\Microsoft\Framework\.NETPortable\v4.0\Profile\Profile104\SupportedFrameworks\\*
2. Create a new file named *MonoAndroid,Version=v1.6+.xml* with the following contents:

	```
	<?xml version="1.0" encoding="utf-8"?>
	<Framework DisplayName="Mono for Android"
	  Identifier="MonoAndroid"
	  Profile="*"
	  MinimumVersion="1.6"
	  MaximumVersion="*" />
	```
3. If you had Visual Studio open, you'll need to restart it



#####Mac Setup:######
At this time, you should have no problems opening the Mono for Android projects on the mac...

If you do have issues, try the following:

1. Edit the file */Library/Frameworks/Mono.framework/Versions/Current/lib/mono/xbuild/Microsoft/Portable/v4.0/Microsoft.Portable.CSharp.targets*
2. Find the PropertyGroup that sets *<TargetFrameworkIdentifier>MonoTouch</TargetFrameworkIdentifier>*
3. Ensure the following lines exist in this PropertyGroup:

	```
	<CscToolExe>smcs</CscToolExe>
	<CscToolPath>/Developer/MonoTouch/usr/bin</CscToolPath>
	```

If you have trouble building the PCL projects in MonoDevelop, then we may need to change the PCL profile - while we are waiiting for this fix - https://bugzilla.xamarin.com/show_bug.cgi?id=7173 - the workaround is http://slodge.blogspot.co.uk/2012/10/a-temporary-solution-for-profile1-only.html

----------

###LINKS###
-   WP8 Dev - https://dev.windowsphone.com/en-us
-   Mono for Android - http://xamarin.com/monoforandroid
-   MonoTouch - http://xamarin.com/monoforandroid
-   Windows Store Dev - http://msdn.microsoft.com/en-us/windows/apps
-	Xamarin.Mobile - http://xamarin.com/mobileapi
-	MvvmCross - https://github.com/slodge/MvvmCross
-   Sphero - http://gosphero.com
-   Some cats - http://catoverflow.com

----------

###THANK YOU###
- Sphero - for a totally awesome toy!
- Nokia - for the best phone ever - Lumia 920
- Xamarin - Thanks to all the folks at Xamarin who make coding for mobile in C# a dream :)
- Daniel Plaisted - for PCL skill, intelligence and determination
- Denmark - for Anders Hejlsberg
- James Newton-King - for JSON.Net which I use everywhere
- Paul Foster - for talking about Sphero 
- Justin Angel - for the amazing WP8 What's New guide
- the ITR Mobility team - for MonoCross which inspired MvvmCross a year ago
- ChrisNTR - for MonoMobile.Extensions
- Jonathan Dick (@redth) - Thanks for saying yes to letting me steal this readme format :)

----------

###IMPORTANT###
This app is in no way endorsed by Sphero - but we love them!

This app talks to your Sphero over BlueTooth using some of the publicly available low level APIs. 

It is definitely possible to get carried away and to have too much fun with your balls. *Please play nicely.*
