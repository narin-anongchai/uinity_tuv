using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crate : InteractableItemBase {

    public GameObject foodCheck;
    public GameObject text1;
    public Button buttontext1;
    private bool mIsOpen = false;

   // private bool isDestroyed = true;
    void start () {
        /* Button buttontext1panel = buttontext1.GetComponent<Button>();
        buttontext1.onClick.AddListener(closetext1); */
    }
    public override void OnInteract()
    {
        InteractText = "Press F to ";
        GetComponent<Animator>().SetBool("open",true);
          /* mIsOpen = !mIsOpen;
         InteractText += mIsOpen ? "to close" : "to open";
         GetComponent<Animator>().SetBool("open", mIsOpen);*/
        StartCoroutine(closeCrate());
       // Debug.Log(foodCheck);
//เช็คว่า อาหาร อยู่หรือไหม ถ้า อยู่ให้  แสดง หน้าต่าง pop up ถ้าไม่มี ก็ไม่ค้องแสดง
        if(foodCheck != null){
            text1.SetActive(true);
        }
    }
    IEnumerator closeCrate()
    {
        yield return new WaitForSeconds(5);
        GetComponent<Animator>().SetBool("open",false);
    }
}
