using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

public class Condition
{
    //[HideInInspector]

    public float curValue;
    public float maxValue;
    public float startValue;
    public Text uiValueText;

    public void Add(float amount)
    {
        curValue = Mathf.Min(curValue + amount, maxValue);
    }

    public void Subtract(float amount)
    {
        curValue = Mathf.Max(curValue - amount, 0.0f);
    }
}

public class PlayerStats : MonoBehaviour
{
    public Condition health;
    public Condition attack;
    public Condition defense;

    public void Start()
    {
        health.curValue = health.startValue;
        attack.curValue = attack.startValue;
        defense.curValue = defense.startValue;
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void StatAdd(string statName ,float amount)
    {
        if(statName ==  "health")
        {
            health.Add(amount);
        }
        else if (statName == "attack")
        {
            attack.Add(amount);
        }
        else if (statName == "defense")
        {
            defense.Add(amount);
        }
    }

    public void StatSubtract(string statName ,float amount)
    {
        if (statName == "health")
        {
            health.Subtract(amount);
        }
        else if (statName == "attack")
        {
            attack.Subtract(amount);
        }
        else if (statName == "defense")
        {
            defense.Subtract(amount);
        }
    }
}
