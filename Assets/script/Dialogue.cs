using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text dialogueText;
    public string npcText;
    private bool playerNpc;

    void Start()
    {
        playerNpc = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")|| collision.gameObject.CompareTag("Player1") && playerNpc)
        {
            dialogueText.text = npcText;
            dialogueBox.SetActive(true);
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")&& playerNpc)
        {
            dialogueBox.SetActive(false);
            playerNpc = false;
        }
    }
}
