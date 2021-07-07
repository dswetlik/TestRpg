A test build/experimental code for designing RPG systems

This is a Unity project written in C#. The idea behind this is to create an RPG system with scripts that are easily adaptable, easy to understand, and not too performance intenstive.

All of the scripting is located in Assets/Scripts/. It has some organization to it. You'll find the main scripts that run the game in /Game Management Scripts/ (Engine.cs, in particular). /Container Scripts/ contains container scripts made to contain ScriptableObjects for easy access to item, enemy, and skill database objects (ScriptableObjects) straight from GameObjects (generally UI). /Game Scripts/ and /Item Scripts/ could be put together, as they perform the same role, holding scripts that are just objects. Many of these scripts are children of ScriptableObject, and that's to be able to create and manipulate assets directly in the editor.

Some of the more interesting (in my opinion) scripts to look at are Game Management Scripts/Engine.cs, Item Scripts/Inventory.cs, and Game Scripts/Player.cs. This isn't to say that the other scripts aren't nice, a lot of them are just derived from ScriptableObject and as such are only used to create new instances of that object to act as a database for game values. They're pretty boring.

Don't be too surprised if the code (again, especially Engine.cs) is very messy. This idea has gone under several refactorings as my end-goal has changed over the past year or so. I have yet to go through and do some serious clean-up on code that is no longer used.

Right now the current idea is to make this into a "card-game Arena battle" idea, refactoring away from the previous idea of being a dungeon-crawler, which was a refactor of an open-world idea, which was a refactor of a more unique(?) idea. (Think kind of like Knights of Pen and Paper.) As you can probably tell, this is really a stomping ground for implementation and idea testing that might turn into something if I stop changing the idea.

Not all of the art assets used are my own. The textures and the unused creature .png files lurking in the folders are mine, and they are not great textures. I'm not much of a graphic designer or artist. Don't laugh too hard at the Rat.png, I tried my best. The art assets that are not my own are the icons (like the gold icons).
