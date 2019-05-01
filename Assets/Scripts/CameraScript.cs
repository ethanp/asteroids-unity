using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    private GameObject ship_;

    void Start()
    {
        ship_ = GameManager.instance.GetShip();
    }

    void Update()
    {
        GetComponent<Camera>().backgroundColor = createBackgroundColor();

        transform.position = followShip();
        transform.rotation = lookAtShip();
    }

    private Color createBackgroundColor()
    {
        return new Color(
            r: Mathf.Abs(Mathf.Sin(Time.time)),
            g: Mathf.Abs(Mathf.Cos(Time.time)),
            b: .2f,
            a: .8f);
    }

    private Vector3 followShip()
    {
        return Vector3.Lerp(
            a: transform.position,
            b: ship_.transform.right * 20 + ship_.transform.up * 5,
            t: 1.0f);
    }

    private Quaternion lookAtShip()
    {
        Transform goal = new GameObject().transform;
        goal.LookAt(ship_.transform);

        return Quaternion.Lerp(
            a: transform.rotation,
            b: goal.rotation,
            t: 1.0f);
    }
}
