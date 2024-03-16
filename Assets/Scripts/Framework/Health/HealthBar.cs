using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
   [SerializeField] private Health health;
   [SerializeField] private Slider slider;
   
   [Header("Fade Settings")]
   [SerializeField] private bool canFade;
   [SerializeField] private float timeBeforeFading;
   [SerializeField] private float fadeSpeed = 0.1f;
   
   private Image fill;

   private void Awake()
   {
      slider.maxValue = health.GetCurrentHealth();
      slider.value = health.GetCurrentHealth();
      if (!canFade) return;
      fill = slider.fillRect.GetComponent<Image>();
   }

   public void UpdateHealthBar()
   {
      StopAllCoroutines();
      
      slider.value = health.GetCurrentHealth();

      if (!canFade) return;
      var newColor = fill.color;
      newColor.a = 1;
      fill.color = newColor;
      
      StartCoroutine(FadeBar());
   }

   private IEnumerator FadeBar()
   {
      yield return new WaitForSeconds(timeBeforeFading);
      var newColor = fill.color;

      for (var i = 1f; i > 0; i -= .2f)
      {
         newColor.a = i;
         fill.color = newColor;
         yield return new WaitForSeconds(fadeSpeed);
      }
   }
}
