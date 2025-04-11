using UnityEngine;
using System.Collections.Generic;

public class ScrewFacePlacer : MonoBehaviour
{
    public GameObject screwPrefab;
    public float offsetDistance = 0.01f;
    public int screwCount = 10;

    // Tweak this to match the direction you want screws to face
    public Vector3 preferredDirection = Vector3.forward;
    public float angleThreshold = 45f; // degrees

    void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null) return;

        Mesh mesh = meshFilter.mesh;

        Vector3[] vertices = mesh.vertices;
        Vector3[] normals = mesh.normals;
        int[] triangles = mesh.triangles;

        List<int> validTriangleIndices = new List<int>();

        // Step 1: Collect valid triangles based on normal direction
        for (int i = 0; i < triangles.Length; i += 3)
        {
            Vector3 n0 = normals[triangles[i]];
            Vector3 n1 = normals[triangles[i + 1]];
            Vector3 n2 = normals[triangles[i + 2]];
            Vector3 avgNormal = (n0 + n1 + n2).normalized;

            Vector3 worldNormal = transform.TransformDirection(avgNormal);
            float angle = Vector3.Angle(worldNormal, transform.TransformDirection(preferredDirection));

            if (angle <= angleThreshold)
            {
                validTriangleIndices.Add(i);
            }
        }

        // Step 2: Randomly pick from valid triangles
        for (int i = 0; i < screwCount && validTriangleIndices.Count > 0; i++)
        {
            int triIndex = validTriangleIndices[Random.Range(0, validTriangleIndices.Count)];

            Vector3 v0 = vertices[triangles[triIndex]];
            Vector3 v1 = vertices[triangles[triIndex + 1]];
            Vector3 v2 = vertices[triangles[triIndex + 2]];

            Vector3 n0 = normals[triangles[triIndex]];
            Vector3 n1 = normals[triangles[triIndex + 1]];
            Vector3 n2 = normals[triangles[triIndex + 2]];

            Vector3 bary = GetRandomBarycentric();
            Vector3 localPos = v0 * bary.x + v1 * bary.y + v2 * bary.z;
            Vector3 localNormal = (n0 * bary.x + n1 * bary.y + n2 * bary.z).normalized;

            Vector3 worldPos = transform.TransformPoint(localPos);
            Vector3 worldNormal = transform.TransformDirection(localNormal);
            Vector3 screwPos = worldPos + worldNormal * offsetDistance;

            Quaternion screwRot = Quaternion.LookRotation(-worldNormal);
            screwRot *= Quaternion.Euler(-90f, 0f, 0f); // adjust this based on your screw prefab

            Instantiate(screwPrefab, screwPos, screwRot, transform);
        }
    }

    Vector3 GetRandomBarycentric()
    {
        float u = Random.value;
        float v = Random.value;
        if (u + v > 1f)
        {
            u = 1f - u;
            v = 1f - v;
        }
        return new Vector3(1 - u - v, u, v);
    }
}
