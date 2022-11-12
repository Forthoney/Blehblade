using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollCredit : MonoBehaviour
{
    [SerializeField] private float speed;
    // Start is called before the first frame update
    RectTransform m_RectTransform;

    void Start()
    {
        //Fetch the RectTransform from the GameObject
        m_RectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        m_RectTransform.anchoredPosition += new Vector2(0, speed * Time.deltaTime);
    }
}
