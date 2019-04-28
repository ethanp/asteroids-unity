using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField]
    private float travelVelocity;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, travelVelocity);
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
