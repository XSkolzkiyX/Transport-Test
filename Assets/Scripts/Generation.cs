using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation : MonoBehaviour
{
    public GameObject ResourcePrefab;
    public int ResourceAmount, ResourceCoordLimit;
    private int CountIron, CountCoal, CountCopper, CountOil;

    void Start()
    {
        GetComponent<CameraScript>().CoordLimit = ResourceCoordLimit;

        GameObject IronSources = new GameObject();
        IronSources.name = "IronSources";
        GameObject CoalSources = new GameObject();
        CoalSources.name = "CoalSources";
        GameObject CopperSources = new GameObject();
        CopperSources.name = "CopperSources";
        GameObject OilSources = new GameObject();
        OilSources.name = "OilSources";

        for (int i = 0; i < ResourceAmount; i++)
        {
            GameObject Resource = Instantiate(ResourcePrefab);
            Resource.transform.position = new Vector2(Random.Range(-ResourceCoordLimit, ResourceCoordLimit), Random.Range(-ResourceCoordLimit, ResourceCoordLimit));
            int TypeOfObject = Random.Range(0, 100);
            Resource.AddComponent<SourceScript>();
            Resource.AddComponent<CircleCollider2D>();
            if (TypeOfObject >= 0 && TypeOfObject <= 40)
            {
                Resource.GetComponent<SourceScript>().TypeOfObject = 1;
                CountIron++;
                Resource.GetComponent<SourceScript>().NumOfObject = CountIron;
                Resource.transform.parent = IronSources.transform;
                GetComponent<CameraScript>().textTargets[i] = Resource;
            }
            else if (TypeOfObject > 40 && TypeOfObject <= 65)
            {
                Resource.GetComponent<SourceScript>().TypeOfObject = 2;
                CountCoal++;
                Resource.GetComponent<SourceScript>().NumOfObject = CountCoal;
                Resource.transform.parent = CoalSources.transform;
                GetComponent<CameraScript>().textTargets[i] = Resource;
            }
            else if (TypeOfObject > 65 && TypeOfObject <= 90)
            {
                Resource.GetComponent<SourceScript>().TypeOfObject = 3;
                CountCopper++;
                Resource.GetComponent<SourceScript>().NumOfObject = CountCopper;
                Resource.transform.parent = CopperSources.transform;
                GetComponent<CameraScript>().textTargets[i] = Resource;
            }
            else if (TypeOfObject > 90 && TypeOfObject <= 100)
            {
                Resource.GetComponent<SourceScript>().TypeOfObject = 4;
                CountOil++;
                Resource.GetComponent<SourceScript>().NumOfObject = CountOil;
                Resource.transform.parent = OilSources.transform;
                GetComponent<CameraScript>().textTargets[i] = Resource;
            }
            else
            {
                Debug.Log("Invalid value in Resource Generation");
                TypeOfObject = Random.Range(0, 100);
            }
        }
        Debug.Log("Count of Iron: " + CountIron);
        Debug.Log("Count of Coal: " + CountCoal);
        Debug.Log("Count of Copper: " + CountCopper);
        Debug.Log("Count of Oil: " + CountOil);
    }
}
