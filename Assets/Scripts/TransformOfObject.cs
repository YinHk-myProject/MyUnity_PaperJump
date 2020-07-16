using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformOfObject : MonoBehaviour
{
    private GameObject cam;
    // Start is called before the first frame update
    void Start()
    {
      cam = GameObject.FindWithTag("MainCamera");  
    }

    // Update is called once per frame
    void Update()
    {
      transform.parent = cam.transform;
    }
}
