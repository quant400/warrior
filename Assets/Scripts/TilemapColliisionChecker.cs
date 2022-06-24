using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapColliisionChecker : MonoBehaviour
{
    private bool isColliding;

    // Start is called before the first frame update
    void Start()
    {
        isColliding = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsCollidingWithPlayer()
    {
        return isColliding;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            isColliding = true;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            isColliding = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            isColliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            isColliding = false;
        }
    }
}
