using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] float travelVelocity;

    void Start()
    {
        GetComponent<Rigidbody>().velocity += transform.up * 15;
        StartCoroutine(die());
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddForce(transform.up * 10);
    }

    // Clean up resources for performance.
    IEnumerator die()
    {
        yield return new WaitForSeconds(7);
        Destroy(gameObject);
    }
}
