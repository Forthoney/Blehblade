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
    private Dictionary<GameObject, bool> puzzleProgress = new Dictionary<GameObject, bool>();

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
        foreach (GameObject obj in puzzleObjects)
        {
            puzzleProgress[obj] = false;
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
        throw new NotImplementedException();
    }

    private void ShowEndgameSequence()
    {
        throw new NotImplementedException();
    }

    public void PlayerUse(GameObject item)
    {
        if (puzzleProgress.ContainsKey(item))
        {
            puzzleProgress[item] = true;
        }
    }

    private void Defeat()
    {
        ShowEndgameSequence();
    }
}
