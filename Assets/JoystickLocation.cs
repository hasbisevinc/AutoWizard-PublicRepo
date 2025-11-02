using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoystickLocation : MonoBehaviour
{
    private float defaultScale = 1.5f;
    private float maxScale = 30f;

    private bool clicked = false;

    void Start()
    {
        gameObject.GetComponent<Image>().color = new Color32(255,255,255,0);
        gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color32(255,255,255,0);
        transform.localScale  = new Vector3(maxScale, maxScale, maxScale);
    }

    void Update()
    {
        if (Input.touchCount > 0) {
           var fingerPos =  Input.GetTouch(0);
           if (fingerPos.phase == TouchPhase.Began)
           {
               if (!clicked) {
                    gameObject.SetActive(true);
                    Vector3 pos = fingerPos.position;
                    transform.position = pos;
                    transform.localScale  = new Vector3(defaultScale, defaultScale, defaultScale);
                    gameObject.GetComponent<Image>().color = new Color32(255,255,255,255);
                    gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color32(255,255,255,255);
               }
               clicked = true;
           } else if (fingerPos.phase == TouchPhase.Ended)
           {
               gameObject.GetComponent<Image>().color = new Color32(255,255,255,0);
               gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color32(255,255,255,0);
               transform.localScale  = new Vector3(maxScale, maxScale, maxScale);
               clicked = false;
           }
        }

    }

    
}
