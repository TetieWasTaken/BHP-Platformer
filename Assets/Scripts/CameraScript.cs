using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float cameraSpeed = 7f;
    public GameObject player;
    public GameObject POI;
    public Texture2D cursorTexture;

    public float offsetX = 0;
    public float offsetY = 2;
    public float lookAhead = 1.5f;

    public float minX = -100;
    public float maxX = 100;
    public float minY = -100;
    public float maxY = 100;

    // public bool isFollowingPlayer = true;

    private float desiredX;
    private float desiredY;
    private float lastKnownY = 0;

    public bool disablePlayerBasedFunctions = false;
    public float POIBias = 0;
    public bool useAutoBias = false;
    public float autoBiasExponent = 1.5f;

    void Start()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }

    void LateUpdate()
    {
        if (!POI)
        {
            desiredX = player.transform.position.x + offsetX;
            desiredY = offsetY + lastKnownY;

            if (!disablePlayerBasedFunctions)
            {
                // Look ahead
                if (player.GetComponent<HeroKnight>().m_facingDirection == 1)
                {
                    desiredX += lookAhead;
                }
                else if (player.GetComponent<HeroKnight>().m_facingDirection == -1)
                {
                    desiredX -= lookAhead;
                }

                // Follow player on ground or when out of range
                if (player.GetComponent<HeroKnight>().m_grounded || player.transform.position.y > transform.position.y + 4 || player.transform.position.y < transform.position.y - 3)
                {
                    lastKnownY = player.transform.position.y;
                }
            }

            desiredX = Mathf.Clamp(desiredX, minX, maxX);
            desiredY = Mathf.Clamp(desiredY, minY, maxY);
        }
        else
        {
            if (useAutoBias)
            {
                POIBias = Mathf.Clamp(Mathf.Pow(Vector2.Distance(player.transform.position, POI.transform.position), autoBiasExponent), 0, 1000);
                // reverse bias
                POIBias = 1000 - POIBias;

            }

            desiredX = Mathf.Lerp(player.transform.position.x + offsetX, POI.transform.position.x, POIBias / 1000);
            desiredY = Mathf.Lerp(player.transform.position.y + offsetY, POI.transform.position.y, POIBias / 1000);
        }

        transform.position = new Vector3(
            Mathf.Lerp(transform.position.x, desiredX, cameraSpeed * 0.5f * Time.deltaTime),
            Mathf.Lerp(transform.position.y, desiredY, cameraSpeed * Time.deltaTime),
            -10
        );
    }
}
