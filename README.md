# About 

A Simple Action RPG Game based on Tupi-Guarani mythology in Brazil made on top of Unity 3D using C# 

## Note: Cyanogen users
O audio pode não funcionar corretamente em Androids com CyanogenMod por conta de um bug em um módulo que habilita automaticamente a funcionalidade Audio Fast Path

The audio may not work properly on Devices that uses CyanogenMod due to a bug on a module that automaticly enable the Audio Fast Path  

para corrigir:
To fix it:

1. (optional) adb shell pm list features shows `feature:android.hardware.audio.low_latency`
1. Rename/remove `/system/etc/permissions/android.hardware.audio.low_latency.xml`
1. reboot

mas informações em: http://forum.unity3d.com/threads/android-sound-problem.359341/page-2

# Features


1. Unity UI Screens 
1. Loading Screen with Loading Bar
1. Pause
1. Combat Text and Hit Effect
1. Conversation Panel
1. Background Music and some audio effects
1. Level System
1. Life Bar with Unity UI    
1. Isometric 2.5D World with Isometric 2.5D Toolset asset
1. Lighting 
1. Sprite Based Character with WASD Control
1. Mobile Suport and Touch Jostick
1. AI Pathfinding ( adapted from Sebastian Lague Youtube tutorial https://www.youtube.com/watch?v=nhiFx28e7JY ) 
1. Some Mecanisms using  Collision detection
1. Inventory, Stats Controller, Sound Controller Scripts
1. Day Night Cycle 
1. Simple Object Pooling System
1. Scenario Presention


# Screenshots

![Image of Caapora](https://github.com/romulolink/Caapora2.5D/blob/master/Caapora0.png)

![Image of Caapora](https://github.com/romulolink/Caapora2.5D/blob/master/Caapora1.png)

![Image of Caapora](https://github.com/romulolink/Caapora2.5D/blob/master/Caapora2.png)

![Image of Caapora](https://github.com/romulolink/Caapora2.5D/blob/master/Caapora3.png)