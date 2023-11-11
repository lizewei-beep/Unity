using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EhealthBar : MonoBehaviour
{
    public static int HealthCurrent;
    public static int HealthMax;
    private Image healthBar;
    void Start()
    {
        healthBar = GetComponent<Image>();
    }

    void Update()
    {
        healthBar.fillAmount = (float)HealthCurrent / HealthMax;
    }
}
