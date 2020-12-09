using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadLevelAsync : MonoBehaviour {

	// Use this for initialization
	void Start () {
       // Screen.SetResolution(900, 600, false);
        Invoke("Load", 2);
		
	}
	
	// Update is called once per frame
	void Load () {
        SceneManager.LoadSceneAsync(1);
		
	}
}
