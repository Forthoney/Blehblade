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

    [SerializeField] private GameObject speaker;

    public int level;

    // Time limit
    // List of puzzle objects (must have InteractableObject script attached)
    [SerializeField] public List<GameObject> puzzleObjects;
    private HashSet<int> _usedObjects = new HashSet<int>();
    public Camera mainCamera;
    public float time;
    public float remainingTime;
    public Vector3 defaultPos;

    private const int MaxItems = 3;
    private const float LeftX = -3;
    private const float RightX = 3;
    private readonly List<int> _inventory = new List<int>();

    private bool _isPlaying = true;

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

        speaker.GetComponent<AudioManageScript>().playSound(level);
    }
    
    void Update()
    {
        remainingTime -= Time.deltaTime;
        if (remainingTime < 0 && _isPlaying)
        {
            Defeat();
            _isPlaying = false;
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
        playerBeyblade.GetComponent<Beyblade>().EndBeyblade();
        Debug.Log("Defeat");
    }

    private void Win()
    {
        enemyBeyblade.GetComponent<Beyblade>().EndBeyblade();
        Debug.Log("Win");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public Vector3 PlaceInInventory(int objectId)
    {
        float dist = RightX - LeftX;
        Vector3 Position(int i) => new Vector3((float) i / (MaxItems + 1) * dist - dist / 2f, defaultPos.y, defaultPos.z);
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
