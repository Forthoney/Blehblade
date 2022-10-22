using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultLocation : MonoBehaviour
{
    //create a struct to make an array that holds a xyz location
    //and a boolean to see if the spot if taken.
    public struct inventoryLocations
    {
        public Vector3 XYZ;
        public bool taken;
    }

    [SerializeField] public inventoryLocations[] defaultXYZ;

    // Start is called before the first frame update
    void Start()
    {
        //for x = -2 to x = -4. all of taken is false at first.
        //locations may have to be adjusted.
        defaultXYZ[0].XYZ = new Vector3(2.5f, 0f, -9f);
        defaultXYZ[0].taken = false;

        defaultXYZ[1].XYZ = new Vector3(-2.5f, 0f, -9f);
        defaultXYZ[1].taken = false;

        defaultXYZ[2].XYZ = new Vector3(-3f, 0f, -9f);
        defaultXYZ[2].taken = false;

        defaultXYZ[3].XYZ = new Vector3(-3.5f, 0f, -9f);
        defaultXYZ[3].taken = false;

        defaultXYZ[4].XYZ = new Vector3(-4f, 0f, -9f);
        defaultXYZ[4].taken = false;

    }

    //called when object is picked up from background art.
    //returns the default location that is not used yet.
    //take in previous location, so that if the inventory is full, the default is set back
    //to where it is from.
    public Vector3 GetDefaultLocation(Vector3 previousLocation)
    {
        //for loop through all defaultXYZ, taken or not
        for (int i = 0; i < 5; i++)
        {
            //if not taken
        if (defaultXYZ[i].taken == false)
            {
                //it is now taken
            defaultXYZ[i].taken = true;
            //return the location. kill loop by return.
            return defaultXYZ[i].XYZ;
            }
        }
        return previousLocation;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
