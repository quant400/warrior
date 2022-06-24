using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeChecker : MonoBehaviour
{
    private Rigidbody2D rBody;

    // Start is called before the first frame update
    void Start()
    {
        rBody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Slime"))
        {
            rBody.drag = 10.0f;

            //rBody.angularDrag = 5.0f;

            rBody.gravityScale = 3.0f;

            //Debug.Log("Entered Slime");
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Slime"))
        {
            rBody.drag = 10.0f;

            //rBody.angularDrag = 5.0f;

            rBody.gravityScale = 3.0f;

            //Debug.Log("Entered Slime");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Slime"))
        {
            rBody.drag = 0.0f;

            rBody.angularDrag = 0.05f;

            rBody.gravityScale = 1.0f;

            //Debug.Log("Exited Slime");
        }
    }
}
