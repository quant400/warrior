using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


namespace StarterAssets
{
    public class HorizontalMovingObstacleScript : MonoBehaviour
    {

        public float distance;
        public float duration;

        public bool isFollowingPlayerPlatform = false;

        private Vector3 startPosition;

        private StarterAssetsInputs _input;

        // Start is called before the first frame update
        void Start()
        {
            _input = GameObject.FindGameObjectWithTag("Player").GetComponent<StarterAssetsInputs>();

            startPosition = transform.localPosition;

            //transform.DOMoveX(transform.position.x + distance, duration).SetLoops(-1, LoopType.Yoyo);

            Vector3 localPosition = transform.localPosition;

            localPosition.x += distance;

            transform.DOLocalMove(localPosition, duration).SetLoops(-1, LoopType.Yoyo);
        }

        private void OnEnable()
        {
            //transform.DOMoveX(transform.position.x + distance, duration).SetLoops(-1, LoopType.Yoyo);

            DOTween.Play(gameObject.transform);
        }

        private void OnDisable()
        {
            //DOTween.Kill(gameObject.transform);

            DOTween.Pause(gameObject.transform);

            //DOTween.Kill(gameObject.transform);

            //transform.localPosition = startPosition;
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnDestroy()
        {
            DOTween.Kill(gameObject.transform);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (_input.move.x == 0)
            {
                if (collision.transform.CompareTag("PlayerBody") && isFollowingPlayerPlatform)
                {
                    collision.transform.SetParent(gameObject.transform);
                }
            }
            else
            {
                if (collision.transform.CompareTag("PlayerBody") && isFollowingPlayerPlatform)
                {
                    collision.transform.SetParent(null);
                }
                    
            }

        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (_input.move.x == 0)
            {
                if (collision.transform.CompareTag("PlayerBody") && isFollowingPlayerPlatform)
                {
                    collision.transform.SetParent(gameObject.transform);
                }
            }
            else
            {
                if (collision.transform.CompareTag("PlayerBody") && isFollowingPlayerPlatform)
                {
                    collision.transform.SetParent(null);
                }
            }

        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.transform.CompareTag("PlayerBody") && isFollowingPlayerPlatform)
            {
                collision.transform.SetParent(null);
            }
        }


    }
}
