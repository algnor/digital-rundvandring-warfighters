using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    public GameObject bird;
    public float distance;
    // Start is called before the first frame update
    void Start()
    {
        transform.eulerAngles = new Vector3(0f, Random.Range(0, 360), 0f);
        bird.transform.localPosition = new Vector3(distance, 0, 0);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, -1, 0));
    }
}
