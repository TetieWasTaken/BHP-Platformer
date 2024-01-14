using UnityEngine;
using UnityEngine.SceneManagement;
using System;


public class Finish : MonoBehaviour
{
    public GameObject camera;
    public GameObject player;

    public float savedOffsetY;
    public float savedOrthographicSize;
    private bool isTriggered = false;

    private bool hasPlayed = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player")
        {
            return;
        }

        savedOffsetY = camera.GetComponent<CameraScript>().offsetY;
        savedOrthographicSize = camera.GetComponent<Camera>().orthographicSize;

        isTriggered = true;
        camera.GetComponent<CameraScript>().POI = this.gameObject;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag != "Player")
        {
            return;
        }

        isTriggered = false;

        camera.GetComponent<CameraScript>().offsetY = savedOffsetY;
        camera.GetComponent<Camera>().orthographicSize = savedOrthographicSize;

        camera.GetComponent<CameraScript>().POI = null;
    }

    void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // SceneManager.LoadScene("Level0" + (currentSceneIndex + 1));
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    void Update()
    {
        if (!isTriggered)
        {
            return;
        }

        float distance = Vector2.Distance(player.transform.position, transform.position);

        // curve fitting
        float desiredOrtho = 366184.1f + (2.200014f - 366184.1f) / (1 + (float)Math.Pow((distance / 915300), 1.000014f));
        float desiredOffsetY = 2.05517f + (0.5544997f - 2.05517f) / (1 + (float)Math.Pow((distance / 7.135192f), 6.661377f));

        camera.GetComponent<CameraScript>().offsetY = desiredOffsetY;
        camera.GetComponent<Camera>().orthographicSize = desiredOrtho;

        if (player.gameObject.transform.position.x < transform.position.x + 2 && player.gameObject.transform.position.x > transform.position.x - 2 && player.gameObject.transform.position.y < transform.position.y + 2 && player.gameObject.transform.position.y > transform.position.y - 2)
        {
            player.gameObject.GetComponent<HeroKnight>().m_animator.SetInteger("AnimState", 0);
            player.gameObject.GetComponent<HeroKnight>().enabled = false;
            player.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

            GetComponent<Animator>().enabled = true;

            if (!hasPlayed)
            {
                GetComponent<AudioSource>().Play();
                hasPlayed = true;
            }

            Invoke("LoadNextScene", 4.5f);
        }
    }
}
