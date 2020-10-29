using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    
    public static void Explode(Transform target, float radius, float power, float lift, string layer, bool debug = false)
    {
        Vector3 explosionPos = target.position;

        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);

        foreach (Collider hit in colliders)
        {
            if (hit.gameObject.layer == LayerMask.NameToLayer(layer))
            {
                if (hit.GetComponent<Animation>())
                {
                    hit.GetComponent<Animation>().Stop();
                }
                if (hit.GetComponent<Rigidbody>())
                {
                    if (debug) Debug.Log("Explode: " + hit.gameObject.name);
                    hit.GetComponent<Rigidbody>().AddExplosionForce(power, explosionPos, radius, lift);
                }
                else
                {
                    if (debug) Debug.Log("Explode (Add Collider):" + hit.gameObject.name);
                    var n = hit.gameObject.AddComponent<Rigidbody>();
                    n.AddExplosionForce(power, explosionPos, radius, lift);
                }

                //Destroy(target.gameObject, 10f);
            }
        }
    }

    public static float GetDistanceToGround(Transform source, bool precision = false)
    {
        float distance = 0f;

        RaycastHit hit;
        if (Physics.Raycast(source.position, -source.up, out hit))
        {
            if (precision)
            {
                distance = hit.distance;
            }
            else
            {
                distance = Mathf.Round(hit.distance * 3.28084f);
            }

        }

        return distance;
    }

    public static float GetDistanceToGround(Transform source, Transform target)
    {
        float distance = 0f;

        if (target)
        {
            // Determine vertical line down
            float angleTo90 = 180 + Vector3.Angle(source.position, target.position);
            Vector3 downDirection = -source.up - new Vector3(0f, angleTo90, 0f);

            //Debug.DrawRay(source.position, downDirection.normalized * 500f, Color.green);

            RaycastHit hit;
            if (Physics.Raycast(source.position, downDirection.normalized, out hit))
            {
                distance = hit.distance;
            }
        }
        

        return distance;
    }

    public static float GetDistanceToTarget(Transform source, Transform target, bool precision = false)
    {
        /*
        float distance = 0f;
        
        RaycastHit hit;
        if (Physics.Raycast(source.position, (target.position - source.position), out hit))
        {
            //Debug.Log(hit.distance.ToString());
            distance = hit.distance;
        }
        return distance;
        */
        float distance = Vector3.Distance(source.position, target.position);

        return precision ? distance : Mathf.Round(distance * 3.28084f);
    }

    public static float GetDistanceToTargetAtGround(Transform source, Transform target)
    {
        float distance = 0f;
        if (target)
        {
            float height = GetDistanceToGround(source, target);
            float longAngle = Vector3.Distance(source.position, target.position);

            float h = Convert.ToSingle(Math.Pow(longAngle, 2)) - Convert.ToSingle(Math.Pow(height, 2));
            distance = Mathf.Sqrt(Mathf.Abs(h));

            //Debug.Log("height: " + height + " distance: " + distance);
        }

        return distance;
    }

    public static float GetHeading(Rigidbody rb)
    {
        return Mathf.Round(rb.rotation.eulerAngles.y);
    }

    public static float GetHeadingToTarget(Transform source, Transform target, bool precision = false)
    {
        Vector3 targetDirection = (target.transform.position - source.transform.position);
        float angleToTarget = (Mathf.Atan2(targetDirection.z, targetDirection.x) * Mathf.Rad2Deg) - 90f;
        float heading = angleToTarget;
        if (angleToTarget >= -90f && angleToTarget < 0f)
        {
            // North-East
            //Debug.Log("NE:" + angleToTarget);
            heading = heading * -1f;
        }
        else if (angleToTarget >= -180f && angleToTarget < -90f)
        {
            // South-East
            //Debug.Log("SE:" + angleToTarget);
            heading = heading * -1f;
        }
        else if (angleToTarget >= -270f && angleToTarget < -180f)
        {
            // South-West
            //Debug.Log("SW:" + angleToTarget);
            heading = heading * -1f;
        }
        else if (angleToTarget >= 0f && angleToTarget < 90f)
        {
            // North-West
            //Debug.Log("NW:" + angleToTarget);
            heading = (heading - 360f) * -1f;
        }

        return precision ? heading : Mathf.Round(heading);
    }

    public static float ToFeet(float number, bool round = true)
    {
        return round ? Mathf.Round(number * 3.28084f) : number * 3.28084f;
    }
}
