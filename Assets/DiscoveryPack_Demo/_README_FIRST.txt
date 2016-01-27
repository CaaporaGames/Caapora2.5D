
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


    PopcornFX
        http://www.popcornfx.com
        http://www.facebook.com/3D.PopcornFX
        http://www.twitter.com/popcornfx

================================================================================

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

================================================================================

HOW TO IMPORT THIS PACKAGE IN UNITY?

This package contains several effects along with scenes to showcase their use in
Unity, a packaged version for use in the PopcornFX Editor and a free version of
the plugin.

You will need either the full PopcornFX Plugin or the included limited one for
this package to work in Unity.
The included assemblies are a free version that can only play signed effects.

To properly import the package with working sample scenes, proceed as follows:

   1. Import the FX Pack package (the package containing this README file).

   2. In your player settings make sure to set the color space to linear.

   4. Load a demo scene, hit play, enjoy the demo.

Starting with Unity 5, packages imports no longer overwrite existing assets,
so if you want to use the full plugin, you'll either have to delete the free
one first (which would break all the components in the prefabs and sample
scenes) or import the full plugin in another project and manually overwrite the
files with Unity closed (this won't break the components).

You can help us get this import issue noticed here :
feedback.unity3d.com/suggestions/ask-the-user-for-package-imports-behaviour-keep-both-or-overwrite
or there
forum.unity3d.com/threads/unity5-and-custom-packages-imports.311807/


================================================================================

HOW TO IMPORT THIS PACKAGE IN THE PopcornFX EDITOR?

There is a pkkg file in the StreamingAssets directory which contains the editor-
usable pack. To use it, proceed as follows:

	1. In the PopcornFX editor, on the project screen on startup, create a
	new project at your Unity project's root folder.

	2. Select the project and click Settings. Under "General", set the Axis
	System to LeftHand_Y_Up. Under Baking, add a new baking platform (protip:
	name it "Unity"), set the path to "../Assets/StreamingAssets/PackFx".

	3. Open your newly created project. In the main window right click and
	select import package. In the pop up explorer, select the pkkg file
	located in StreamingAssets.

	4. You need to manually rebuild the testArea backdrop mesh. To do so,
	locate the file under "Meshes", double click and then click "Ok" when
	asked if you want to rebuild it.

	5. To apply your modifications/additions to the package imported in
	Unity, select the appropriate effect(s) and right click -> Bake. Make
	sure you tick "Unity" and "Bake dependencies".

MODIFIED EFFECTS WON'T LOAD WITH THE FREE PLUGIN!
