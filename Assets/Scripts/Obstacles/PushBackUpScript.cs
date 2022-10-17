using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace StarterAssets
{
    public class PushBackUpScript : MonoBehaviour
    {
        [SerializeField] float pushBackForce = 2.0f;

        private bool firstTimeCollision;

        private Rigidbody2D playerRBody;

        private bool forceActive;
        //private bool canForceActCoroutine;

        [SerializeField] bool impusleOnlyToggle = false;

        private StarterAssetsInputs _input;

        private bool playPushBackDelayCoroutine;

        // Start is called before the first frame update
        void Start()
        {
            _input = GameObject.FindGameObjectWithTag("Player").GetComponent<StarterAssetsInputs>();

            firstTimeCollision = false;

            forceActive = false;

            //canForceActCoroutine = false;

            playPushBackDelayCoroutine = false;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (forceActive)
            {
                if (impusleOnlyToggle)
                {
                    playerRBody.AddForce((new Vector3(-1, 0, 0)) * pushBackForce, ForceMode2D.Impulse);

                    playerRBody.AddForce((new Vector3(0, 1, 0)) * pushBackForce/2, ForceMode2D.Impulse);

                    if (!playPushBackDelayCoroutine)
                    {
                        StartCoroutine(PlayPushBackDelay(0.5f));
                    }
                }
                else
                {
                    if(_input.move.x != 0)
                    {
                        playerRBody.AddForce(new Vector3(-1, 0, 0) * pushBackForce * 2, ForceMode2D.Force);

                        playerRBody.AddForce(new Vector3(-1, 0, 0) * pushBackForce / 10, ForceMode2D.Impulse);

                        if (!playPushBackDelayCoroutine)
                        {
                            StartCoroutine(PlayPushBackDelay(0.5f));
                        }
                    }
                    else
                    {
                        playerRBody.AddForce(new Vector3(-1, 0, 0) * pushBackForce * 2, ForceMode2D.Force);

                        //playerRBody.AddForce(new Vector3(-1, 0, 0) * pushBackForce / 10, ForceMode2D.Impulse);

                        if (!playPushBackDelayCoroutine)
                        {
                            StartCoroutine(PlayPushBackDelay(0.5f));
                        }
                    }


                }

            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.CompareTag("PlayerBody"))
            {
                if (!firstTimeCollision)
                {
                    playerRBody = collision.transform.GetComponent<Rigidbody2D>();

                    firstTimeCollision = true;
                }

                forceActive = true;
            }

        }

        /*
        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.transform.CompareTag("PlayerBody"))
            {
                if (!firstTimeCollision)
                {
                    playerRBody = collision.transform.GetComponent<Rigidbody2D>();

                    firstTimeCollision = true;
                }

                forceActive = true;
            }
        }
        */

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.transform.CompareTag("PlayerBody"))
            {
                forceActive = false;
            }
        }

        /*
        IEnumerator canForceAct(float secs)
        {
            canForceActCoroutine = true;

            yield return new WaitForSeconds(secs);

            canForceActCoroutine = false;
        }
        */

        IEnumerator PlayPushBackDelay(float secs)
        {
            playPushBackDelayCoroutine = true;

            AudioManager.Instance.PlayPushBackSound();

            yield return new WaitForSeconds(secs);

            playPushBackDelayCoroutine = false;
        }
    }
}
