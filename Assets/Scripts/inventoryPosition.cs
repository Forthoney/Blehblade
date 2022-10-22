// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class NewBehaviourScript : MonoBehaviour
// {
//     [SerializeField] private int maxItems = 0;
//     [SerializeField] private int screenWidth = 0; // set this in editor, total width
//     private List<bool> itemTracking = new List<bool>();
    
//     void Start(){
//         for (int i = 0; i < maxItems; i++){
//             itemTracking.get(i) = false;
//         }
//     }

//     public int getItemDefaultPos(){
//         for (int i = 1; i <= maxItems; i++){
//             if (itemTracking.get(i - 1) == false){
//                 itemTracking.set(i - 1, true)
//                 return (i / (maxItems + 1) * screenWidth);
//             }
//         }
//     }
//     public void removeItem(int xPos){
//         index = (xPos / screenWidth * (maxItems + 1)) - 1
//         itemTracking.set(index, false)


//     }
// }
