using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
    public int moneyStartValue;
    public int moneyCurValue;
    public Text moneyText;

    private void Awake()
    {
        moneyCurValue = moneyStartValue;
    }
    private void Update()
    {
        moneyText.text = GetThousandCommaText(moneyCurValue).ToString();
    }
    public string GetThousandCommaText(int data) 
    { 
        return string.Format("{0:#,###}", data); 
    }

}
