using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MegaChargeValue : MonoBehaviour
{
   [SerializeField] private Slider MegaChargeSlider;
   [SerializeField] private TextMeshProUGUI MegaChargeText;
   [SerializeField] private int maxCharge;
   [SerializeField] private int minCharge;
   [SerializeField] private float currentCharge;
   [SerializeField] private float chargeAmount;
   [SerializeField] private bool _isMegaCharge;
   private void Start()
   {
      currentCharge = 0;
      chargeAmount = 5;
      maxCharge =50;
      EventSystem.MegaCharge += HandleMegaCharge;
      

   }

   private void HandleMegaCharge(bool megaCharge)
   {
      _isMegaCharge = megaCharge;
   }

   private void Update()
   {
      if (_isMegaCharge)
      {
         currentCharge = 0;
      }

      if (currentCharge == maxCharge)
      {
         EventSystem.MegaChargeReady?.Invoke(true);
      }
      currentCharge += chargeAmount * Time.deltaTime;
      currentCharge = Mathf.Clamp(currentCharge, minCharge, maxCharge);
      MegaChargeSlider.value = currentCharge;
      MegaChargeSlider.maxValue = maxCharge;
      MegaChargeSlider.minValue = minCharge;
      MegaChargeText.text = currentCharge.ToString() + "/" + maxCharge.ToString();
      
   }
}
