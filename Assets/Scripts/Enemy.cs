using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

public class Enemy : MonoBehaviour
{

    public Transform waypointParent;

    private Transform[] points;
    private int currentWaypoint = 1;
    private bool isOn = true;
    private Renderer rend;

    public float waypointDistance = 0.6f;
    public float speed = 1f;

    public Color onColour;
    public Color offColour;

    public float flashSpeed = 0.2f;

    public NavMeshAgent agent;
    public SerializedObject halo;

    // Start is called before the first frame update
    void Start()
    {
        points = waypointParent.GetComponentsInChildren<Transform>();
        
        halo = new SerializedObject(GetComponent("Halo"));
        InvokeRepeating("Flash", 0, flashSpeed);
        rend = GetComponent<Renderer>();        
    }

    void OnDrawGizmos()
    {
        points = waypointParent.GetComponentsInChildren<Transform>();
        if (points != null)
        {
            Gizmos.color = Color.red;

            for (int i = 1; i < points.Length -1; i++)
            {
                Transform pointA = points[i];
                Transform pointB = points[i + 1];
                Gizmos.DrawLine(pointA.position, pointB.position);
            }

            for (int i = 1; i < points.Length; i++)
            {
                Gizmos.DrawSphere(points[i].position, waypointDistance);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Get current waypoint
        Transform currentPoint = points[currentWaypoint];

        //Move towards current waypoint
        //transform.position = Vector3.MoveTowards(transform.position, currentPoint.position, speed * Time.deltaTime);

        agent.SetDestination(currentPoint.position);

        //Check if distance between waypoint is close
        float distance = Vector3.Distance(transform.position, currentPoint.position);

        //Switch to next waypoint
        if (distance < waypointDistance)
        {
            currentWaypoint++;
        }

        if (currentWaypoint == points.Length)
        {
            currentWaypoint = 1;
        }


    }

    private void Flash()
    {
        
        if (isOn)
        {
            halo.FindProperty("m_Color").colorValue = offColour;

            rend.material.shader = Shader.Find("_Color");
            rend.material.SetColor("_Color", offColour);
            rend.material.shader = Shader.Find("Specular");
            rend.material.SetColor("_SpecColor", offColour);
            isOn = false;
        }
        else
        {
            halo.FindProperty("m_Color").colorValue = onColour;
            rend.material.shader = Shader.Find("_Color");
            rend.material.SetColor("_Color", onColour);
            rend.material.shader = Shader.Find("Specular");
            rend.material.SetColor("_SpecColor", onColour);
            isOn = true;
        }

        halo.ApplyModifiedProperties();       

    }
}
