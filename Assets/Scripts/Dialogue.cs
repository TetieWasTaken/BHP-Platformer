using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour
{
    // voice lines when??1233456890

    public string[] lines = new string[] {
        "TEST 1",
        "Lorum ipsum dolor sit amet, consectetur adipiscing elit. Nullam euismod, nisl eget ultricies ultricies, nunc nunc aliquam nunc, quis aliquam nunc nunc quis.",
        "TEST 3"
    };

    public int currentLine = 0;
    public float waitTime = 0.75f;

    public Text text;
    public GameObject drempelText;
    public int[] queue;
    public bool isTyping = false;

    public string drempel = "Drempel x - naam - jaar";

    private void Start()
    {
        string[] drempelArray = drempel.Split(new string[] { " - " }, System.StringSplitOptions.None);

        drempelText.transform.GetChild(0).GetComponent<Text>().text = drempelArray[0];
        drempelText.transform.GetChild(1).GetComponent<Text>().text = drempelArray[1];
        drempelText.transform.GetChild(2).GetComponent<Text>().text = drempelArray[2];

        StartCoroutine(FadeInDrempelText());

        StartCoroutine(FadeOutDrempelText());
    }

    private IEnumerator FadeInDrempelText()
    {
        for (float i = 0; i <= 1.5; i += 0.01f)
        {
            drempelText.transform.GetChild(0).GetComponent<Text>().color = new Color(1, 1, 1, i);
            drempelText.transform.GetChild(1).GetComponent<Text>().color = new Color(1, 1, 1, i - 0.25f);
            drempelText.transform.GetChild(2).GetComponent<Text>().color = new Color(1, 1, 1, i - 0.5f);
            yield return new WaitForSeconds(0.01f);
        }
    }

    private IEnumerator FadeOutDrempelText()
    {
        yield return new WaitForSeconds(5f);

        for (float i = 1; i >= 0; i -= 0.01f)
        {
            drempelText.transform.GetChild(0).GetComponent<Text>().color = new Color(1, 1, 1, i);
            drempelText.transform.GetChild(1).GetComponent<Text>().color = new Color(1, 1, 1, i);
            drempelText.transform.GetChild(2).GetComponent<Text>().color = new Color(1, 1, 1, i);
            yield return new WaitForSeconds(0.01f);
        }

        if (SceneManager.GetActiveScene().name == "Introduction")
        {
            GameObject.Find("DeLorean").GetComponent<Animator>().enabled = true;
        }
    }

    IEnumerator DisplayLine()
    {
        foreach (char c in lines[currentLine])
        {
            text.text += c;
            yield return new WaitForSeconds(waitTime);
        }

        currentLine++;

        yield return new WaitForSeconds(0.500f);

        if (queue.Length > 0)
        {
            currentLine = queue[0];
            int[] newQueue = new int[queue.Length - 1];
            for (int i = 0; i < newQueue.Length; i++)
            {
                newQueue[i] = queue[i + 1];
            }
            queue = newQueue;
            text.text = "";
            isTyping = true;
            StartCoroutine(DisplayLine());
        }
        else
        {
            text.text = "";
            isTyping = false;
        }
    }

    public void NextLine()
    {
        if (currentLine < lines.Length)
        {
            if (queue.Length == 0 && !isTyping)
            {
                text.text = "";
                isTyping = true;
                StartCoroutine(DisplayLine());
            }
            else
            {
                int[] newQueue = new int[queue.Length + 1];
                for (int i = 0; i < queue.Length; i++)
                {
                    newQueue[i] = queue[i];
                }
                newQueue[queue.Length] = currentLine + 1;
                queue = newQueue;
            }
        }
    }
}
