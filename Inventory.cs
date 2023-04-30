using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Inventory : MonoBehaviour
{
    AudioSource audioSource;

    public Dictionary<string, int> items = new Dictionary<string, int>();
    public Dictionary<string, int> itemlist;
    
    public AudioClip motorStartup;
    public AudioClip correctClip;
    public AudioClip wrongClip;

    public Button deliveryButton;
    public GameObject inventory;

    public TextMeshProUGUI EggsCountText;
    public TextMeshProUGUI MilkCountText;
    public TextMeshProUGUI JamCountText;
    public TextMeshProUGUI countDownText;

    public float countDownTime = 5.0f;

    public bool buttonIsPressed;

    private float startTimeBtn;
    private float startTimeInv;

    private bool isCountingDownBtn;
    private bool isCountingDownInv;
    private bool canFulfillOrder;
    private bool deliveryOK;
    private bool showButton;
    private bool showInventory;
    private bool buttonCanBePressed;


    void Start(){
        isCountingDownBtn = false;
        isCountingDownInv = true;
        
        showButton = false;
        
        showInventory = true;

        audioSource = GetComponent<AudioSource>();

        items.Add("Eggs", 3);
        items.Add("Milk", 3);
        items.Add("Jam", 3);
    }


    public void AddItem(string itemName, int quantity){
        if(items.ContainsKey(itemName)){
            items[itemName] += quantity;
        } else {
            items.Add(itemName, quantity);
        }
    }


    public void RemoveItem(string itemName, int quantity){        
        if(items.ContainsKey(itemName)){
            items[itemName] -= quantity;
        }
    }


    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("House")){
            startTimeBtn = Time.time;
            isCountingDownBtn = true;

            showButton = true;
            buttonCanBePressed = true;

            PlayerMover.IsDriving = false;
            
            House house = other.GetComponent<House>();
            itemlist = house.wishlist;

            canFulfillOrder = true;
            
            foreach (KeyValuePair<string, int> item in itemlist)
            {
                if (items[item.Key] < item.Value)
                {
                    canFulfillOrder = false;
                    break;
                }
            }

            foreach(KeyValuePair<string, int> item in items){
                //Debug.Log("inv: " + item.Key + ", " + item.Value);
            }

            if(canFulfillOrder){
                Debug.Log("Can fulfill");
            } else {
                Debug.Log("Cannot fulfill");
            }

        } else if(other.CompareTag("Shop")){
            startTimeBtn = Time.time;
            isCountingDownBtn = true;

            showInventory = true;

            PlayerMover.IsDriving = false;

            Store store = other.GetComponent<Store>();
            itemlist = store.storelist;

            foreach (KeyValuePair<string, int> item in itemlist){
                AddItem(item.Key, item.Value);
            }

            foreach(KeyValuePair<string, int> item in items){
                //Debug.Log("inv: " + item.Key + ", " + item.Value);
            }
        } 
        
        else if(other.CompareTag("End")){
            SceneManager.LoadScene("Win");
        }
    }


    void OnTriggerStay2D(Collider2D other){
        if(other.CompareTag("House")){
            House house = other.GetComponent<House>();
            itemlist = house.wishlist;
            if(buttonCanBePressed){
                if(buttonIsPressed){
                    buttonCanBePressed = false;
                    showButton = false;
                    if (canFulfillOrder)
                    {
                        foreach (KeyValuePair<string, int> item in itemlist){
                            RemoveItem(item.Key, item.Value);
                        }

                        PlayerMover.points += 100;

                        audioSource.PlayOneShot(correctClip, 1.0f);

                        buttonIsPressed = false;
                    }
                    else if(!canFulfillOrder)
                    {
                        PlayerMover.lives -= 1;
                        Debug.Log(PlayerMover.lives);
                        audioSource.PlayOneShot(wrongClip, 1.0f);
                        buttonIsPressed = false;
                    }
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("House")){
            if(buttonCanBePressed && !buttonIsPressed && canFulfillOrder){
                PlayerMover.lives -= 1;
                audioSource.PlayOneShot(wrongClip, 0.7f);
                Debug.Log(PlayerMover.lives);
            } else if (buttonCanBePressed && !buttonIsPressed && !canFulfillOrder){
                PlayerMover.points += 100;
                audioSource.PlayOneShot(correctClip, 1.0f);
            }
        }

        buttonCanBePressed = false;
    }


    void FixedUpdate(){
        if(isCountingDownBtn){
            float remainingTimeBtn = countDownTime - (Time.time - startTimeBtn);
            countDownText.text = remainingTimeBtn.ToString("0");
            if(remainingTimeBtn <= 0.0f){
                remainingTimeBtn = 0.0f;
                isCountingDownBtn = false;
                PlayerMover.IsDriving = true;
                audioSource.PlayOneShot(motorStartup, 1.0f);
                showButton = false;
                showInventory = false;
            }
        } else {
            countDownText.text = "";
        }

        if(isCountingDownInv){
            float remainingTimeInv = countDownTime - (Time.time - startTimeInv);
            if(remainingTimeInv <= 0.0f){
                remainingTimeInv = 0.0f;
                showInventory = false;
                isCountingDownInv = false;
                PlayerMover.IsDriving = true;
                audioSource.PlayOneShot(motorStartup, 1.0f);
            }
        }

        if(showButton){
            deliveryButton.gameObject.SetActive(true);
        } else {
            deliveryButton.gameObject.SetActive(false);
        }

        if(showInventory){
            inventory.gameObject.SetActive(true);
        } else {
            inventory.gameObject.SetActive(false);
        }

        foreach(KeyValuePair<string, int> item in items){
            switch(item.Key){
                case "Eggs":
                    EggsCountText.text = item.Value.ToString();
                    break;
                case "Milk":
                    MilkCountText.text = item.Value.ToString();
                    break;
                case "Jam":
                    JamCountText.text = item.Value.ToString();
                    break;
            }
        }

    }    
}