using UnityEngine;
using UnityEngine.SceneManagement;

public class DeLorean : MonoBehaviour
{
    public GameObject dialogueBoxManager;
    public GameObject explosionEffect;

    private float timer = 0f;
    private bool timerEnabled = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (SceneManager.GetActiveScene().name == "Introduction") {
            if (other.gameObject.CompareTag("Trigger"))
            {
                other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                dialogueBoxManager.GetComponent<Dialogue>().NextLine();
            }
            else if (other.gameObject.CompareTag("Explosion"))
            {
                explosionEffect.SetActive(true);
                GetComponent<SpriteRenderer>().enabled = false;

                Invoke("LoadNextScene", 0.5f);
            }
        } else if (SceneManager.GetActiveScene().name == "Level04") {

            if (other.gameObject.CompareTag("Player"))
            {
                timerEnabled = true;
                timer = 0f;
            }
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Level04") {
            if (timerEnabled)
            {
                timer += Time.deltaTime;
                if (timer >= 3f)
                {
                    timerEnabled = false;
                    timer = 0f;
                    GameObject.Find("DeLorean").GetComponent<Animator>().enabled = true;
                    GameObject.Find("HeroKnight").SetActive(false);
                    GameObject.Find("Main Camera").GetComponent<CameraScript>().player = GameObject.Find("DeLorean");
                }
            }

            if (GameObject.Find("DeLorean").transform.position.x >= 170f) {
                SceneManager.LoadScene("Credits");
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (SceneManager.GetActiveScene().name == "Level04") {
            if (other.gameObject.CompareTag("Player"))
            {
                timerEnabled = false;
                timer = 0f;
            }
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene("Level01");
    }
}
