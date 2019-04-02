using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
	public Text playerDisplay;
	
	// Use this for initialization
	private void Start () {
		if (DBManager.LoggedIn)
		{
			playerDisplay.text = "สวัสดี คุณ: " + DBManager.username +" คุณมีคะแนนสะสม =" + DBManager.score;
		}
		else 
		playerDisplay.text = "คุณยังไม่เข้าสู่ระบบ";
	}
	public void CreditSceneX() {
		SceneManager.LoadScene("CreditScene");
	}
	public void GoToRegister(){
		SceneManager.LoadScene("register_Scene");
	}
	public void GoToLogin() {
		SceneManager.LoadScene("login_Scene");
	}
	public void ExitGame(){
		Application.Quit();
	}
	public void logoutUser(){
		DBManager.LogOut();
		playerDisplay.text = "คุณยังไม่เข้าสู่ระบบ";
	}
}
