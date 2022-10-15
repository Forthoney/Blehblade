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
    [SerializeField] public float time;
    // List of puzzle objects (must have InteractableObject script attached)
    [SerializeField] public List<GameObject> puzzleObjects;
    private HashSet<GameObject> _usedObjects;
    public Camera mainCamera;

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
        _usedObjects = new HashSet<GameObject>();
        // Calling mainCamera is expensive (https://stackoverflow.com/a/61998177/18077664), so we call it once
        // here, then take it from here every time we need it for dragging an object.
        mainCamera = Camera.main;
    }
    
    void Start()
    {
        playerBeyblade.GetComponent<Beyblade>().StartBeyblade();
        enemyBeyblade.GetComponent<Beyblade>().StartBeyblade();
    }
    
    void Update()
    {
        time -= Time.deltaTime;
        if (time < 0)
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
}
