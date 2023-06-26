using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Warrior
{
    public class FallingSlime : MonoBehaviour
    {
        //SpriteRenderer childSprite;

        Rigidbody2D rBody;

        //[SerializeField] float fadeInTime = 1f;

        [SerializeField] float fadeOutTime = 1f;

        private Vector3 origPosition;

        // Start is called before the first frame update
        void Start()
        {
            origPosition = gameObject.transform.localPosition;

            //childSprite = gameObject.transform.GetChild(0).transform.GetComponent<SpriteRenderer>();

            rBody = gameObject.transform.GetComponent<Rigidbody2D>();

            //Color origChildColor = childSprite.color;

            //origChildColor.a = 0.0f;

            //childSprite.color = origChildColor;

            //childSprite.DOFade(59, fadeInTime);
        }

        // Update is called once per frame
        void Update()
        {

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

        void OnDestroy()
        {
            //DOTween.Kill(childSprite);

            //DOTween.Kill(gameObject.transform);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Slime"))
            {
                StartCoroutine(PositionChange(fadeOutTime));
            }
        }

        IEnumerator PositionChange(float secs)
        {
            //childSprite.DOFade(0, fadeOutTime);

            yield return new WaitForSeconds(secs);

            gameObject.transform.localPosition = origPosition;

            //Debug.Log(origPosition);

            rBody.velocity = Vector3.zero;

            //childSprite.DOFade(59, fadeInTime);
        }
    }
}
