using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    /*
    [SerializeField] private GameObject asteroidPrefab;

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.transform.localScale.z > 1)
        {
            // Create children.
            GameObject leftChild = Instantiate(asteroidPrefab);
            GameObject rightChild = Instantiate(asteroidPrefab);

            // Set parent.
            leftChild.transform.parent = transform.parent;
            rightChild.transform.parent = transform.parent;

            // Set position.
            leftChild.transform.position = new Vector3(
                transform.position.x-1,
                transform.position.y,
                transform.position.z);
            rightChild.transform.position = new Vector3(
                transform.position.x + 1,
                transform.position.y,
                transform.position.z);

            // Set scale.
            Vector3 smallerScale = transform.localScale * .7f;
            leftChild.transform.localScale = smallerScale;
            rightChild.transform.localScale = smallerScale;

            // TODO set velocity.
        }
        Destroy(gameObject);
    }
    */
}
