using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour
{
    public Text text_health;
    public GameObject screen_youDied;

    private PlayerController m_player;
    private void Awake() => m_player = GameObject.Find("Player").GetComponent<PlayerController>();

    // Update is called once per frame
    void Update()
    {
        text_health.text = "HEALTH: " + m_player.health;
        if (m_player.health <= 0)
        {
            screen_youDied.SetActive(true);
            if (Input.anyKeyDown)
                Application.LoadLevel(Application.loadedLevel);
        }
        else screen_youDied.SetActive(false);
    }
}
