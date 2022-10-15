using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beyblade : MonoBehaviour
{
    [SerializeField] private Vector3 initSpeed;
    private float beybladeSpeed;
    private bool gameRunning = false;
    [SerializeField] private GameObject eventControl;
    [SerializeField] private bool isPlayer;


    // Physics
    private Rigidbody rb;

    // Keystrokes
    private bool spacePressed = false;
    private bool wPressed = false;
    private bool sPressed = false;
    private bool aPressed = false;
    private bool dPressed = false;

    public float vel;





    // Start is called before the first frame update

    public void Start()
    {
        vel = 100;
        rb = GetComponent<Rigidbody>();

    }
    public void StartBeyblade()
    {
        gameRunning = true;
        rb.velocity = initSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        
        updateKeyInputs();

        

        if (wPressed)
        {
            rb.AddForce(new Vector3(vel, 0, 0));
        }

        if (sPressed)
        {
            rb.AddForce(new Vector3(-vel, 0, 0));
        }

        if (aPressed)
        {
            rb.AddForce(new Vector3(0, 0, vel));
        }

        if (dPressed)
        {
            rb.AddForce(new Vector3(0, 0, -vel));
        }

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

    void updateKeyInputs()
    {
        spacePressed = Input.GetKeyDown("space");
        wPressed = Input.GetKeyDown("w");
        sPressed = Input.GetKeyDown("s");
        aPressed = Input.GetKeyDown("a");
        dPressed = Input.GetKeyDown("d");
    }
}
