using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


namespace StarterAssets
{
    public class VerticalMovingObstacleScript : MonoBehaviour
    {
        public float distance;
        public float duration;

        public bool isFollowingPlayerPlatform = false;

        private StarterAssetsInputs _input;

        // Start is called before the first frame update
        void Start()
        {
            _input = GameObject.FindGameObjectWithTag("Player").GetComponent<StarterAssetsInputs>();

            transform.DOMoveY(gameObject.transform.position.y + distance, duration).SetLoops(-1, LoopType.Yoyo);

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void MoveUp()
        {
            Vector3 localPosition = transform.localPosition;

            localPosition.x += distance;

            transform.DOLocalMove(localPosition, duration).OnComplete(() => MoveDown()).SetEase(Ease.Linear);
        }


        private void MoveDown()
        {
            Vector3 localPosition = transform.localPosition;

            localPosition.x -= distance;

            transform.DOLocalMove(localPosition, duration).OnComplete(() => MoveUp()).SetEase(Ease.Linear);
        }

        void OnDestroy()
        {
            DOTween.Kill(gameObject.transform);
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
            if(_input.move.x == 0)
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
