using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class foodquestscript2 : MonoBehaviour
{
    //private PlayerController _PlayerController;
    private InteractableItemBase InventoryItemBase;
    public GameObject panelquest1;
    public GameObject foodsheep1;
   private bool collid_true = false;
    // Start is called before the first frame update
    void Start()
    {
    }
    public void questionPanel()
    {
      collid_true = true;
       // animator.SetTrigger("pickup");
      // panelquest1.SetActive(true);
        
    }

    void Update()
    {
       // if(_PlayerController.ItemisPickup == true ){
       //      panelquest1.SetActive(true);
      //  }



    }
    void OnTriggerEnter(Collider other)
    {
       if (other.gameObject.tag == "Player")
        {
           // if(collid_true){
            panelquest1.SetActive(true);
            foodsheep1.SetActive(true);
        }
    }
    
  /*   private void OnTriggerExit(Collider other)
    {
       
    }*/
}
