using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class loginScript : MonoBehaviour {

	public InputField nameField;
	public InputField passwordField;

	public Button submitButton;

	public Text loginResultText1;
	public Text loginResultText;

	public GameObject panelpopupin;
	public GameObject panelpopup;
	public void CallLogin()
	{
		StartCoroutine(Login());
	}
	IEnumerator Login() {
		WWWForm form = new WWWForm();
		form.AddField("name", nameField.text);
		form.AddField("password", passwordField.text);
		WWW www = new WWW("http://thepudom.ac.th/sqlconnect/login.php", form);
		yield return www;
		if (www.text[0] == '0'){
			DBManager.username = nameField.text;
			DBManager.score = int.Parse(www.text.Split('\t')[1]);
			loginResultText.text = "เข้าสู่ระบบเรียบร้อย กดตกลง เพื่อเข้าเล่นเกม...";
			//SceneManager.LoadScene("MainMenu");
			panelpopup.SetActive(true);
		} else {
			Debug.Log("User Login Failed. Error #" + www.text);
			loginResultText1.text = "ชื่อผู้ใช้/รหัสผ่าน ไม่ถูกต้อง โปรดลองอีกครั้ง";
			panelpopupin.SetActive(true);
		}
	}
	public void VerifyInputs() {
		submitButton.interactable = (nameField.text.Length >= 5 && passwordField.text.Length >= 5);
	}
	public void GoToMainMenu(){
		SceneManager.LoadScene("MainMenu");
	}
	public void GoToGame1() {
		SceneManager.LoadScene("levelSelectScene");
	}
}
