using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelQuest1 : MonoBehaviour
{
    public Button sendAns;
    public Button GoBackAns;
    
    public GameObject panelquestx;
    public GameObject foodquest1;
    public Text answerText;
    public string answ;
   // public Text map1scoretext;
    //public int map1score = 0;
    // Start is called before the first frame update
    void Start()
    {
        Button sendAnswer = sendAns.GetComponent<Button>();
        sendAnswer.onClick.AddListener(sendansv);    
    
        Button GoBackAnsw = GoBackAns.GetComponent<Button>();
        GoBackAnsw.onClick.AddListener(gobackv);

        //map1scoretext.text = "" + DBManager.score;
    }
    void sendansv() {
        if(answerText.text == answ){
            panelquestx.SetActive(false);

            DBManager.score += 1;

            Destroy(foodquest1);
        }
    }
    void gobackv () {
        panelquestx.SetActive(false);
        foodquest1.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
       // map1scoretext.text = map1score.ToString();
    }
}