# MicrosoftDynamicsCRM
3CX integration with Dynamics CRM
This is a plugin for version 15.0 and 15.5 of the 3cx phone system to intergrate with Microsoft Dynamics 365. All of this code came from the 3CX built in zip package I pulled from a 15.5 trial install, I just tweaked it a bit to do a few other things that deal with contacts, cases, and accounts.
Essentially all of these files need to be placed in a zip and then uploaded to 3CX via the settings > CRM Integration section. This will then get pulled down into the 3CX client application(if the users extension is enabled for the integrations) and compiled on load of the plugins. After it is initially downloaded you can then directly edit the cs files in 
C:\ProgramData\3CXPhone for Windows\PhoneApp\DotNetScripts\MicrosoftDynamicsCRM
and then in the application go to the gears in lower right > advanced > integrations and then it will recompile the scripts. Log files in %appdata%\3cx will log data for the integration as well as errors on compiling the scripts.

If you change the name of the zip you'll need to change the value for Script_1_ProjectFolder in 3CXCRM.ini 

I've had some odd issues where the project doesnt compile client side after incrementing the VERSION file, seems it removes everything in the DotNetScripts folder and after a couple client app restarts it downloads everything again and loads the plugin properly. Odd that it takes a couple application shutdown and restarts to clear it though. Not sure if I'm doing something wrong or bug.

Disclaimer : I dont code for a living, this is all probably pretty hacky, but whatever... if it works cool.
