using UnityEngine;
using System.Linq;

public class Fan : MonoBehaviour
{
    public float power = 5f;
    public float height = 5f;
    public float width = 1f;
    public float rotation = 0f;

    public bool isEnabled = true;

    public GameObject particleObject;
    // public float particleSpeed = 5f;
    private float particleSpeed;

    // todo: what

    private GameObject lastCube;


    void Start()
    {
        rotation = transform.rotation.eulerAngles.z;
        particleSpeed = power;
    }

    void FixedUpdate()
    {
        if (!isEnabled)
        {
            return;
        }

        // uhhhhhhhhhhhhhhhhhhhhhhhh


        if (rotation == 0f)
        {
            particleObject.transform.Translate(new Vector3(0, particleSpeed * Time.deltaTime, 0));

            if (particleObject.transform.position.y > transform.position.y + height)
            {
                particleObject.SetActive(false);
                particleObject.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                particleObject.SetActive(true);
            }
        }
        else if (rotation == 90f)
        {
            particleObject.transform.Translate(new Vector3(0, particleSpeed * Time.deltaTime, 0));

            if (particleObject.transform.position.x < transform.position.x - height)
            {
                particleObject.SetActive(false);
                particleObject.transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
                particleObject.SetActive(true);
            }
        }
        else if (rotation == 180f)
        {
            particleObject.transform.Translate(new Vector3(0, -particleSpeed * Time.deltaTime, 0));

            if (particleObject.transform.position.y < transform.position.y - height)
            {
                particleObject.SetActive(false);
                particleObject.transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
                particleObject.SetActive(true);
            }
        }
        else if (rotation == 270f)
        {
            particleObject.transform.Translate(new Vector3(0, particleSpeed * Time.deltaTime, 0));

            if (particleObject.transform.position.x > transform.position.x + height)
            {
                particleObject.SetActive(false);
                particleObject.transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
                particleObject.SetActive(true);
            }
        }

        Collider2D[] colliders = new Collider2D[0];

        if (rotation == 0f)
        {
            colliders = Physics2D.OverlapAreaAll(new Vector2(transform.position.x - width / 2, transform.position.y + 2), new Vector2(transform.position.x + width / 2, transform.position.y + 1 + height));
        }
        else if (rotation == 90f)
        {
            colliders = Physics2D.OverlapAreaAll(new Vector2(transform.position.x + 2, transform.position.y + width / 2), new Vector2(transform.position.x - 1 - height, transform.position.y + width / 2));
        }
        else if (rotation == 180f)
        {
            colliders = Physics2D.OverlapAreaAll(new Vector2(transform.position.x - width / 2, transform.position.y - 2), new Vector2(transform.position.x + width / 2, transform.position.y - 1 - height));
        }
        else if (rotation == 270f)
        {
            colliders = Physics2D.OverlapAreaAll(new Vector2(transform.position.x - 2, transform.position.y + width / 2), new Vector2(transform.position.x + 1 + height, transform.position.y + width / 2));
        }

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.tag == "Player" || collider.gameObject.tag == "Cube")
            {
                if (collider.gameObject.tag == "Cube")
                {
                    lastCube = collider.gameObject;

                    if (!collider.gameObject.GetComponent<Cube>().isHeld)
                    {
                        collider.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                    }
                }

                if (rotation == 0f)
                {
                    collider.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(collider.gameObject.GetComponent<Rigidbody2D>().velocity.x, power);
                }
                else if (rotation == 90f)
                {
                    collider.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-power, collider.gameObject.GetComponent<Rigidbody2D>().velocity.y);
                }
                else if (rotation == 180f)
                {
                    collider.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(collider.gameObject.GetComponent<Rigidbody2D>().velocity.x, -power);
                }
                else if (rotation == 270f)
                {
                    collider.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(power, collider.gameObject.GetComponent<Rigidbody2D>().velocity.y);
                }
            }
        }

        if (lastCube != null && !colliders.Contains(lastCube.GetComponent<Collider2D>()))
        {

            if (!lastCube.GetComponent<Cube>().isHeld)
            {
                lastCube.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        if (rotation == 0f)
        {
            Gizmos.DrawWireCube(new Vector2(transform.position.x, transform.position.y + 1 + height / 2), new Vector2(width, height));
        }
        else if (rotation == 90f)
        {
            Gizmos.DrawWireCube(new Vector2(transform.position.x - 1 - height / 2, transform.position.y), new Vector2(height, width));
        }
        else if (rotation == 180f)
        {
            Gizmos.DrawWireCube(new Vector2(transform.position.x, transform.position.y - 1 - height / 2), new Vector2(width, height));
        }
        else if (rotation == 270f)
        {
            Gizmos.DrawWireCube(new Vector2(transform.position.x + 1 + height / 2, transform.position.y), new Vector2(height, width));
        }
    }
}
