using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearMovement : MonoBehaviour
{
    private GameObject playerBody;

    private GameObject bearBody;

    private Rigidbody2D bearRBody2D;

    SpriteRenderer bearSpriteRenderer;

    private Animator bearAnimator;

    private bool playerInRange;

    [SerializeField]
    private float speed = 5.0f;

    private Vector3 moveDirection;

    Vector2 origVelocity;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GameObject.FindGameObjectWithTag("PlayerBody");

        bearBody = gameObject.transform.GetChild(0).gameObject;

        bearAnimator = bearBody.GetComponent<Animator>();

        bearRBody2D = bearBody.GetComponent<Rigidbody2D>();

        bearSpriteRenderer = bearBody.GetComponent<SpriteRenderer>();

        playerInRange = false;

        moveDirection = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange)
        {
            bearAnimator.SetBool("bearStill", false);
        }
        else if(!playerInRange && origVelocity.x == 0)
        {
            bearAnimator.SetBool("bearStill", true);
        }
    }

    private void FixedUpdate()
    {
        if(playerInRange)
        {
            if (playerBody.transform.position.x < bearBody.transform.position.x)
            {
                moveDirection = new Vector3(-1, 0, 0);

                bearSpriteRenderer.flipX = true;
            }
            else
            {
                moveDirection = new Vector3(1, 0, 0);

                bearSpriteRenderer.flipX = false;
            }

            bearRBody2D.AddForce(moveDirection * speed * 100, ForceMode2D.Force);

            origVelocity = bearRBody2D.velocity;

            origVelocity.x = Vector2.ClampMagnitude(bearRBody2D.velocity, speed).x;
        }
        else
        {
            origVelocity = bearRBody2D.velocity;

            //Debug.Log("bear position = " + bearBody.transform.position.x);

            //Debug.Log("gameObject position = " + gameObject.transform.position.x);

            if(Mathf.Abs(bearBody.transform.position.x - gameObject.transform.position.x) > 0.01)
            {
                if (bearBody.transform.position.x > gameObject.transform.position.x)
                {
                    moveDirection = new Vector3(-1, 0, 0);

                    bearSpriteRenderer.flipX = true;

                    bearRBody2D.AddForce(moveDirection * speed * 100, ForceMode2D.Force);

                    origVelocity = bearRBody2D.velocity;

                    origVelocity.x = Vector2.ClampMagnitude(bearRBody2D.velocity, speed).x;
                }
                else if (bearBody.transform.position.x < gameObject.transform.position.x)
                {
                    moveDirection = new Vector3(1, 0, 0);

                    bearSpriteRenderer.flipX = false;

                    bearRBody2D.AddForce(moveDirection * speed * 100, ForceMode2D.Force);

                    origVelocity = bearRBody2D.velocity;

                    origVelocity.x = Vector2.ClampMagnitude(bearRBody2D.velocity, speed).x;
                }
            }
            else
            {
                origVelocity.x = Vector2.ClampMagnitude(bearRBody2D.velocity, 0.0f).x;
            }    
        }

        bearRBody2D.velocity = origVelocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBody"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBody"))
        {
            playerInRange = false;
        }
    }
}
