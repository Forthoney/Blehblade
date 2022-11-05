using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EventController : MonoBehaviour
{
    [SerializeField] private GameObject playerBeyblade;
    [SerializeField] private GameObject enemyBeyblade;
    // Time limit
    // List of puzzle objects (must have InteractableObject script attached)
    [SerializeField] public List<GameObject> puzzleObjects;
    private HashSet<GameObject> _usedObjects;
    public Camera mainCamera;
    public float time;
    public float remainingTime;

    [SerializeField] private int maxItems;
    [SerializeField] private float screenWidth; // set this in editor, total width
    [SerializeField] private float leftX;
    [SerializeField] private float rightX;
    [SerializeField] private List<bool> itemTracking = new List<bool>();
    [SerializeField] private Vector3 defaultPos;

    // Boilerplate to enable Singleton behavior
    private static EventController _instance;
    public static EventController Instance
    {
        get
        {
            if (_instance is null)
            {
                Debug.LogError("Null EventController");
            }
            return _instance;
        }
    }

    void Awake()
    {
        for (int i = 0; i < maxItems; i++)
        {
            itemTracking.Add(false);
        }
        _instance = this;
        _usedObjects = new HashSet<GameObject>();
        // Calling mainCamera is expensive (https://stackoverflow.com/a/61998177/18077664), so we call it once
        // here, then take it from here every time we need it for dragging an object.
        mainCamera = Camera.main;
    }
    
    void Start()
    {
        playerBeyblade.GetComponent<Beyblade>().StartBeyblade();
        enemyBeyblade.GetComponent<Beyblade>().StartBeyblade();
        remainingTime = time;
    }
    
    void Update()
    {
        remainingTime -= Time.deltaTime;
        if (remainingTime < 0)
        {
            Defeat();
        }
    }
    
    public void PlayerUse(GameObject item)
    {
        if (!_usedObjects.Contains(item))
        {
            _usedObjects.Add(item);
        }

        if (_usedObjects.Count == puzzleObjects.Count)
        {
            Win();
        }
    }

    private void Defeat()
    {
        Debug.Log("Defeat");
    }

    private void Win()
    {
        Debug.Log("Win");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public Vector3 getPos()
    {
        float dist = rightX - leftX;
        for (int i = 1; i <= maxItems; i++)
        {
            if (itemTracking[i - 1] == false)
            {
                itemTracking[i - 1] = true;
                return new Vector3(((float)i / (maxItems + 1) * dist) - (dist/2), defaultPos.y, defaultPos.z);
            }
        }
        return defaultPos;
    }

    public void removeItem(Vector3 pos)
    {
        float xPos = pos.x;
        float dist = rightX - leftX;
        int index = (int)Mathf.Round(( (xPos+(dist/2)) / dist * (maxItems + 1)) - 1);

        itemTracking[index] = false;
    }
}
