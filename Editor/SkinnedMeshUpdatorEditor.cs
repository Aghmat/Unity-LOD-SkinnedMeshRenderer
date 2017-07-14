using UnityEngine;
using System.Collections;
using UnityEditor;
using System;

[CustomEditor(typeof(SkinnedMeshUpdater))]
public class SkinnedMeshUpdatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SkinnedMeshUpdater skinnedMeshUpdater = (SkinnedMeshUpdater) target;
        if (GUILayout.Button("Generate Skeletons"))
        {
            skinnedMeshUpdater.newSkeleton_LOD = new LODParameters[skinnedMeshUpdater.skinnedMeshRenderers.Length];

            for (int i = 0; i < skinnedMeshUpdater.skinnedMeshRenderers.Length; ++i)
            {
                skinnedMeshUpdater.newSkeleton_LOD[i].bones = SortedBones(skinnedMeshUpdater.skinnedMeshRenderers[i], skinnedMeshUpdater.gameObject);
            }
        }
    }

    public Transform[] SortedBones(SkinnedMeshRenderer newMeshRenderer, GameObject go)
    {
        Transform[] childrenTransforms = go.transform.GetComponentsInChildren<Transform>(true);

        // sort bones.
        Transform[] bones = new Transform[newMeshRenderer.bones.Length];
        for (int boneOrder = 0; boneOrder < newMeshRenderer.bones.Length; boneOrder++)
        {
            bones[boneOrder] = Array.Find<Transform>(childrenTransforms, c =>
                                                     c.name == newMeshRenderer.bones[boneOrder].name);
        }

        return bones;
    }
}
