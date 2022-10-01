using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    [SerializeField] private GameObject sceneManager;
    [SerializeField] private GameObject playerBeyblade;
    [SerializeField] private GameObject enemyBeyblade;
    [SerializeField] public float time;
    [SerializeField] public List<GameObject> puzzleObjects;
    private HashSet<GameObject> _usedObjects;
    private int _remainingSteps;

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
        _remainingSteps = puzzleObjects.Count;
    }
    
    // Start is called before the first frame update
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
            _remainingSteps -= 1;
        }

        if (_remainingSteps == 0)
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
    }
}
