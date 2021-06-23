A test build/experimental code for designing RPG systems

This is a Unity project written in C#. The idea behind this is to create an RPG system with scripts that are easily adaptable, easy to understand, and not too performance intenstive.

All of the scripting is located in Assets/Scripts/ It's somewhat organized, the newest or biggest of the scripts have not been organized yet. The main code file here is the Engine.cs file. (Just about) Everything runs through this file, including movement, battle, UI, and inventory systems. The items, quests, locactions, and enemies are all created through ScriptableObjects, so it's very easy to create new assets or edit old ones.

Some of the other more interesting (in my opinion) scripts to look at are Item Scripts/Inventory.cs and Game Scripts/Player.cs. This isn't to say that the other scripts aren't nice, a lot of them are just derived from ScriptableObject and as such are only used to create new instances of that object to act as a database for game values. They're pretty boring.

In the Builds folder, there is a .apk file to run the first build of the game. It contains one working floor, a floor with a messed up baked lighting, and an overworld with nothing. The main focus of this project was not the playability or looks of the game, but building the working backbone for an RPG game. When playing, note that there are no skills/magic for the player quite yet. I was in the process of implementing that when I got sidetracked by a different project.

All of the assets are my own, including textures. They are not great textures, I'm not much of a graphic designer or artist. Don't laugh too hard at the Rat.png, I tried my best.
