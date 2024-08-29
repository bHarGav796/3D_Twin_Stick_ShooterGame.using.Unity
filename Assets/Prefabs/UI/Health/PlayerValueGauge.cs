using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerValueGauge : MonoBehaviour
{
    [SerializeField] Image AmtImage;
    [SerializeField] TextMeshProUGUI AmtText;

    internal void UpdateValue(float health, float delta, float maxHealth)
    {
        AmtImage.fillAmount = health / maxHealth;
        int healthAsInt = (int)health >= 0 ? (int)health : 0;
        AmtText.SetText(healthAsInt.ToString());
    }
}
