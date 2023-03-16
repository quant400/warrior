using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderDitchCollisionIgnore : MonoBehaviour
{
    Collider2D collider2DComp;

    private void Awake()
    {
        collider2DComp = gameObject.GetComponent<Collider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("PlayerBody") || collision.transform.CompareTag("GroundCheck"))
        {
            Physics2D.IgnoreCollision(collision.collider, collider2DComp);
        }
    }
    
}
