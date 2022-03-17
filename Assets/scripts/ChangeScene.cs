using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour{

    public void LoadScene(string SceneToLoad){ // Laddar upp den nya scenen i spelet.
        Debug.Log("LoadScene: " + SceneToLoad);
        SceneManager.LoadScene(SceneToLoad);
    }
}