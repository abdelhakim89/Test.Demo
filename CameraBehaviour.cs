using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [Header("Camera Settings")]
    public Transform playerPosition;
    private Vector3 velocity = Vector3.zero;
    public Vector3 offset;
    public float distance;
    private void LateUpdate()
    {
        if (playerPosition != null)
        {
            transform.position = Vector3.SmoothDamp(transform.position, playerPosition.position +
                                                    offset * distance, ref velocity,Time.deltaTime);
            transform.LookAt(playerPosition);
        }
    }    
}
