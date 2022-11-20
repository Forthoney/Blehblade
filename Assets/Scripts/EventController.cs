using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EventController : MonoBehaviour
{
    [Header("Beyblades")]
    [SerializeField] private GameObject playerBeyblade;
    [SerializeField] private GameObject enemyBeyblade;

    [SerializeField] private GameObject speaker;
    [SerializeField] public List<GameObject> puzzleObjects;
    
    public Camera mainCamera;
    public float time;
    public float remainingTime;
    public Vector3 defaultPos;

    private GameObject _dialogueManager;
    private const int MaxItems = 3;
    private const float Dist = 9;
    private readonly List<int> _inventory = new List<int>();
    private readonly HashSet<int> _usedObjects = new HashSet<int>();

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
        _dialogueManager = GameObject.Find("DialogueManager");
    }
    
    void Start()
    {
        StartCoroutine(SetupGame());
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

    private IEnumerator SetupGame()
    {
        var fadeFilter = GameObject.Find("Canvas").GetComponentInChildren<Image>();
        for (float i = 4; i > 0; i -= Time.deltaTime)
        {
            // set color with i as alpha
            fadeFilter.color = new Color(1, 1, 1, i);
            yield return null;
        }
        playerBeyblade.GetComponent<Beyblade>().StartBeyblade();
        enemyBeyblade.GetComponent<Beyblade>().StartBeyblade();
        remainingTime = time;
        speaker.GetComponent<AudioManageScript>().playSound(SceneManager.GetActiveScene().buildIndex);
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

    public void Defeat()
    {
        enemyBeyblade.GetComponent<Beyblade>().EndBeyblade();
        playerBeyblade.GetComponent<Beyblade>().EndBeyblade();
    }

    private void Win()
    {
        enemyBeyblade.GetComponent<Beyblade>().EndBeyblade();
        Debug.Log("Win");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public Vector3 PlaceInInventory(int objectId)
    {
        Debug.LogFormat("Add Item: {0}", _inventory.Count);
        Vector3 Position(int n) => new Vector3((float) n / (MaxItems + 1) * Dist - Dist / 2f, defaultPos.y, defaultPos.z);
        for (int i = 0; i < _inventory.Count; i++)
        {
            if (_inventory[i] == -1)
            {
                _inventory[i] = objectId;
                Debug.LogFormat("Add Item at {0}", i);
                Debug.Log(MaxItems);
                Debug.Log(Dist);
                Debug.Log(i);
                return Position(i + 1);
            }
        }
        _inventory.Add(objectId);
        return Position(_inventory.Count);
    }

    public void RemoveItem(int objectId)
    {
        Debug.LogFormat("Remove Item: {0}", _inventory.Count);
        for (int i = 0; i < _inventory.Count; i++)
        {
            if (_inventory[i] == objectId)
            {
                _inventory[i] = -1;
                return;
            }
        }

        throw new MissingMemberException();
    }

    public void DisplayDialogue(int dialogueID)
    {
        Debug.Log(dialogueID);
        _dialogueManager.SetActive(true);
        var dialogue = _dialogueManager.GetComponent<DialogueManager>();
        dialogue.DisplayDialogue(dialogueID);
    }
}
