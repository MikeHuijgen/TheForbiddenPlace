using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
   [SerializeField] private Slider slider;
   
   [Header("Fade Settings")]
   [SerializeField] private bool canFade;
   [SerializeField] private float timeBeforeFading;
   [SerializeField] private float fadeSpeed = 0.1f;
   
   private Health _health;
   private Image fill;

   private void Awake()
   {
      _health = GetComponentInParent<Health>();
      slider.maxValue = _health.GetCurrentHealth();
      slider.value = _health.GetCurrentHealth();
      if (!canFade) return;
      fill = slider.fillRect.GetComponent<Image>();
   }

   public void UpdateHealthBar()
   {
      StopAllCoroutines();
      
      slider.value = _health.GetCurrentHealth();

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
