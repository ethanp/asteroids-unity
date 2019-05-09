using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float travelVelocity;

    private void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * 2;
        StartCoroutine(die());
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * 10);
    }

    // Clean up resources for performance.
    private IEnumerator die()
    {
        yield return new WaitForSeconds(7);
        Destroy(gameObject);
    }
}
