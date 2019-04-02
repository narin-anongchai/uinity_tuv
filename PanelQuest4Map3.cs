using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PanelQuest4Map3 : MonoBehaviour
{
    public Button sendAns;
    public Button GoBackAns;
    
    public GameObject panelquestx;
    public GameObject foodquest1;
    public Text answerText;
    public string answ;

    // Start is called before the first frame update
    void Start()
    {
        Button sendAnswer = sendAns.GetComponent<Button>();
        sendAnswer.onClick.AddListener(sendansv);    
    
        Button GoBackAnsw = GoBackAns.GetComponent<Button>();
        GoBackAnsw.onClick.AddListener(gobackv);
    }
    void sendansv() {
        if(answerText.text == answ){
            panelquestx.SetActive(false);

            DBManager.score += 1;

            Destroy(foodquest1);


SceneManager.LoadScene("levelSelectScene");
        }
    }
    void gobackv () {
        panelquestx.SetActive(false);
        foodquest1.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}