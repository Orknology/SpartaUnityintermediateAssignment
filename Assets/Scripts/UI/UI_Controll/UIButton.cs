using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : MonoBehaviour
{
    [SerializeField] private GameObject currentScreen;
    [SerializeField] private GameObject targetUI;
    [SerializeField] private RectTransform UIPosition;
    private Vector2 savedPosition = new Vector2();

    private void Awake()
    {
        savedPosition = UIPosition.anchoredPosition;
    }
    public void OnAccessClick()
    {
        savedPosition = UIPosition.anchoredPosition;
        currentScreen.SetActive(false);
        targetUI.SetActive(true);
    }
    public void OnExitClick()
    {
        currentScreen.SetActive(false);
        targetUI.SetActive(true);
        UIPosition.anchoredPosition = savedPosition;
    }
}
