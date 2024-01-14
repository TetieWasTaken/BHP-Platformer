using UnityEngine;

public class InvisWallEffect : MonoBehaviour
{
    public GameObject player;
    public float distanceScale = 6f;

    void Update()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position - new Vector3(0, 3f, 0));
        float alpha = 1 - (distance / distanceScale);

        GetComponent<SpriteRenderer>().color = new Color(167, 240, 255, alpha);
    }
}
