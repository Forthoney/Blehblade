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
    private HashSet<int> _usedObjects = new HashSet<int>();
    public Camera mainCamera;
    public float time;
    public float remainingTime;

    [SerializeField] private int maxItems;
    [SerializeField] private float screenWidth; // set this in editor, total width
    [SerializeField] private float leftX;
    [SerializeField] private float rightX;
    private List<int> _inventory = new List<int>();
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
        _instance = this;
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
    
    public void PlayerUse(int objectId)
    {
        if (!_usedObjects.Contains(objectId))
        {
            _usedObjects.Add(objectId);
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

    public Vector3 PlaceInInventory(int objectId)
    {
        float dist = rightX - leftX;
        Vector3 Position(int i) => new Vector3((float) i / (maxItems + 1) * dist - dist / 2f, defaultPos.y, defaultPos.z);
        for (int i = 0; i < _inventory.Count; i++)
        {
            if (_inventory[i] == -1)
            {
                _inventory[i] = objectId;
                return Position(i);
            }
        }
        _inventory.Add(objectId);
        Debug.Log(_inventory.Count);
        return Position(_inventory.Count);
    }

    public void removeItem(int objectId)
    {
        Debug.Log(_inventory.Count);
        for (int i = 0; i < _inventory.Count; i++)
        {
            Debug.Log(_inventory[i]);
            if (_inventory[i] == objectId)
            {
                _inventory[i] = -1;
                return;
            }
        }

        throw new MissingMemberException();
    }
}
