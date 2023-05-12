using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class UIHealthBar : MonoBehaviour
{
    [SerializeField] private Animator healthAnimator;
    [SerializeField] private Vector2 minAndMaxAnimSpeed;
    [SerializeField] private Image filledImage;
    [SerializeField] private Health healthToLook;
    private void Awake()
    {
        healthToLook.OnHPChanged.AddListener(OnHPChanged);
    }
    private void OnHPChanged(float health, float maxHP)
    {
        float newAnimSpeed = Mathf.Lerp(minAndMaxAnimSpeed.x, minAndMaxAnimSpeed.y, 1 - health / maxHP);
        filledImage.fillAmount = health / maxHP;
        healthAnimator.SetFloat("animSpeed", newAnimSpeed);
    }
}
