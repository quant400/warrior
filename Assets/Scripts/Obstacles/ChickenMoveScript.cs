using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenMoveScript : MonoBehaviour
{
    Rigidbody2D rbody2D;
    private Vector3 inputDirection;

    [SerializeField]
    private bool moveRight;
    [SerializeField]
    private float _speed = 5.0f;

    Vector2 origVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rbody2D = gameObject.GetComponent<Rigidbody2D>();

        if(moveRight)
        {
            inputDirection = new Vector3(1, 0, 0);
        }
        else
        {
            inputDirection = new Vector3(-1, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rbody2D.AddForce(inputDirection * _speed, ForceMode2D.Force);

        origVelocity = rbody2D.velocity;

        origVelocity.x = Vector2.ClampMagnitude(rbody2D.velocity, _speed).x;

        rbody2D.velocity = origVelocity;
    }
}
