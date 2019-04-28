using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField]
    private float travelVelocity;
    
    void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddForce(transform.up * 10);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z > 40)
        {
            // Clean up resources for performance.
            Destroy(gameObject);
        }
    }
}
