using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class StatUI : MonoBehaviour
{
    private PlayerStats playerStats;

    public Text healthText;
    public Text attackText;
    public Text defeseText;

    public void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
        healthText.text = playerStats.health.startValue.ToString();
        attackText.text = playerStats.attack.startValue.ToString();
        defeseText.text = playerStats.defense.startValue.ToString();
    }

    public void Update()
    {
        healthText.text = playerStats.health.curValue.ToString();
        attackText.text = playerStats.attack.curValue.ToString();
        defeseText.text = playerStats.defense.curValue.ToString();
    }
}
