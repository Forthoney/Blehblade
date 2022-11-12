# Scripts Manual
## Object Scripts
Any non-static item belongs to the interface `IInteractiveObject`.
The interface only has an `Activate` method.
There are three classes in the interface:
### `InteractableObject`
This is the standard script which can be picked up and placed somewhere to
trigger a collider. 
Any object that is to be picked up and **used** by the player has this script attached.
### `ReactionObject`
This script attaches to items which do not react to user input directly.
Instead, it performs some behavior when the user successfully uses a different item.
This behavior is usually changing sprites or moving to somewhere else
### `UnholdableObject`
This script attaches to items that the player can use by simply clicking on it.
The item does not move to the player's inventory, but rather immediately responds to a click.

# Prefabs
In the editor, each of the script correspond to a prefab/prefab variant.
The prefabs are located in the `Assets/Prefabs/InteractionObject` folder

## Object-Collider Interaction
Instead of having to name every collider, I automatically made it so that an `InteractableObject` will recognize
a prefab with the name "<Object>Target" where <Object> is the name of the `InteractableObject`.
For example, the "Cheese" object's corresponding collider is the "CheeseTarget".
**You must name the collider this way.**

## Customizing Prefab Behavior
For an object to change its sprite after it has been activated, you must check the "Change Material"
checkbox and give it an appropriate material.

To activate some other InteractionObjects upon use, include the triggered objects in the "Trigger Objects"
list of the triggering object.