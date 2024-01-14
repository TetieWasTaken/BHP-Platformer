using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    public bool isGrounded;
    public LayerMask groundLayer;
    public Vector2 boxSize = new Vector2(1, 0.1f);
    public float castDistance = 0.1f;

    public float cameraSpeed = 0.1f;
    public GameObject camera;

    public GameObject dialogueBoxManager;

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float moveBy = x * moveSpeed;
        rb.velocity = new Vector2(moveBy, rb.velocity.y);

        if (x > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (x < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isGrounded = Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer);

            if (isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
    }

    void LateUpdate()
    {
        camera.transform.position = Vector3.Lerp(camera.transform.position, new Vector3(transform.position.x, transform.position.y, -10), cameraSpeed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Trigger"))
        {
            other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            dialogueBoxManager.GetComponent<Dialogue>().NextLine();
        }
    }
}
