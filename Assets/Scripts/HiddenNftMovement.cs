using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Warrior
{
    public class HiddenNftMovement : MonoBehaviour
    {
        [SerializeField] private Vector2 parallaxEffectMultiplier;
        [SerializeField] private bool infiniteHorizontal;
        [SerializeField] private bool infiniteVertical;

        [SerializeField] private Sprite sprite;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private GameObject hiddenNFT;
        [SerializeField] private GameObject player;

        private Transform cameraTransform;
        private Vector3 lastCameraPosition;
        private float textureUnitSizeX;
        private float textureUnitSizeY;

        private float length, startpos;
        public float parallaxEffect;

        private void Start()
        {
            cameraTransform = Camera.main.transform;
            lastCameraPosition = cameraTransform.position;

            Texture2D texture = sprite.texture;
            textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
            textureUnitSizeY = texture.height / sprite.pixelsPerUnit;


            startpos = transform.position.x;
            length = spriteRenderer.bounds.size.x;

            //Debug.Log("length = " + length);
        }


        private void Update()
        {
            if (player.transform.position.x - hiddenNFT.transform.position.x > 83)
            {
                ResetNFTPosition();
            }


            Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
            //transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier.x, deltaMovement.y * parallaxEffectMultiplier.y);
            transform.position += new Vector3(transform.position.x, deltaMovement.y * parallaxEffectMultiplier.y);
            lastCameraPosition = cameraTransform.position;


            float temp = (cameraTransform.position.x * (1 - parallaxEffect));
            float dist = (cameraTransform.position.x * parallaxEffect);

            transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

            /*
            if (temp > startpos + length)
            {
                startpos += length;
            }
            else if (temp < startpos - length)
            {
                startpos -= length;
            }
            */


            if (infiniteVertical)
            {
                if (Mathf.Abs(cameraTransform.position.y - transform.position.y) >= textureUnitSizeY)
                {
                    float offsetPositionY = (cameraTransform.position.y - transform.position.y) % textureUnitSizeY;
                    transform.position = new Vector3(transform.position.x, cameraTransform.position.y + offsetPositionY);
                }
            }
        }

        private void ResetNFTPosition()
        {
            //gameObject.transform.position = new Vector3(nextBG.transform.position.x - difference, gameObject.transform.position.y, gameObject.transform.position.z);

            startpos = spriteRenderer.gameObject.GetComponent<ParallaxBackground>().startpos;
        }
    }
}
