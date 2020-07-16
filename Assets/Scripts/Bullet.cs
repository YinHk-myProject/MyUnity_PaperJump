using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float xValue,yValue,speed;
    private Rigidbody2D rd;

    // Start is called before the first frame update
    void Start()
    {
      rd = gameObject.GetComponent<Rigidbody2D>();  
    }
     
    // Update is called once per frame
    void FixedUpdate()
    {
      rd.velocity = new Vector2(xValue, yValue)*speed;  
    }
}
