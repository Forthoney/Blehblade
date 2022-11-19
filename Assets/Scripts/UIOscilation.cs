using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIOscilation : MonoBehaviour
{
    private int sizeLow = 24;
    private int sizeHigh = 50;
    private float dSize = 10f;
    private int minRotation = -60;
    private int maxRotation = 60;
    private float dRot = 20f;
    private float currRot = 0f;

    public TextMeshProUGUI tmp;
    public RectTransform rtf;
    public bool isText;

    // Start is called before the first frame update
    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        rtf = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isText)
        {
            if (tmp.fontSize > sizeHigh)
            {
                dSize = -10;
            }
            else if (tmp.fontSize < sizeLow)
            {
                dSize = 10;
            }
            tmp.fontSize += (dSize * Time.deltaTime);
        }

        if (currRot > maxRotation)
        {
            dRot = -20f;
        }
        else if (currRot < minRotation)
        {
            dRot = 20f;
        }
        rtf.rotation = Quaternion.Euler(
            currRot,
            currRot * 1.1f,
            currRot * 0.9f);
        currRot += (dRot * Time.deltaTime);
    }
}
