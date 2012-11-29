# Ball Control #
#### a BlueTooth/Accelerometer/Camera/VoiceControl app for fun and for Xamarin’s Developer Showdown ####

Ball Control is an open source Sphero controller.

To use Ball Control, you first need a Ball - http://gosphero.com

Ball Control provides all this functionality on *WP8* and makes use of *Portable Class Libraries*, *Xamarin.Mobile library*, and *MvvmCross*

Ball Control includes a first version using *Xaramin Mono for Android* - this is in draft at present.

Ball Control will also soon provide *WinRT* and *Xamarin MonoTouch for iOS* version :)

The app allows you to control your ball - currently you can control it's heading, it's rolling, it's colour and it's tail. More will follow - especially if you join this project and help make it even more awesome.

**IMPORTANT:** Before you attempt to compile and use the code, please read the ***GETTING STARTED*** section first!!!
 
![Ball Control Banner](https://raw.github.com/slodge/BallControl/master/wide.png)

#[Watch an intro video about the project!](http://www.youtube.com/watch?v=TODO&feature=youtube_gdata "Watch an Intro Video about the Project!")#

----------


###PROJECT DETAILS###
The goal of this application was an entry into the contest, specifically showing off the use of Xamarin.Mobile. However it was also a chance for me to write some fun code after being on 'business logic' for a few months. This application uses MvvmCross and Portable Class Libraries throughout. There are no #if statements allowed.

This project includes:

- Xamarin.Mobile
	- Media Picker
		- Really easy way to take a photo - took seconds to add to my app

- MvvmCross
	- Code Sharing
		- Share as much code as possible across platforms
		- Model View View Model pattern
		- Common Model, ViewModel code layer
		- Simple IoC framework
		- Plugins 

- Windows Phone 8 SDK
	- Voice Control
	- BlueTooth
	- Accelerometer

- Android SDK with Xamarin's Mono for Android
	- BlueTooth
	- Accelerometer
	- more coming soon....

Other platforms and features will be added soon....
	
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
At this time, you should have no problems opening the MonoTouch and Mono for Android projects on the mac.

If you do have issues, try the following:

1. Edit the file */Library/Frameworks/Mono.framework/Versions/Current/lib/mono/xbuild/Microsoft/Portable/v4.0/Microsoft.Portable.CSharp.targets*
2. Find the PropertyGroup that sets *<TargetFrameworkIdentifier>MonoTouch</TargetFrameworkIdentifier>*
3. Ensure the following lines exist in this PropertyGroup:

	```
	<CscToolExe>smcs</CscToolExe>
	<CscToolPath>/Developer/MonoTouch/usr/bin</CscToolPath>
	```

----------

###LINKS###
-	Xamarin.Mobile - http://xamarin.com/mobileapi
-	MvvmCross - https://github.com/slodge/MvvmCross


----------

###THANK YOU'S###
- Xamarin - Thanks to all the folks at Xamarin who make coding for mobile in C# a dream :)
- Nokia - for the best phone ever - Lumia 920
- Sphero - for a totally awesome toy!
- James Newton-King - for JSON.Net!
- the ITR Mobility team - for MonoCross
- Jonathan Dick (@redth) - Thanks for saying yet to letting me steal this readme format :)

----------

###IMPORTANT###
This app talks to your sphero over BlueTooth. 

This app is in no way endorsed by Sphero - but we'd love them to say 'Hi'

It is definitely possible to get carried away and to have too much fun with your balls. Please play nicely.