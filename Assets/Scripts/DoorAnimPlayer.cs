using UnityEngine;

public class DoorAnimPlayer : MonoBehaviour
{
    public GameObject door;

    public void Animate()
    {
        door.GetComponent<Animator>().SetBool("IsOpen", true);

        GetComponent<AudioSource>().Play();
    }

    public void AnimateReverse()
    {
        door.GetComponent<Animator>().SetBool("IsOpen", false);

        GetComponent<AudioSource>().Play();
    }
}
