using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearAnimation : MonoBehaviour
{
    private bool playerInRange;

    private bool bearPush;

    private bool bearPushBackDelayCoroutine;

    private Animator bearAnimator;

    private Rigidbody2D playerRBody;

    [SerializeField] 
    private float pushBackForce = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerInRange = false;

        bearPush = false;

        bearPushBackDelayCoroutine = false;

        bearAnimator = gameObject.GetComponent<Animator>();

        playerRBody = GameObject.FindGameObjectWithTag("PlayerBody").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInRange)
        {
            bearAnimator.SetBool("bearAttack", true);

            if(!bearPushBackDelayCoroutine)
            {
                bearPush = true;
            }
            
        }
        /*
        else
        {
            bearAnimator.SetBool("bearStill", true);
        }
        */
    }

    private void FixedUpdate()
    {
        if(bearPush)
        {
            bearPush = false;

            if (!bearPushBackDelayCoroutine)
            {
                StartCoroutine(BearPushBackDelay(0.5f));
            }
        }
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

    IEnumerator BearPushBackDelay(float secs)
    {
        bearPushBackDelayCoroutine = true;

        //yield return new WaitForSeconds(0.05f);

        playerRBody.velocity = Vector3.zero;

        playerRBody.AddForce((new Vector3(-1, 0, 0)) * pushBackForce * 2, ForceMode2D.Impulse);

        playerRBody.AddForce((new Vector3(0, 1, 0)) * pushBackForce / 2, ForceMode2D.Impulse);

        yield return new WaitForSeconds(secs);

        bearAnimator.SetBool("bearAttack", false);

        bearPushBackDelayCoroutine = false;
    }
}
