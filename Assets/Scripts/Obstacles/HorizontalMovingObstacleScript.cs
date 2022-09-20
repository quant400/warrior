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

        public bool nonLinearMovement = false;

        public bool isFollowingPlayerPlatform = false;

        private Vector3 startPosition;

        private StarterAssetsInputs _input;

        private bool tweenEnabled = false;

        // Start is called before the first frame update
        void Start()
        {
            _input = GameObject.FindGameObjectWithTag("Player").GetComponent<StarterAssetsInputs>();

            startPosition = transform.localPosition;

            //transform.DOMoveX(transform.position.x + distance, duration).SetLoops(-1, LoopType.Yoyo);

            Vector3 localPosition = transform.localPosition;

            localPosition.x += distance;

            //transform.DOLocalMove(localPosition, duration).SetLoops(-1, LoopType.Yoyo);

            if(!nonLinearMovement)
            {
                MoveRight();
            }
            else
            {
                transform.DOLocalMove(localPosition, duration).SetLoops(-1, LoopType.Yoyo);
            }
            

            tweenEnabled = true;
        }

        private void OnEnable()
        {
            //transform.DOMoveX(transform.position.x + distance, duration).SetLoops(-1, LoopType.Yoyo);
            if (tweenEnabled)
            {
                DOTween.Play(gameObject.transform);
            }
        }

        private void OnDisable()
        {
            //DOTween.Kill(gameObject.transform);

            if (tweenEnabled)
            {
                DOTween.Pause(gameObject.transform);
            }

            //DOTween.Kill(gameObject.transform);

            //transform.localPosition = startPosition;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void MoveRight()
        {
            Vector3 localPosition = transform.localPosition;

            localPosition.x += distance;

            transform.DOLocalMove(localPosition, duration).OnComplete(() => MoveLeft()).SetEase(Ease.Linear);
        }


        private void MoveLeft()
        {
            Vector3 localPosition = transform.localPosition;

            localPosition.x -= distance;

            transform.DOLocalMove(localPosition, duration).OnComplete(() => MoveRight()).SetEase(Ease.Linear);
        }

        void OnDestroy()
        {
            if (tweenEnabled)
            {
                DOTween.Kill(gameObject.transform);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(_input)
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
