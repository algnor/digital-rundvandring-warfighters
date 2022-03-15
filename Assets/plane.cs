using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;
using UnityEngine.UI;

public class plane : MonoBehaviour
{
    public ARRaycastManager raycastManager;
    public GameObject prefab;
    private GameObject btn;
    private GameObject placeText;
    private GameObject[] BTNS;
   
    public GameObject currentSkrapan;

    private int year=0;

    List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private void Start()
    {
        btn = GameObject.Find("Canvas/Button");
        placeText = GameObject.Find("Canvas/placeText");
        BTNS = GameObject.FindGameObjectsWithTag("placeBTN");
        for (int i = 0; i < BTNS.Length; i++)
        {
            BTNS[i].SetActive(false);
        }
        if (GameObject.FindGameObjectsWithTag("building").Length >0)
        {
           currentSkrapan = Instantiate(prefab, GameObject.FindGameObjectWithTag("building").transform.position, Quaternion.Euler(0, 0, 270), transform);
        }
        else
        {
            currentSkrapan = Instantiate(prefab, new Vector3(0.25f,-0.16f,1.5f), Quaternion.Euler(0, 0, 270), transform);

        }
        Debug.Log(currentSkrapan.transform.position);

    }

    private void Update()
    {
        if(year == 1)        {

        }
    }

    public void Place()
    {
        if (raycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits))
        {
            if (hits.Count > 0)
            {

                currentSkrapan = Instantiate(prefab, hits[0].pose.position, Quaternion.Euler(0, 0, 270), transform);

            }
        }

        btn.SetActive(false);
        placeText.SetActive(false);
        for (int i = 0; i < BTNS.Length; i++)
        {
            BTNS[i].SetActive(true);
        }

    }

    public void changeYear(int input)
    {
        if(input == 1)
        {
            year++;
        }
        else
        {
            year--;
        }
        
    }
}
