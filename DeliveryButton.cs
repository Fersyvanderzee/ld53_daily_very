using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryButton : MonoBehaviour
{
    public Inventory inventory;

    public void ButtonClicked(){
        inventory.buttonIsPressed = true;
    }
}
