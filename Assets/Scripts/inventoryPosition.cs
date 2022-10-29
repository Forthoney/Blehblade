using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPosition : MonoBehaviour
{
    [SerializeField] private int maxItems = 0;
    [SerializeField] private int screenWidth = 0; // set this in editor, total width
    private List<bool> itemTracking = new List<bool>();
    [SerializeField] private Vector3 defaultPos;
    
     void Start(){
         for (int i = 0; i < maxItems; i++){
             itemTracking[i] = false;
         }
     }

     public Vector3 getPos(){
         for (int i = 1; i <= maxItems; i++){
             if (itemTracking[i - 1] == false){
                itemTracking[i - 1] = true;
                 return new Vector3((i / (maxItems + 1) * screenWidth), defaultPos.y, defaultPos.z);
             }
         }
        return new Vector3(0, 0, 0);
     }
     public void removeItem(Vector3 pos){
        float xPos = pos.x;
        int index = (int)Mathf.Round((xPos / screenWidth * (maxItems + 1)) - 1);
        itemTracking[index] = false;
     }
}
