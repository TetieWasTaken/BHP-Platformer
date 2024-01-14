using UnityEngine;
using System.Collections.Generic;

public class Cube : MonoBehaviour
{
    public GameObject player;
    public GameObject forceField;
    public GameObject E_Text;
    public Texture2D cursorCubeTexture;
    private Texture2D defaultCursorTexture;

    public float interactDistance = 0.4f;

    public bool isHeld = false;
    private LineRenderer lineRenderer;

    // interactactable/??!?!?!?!?!?!?!?!?
    private bool interactactableInRange = false;
    
    private GameObject rb;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;

        GameObject camera = GameObject.Find("Main Camera");

        defaultCursorTexture = camera.GetComponent<CameraScript>().cursorTexture;
    }

    private void FixedUpdate() {
        if (isHeld) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            Vector3 playerPos = forceField.transform.position;
            playerPos.z = 0;

            if (Vector3.Distance(mousePos, playerPos) > 2.5) {
                mousePos = playerPos + (mousePos - playerPos).normalized * 2.5f;
            }

            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            // GetComponent<Rigidbody2D>().MovePosition(mousePos);

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactDistance);
            foreach (Collider2D collider in colliders) {
                if (collider.gameObject.tag == "Interactable") {
                    interactactableInRange = true;
                    transform.position = collider.transform.position + new Vector3(0, 0.7f, collider.transform.position.z * -1 + 1f);
                }
            }

            Debug.Log(interactactableInRange);

            if (!interactactableInRange) {
                GetComponent<Rigidbody2D>().MovePosition(mousePos);
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            } else {
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);    
            }

            interactactableInRange = false;

            lineRenderer.SetPosition(0, playerPos);
            lineRenderer.SetPosition(1, transform.position);

            if (Input.GetKeyDown(KeyCode.E)) {
                isHeld = false;

                Cursor.SetCursor(defaultCursorTexture, Vector2.zero, CursorMode.Auto);

                GetComponent<Rigidbody2D>().gravityScale = 1;
                Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);
                forceField.SetActive(false);
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);

                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
            }
        } else if (Vector3.Distance(transform.position, player.transform.position + new Vector3(0, 0.6f, 0)) < 2.5) {
            E_Text.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E)) {
                isHeld = true;

                Cursor.SetCursor(cursorCubeTexture, Vector2.zero, CursorMode.Auto);

                GetComponent<Rigidbody2D>().gravityScale = 0f;
                Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                forceField.SetActive(true);

                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

                GetComponent<AudioSource>().Play();
            }
        } else {
            E_Text.SetActive(false);
        }
    }
}
