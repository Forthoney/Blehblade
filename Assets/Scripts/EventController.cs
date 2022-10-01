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
    private Dictionary<GameObject, bool> _puzzleProgress;

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
        _puzzleProgress = new Dictionary<GameObject, bool>();
        foreach (GameObject obj in puzzleObjects)
        {
            _puzzleProgress.Add(obj, false);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        ShowIntroSequence();
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

    private void ShowIntroSequence()
    {
        
    }

    private void ShowEndgameSequence()
    {
        
    }

    public void PlayerUse(GameObject item)
    {
        if (_puzzleProgress.ContainsKey(item))
        {
            _puzzleProgress[item] = true;
        }
    }

    private void Defeat()
    {
        ShowEndgameSequence();
    }
}
