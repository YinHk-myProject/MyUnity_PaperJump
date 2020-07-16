using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{    
    private GameObject zombie;
    private Rigidbody2D rd;
    private Vector2 direction;
    public float rotateZ,speed;

    // Start is called before the first frame update
    void Start()
    {
      speed = Random.Range(3.5f,8f);
      rd = gameObject.GetComponent<Rigidbody2D>();
      zombie = GameObject.Find("zombie2(Clone)");
      direction = (transform.position-zombie.transform.position).normalized*speed;
      rd.AddForce(direction, ForceMode2D.Impulse);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      transform.Rotate(0, 0, rotateZ); 
      if(GameObject.Find("zombie2(Clone)")==null)
         {
           Destroy(gameObject);
         }
       
    }
}
