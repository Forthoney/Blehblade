using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public List<string> lines;
    [SerializeField] private float timePerLine;

    private TextMeshPro _textMeshPro;
    private int _currentLine;
    private bool _displaying = true;
    private float _currTimePast;

    private void Awake()
    {
        _textMeshPro = gameObject.GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        if (_displaying)
        {
            gameObject.SetActive(true);
            if (_currTimePast >= timePerLine)
            {
                _currTimePast = 0;
                _displaying = false;
            }
            _currTimePast += Time.deltaTime;
            _textMeshPro.text = lines[_currentLine];
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void DisplayDialogue(int lineNum)
    {
        if (lineNum < 0) return;
        
        _currentLine = lineNum;
        _currTimePast = 0;
        _displaying = true;
    }
}
