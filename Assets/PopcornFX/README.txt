
                '      .'
           |   '|    .'
          . \_/  \_.',
    ..,_.' _____    (
     `.   |  __ \   '..o    o    o    o    o    o    o    o    o    o    o    o   o
       :  | |__) | ,' ___    _ __     ___    ___    _ __   _ __     _____  __   __
   ._-'   |  ___/ /  / _ \  | '_ \   / __/  / _ \  | '__| | '_ \   |  ___| \ \ / /
     `.   | |     ( | (_) | | |_) | | (_   | (_) | | |    | | | |  | |_     \ V /
     ,'   |_|    _`. \___/  | .__/   \___\  \___/  |_|    |_| |_|  |  _|     ) (
    ,.'"\  _.._ (  `'       |_|                                    | |      / ' \
   :'   | /    `' o    o    o    o    o    o    o    o    o    o   |_|     /_/ \_\
 ,"     ',
'       '
                                                      Realtime Particle FX Solution


    This program is the property of Persistant Studios SARL.

    PopcornFX
        http://www.popcornfx.com
        http://www.facebook.com/3D.PopcornFX
        http://www.twitter.com/popcornfx

=================================================================================

PopcornFX is a 3D realtime FX Solution for Games & Interactive applications.
PopcornFX is for studios, 3D FX artist, Technical Artist who are looking for a
powerful and complete tool exclusively dedicated to the creation of realtime
effects.

This plugin is to be used with the totally free PopcornFX editor which will
change your way of creating particle effects: better FX faster !!

The plugin allows you to run the FX created with the PopcornFX dedicated editor
in your Unity games.
We also have a free discovery FX pack available to get you started.
Modify it with the PopcornFX editor or directly use its effects in your games.

This plugin requires Unity Pro for versions of Unity prior to Unity 5.

Online Documentation : http://wiki.popcornfx.com/index.php/Unity

For bug reports/support, contact us on support@popcornfx.com or on our forum
thread : forum.unity3d.com/threads/pkfx.297938/

=================================================================================

Popcorn Unity Plugin v2.5

    Supported platforms :
        - Win (D3D9, D3D11, OpenGL)
        - Mac OSX
        - Android (with NEON support)
        - iOS

=================================================================================

HOW TO ?!?

    To add stunning FX to your Unity scene, proceed as follow :

        0. (If importing from the unitypackage file)
        In Unity, import the plugin package under Assets > Import Package >
        Custom Package then locate the plugin unitypackage file. Make sure you
        import everything.

        1. Create a new fx pack at your Unity project's root folder, name it
        anything but "PackFx".

        2. In your newly created pack's settings, make sure to set a new baking
        platform (PROTIP: name it Unity), set
	    "[UnityProjectRoot]/Assets/StreamingAssets/PackFx" for the target path.

        3. Make sure to set the axis system to "Axis_LeftHand_Y_Up" in the
        general settings.

        4. Make some awesome effects. Go crazy!

        5. Bake the effects you want to test in your Unity scenes, make sure you
        check the "Bake dependencies" checkbox. This should bake and copy all the
        required resources in the PackFx folder in the StreamingAssets, which is
        used by PopcornFX's runtime.

        6. In your scene, in the GameObject menu, click "Create PopcornFX" >
        "Empty Effect", drag and drop a pkfx file from the pack in the
        StreamingAssets directory in the "FX" slot.

        7. Attach the PKFxRenderingPlugin component to all your cameras you want
        effects in (or create a new one through "GameObject > Create PopcornFX >
        Camera"). Make sure to tick "Has postFx" if your cam also has image
        effects to prevent axis flipping.

        8. Hit run. You should now have effects in your camera's viewport.

    For a better rendering, make sure to set the color profile to linear in your
    project's player settings (see
    http://docs.unity3d.com/Manual/LinearLighting.html).

DEPLOYMENT :

        1. Make sure your baked PackFx is up-to-date.

    /!\ For android deployment :

            1.b In the "Assets" Menu, click "Android Deployment>Create PKFx
            Packs's Index". This will create the necessary index and hashes to
            copy the pack from the application package to the device's
            filesystem.

        2. Build your game and run it!

    /!\ For iOS deployment :

            2.b In Xcode, go to your project's "Build Phases" tab, under "Link
            Binary With Libraries", add an item and select "libc++.dylib".
            Your app should then compile and link correctly.

=================================================================================

CHANGELOG
    v2.5
        - Fixed a crash when deleting hot-reloaded effects.
        - Fixed a crash when calling StopEffect in OnDisable.
        - Fixed a crash when getting in and out of a scene with effects using
        spacial layers.
        - Fixed race-condition issues.
        - Fixed a bug where localspace effects and attributes were 1 frame behind
        - Added an Alive() method to the PKFxFX component that returns true as
        long as an effect is spawning particles.
        - Added the possibility to enable the killing of individual effects.
        - Added a main conf file for settings (holds the log in file and effects
        killing settings so far).
        - Added support of PopcornFX's sound layers (see online documentation).
        - Added support for audio waveform sampling.
        - Added support for audio sampling (waveform and spectrum) for iOS.
	- Added PKFxManager.UnloadEffect(string path);

    v2.4
        - Synchronized with PopcornFX Editor v1.8.X (make sure to upgrade your
          packs).
        - New fat library for ios64 support.
        - Fixed a bug where PopcornFX components would interfere with other
          plugins' components.
        - Effects hot-reload in Windows and MacOSX editor mode.
        - Int/Int2/3/4 attributes support.
        - Float attributes now support min/max values.

    v2.3
        - Fixed a bug where fx wouldn't load in specific pack architectures.
        - Added PKFxManager.GetStats() to retrieve stats from the PopcornFX run-
          time.
        - Added a link to the online documentation in the Help menu ("PopcornFX
          Wiki").
        - Added a version identifier in the PKFxRenderingPlugin component's
          inspector view.
        - Added "PopcornFX Settings" in the Edit menu with options to enable or
          disable the runtime log to a file (requires a restart to take effect).
        - Added "Create PopcornFX" in the GameObject menu to create empty
          effects or PopcornFX-enabled cameras.
        - Fixed UI refresh after drag'n'drop of a pkfx file in the FX field of
          a PKFxFX component.
        - Added a warning when in editor if the color space is not set to linear

    v2.2
        - Fixed audio sampling on MacosX
        - Fixed DX11 LOD bias
        - OSX binaries now Universal (x86 and x86_64)
        - Soft animation blending now supported in GL/DX9/DX11
        - Fixed DX11 depth texture update bug (soft particles and distortion)
        - Fixed component.camera api deprecation for Unity 5
        - Drag'n'drop .pkfx files on FX components instead of the unconvenient
          list

    v2.1
        - Proper color space detection (sRGB/gamma correction)
        - Distortions blur pass (Blue channel)
        - Fixed distortions bug
        - Massive renames to comply with naming convention
          /!\   /!\   /!\   /!\   /!\   /!\   /!\   /!\   /!\   /!\   /!\   /!\
          /!\/!\/!\ THIS WILL BREAK MANY PREEXISTING FX COMPONENT!    /!\/!\/!\
          /!\   /!\   /!\   /!\   /!\   /!\   /!\   /!\   /!\   /!\   /!\   /!\
        - DX11 bugfixes, proper distortions and soft particles
        - Fixed mobile crash on some sampling functions
        - Masked event ids to prevent interfering with other native plugins
        - Fixed bug where additive meshes were never culled
	    - Fixed lost devices in DX9
	    - Fixed depth/distortion FOV bug
	    - Fixed distortion viewport bug
	    - Windows x32/x64 support.

    v2.0
        - Distortions!
        - Mobile depth-related rendering features (soft particles, distortion).

        - PackFX hierarchy : effects are now accessible at any pack subdirectory
        - Packs must now be baked in the StreamingAssets directory.
          /!\   /!\   /!\   /!\   /!\   /!\   /!\   /!\   /!\   /!\   /!\   /!\
          /!\/!\/!\ THIS WILL BREAK ANY PREEXISTING FX COMPONENT!     /!\/!\/!\
          /!\   /!\   /!\   /!\   /!\   /!\   /!\   /!\   /!\   /!\   /!\   /!\

    v1.08
        - Refactoring of the PKFxManager static class to account for iOS' static
        libraries limitation.

=================================================================================

Known bugs.

    - VelocityCapsuleAligned billboarders produce visual glitches on Android.
