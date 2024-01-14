using UnityEngine;

public class TransitionScript : MonoBehaviour
{
    public float cameraSpeed = 2f;
    public GameObject camera;

    void LateUpdate()
    {
        camera.transform.position = Vector3.Lerp(camera.transform.position, new Vector3(transform.position.x, transform.position.y, -10), cameraSpeed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Trigger"))
        {
            // Load next level
            Debug.Log("NEXT LEVEL");
        }
    }
}
