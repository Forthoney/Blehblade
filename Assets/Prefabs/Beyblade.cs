using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beyblade : MonoBehaviour
{
    [SerializeField] private float initSpeed;
    private float beybladeSpeed;
    private bool gameRunning = false;
    [SerializeField] private GameObject eventControl;
    [SerializeField] private bool isPlayer;

    // Start is called before the first frame update
    public void StartBeyblade()
    {
        gameRunning = true;
        beybladeSpeed = initSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameRunning)
        {
            beybladeSpeed -= Time.deltaTime;
            if (beybladeSpeed <= 0)
            {
                //eventControl.defeat(isPlayer);
                gameRunning = false;
            }

        }
    }
}
