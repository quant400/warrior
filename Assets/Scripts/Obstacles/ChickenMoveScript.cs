using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenMoveScript : MonoBehaviour
{
    Rigidbody2D rbody2D;
    SpriteRenderer spriteRenderer;
    private Vector3 inputDirection;

    [SerializeField]
    private bool moveRight;
    [SerializeField]
    private bool moveRightAnLeft;
    [SerializeField]
    private float _speed = 5.0f;

    private Vector3 leftDirection;

    private Vector3 rightDirection;

    Vector2 origVelocity;

    // Start is called before the first frame update
    void Start()
    {
        leftDirection = new Vector3(-1, 0, 0);

        rightDirection = new Vector3(1, 0, 0);

        rbody2D = gameObject.GetComponent<Rigidbody2D>();

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        if (moveRight)
        {
            inputDirection = rightDirection;
        }
        else if (moveRightAnLeft)
        {
            StartCoroutine(MoveLeftAndRight(5f));
        }
        else
        {
            inputDirection = leftDirection;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(inputDirection == leftDirection)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

        rbody2D.AddForce(inputDirection * _speed, ForceMode2D.Force);

        origVelocity = rbody2D.velocity;

        origVelocity.x = Vector2.ClampMagnitude(rbody2D.velocity, _speed).x;

        rbody2D.velocity = origVelocity;
    }

    IEnumerator MoveLeftAndRight(float secs)
    {
        while(true)
        {
            inputDirection = rightDirection;

            yield return new WaitForSeconds(secs);

            inputDirection = leftDirection;

            yield return new WaitForSeconds(secs);
        }

    }
}
