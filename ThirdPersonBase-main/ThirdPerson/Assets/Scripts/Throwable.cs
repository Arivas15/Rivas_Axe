using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    public Rigidbody axe;
    public float throwForce = 50;
    public Transform target, curve_point;
    private Vector3 oldPosition;
    private bool isReturning;
    private float time = 0.0f;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonUp("Fire1"))
        {
            ThrowAxe();
        }
        if (Input.GetButtonUp("Fire2"))
        {
            ReturnAxe();
        }
        if(isReturning)
        {
            if(time < 1.0f)
            {
                axe.position = getBQCPoint(time, oldPosition, curve_point.position, target.position);
                axe.rotation = Quaternion.Slerp(axe.transform.rotation, target.rotation, 50 * Time.deltaTime);
                time += Time.deltaTime;
            }
            else
            {
                ResetAxe();
            }
        }
    }
    void ThrowAxe()
    {
        isReturning = false;
        axe.transform.parent = null;
        axe.isKinematic = false;
        axe.AddForce(Camera.main.transform.TransformDirection(Vector3.forward) * throwForce, ForceMode.Impulse);
        axe.AddTorque(axe.transform.TransformDirection(Vector3.right) * 100, ForceMode.Impulse);
    }
    void ReturnAxe()
    {
        time = 0.0f;
        oldPosition = axe.position;
        isReturning = true;
        axe.velocity = Vector3.zero;
        axe.isKinematic = true;
    }
    void ResetAxe()
    {
        isReturning = false;
        axe.transform.parent = transform;
        axe.position = target.position;
        axe.rotation = target.rotation;
    }
    Vector3 getBQCPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = (uu * p0) + (2 * u * t * p1) + (tt * p2);
        return p;
    }
}