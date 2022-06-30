using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObstacleScript : MonoBehaviour
{

    private Collider2D obstacleCollider;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        obstacleCollider = gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(obstacleCollider.bounds.center, new Vector3(0, 0, -1), speed * Time.deltaTime);
    }
}
