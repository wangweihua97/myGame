using System;
using System.Collections;
using System.Collections.Generic;
using Item;
using UnityEngine;
using UnityEngine.UI;

public class HealthyUI : MonoBehaviour
{
    public Text text;

    private Transform playerTransform;
    private Slider slider;
    private RectTransform rectTransform;

    private int max;
    private int cur;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        rectTransform = transform as RectTransform;
    }

    // Start is called before the first frame update
    public void Init(Transform transform ,int max ,int cur)
    {
        playerTransform = transform;
        this.max = max;
        this.cur = cur;
        RefreshText();
    }

    private void OnGUI()
    {
        if(!playerTransform)
            return;
        Vector2 pos =Camera.main.WorldToScreenPoint(playerTransform.position)+ new Vector3(0,80f ,0);
        rectTransform.position = pos;
    }

    public void RefreshText()
    {
        text.text = "" + playerTransform.GetComponent<PlayerProperty>().healthy;
        slider.value = (float)playerTransform.GetComponent<PlayerProperty>().healthy / max ;
    }
}
