using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapNo : MonoBehaviour
{
    public TMP_Text mapNo;

    private string tempString;

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


        if(collision.transform.CompareTag("Ground"))
        {
            if(mapNo.text != collision.transform.parent.gameObject.name && collision.transform.parent.gameObject.name != "Grid")
            {
                tempString = collision.transform.parent.gameObject.name;

                mapNo.text = tempString.TrimStart('M', 'a', 'p', 'G', 'r', 'i', 'd');
            }
            
        }

    }
}
