using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
   [SerializeField] private Health health;
   [SerializeField] private Slider slider;


   private Color barColor;

   private void Awake()
   {
      slider.maxValue = health.GetCurrentHealth();
      slider.value = health.GetCurrentHealth();
   }

   public void UpdateHealthBar()
   {
      slider.value = health.GetCurrentHealth();
   }
}
