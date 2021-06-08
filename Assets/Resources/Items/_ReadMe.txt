Inventory Sorting with Item IDs

*** Remember to create the item assets with an ID that corresponds to the alphabetical order of the asset! ***

For Example:
	Name:	ID:
	Apple	0
	Banana	1
	Bone	2
	Bow		3
	Zebra	4

This will ensure that the inventory is always listed out in alphabetical order.

This issue is due to the way that the inventory system is set up, using dual SortedDictionaries to manage the inventory. The SortedDictionaries are sorted automatically based on the ID of the object, so keeping the IDs of the item assets sorted will ensure that the inventory remains in alphabetical order.

The easiest way to ensure this further is to seperate out different object types to a designated range of IDs, such as:

	Type:		ID Range:
	Misc 		0-99
	Weapons		100-299
	Armor		300-499
	Consumables	500-599

This will give extra room to add items without the need to shift every single item over one. This, of course, only holds up so long as no more than 100 IDs are needed for Miscellaneous items, 200 IDs for Weapons, etc..

Thank you for reading!