using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable CS0618
public class RouteCreater : MonoBehaviour
{
    public float CurrentPointInRoute, CurrentRouteNumber = 0;
    public bool Creating = false;
    private GameObject CurrentRoute, RouteList, Points, Connections;
    private int CountOfRoutes, CountOfPointsInRoute;
    public GameObject CirclePrefab;
    Color RouteColor;
    void Start()
    {
        RouteList = new GameObject("List of Rotes");
        GetComponent<CameraScript>().RouteList = RouteList;
    }

    void Update()
    {
        CurrentPointInRoute = Mathf.Clamp(CurrentPointInRoute, 0, Mathf.Infinity);
        CurrentRouteNumber = Mathf.Clamp(CurrentRouteNumber, 0, Mathf.Infinity);
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (CurrentRouteNumber == 0 || GameObject.Find("Route ¹" + CurrentRouteNumber).transform.childCount > 0)
            {
                Creating = true;
                CurrentRouteNumber++;
                CreateRoute();
            }
        }
        if (Creating != false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                FillRoute();
            }
            if (Input.GetMouseButtonDown(1))
            {
                DeleteFromRoute();
            }
            if (Input.GetKeyDown(KeyCode.Return) && GameObject.Find("Route ¹" + CurrentRouteNumber).transform.childCount > 1)
            {
                Creating = false;
            }
        }
    }

    void CreateRoute()
    {
        RouteColor = Random.ColorHSV();
        //Debug.Log(RouteColor);
        CurrentRoute = new GameObject("Route ¹" + CurrentRouteNumber);
        CurrentRoute.transform.parent = RouteList.transform;
        CurrentPointInRoute = 0;
        Points = new GameObject("Points");
        Points.transform.parent = CurrentRoute.transform;
        Connections = new GameObject("Connections");
        Connections.transform.parent = CurrentRoute.transform;
    }

    void FillRoute()
    {
        CurrentPointInRoute++;
        
        GameObject NewRoute = Instantiate(CirclePrefab);
        NewRoute.transform.parent = Points.transform;
        NewRoute.GetComponent<SpriteRenderer>().color = RouteColor;
        Vector3 mousePos = Input.mousePosition;
        NewRoute.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10));
        NewRoute.name = "Point ¹" + CurrentPointInRoute + " in route ¹" + CurrentRouteNumber;
        if (CurrentPointInRoute > 1)
        {
            DrawLine();
        }
    }

    void DeleteFromRoute()
    {
        Destroy(GameObject.Find("Point ¹" + CurrentPointInRoute + " in route ¹" + CurrentRouteNumber));
        float LastPointInRoute = CurrentPointInRoute - 1;
        Destroy(GameObject.Find("Connect btw" + CurrentPointInRoute + " & " + LastPointInRoute + " in " + CurrentRoute + " route"));
        CurrentPointInRoute--;
    }

    void DrawLine()
    {
        List<Vector3> pos = new List<Vector3>();
        float LastPointInRoute = CurrentPointInRoute - 1;
        pos.Add(GameObject.Find("Point ¹" + LastPointInRoute + " in route ¹" + CurrentRouteNumber).transform.position);
        pos.Add(GameObject.Find("Point ¹" + CurrentPointInRoute + " in route ¹" + CurrentRouteNumber).transform.position);
        LineRenderer Connection = new GameObject("Connect btw" + CurrentPointInRoute + " & " + LastPointInRoute + " in " + CurrentRoute + " route").AddComponent<LineRenderer>();
        Connection.transform.parent = Connections.transform;
        Connection.material = new Material(Shader.Find("Sprites/Default"));
        Connection.SetColors(RouteColor, RouteColor);
        Connection.SetWidth(0.25f, 0.25f);
        Connection.SetPositions(pos.ToArray());
    }
}