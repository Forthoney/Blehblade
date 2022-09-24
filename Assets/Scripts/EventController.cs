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

    // Start is called before the first frame update
    void Start()
    {
        ShowIntroSequence();
        playerBeyblade.GetComponent<Beyblade>().StartBeyblade();
        enemyBeyblade.GetComponent<Beyblade>().StartBeyblade();
        time = time;
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

    public void PlayerUse(GameObject interactableItem)
    {
        
    }

    private void Defeat()
    {
        throw new NotImplementedException();
    }
}
