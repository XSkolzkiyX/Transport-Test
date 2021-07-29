using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SourceScript : MonoBehaviour
{
    public int TypeOfObject, NumOfObject, MinIron = 1200, MaxIron = 125000, MinCoal = 5400, MaxCoal = 78000, MinCopper = 3200, MaxCopper = 87000, MinOil = 750, MaxOil = 6500;
    public int ResInSource;

    void Start()
    {
        switch(TypeOfObject)
        {
            case 1:
                gameObject.name = "Iron " + NumOfObject;
                ResInSource = Random.Range(MinIron, MaxIron);
                GetComponent<SpriteRenderer>().color = Color.gray;
                break;
            case 2:
                gameObject.name = "Coal " + NumOfObject;
                ResInSource = Random.Range(MinCoal, MaxCoal);
                GetComponent<SpriteRenderer>().color = Color.black;
                break;
            case 3:
                gameObject.name = "Copper "  + NumOfObject;
                ResInSource = Random.Range(MinCopper, MaxCopper);
                GetComponent<SpriteRenderer>().color = Color.yellow;
                break;
            case 4:
                gameObject.name = "Oil " + NumOfObject;
                ResInSource = Random.Range(MinOil, MaxOil);
                GetComponent<SpriteRenderer>().color = Color.blue;
                break;
            default:
                Debug.Log("Not found this type of object");
                break;
        }
    }
}