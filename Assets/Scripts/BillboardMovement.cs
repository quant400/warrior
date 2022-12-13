using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardMovement : MonoBehaviour
{
    private GameObject playerBody;

    private Vector3 playerPos;

    Vector3 gameObjectPos;

    public int updateLocationTimes = 1;

    private Collider objectCollider;

    private bool objectInCollision;

    public Sprite[] billboardAds;

    private SpriteRenderer billboardSprite;



    // Start is called before the first frame update
    void Start()
    {
        int rand = 0;

        playerBody = GameObject.FindGameObjectWithTag("PlayerBody");

        playerPos = playerBody.transform.position;

        gameObjectPos = gameObject.transform.position;

        objectCollider = gameObject.GetComponent<Collider>();

        objectInCollision = false;

        billboardSprite = gameObject.transform.GetChild(1).transform.GetComponent<SpriteRenderer>();
        
        rand = UnityEngine.Random.Range(0, billboardAds.Length);

        billboardSprite.sprite = billboardAds[rand];

        /*

        gameObjectPos.x = UnityEngine.Random.Range(playerPos.x + 100, playerPos.x + 200);

        gameObject.transform.position = gameObjectPos;
        */
    }

    // Update is called once per frame
    void Update()
    {
        gameObjectPos = gameObject.transform.position;

        playerPos = playerBody.transform.position;

        if(updateLocationTimes > 0)
        {
            if (playerPos.x > gameObject.transform.position.x + 100)
            {
                gameObjectPos.x = UnityEngine.Random.Range(playerPos.x + 300, playerPos.x + 500);

                gameObject.transform.position = gameObjectPos;

                if(objectInCollision)
                {
                    gameObjectPos.x = UnityEngine.Random.Range(playerPos.x + 300, playerPos.x + 500);

                    gameObject.transform.position = gameObjectPos;
                }

                billboardSprite.sprite = billboardAds[UnityEngine.Random.Range(0, billboardAds.Length)];

                updateLocationTimes--;
            }
        }


        if(objectInCollision && playerPos.x < gameObject.transform.position.x - 200)
        {
            gameObjectPos.x = UnityEngine.Random.Range(playerPos.x + 300, playerPos.x + 500);

            gameObject.transform.position = gameObjectPos;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            objectInCollision = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            objectInCollision = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            objectInCollision = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            objectInCollision = false;
        }
    }
}
