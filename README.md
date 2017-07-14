# Unity-LOD-SkinnedMeshRenderer
A LOD (Level of detail) implementation in Unity 5 for SkinnedMeshRenderers

## Instructions
* Import the Unity .unitypackage into your project
* Add the SkinnedMeshUpdater.cs script to your character prefab
* Fill in the parameters (view the tootltips to get context)
* Generate the skeleton after the skinnedMeshRenderers has been updated
* Apply the prefab.

## Requirements
* Pre-decimated models (bones may differ)

## Demo
* In the DEMO a folder an example scene exists, extract it to an existing project to view
* An example build can be found at [itch.io](https://aghmat.itch.io/unity-lod-skinnedmeshrenderer)

### Inspector

![alt text](https://github.com/Aghmat/Unity-LOD-SkinnedMeshRenderer/blob/master/IMAGES/inspector.JPG)

### TODO
* Implement crossfade to ensure less noticable transitions
