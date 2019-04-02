using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    public void GoToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
