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
    private bool _inProgress = true;

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
        remainingTime = time;
    }
    
    void Update()
    {
        remainingTime -= Time.deltaTime;
        if (remainingTime < 0 && _inProgress)
        {
            _inProgress = false;
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
        StartCoroutine(PrepareForFinisher());
        Debug.Log("Defeat");
    }

    private IEnumerator PrepareForFinisher()
    {
        var playerBeybladeControl = playerBeyblade.GetComponent<Beyblade>();
        var enemyBeybladeControl = enemyBeyblade.GetComponent<Beyblade>();
        playerBeybladeControl.Decelerate();
        enemyBeybladeControl.PushTo(-playerBeyblade.transform.position + enemyBeyblade.transform.position);
        yield return new WaitForSeconds(0.4f);
        
        playerBeyblade.GetComponent<Rigidbody>().isKinematic = true;
        enemyBeybladeControl.PushTo((playerBeyblade.transform.position - enemyBeyblade.transform.position).normalized);
        yield return new WaitForSeconds(0.2f);
    }

    private void Win()
    {
        Debug.Log("Win");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
