using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider healthSlider;

    private Transform _attachPoint;

    public void Init(Transform attachPoint)
    {
        _attachPoint = attachPoint;
    }

    public void SetHealthSliderValue(float health, float delta, float maxHealth)
    {
        healthSlider.value = health/maxHealth;
    }

    private void Update()
    {
        if(transform!=null)
        {
            Vector3 attachScreenPoint = Camera.main.WorldToScreenPoint(_attachPoint.position);
            transform.position = attachScreenPoint;
        }
    }

    internal void OnOwnerDead(GameObject Killer)
    {
        Destroy(gameObject);
    }
}
