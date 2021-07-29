using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportDroneScript : MonoBehaviour
{
    public float Speed, WaitOnPoint;
    public GameObject RouteForDrone;
    public GameObject RealRoute, Target;
    public int CurrentPos, MaxPos, Direction = 1;
    public bool JustStarted = true;
    private bool CoolDownStarted = false, NoNeedToExpand = false;
    float RadiusK = 0;
    Collider2D Radius;

    void Start()
    {
        Radius = gameObject.AddComponent<CircleCollider2D>();
        Rigidbody2D Rb = gameObject.AddComponent<Rigidbody2D>();
        Rb.isKinematic = true;
        Radius.isTrigger = true;
    }

    void Update()
    {
        RadiusK = Mathf.Clamp(RadiusK, 0, 3);
        //CurrentPos = Mathf.Clamp(CurrentPos, -1, MaxPos);
        //MaxPos = Mathf.Clamp(MaxPos, 0, Mathf.Infinity);
        if (RouteForDrone.transform.hasChanged != false)
        {
            JustStarted = true;
            RealRoute = RouteForDrone.transform.GetChild(0).gameObject;
            MaxPos = RealRoute.transform.childCount - 1;
        }

        if(RouteForDrone!=null)
        {
            if(CurrentPos >= MaxPos)
            {
                Direction = -Direction;
                JustStarted = false;
            }
            else if (CurrentPos < 0 && JustStarted != true)
            {
                Direction = -Direction;
            }
            Target = RealRoute.transform.GetChild(CurrentPos + Direction).gameObject;
            if (transform.position == Target.transform.position && CoolDownStarted != true)
            {
                StartCoroutine(CoolDown());
                StartFilling();
                CoolDownStarted = true;
            }
        }

        if (Target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, Speed * Time.deltaTime);
        }

        if (Radius != null && NoNeedToExpand != true)
        {
            RadiusK++;
            //Radius.transform.localScale = new Vector2(RadiusK, RadiusK);
            Radius.GetComponent<CircleCollider2D>().radius = RadiusK;
        }
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSecondsRealtime(WaitOnPoint);
        CurrentPos += Direction;
        CoolDownStarted = false;
        RadiusK = 0;    
    }

    void StartFilling()
    {
        RadiusK = 0;
    }

    private void OnTriggerEnter2D(Collider2D TargetSource)
    {
        Debug.Log("Got TargetSource");
        NoNeedToExpand = true;
        TargetSource.GetComponent<SourceScript>();
    }
}