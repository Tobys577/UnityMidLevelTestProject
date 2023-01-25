# UnityMidLevelTestProject

Time taken: 5 hours, 21 minutes.

Unity Version: 2021.3.16f

Technologies used: Unity, Firebase, Photon (PUN)

### Build
A windows build of the project can be found here: https://drive.google.com/file/d/1ChsvbrPnIPD1QO6L1bqYlVKV570ZasKT/view?usp=share_link

It can be run multiple times to allow you to test multiplayer on the same device, or transfered to another PC to play with another person! It can also be ran at
the same time as within the Unity editor.

### Controls

WS / Up-Down -> Increase / decrease the throttle (shown by the thruster at the bottom glowing brighter or getting dimmer)

AD / Left-Right -> Rotate the rocket to change it's trajectory.

### Description
This is an example piece of freelance work that shows a tiny game where you can join 1 friend online in flying little rockets around the screen. All of the
code is commented and organised to be easy to look through and understand. The gravity can be altered in the settings menu, and the second player to join will set the gravity overall for that game, unless no gravity has been chosen since the last player changed it.

I chose to use Photon (PUN) as the networking solution for this project due to it's quick setup and it's intergration with Unity. I had a tiny bit of experience working with Photon before this project due to editting a few scripts for a previous client, however this was my first indepth look into it. It is nice to use and intergrated incredibly nicely with Unity and any existing scripts. Having programmed custom server-client solutions using sockets before the purely client
side control was a new experience to me, however I felt it was easy to work with after reading through the docs.

Similarly I had not used firebase within Unity before, however it's SDK made it simple to use, purely working with asynchronous code to load and save the gravity values between play sessions.


A 2D example of the project can also be found here: https://drive.google.com/file/d/1s9kCxPduHOnIHJQl8n8rbeKiz1QGAlAh/view?usp=share_link
