using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour
{
    private bool ActivateF1 = true, ActivateF2 = true;

    public float Speed, ZoomSpeed;
    
    public Canvas Can;
    public GameObject[] Texts, textTargets;

    public Font ResFont;
    public GameObject RouteList;

    public float CoordLimit;
    void Start()
    {
        Texts = new GameObject[textTargets.Length];
        for (int i = 0; i < textTargets.Length; i++)
        {
            GameObject ResText = new GameObject();
            ResText.name = " Text To " + i + " Object";
            ResText.transform.parent = Can.transform;
            ResText.AddComponent<Text>();
            ResText.GetComponent<Text>().font = ResFont;
            ResText.GetComponent<Text>().fontStyle = FontStyle.BoldAndItalic;
            ResText.GetComponent<Text>().fontSize = 24;
            ResText.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 80);
            ResText.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
            Texts[i] = ResText;
        }
    }

    void Update()
    {
       
        if(Input.GetKeyDown(KeyCode.F1))
        {
            ActivateF1 = !ActivateF1;
        }

        if (ActivateF1 != false)
        {
            Can.gameObject.SetActive(true);
            for (int i = 0; i < Texts.Length; i++)
            {
                Texts[i].GetComponent<Text>().text = textTargets[i].name + "\n" + textTargets[i].GetComponent<SourceScript>().ResInSource;
                Texts[i].transform.position = Camera.main.WorldToScreenPoint(textTargets[i].transform.position);
            }
        }
        else
        {
            Can.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            ActivateF2 = !ActivateF2;
        }
        if (ActivateF2 != true && RouteList != null)
        {
            RouteList.SetActive(false);
        }
        else
        {
            RouteList.SetActive(true);
        }

        //Moving 1
        float MoveHorizontal = Input.GetAxis("Horizontal") * Time.deltaTime * Speed;
        float MoveVertical = Input.GetAxis("Vertical") * Time.deltaTime * Speed;
        transform.Translate(MoveHorizontal, MoveVertical, 0);

        //Moving 2 (not finished)
        Vector2 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //Debug.Log(objPosition);

        //Borders
        Camera.main.transform.position = new Vector3(Mathf.Clamp(Camera.main.transform.position.x, -CoordLimit, CoordLimit), 
            Mathf.Clamp(Camera.main.transform.position.y, -CoordLimit, CoordLimit), Camera.main.transform.position.z);

        //Field of view
        float FieldOfView = Input.GetAxis("Mouse ScrollWheel");
        GetComponent<Camera>().fieldOfView = Mathf.Clamp(GetComponent<Camera>().fieldOfView += FieldOfView * -ZoomSpeed, 50, 160);
    }
}