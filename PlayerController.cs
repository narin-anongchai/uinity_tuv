using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    #region Private Members

    private Animator _animator;

    private CharacterController _characterController;

    private float Gravity = 20.0f;

    private Vector3 _moveDirection = Vector3.zero;

    private InventoryItemBase mCurrentItem = null;

    private HealthBar mHealthBar;

    private HealthBar mFoodBar;

    private foodquestscript1  _foodquestscript1;

    private int startHealth;

    private int startFood;

    #endregion

    #region Public Member

    public float Speed = 5.0f;

    public float RotationSpeed = 240.0f;

    public Inventory Inventory;

    public GameObject Hand;

    public HUD Hud;

    public float JumpSpeed = 7.0f;

    public Joystick joystick1;

    public Button jumpBut;

    private bool PlayerisJump = false;

    public Button PickUpBut;

    private bool ItemisPickup = false;

    public Button attackBut;

    private bool isAttack = false;
    
    public GameObject wolf;
    
    public GameObject magicAttack;

    public float speed = 100f;
    
    public GameObject prefab;
    public float prefab_speed = 200.0f;

    public Transform prefab_position;

    #endregion

    // Use this for initialization
    void Start()
    {
       // Button movePlay = movePlayer.GetComponent<Button>();
    
        Button attackButton = attackBut.GetComponent<Button>();
        attackButton.onClick.AddListener(Attacker);
        Button PickUpButton = PickUpBut.GetComponent<Button>();
        PickUpButton.onClick.AddListener(PickUpItem);

        Button jumpButton = jumpBut.GetComponent<Button>();
        jumpButton.onClick.AddListener(PlayerJump);

        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
        Inventory.ItemUsed += Inventory_ItemUsed;
        Inventory.ItemRemoved += Inventory_ItemRemoved;

        mHealthBar = Hud.transform.Find("Bars_Panel/HealthBar").GetComponent<HealthBar>();
        mHealthBar.Min = 0;
        mHealthBar.Max = Health;
        startHealth = Health;
        mHealthBar.SetValue(Health);

        mFoodBar = Hud.transform.Find("Bars_Panel/FoodBar").GetComponent<HealthBar>();
        mFoodBar.Min = 0;
        mFoodBar.Max = Food;
        startFood = Food;
        mFoodBar.SetValue(Food);

        InvokeRepeating("IncreaseHunger", 0, HungerRate);
    }

    #region Inventory
    public void PlayerJump() {
        PlayerisJump = true;
    }

    //กดปุ่มหยิบของ 
     public void PickUpItem() {
         if(mInteractItem != null){
            ItemisPickup = true;
         }
    }
    
     public void Attacker() {
         isAttack = true;
     }

    private void Inventory_ItemRemoved(object sender, InventoryEventArgs e)
    {
        InventoryItemBase item = e.Item;

        GameObject goItem = (item as MonoBehaviour).gameObject;
        goItem.SetActive(true);
        goItem.transform.parent = null;

    }

    private void SetItemActive(InventoryItemBase item, bool active)
    {
        GameObject currentItem = (item as MonoBehaviour).gameObject;
        currentItem.SetActive(active);
        currentItem.transform.parent = active ? Hand.transform : null;
    }

    private void Inventory_ItemUsed(object sender, InventoryEventArgs e)
    {
        if (e.Item.ItemType != EItemType.Consumable)
        {
            // If the player carries an item, un-use it (remove from player's hand)
            if (mCurrentItem != null)
            {
                SetItemActive(mCurrentItem, false);
            }

            InventoryItemBase item = e.Item;

            // Use item (put it to hand of the player)
            SetItemActive(item, true);

            mCurrentItem = e.Item;
        }

    }

    private int Attack_1_Hash = Animator.StringToHash("Base Layer.Attack_1");

    public bool IsAttacking
    {
        get
        {
            AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.fullPathHash == Attack_1_Hash)
            {
                return true;
            }
            return false;
        }
    }

    public void DropCurrentItem()
    {
        _animator.SetTrigger("tr_drop");

        GameObject goItem = (mCurrentItem as MonoBehaviour).gameObject;

        Inventory.RemoveItem(mCurrentItem);

        // Throw animation
        Rigidbody rbItem = goItem.AddComponent<Rigidbody>();
        if (rbItem != null)
        {
            rbItem.AddForce(transform.forward * 2.0f, ForceMode.Impulse);

            Invoke("DoDropItem", 0.25f);
        }

    }

    public void DoDropItem()
    {

        // Remove Rigidbody
        Destroy((mCurrentItem as MonoBehaviour).GetComponent<Rigidbody>());

        mCurrentItem = null;
    }

    #endregion

    #region Health & Hunger

    [Tooltip("Amount of health")]
    public int Health = 100;

    [Tooltip("Amount of food")]
    public int Food = 100;

