using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerButtonPrompt : MonoBehaviour
{
    public GameObject pressEPrompt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Climable"))
        {
            pressEPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Climable"))
        {
            pressEPrompt.SetActive(false);
        }
    }
}
