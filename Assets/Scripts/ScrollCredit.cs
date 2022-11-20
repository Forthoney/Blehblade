using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollCredit : MonoBehaviour
{
    [SerializeField] private float speed;
    // Start is called before the first frame update
    private RectTransform _rectTransform;

    void Start()
    {
        //Fetch the RectTransform from the GameObject
        _rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        _rectTransform.anchoredPosition += new Vector2(0, speed * Time.deltaTime);
    }
}
