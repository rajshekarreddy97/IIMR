# IIMR - Intangible Interactions in Mixed Reality


Augmented Reality has been getting increased traction lately with a plethora of AR-enabled devices on the market ranging from smartphones to commercial Head-Mounted Displays (HMDs) such as the Microsoft Hololens. With the rise in AR, a need exists for there to be a more natural and intuitive form of interaction with virtual content that does not interfere with the user experience. Moreover, advanced and sophisticated hardware such as the Leap Motion controller makes this possible by employing sensors that can track hands accurately in real-time. This paper presents a framework, IIMR (Intangible Interactions in Mixed Reality), that operates over a network and allows for mid-air interactions using just the bare-hands. The aim is to provide researchers and developers an inexpensive and accessible solution for developing and prototyping hand interactions in a mixed reality environment. The system consists of three modules, namely a Smartphone, a Google cardboard-like HMD, and a Leap Motion controller. Diverse applications are developed to evaluate the framework, and the results from qualitative usability studies are outlined.

This framework requires a Leap Motion Controller, a smartphone, and Unity version 2018.4.21fi with the iOS/Android package and the Vuforia package installed. 

# Follow the below steps to set up IIMR:

* Create a new 3D project in Unity 2018.4.21fi.
* Switch the platform to iOS/Android.
* Import the IIMR unity package.
* Go to Build settings -> Player settings -> XR settings and enable 'Vuforia augmented reality supported'. Also, enable 'Virtual reality supported' (for stereoscopic rendering), then under Virtual Reality SDKs, click on the '+' sign and select 'Vuforia'.
* Go to the 'Scenes' folder and open the 'IIMR-AR' scene. Click on the 'ARCamera' game object in the Hierarchy panel. In the Inspector panel, select 'Open Vuforia Engine Configuration'. Add your Vuforia license key here.
* The child of the 'ARCamera' game object is the 'HandControllerMobile' game object, which is responsible for getting the hand coordinate data from the server. 
* Add the ‘Interaction.cs’ script from the 'Scripts' folder to the object that requires intractability. Set the visual feedback colors as preferred and the interaction specifications through this script. 
* Go ahead and build the scene onto your device.

# IIMR - server
	
* Open the 'IIMR' scene. This is the server scene and is responsible for interfacing with the leap motion and streaming the hand tracked data to the smartphone. 
* Click on the 'HandController' object in the Hierarchy panel and enter your smartphone's IP address in the Inspector panel.
* Select either 'Flat surface' or 'Head Mounted', depending on your Leap Motion's Position. 
* Connect the leap motion to your system and hit 'Play'. The data will be transmitted through UDP packets to your smartphone AR application. Make sure the AR application is open.
* Use the calibration system to calibrate the framework manually, such that the cube cursor appears exactly over your hands. 
