using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public List<string> lines;
    public int currentLine;
    public bool displaying;
    public float timePerLine;
    public float currTimePast;
    public int currentEnd;
    public TextMeshPro tmp;
    public GameObject go;

    // Update is called once per frame
    void Update()
    {
        if (displaying)
        {
            go.SetActive(true);

            if (currTimePast >= timePerLine)
            {
                currTimePast = 0;
                if (currentLine < currentEnd)
                {
                    currentLine++;

                }
                else
                {
                    displaying = false;
                }
            }
            currTimePast += Time.deltaTime;
            tmp.text = lines[currentLine];
        }
        else
        {
            go.SetActive(false);
        }
    }

    public void displayDialogue(int start, int end)
    {
        currentLine = start;
        currTimePast = 0;
        currentEnd = end;
        displaying = true;
    }
}
