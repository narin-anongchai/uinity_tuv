using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
//using TechTweaking.Bluetooth;
using UnityEngine.SceneManagement;

public class MainScript : MonoBehaviour {
	//public  BluetoothDevice device;
	//public Text statusText;
	//public Text lightStatus;
	public Text playerDisplay;
	public Text scoreDisplay;

	public Button map1but;
	public Button map2but;
	public Button map3but;
	private string type;
//	public Text timerText;
//	private float timer = 10;
	//public Text serailReadText;
	// Use this for initialization
	void Awake () {
		if(DBManager.username == null)
		{
			SceneManager.LoadScene("MainMenu");
		}
		playerDisplay.text = "ชื่อผู้ใช้งาน: " + DBManager.username;
		scoreDisplay.text = "คะแนนสะสม: " + DBManager.score;
		
		map1inter();
		map2inter();
		map3inter();
		
	}
	public void BackToMain() {
		SceneManager.LoadScene("MainMenu");
	}
	public void map1inter() {
		map1but.interactable = (DBManager.score >= 0);
	}
	public void map2inter() {
		map2but.interactable = (DBManager.score >= 4);
	}
	public void map3inter() { 
		map3but.interactable = (DBManager.score >= 8);
		}
		public void CallSaveData() {
		Debug.Log("เรียก function CallSaveData");
		StartCoroutine(SavePlayerData());
	}
	//เก็บค่า score
	IEnumerator SavePlayerData()
	{
		Debug.Log("เรียก function SavePlayerData");
		WWWForm form = new WWWForm();
		form.AddField("name", DBManager.username);
		form.AddField("score", DBManager.score);

		WWW www = new WWW("http://thepudom.ac.th/sqlconnect/savedata.php",form);
		yield return www;

		if(www.text == "0") {
			Debug.Log("Data Saved");
		}else{
			Debug.Log("Save Failed. Error #" + www.text);
		}
	}
	public void GoToMap1() {
		SceneManager.LoadScene("map1");
	}
	public void GoToMap2() {
		SceneManager.LoadScene("map2");
	}
	public void GoToMap3() {
		SceneManager.LoadScene("map3");
	}
	
	void Update() {
		/* if(device.IsConnected){
			statusText.text = "สถานะ : พร้อมใช้งาน";
		}else
		{
			statusText.text = "สถานะ : ไม่พร้อมใช้งาน ติดต่อผู้ดูแล";
		}

		if (device.IsReading) {
			byte [] msg = device.read ();
			if (msg != null ) {
				string content = System.Text.ASCIIEncoding.ASCII.GetString (msg);
				statusText.text = "MSG : " + content;
			} 
		}*/
	}
	
}
