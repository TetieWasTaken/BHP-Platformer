using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public GameObject[] door;
    public GameObject[] fan;
    
    public GameObject activationArea;

    public bool invert = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Cube"))
        {
            // Debug.Log("Button pressed");

            activationArea.GetComponent<Animator>().SetBool("IsPressed", true);

            // Debug.Log("Door length: " + door.Length);
            // Debug.Log("Fan length: " + fan.Length);

            if (door.Length > 0) {
                for (int i = 0; i < door.Length; i++) {
                    door[i].GetComponent<DoorAnimPlayer>().Animate();
                }
            }
            
            if (fan.Length > 0) {
                for (int i = 0; i < fan.Length; i++) {
                    // Debug.Log("Activating fan no. " + i);
                    /* if (invert) {
                        fan[i].GetComponent<Fan>().isEnabled = true;
                    } else {
                        Debug.Log("Disabling fan no. " + i);

                        fan[i].GetComponent<Fan>().isEnabled = false;
                    } */

                    fan[i].GetComponent<Fan>().isEnabled = !fan[i].GetComponent<Fan>().isEnabled;
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Cube"))
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);
            foreach (Collider2D collider in colliders) {
                if (collider.gameObject.CompareTag("Player") || collider.gameObject.CompareTag("Cube")) {
                    return;
                }
            }

            activationArea.GetComponent<Animator>().SetBool("IsPressed", false);
            if (door.Length > 0) {
                for (int i = 0; i < door.Length; i++) {
                    door[i].GetComponent<DoorAnimPlayer>().AnimateReverse();
                }
            } 
            
            if (fan.Length > 0) {
                for (int i = 0; i < fan.Length; i++) {
                    /* if (invert) {
                        fan[i].GetComponent<Fan>().isEnabled = false;
                    } else {
                        fan[i].GetComponent<Fan>().isEnabled = true;
                    } */

                    fan[i].GetComponent<Fan>().isEnabled = !fan[i].GetComponent<Fan>().isEnabled;
                }
            }
        }
    }
}
