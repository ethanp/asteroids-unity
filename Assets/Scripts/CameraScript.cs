using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private GameObject ship_;
    private Vector3 offset_;

    void Start()
    {
        ship_ = GameManager.instance.GetShip();
        offset_ = transform.position - ship_.transform.position;
    }

    void Update()
    {
        GetComponent<Camera>().backgroundColor = createBackgroundColor();

        // TODO the fact that this works (got it from the Unity docs) goes to
        // show how clueless I am here. Also, why doesn't calling
        // `transform.LookAt(ship);` work?
        transform.position = ship_.transform.position + offset_;

        //transform.rotation = lookAtShip();
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
        Vector3 behind = ship_.transform.up * -40;
        Vector3 above = Vector3.zero;  // ship_.transform.forward * -5;
        return Vector3.Lerp(
            a: transform.position,
            b: behind + above,
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
