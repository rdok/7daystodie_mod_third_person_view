# 7 Days to Die Mod - Third Person View
[![nexus-mods-collection-immersive-hud](https://img.shields.io/badge/Nexus%20Mods%20Collection-Immersive%20HUD%20-orange?style=flat-square&logo=spinrilla)](https://next.nexusmods.com/7daystodie/collections/epfqzi) [![nexus-mods-page](https://img.shields.io/badge/Nexus%20Mod-Third%20Person%20View%20-orange?style=flat-square&logo=spinrilla)](https://www.nexusmods.com/7daystodie/mods/5903) [![github-repository](https://img.shields.io/badge/GitHub-Repository-green?style=flat-square&logo=github)](https://github.com/rdok/7daystodie_mod_third_person_view)

> Enables the third person view mode; now with support for controllers.  
> **EAC:** This mod uses custom code that is not compatible with Easy Anti-Cheat.
 
[![Third Person View](https://raw.githubusercontent.com/rdok/7daystodie_mod_third_person_view/main/documentation/showcase.gif)](https://www.nexusmods.com/7daystodie/mods/5903)

## Features
- Activates third-person view mode with the Alt key.
  - Support for controllers: double tap whatever you sneak toggle is mapped to. By default, is the right analog stick.
- To remap the Alt key: go to Options -> Keyboard/Mouse -> Edit Mode -> Change Camera.
  - This is especially recommended if you use Alt+Tab to switch between windows.
  - If you are interested to remap this for the controller as well, please leave a comment with your use case.
- It's recommended to use [Dot Crosshair](https://www.nexusmods.com/7daystodie/mods/5640) and [Immersive Crosshair](https://www.nexusmods.com/7daystodie/mods/5601) for a better experience while in third-person mode.
  - These mods will properly hide the crosshair when in first person view, while enabling when in third person view, giving you the best of both worlds.
  - Refer to the [screenshots tab](https://staticdelivery.nexusmods.com/mods/1059/images/5903/5903-1724496760-686489418.jpeg) for examples.
- Game Version: 1.0. Install with [Vortex](https://www.nexusmods.com/about/vortex/).
   
### Incomplete third person view
> Note that the third-person view is incomplete, as the game developers have not fully implemented it  
- Bow animations, are unfinished (static). 
- Although knife animations are working, they are not hitting anything. Thanks to [lM3Rl](https://www.nexusmods.com/7daystodie/mods/5903?tab=posts&jump_to_comment=143233440) for reporting this issue.
  - Power attacks are not hitting enemies.
- When I spam power attack with club, animation stops. Thanks to [MrSamuelAdams1992](https://www.nexusmods.com/7daystodie/mods/5903?tab=posts&jump_to_comment=143252268) fore reporting this issue.
- Impact drive not dissembling. Thanks to [kiosuku1943](https://www.nexusmods.com/7daystodie/mods/5903?tab=posts&jump_to_comment=143263098) for reporting this issue.
- Missing sound when using wrench on cars. Thanks to [Splico](https://www.nexusmods.com/7daystodie/mods/5903?tab=posts&jump_to_comment=143244357) for reporting this issue.
- Jittery movement when moving sideways.
- I'll update this as we find more issues. I'll further update this mod to fix anything code related; for the missing animations, however, someone else with animation expertise can create a mod instead.  


### Patched
This mod fixes the following issues, so far, with third person view:
  - Enables the step sounds, which have been disabled by the game developers when not in first person view.
  - Support for controllers.

## Changelog
#### v1.2.1 25-Aug-24
- fix: Double tap crouching should only toggle view when using controllers, not a keyboard.
#### v1.2.0 25-Aug-24
- feat: Support for controllers: double tap whatever you sneak toggle is mapped to. By default, is the right analog stick.
#### v1.1.0 24-Aug-24
- feat: Enables the step sounds, which have been disabled by the game developers when in third person view.
#### v1.0.1 24-Aug-24
- fix: Camera not changing to third person view, when having a first person view and getting on a vehicle.
#### v1.0.0 24-Aug-24
- feat: Enable the change camera used in edit mode without requiring the debug mode on.

## Pipeline
> Ordered by priority  
- Hack to go around the issue with equipment not working, or having no animations:
  - Whenever the player uses a such equipment, force the camera back to first person. 
  - On next iteration, integrate Gears, to allow the players to disable this feature if they don't want to use it.
  - [Thanks & Credits for the idea to tositek](https://www.nexusmods.com/7daystodie/mods/5903?tab=posts&jump_to_comment=143244432)
- Knife attacks are not interacting/hitting. [Source](https://www.nexusmods.com/7daystodie/mods/5903?tab=posts&jump_to_comment=143245773)
- Power attacks not hitting. 
- Impact drive not dissembling. 
- Missing sound when using wrench on cars.
- Jittery movement when moving sideways. Thanks to [MrSamuelAdams1992](https://www.nexusmods.com/7daystodie/mods/5903?tab=posts&jump_to_comment=1432522680) for reporting this issue.
- Missing animations for bows; help needed, or some other developer to create this mod separately.
- After releasing the zoom in button holding a weapon, the game snaps the camera too fast. Smoothen this out to improve the UX.
- Provide the options to zoom in or out.