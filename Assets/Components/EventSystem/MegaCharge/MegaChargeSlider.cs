using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MegaChargeValue : MonoBehaviour
{
   // The Mega charge UI is a Slider and text
   [SerializeField] private TextMeshProUGUI MegaChargeText;
   [Header("Slider")]
   [SerializeField] private Slider MegaChargeSlider;
   [SerializeField] private int maxCharge;
   [SerializeField] private int minCharge;
   
   [SerializeField] private float currentCharge;
   [SerializeField] private float chargeAmount;
   [SerializeField] private bool _isMegaCharge;
     
   private void Start()
   {
      // att start set  the amount of charge the speed and the maximum of charge
      currentCharge = 0;
      chargeAmount = 5;
      maxCharge =50;
      // listen to MegaCharge
      EventSystem.MegaCharge += HandleMegaCharge;
      

   }

   private void OnDestroy()
   {
      EventSystem.MegaCharge -= HandleMegaCharge;
   }

   private void HandleMegaCharge(bool megaCharge)
   {
      // link Mega Charge bool to _isMegacharge var
      _isMegaCharge = megaCharge;
   }

   private void Update()
   {
      // if is megacharge true set back Current charge to 0
      if (_isMegaCharge)
      {
         currentCharge = 0;
      }
      // If the Current charge is equal to the max charge invoke MegaChargeReady as true so MegaCharge can be used
      if (currentCharge == maxCharge)
      {
         EventSystem.MegaChargeReady?.Invoke(true);
      }
      //updating the Megacharge Amount, slider bar and UI and text in %
      currentCharge += chargeAmount * Time.deltaTime;
      currentCharge = Mathf.Clamp(currentCharge, minCharge, maxCharge);
      MegaChargeSlider.value = currentCharge;
      MegaChargeSlider.maxValue = maxCharge;
      MegaChargeSlider.minValue = minCharge;
      MegaChargeText.text = currentCharge.ToString("n0") + "/" + maxCharge.ToString();
      
   }
}
