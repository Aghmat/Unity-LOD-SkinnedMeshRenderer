using System.Collections.Generic;
using UnityEngine;

//Used to determine skinned mesh renderers that have LOD
[System.Serializable]
public struct LODParameters
{
    public Transform[] bones;
}
