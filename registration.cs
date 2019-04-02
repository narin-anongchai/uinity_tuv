using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class registration : MonoBehaviour {

	// Use this for initialization
	public InputField namefield;
	public InputField passwordField;
	public Button submitButton;
	public GameObject panelpopup;

	public Text registResultText;

	public void  CallRegister(){
		StartCoroutine(Register());
	}

	IEnumerator Register(){
		WWWForm form = new WWWForm();
		form.AddField("name", namefield.text);
		form.AddField("password", passwordField.text);
		WWW www = new WWW("http://thepudom.ac.th/pythongame/register.php", form);
		yield return www;
		if(www.text == "0"){
			Debug.Log("User Created Successfully.");
			//SceneManager.LoadScene("main_scene");
			registResultText.text = "คุณได้ลงทะเบียนเรียบร้อยแล้ว";
			panelpopup.SetActive(true);
		}
		else 
		{
			registResultText.text = "การลงทะเบียนไม่ถูกต้อง โปรดลองใหม่";
			Debug.Log("User crestion fialed. Errer #"+www.text);
			//panelpopup.SetActive(true);
		}
	}
	public void VeryFyInluts(){
		submitButton.interactable = (namefield.text.Length >= 5 && passwordField.text.Length >=5);
	}
	public void GoToMainMenu(){
	SceneManager.LoadScene("MainMenu");
	}
}
