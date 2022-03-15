using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;



public  class SceneSwitcher : MonoBehaviour
{

    public GameObject[] skrapor;
    public int yearIndex = 0;
    public GameObject ARSession;
    GameObject currentSkrapan;
    GameObject skrapan;
    Transform skrapanTransform;
    public void GetSkrapan()
    {
        currentSkrapan = ARSession.GetComponent<plane>().currentSkrapan;
        skrapan = Instantiate(currentSkrapan);
        skrapan.SetActive(false);
        skrapanTransform = skrapan.transform;

    }

    public void reLoad(int value)
    {
        Debug.Log(yearIndex);
        if (yearIndex > skrapor.Length-1) yearIndex = 0;
        if (yearIndex == -1) yearIndex = skrapor.Length -1;
        GameObject oldSkrapan = currentSkrapan;
        currentSkrapan = Instantiate(skrapor[yearIndex], skrapanTransform.position, skrapanTransform.rotation, ARSession.transform);
        Destroy(oldSkrapan);

        yearIndex += value;
    }
    public void LoadSceneOnTop()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByBuildIndex(0));
        SceneManager.LoadScene("test1", LoadSceneMode.Additive);
        int n = SceneManager.sceneCount;
        
        
            
        


    }
 
    public static void UnLoadSceneOnTop(Scene scene)
    {
        int n = SceneManager.sceneCount;
        if (n > 1)
        {
            SceneManager.UnloadSceneAsync(scene.ToString());
        }
    }
}