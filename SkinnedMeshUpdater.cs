using UnityEngine;

public class SkinnedMeshUpdater : MonoBehaviour
{
    private GameObject player;
    private const string distanceCheck = "DistanceCheck";
    private int[] LOD; //LOD indexes
    private int currentLOD; //active LOD
    private float[] distances; //LOD distance range

    [Tooltip("The name of the player gameobject")]
    public string playerString;
    [Tooltip("Drag the relevent SkinnedMeshRenderer from the project starting from LOD_0 to LOD_n.")]
    public SkinnedMeshRenderer[] skinnedMeshRenderers;
    [Tooltip("Assigned via the Generate Skeletons button, should be updated when a new SkinnedMeshRenderer is added.")]
    public LODParameters[] newSkeleton_LOD;
    [Tooltip("Check if you want to update the material with the mesh.")]
    public bool updateMaterial;
    [Tooltip("Interval at which distances is calculated [LOD_0 = distanceInterval, LOD_n = distanceInterval * n]")]
    public float distanceInterval;
    [Tooltip("How often checks are performed in seconds")]
    public float invokeRate;


    private void Start()
    {
        player = GameObject.Find(playerString);
        LOD = new int[skinnedMeshRenderers.Length];
        distances = new float[skinnedMeshRenderers.Length - 1];

        //assigns the LOD levels
        for (int i = 0; i < skinnedMeshRenderers.Length; ++i)
        {
            LOD[i] = i;
        }
        //assigns rayDistances
        for (int i = 0; i < distances.Length; ++i)
        {
            distances[i] = distanceInterval * (i + 1);
        }

        currentLOD = LOD[0];
    }

    private void Update()
    {
        if (!IsInvoking(distanceCheck))
            Invoke(distanceCheck, invokeRate);
    }

    void DistanceCheck()
    {
        //checks the distance from enemy to player ignoring all other layers to determine LOD
        if (player != null)
        {
            //only determines n - 1 LOD levels where if the ray does not find the player it will default to the 
            //highest LOD level
            for (int i = 0; i < distances.Length; ++i)
            {
                if (Vector3.Distance(player.transform.position, transform.position) < distances[i])
                {
                    if (!currentLOD.Equals(LOD[i])) //will only update if it is not already set 
                    {
                        currentLOD = LOD[i];
                        UpdateMeshRenderer(currentLOD);
                    }

                    return;
                }
            }
            //defaults to the highest LOD level
            if (!currentLOD.Equals(LOD[LOD.Length - 1]))
            {
                currentLOD = LOD[LOD.Length - 1];
                UpdateMeshRenderer(currentLOD);
            }
        }
    }

    //tsubaki https://gist.github.com/tsubaki/ea6ece1cd9a851ff977e#file-skinnedmeshupdater-cs
    public void UpdateMeshRenderer(int lod)
    {
        // update mesh
        SkinnedMeshRenderer meshrenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        meshrenderer.sharedMesh = skinnedMeshRenderers[lod].sharedMesh;

        if (updateMaterial)
            meshrenderer.sharedMaterial = skinnedMeshRenderers[lod].sharedMaterial;

        // update bones
        meshrenderer.bones = newSkeleton_LOD[lod].bones;
    }
}
