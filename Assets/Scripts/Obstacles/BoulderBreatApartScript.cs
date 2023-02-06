using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderBreatApartScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject boulderSprite;

    [SerializeField] GameObject boulderParts;

    private Vector3 position;

    private Quaternion rotation;

    private Collider2D boulderCollider;

    void Start()
    {
        boulderCollider = gameObject.GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        position = gameObject.transform.position;

        rotation = gameObject.transform.rotation;
    }

    private void OnDisable()
    {
        gameObject.transform.position = position;

        gameObject.transform.rotation = rotation;

        boulderParts.SetActive(false);

        boulderSprite.SetActive(true);

        boulderCollider.enabled = true;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("BoulderBreak"))
        {
            boulderParts.SetActive(true);

            boulderSprite.SetActive(false);

            boulderCollider.enabled = false;
        }
    }
}
