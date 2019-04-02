using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class backMenu : MonoBehaviour {

	public GameObject _myScriptC;
	private MainScript _closeConnectC;

	void Start(){
		_closeConnectC = _myScriptC.GetComponent<MainScript>();
	}

	// Use this for initialization
	public void GoToMainMenu(){
		//_closeConnectC.CloseConnection();
		DBManager.LogOut();
		SceneManager.LoadScene("main_scene");
	}
}
