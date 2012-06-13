# CAP Alerting Desktop

This is a simple desktop application that is used to display emergency alerts via CAP 1.2 (Common Alert Protocol) XML feeds.  It was originally written to work with Rave Mobile Safety's Rave Alert service, but can be adapted to any emergency alert service that provides a CAP feed.  CAP is a standard for XML as RSS is a standard for XML.

This software was designed to be small, lightweight and easy to implement for our IT staff.


## Quick Start

1. From the Program/bin/Release folder copy CAPDesktop.exe to a location on a desktop machine.

2. Open up Windows Task Scheduler and create a new task.  

3. Create a daily task and make sure to set it to run every minute.

4. Select the executable file you placed on your computer.  Also, make sure you include the XML location in the argument box.  This can either be a web url or a local Windows location.


## What Happens

If you are using an alert system like Rave Alert, you can post a CAP Alert.  In Rave you can identify the event (which will display as the title), the description (which will be the description), the effective time (which is when the alert will display itself), and expiration time (which is when the alert will go away).  This values are in the CAP XML feed.  Every minute the script compares the current time with the effective and expire times and then displays the message based on what was defined in the XML.  When the script is triggered the message will take over the screen and display in clear, red type.     


## Extending The Code

I plan on extending this service in the near future, but if you have any ideas or thoughts on making it more user-friendly, have a crack at it.  The source code is included for your use.


## Disclaimer

This software should be tested first before used in a production environment.  I assume no responsibility whatsoever for its use by other parties, and makes no guarantees, expressed or implied, about its quality, reliability, or any other characteristic.  Please do not use this software for malicious intentions.



