using UnityEngine;

public class HelpScript : MonoBehaviour
{
    public GameObject panel;
    public GameObject player;

    public void togglePanel() {
        panel.SetActive(!panel.activeSelf);

        if (panel.activeSelf) {
            player.gameObject.GetComponent<HeroKnight>().m_animator.SetInteger("AnimState", 0);
            player.gameObject.GetComponent<HeroKnight>().enabled = false;
            player.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        } else {
            player.gameObject.GetComponent<HeroKnight>().enabled = true;
        }
    }
}