// อัตราการลดของอาคาร
    [Tooltip("Rate in seconds in which the hunger increases")]
   // public float HungerRate = 0.5f;
    private float HungerRate = 1.0f;

    public void IncreaseHunger()
    {
        Food--;
        if (Food < 0)
            Food = 0;

        mFoodBar.SetValue(Food);

        if (IsDead)
        {
            CancelInvoke();
            _animator.SetTrigger("death");
            SceneManager.LoadScene("levelSelectScene");
        }
    }

    public bool IsDead
    {
        get
        {
            return Health == 0 || Food == 0;
        }
    }

    public bool IsArmed
    {
        get
        {
            if (mCurrentItem == null)
                return false;

            return mCurrentItem.ItemType == EItemType.Weapon;
        }
    }


    public void Eat(int amount)
    {
        Food += amount;
        if (Food > startFood)
        {
            Food = startFood;
        }
        mFoodBar.SetValue(Food);
    }

    public void Rehab(int amount)
    {
        Health += amount;
        if (Health > startHealth)
        {
            Health = startHealth;
        }

        mHealthBar.SetValue(Health);
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;
        if (Health < 0)
            Health = 0;

        mHealthBar.SetValue(Health);

        if (IsDead)
        {
            _animator.SetTrigger("death");
            SceneManager.LoadScene("levelSelectScene");
        }

    }

    #endregion

    void FixedUpdate()
    {
        if (!IsDead)
        {
            // Drop item
            if (mCurrentItem != null && Input.GetKeyDown(KeyCode.R))
            {
                DropCurrentItem();
            }
        }
    }

    private bool mIsControlEnabled = true;

    public void EnableControl()
    {
        mIsControlEnabled = true;
    }

    public void DisableControl()
    {
        mIsControlEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(joystick1.Horizontal);
        if (!IsDead && mIsControlEnabled)
        {
            //หยิบ items
            // Interact with the item
            //if (mInteractItem != null && Input.GetKeyDown(KeyCode.F))
            if (ItemisPickup == true)
            {
                // Interact animation
                mInteractItem.OnInteractAnimation(_animator);
               //เรียก function
                InteractWithItem();
                ItemisPickup = false;
                Food += 20;
                //หยุด เอนิมเมชั่น
            }

            
            //if (mCurrentItem != null && Input.GetMouseButtonDown(0))
            {
                // Dont execute click if mouse pointer is over uGUI element
                //if (!EventSystem.current.IsPointerOverGameObject())
                if (isAttack == true)
                {
                    // TODO: Logic which action to execute has to come from the particular item
                    _animator.SetTrigger("attack_1");
                    /* GameObject instmagicAttack = Instantiate(magicAttack,transform.position,Quaternion.identity) as GameObject;
                    Rigidbody instmagicAttackRigidbody = instmagicAttack.GetComponent<Rigidbody>();
                    instmagicAttack.GetComponent<Rigidbody>().AddForce(Vector3.forward * speed); */
                    StartCoroutine(prefab_effect());
                    attackBut.interactable = false;
                    isAttack = false;
                }
                else
                {
                }
            }

            // Get Input for axis
            //float h = Input.GetAxis("Horizontal");
            float h = joystick1.Horizontal;
            //float v = Input.GetAxis("Vertical");
            float v = joystick1.Vertical;

            // Calculate the forward vector
            Vector3 camForward_Dir = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 move = v * camForward_Dir + h * Camera.main.transform.right;

            if (move.magnitude > 1f) move.Normalize();

            // Calculate the rotation for the player
            move = transform.InverseTransformDirection(move);

            // Get Euler angles
            float turnAmount = Mathf.Atan2(move.x, move.z);

            transform.Rotate(0, turnAmount * RotationSpeed * Time.deltaTime, 0);

            if (_characterController.isGrounded)
            {
                _moveDirection = transform.forward * move.magnitude;

                _moveDirection *= Speed;

                //if (Input.GetButton("Jump"))
                if(PlayerisJump == true || Input.GetButton("Jump"))
                {
                    _animator.SetBool("jumpair", true);
                    _moveDirection.y = JumpSpeed;
                    PlayerisJump = false;

                }
                else
                {
                    _animator.SetBool("jumpair", false);
                    _animator.SetBool("walk", move.magnitude > 0);
                }
            }

            _moveDirection.y -= Gravity * Time.deltaTime;

            _characterController.Move(_moveDirection * Time.deltaTime);
        }
    }

    IEnumerator prefab_effect()
    {
        
            yield return new WaitForSeconds(1.0f);
            GameObject instant_prefab = Instantiate(prefab, prefab_position.position,Quaternion.identity) as GameObject;
            Rigidbody instant_prefabRigidbody = instant_prefab.GetComponent<Rigidbody>();
            attackBut.interactable = true;
    }
    public void InteractWithItem()
    {
        if (mInteractItem != null)
        {
            mInteractItem.OnInteract();

            if (mInteractItem is InventoryItemBase)
            {
                InventoryItemBase inventoryItem = mInteractItem as InventoryItemBase;
                Inventory.AddItem(inventoryItem);
                inventoryItem.OnPickup();

                if (inventoryItem.UseItemAfterPickup)
                {
                    Inventory.UseItem(inventoryItem);
                }
            }
        }

        Hud.CloseMessagePanel();
        mInteractItem = null;
    }

    private InteractableItemBase mInteractItem = null;

    private void OnTriggerEnter(Collider other)
    {
        InteractableItemBase item = other.GetComponent<InteractableItemBase>();

        if (item != null)
        {
            if (item.CanInteract(other))
            {

                mInteractItem = item;

                Hud.OpenMessagePanel(mInteractItem);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        InteractableItemBase item = other.GetComponent<InteractableItemBase>();
        if (item != null)
        {
            Hud.CloseMessagePanel();
            mInteractItem = null;
        }
    }
}
