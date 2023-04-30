using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    public string[] possibleItems = {"Eggs", "Milk", "Jam"};

    public Dictionary<string, int> storelist = new Dictionary<string, int>();

    public SpriteRenderer sprite;

    void Start(){        
        System.Random rnd = new System.Random();

        int numberOfItems = 2;
        string item;
        string itemListString = "";

        for(int i = 0; i < numberOfItems; i++){
            item = possibleItems[rnd.Next(possibleItems.Length)];
            itemListString += item;
            AddItem(item, 1);
        }

        switch(itemListString) 
        {
            case "EggsEggs":
                sprite.sprite = Resources.Load<Sprite>("eggs_eggs");
                break;

            case "EggsMilk":
                sprite.sprite = Resources.Load<Sprite>("eggs_milk");
                break;

            case "EggsJam":
                sprite.sprite = Resources.Load<Sprite>("eggs_jam");
                break;

            case "MilkEggs":
                sprite.sprite = Resources.Load<Sprite>("milk_eggs");
                break;

            case "MilkMilk":
                sprite.sprite = Resources.Load<Sprite>("milk_milk");
                break;

            case "MilkJam":
                sprite.sprite = Resources.Load<Sprite>("milk_jam");
                break;

            case "JamEggs":
                sprite.sprite = Resources.Load<Sprite>("jam_eggs");
                break;

            case "JamMilk":
                sprite.sprite = Resources.Load<Sprite>("jam_milk");
                break;

            case "JamJam":
                sprite.sprite = Resources.Load<Sprite>("jam_jam");
                break;
    
            default:
                Debug.Log("Nothing choosen");
                break;
        }
    }
    
    public void AddItem(string itemName, int quantity){
        if(storelist.ContainsKey(itemName)){
            storelist[itemName] += quantity;
        } else {
            storelist.Add(itemName, quantity);
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            sprite.gameObject.SetActive(false);
        }
    }
}