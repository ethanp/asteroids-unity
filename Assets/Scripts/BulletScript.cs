using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z > 4)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 1));
    }
}
