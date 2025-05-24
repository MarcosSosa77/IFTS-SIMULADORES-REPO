using UnityEngine;

public class FaceToPlayer : MonoBehaviour
{
    [SerializeField]private Transform mainCamera;  

    void LateUpdate()
    {
        if (mainCamera != null)
        {
            transform.LookAt(mainCamera);
            transform.Rotate(0, 180f, 0); // Optional: flip if needed
        }
    }
}
