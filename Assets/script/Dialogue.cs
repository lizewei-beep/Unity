using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text dialogueText;
    public string npcText;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if(Input.GetKey(KeyCode.UpArrow))
        if(collision.gameObject.CompareTag("Player")|| collision.gameObject.CompareTag("Player1"))

            //if(collision.gameObject.CompareTag("Player")&& Input.GetKey(KeyCode.UpArrow) || collision.gameObject.CompareTag("Player1") && (Input.GetKeyDown("w")))
            {
            dialogueText.text = npcText;
            dialogueBox.SetActive(true);
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Player1"))
        {
            dialogueBox.SetActive(false);
        }
    }
}
