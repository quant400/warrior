using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarterAssets
{
    public class BoulderPushScript : MonoBehaviour
    {
        private bool forceActive = false;

        private Rigidbody2D playerRBody;

        private StarterAssetsInputs _input;

        [SerializeField] float pushBackForce = 2.0f;

        private bool forceDeactiveDelayCoroutine = false;

        // Start is called before the first frame update
        void Start()
        {
            _input = GameObject.FindGameObjectWithTag("Player").GetComponent<StarterAssetsInputs>();

            playerRBody = GameObject.FindGameObjectWithTag("PlayerBody").GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void FixedUpdate()
        {
            if(forceActive)
            {
                //playerRBody.AddForce(new Vector3(-1, 0, 0) * pushBackForce * 2, ForceMode2D.Force);

                playerRBody.AddForce(new Vector3(-1, 0, 0) * pushBackForce, ForceMode2D.Impulse);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.transform.CompareTag("PlayerBody"))
            {
                forceActive = true;

                //Debug.Log("forceActive = " + forceActive);
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.transform.CompareTag("PlayerBody"))
            {
                if(!forceDeactiveDelayCoroutine)
                {
                    StartCoroutine(ForceDeactiveDelay(0.2f));
                }
            }
        }

        IEnumerator ForceDeactiveDelay(float secs)
        {
            forceDeactiveDelayCoroutine = true;

            AudioManager.Instance.PlayPushBackSound();

            yield return new WaitForSeconds(secs);

            forceActive = false;

            //Debug.Log("forceActive = " + forceActive);

            forceDeactiveDelayCoroutine = false;
        }
    }

}
