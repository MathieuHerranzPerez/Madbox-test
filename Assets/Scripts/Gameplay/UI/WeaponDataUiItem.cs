using System;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDataUiItem : MonoBehaviour
{
    public event Action<WeaponData> OnClick;

    [SerializeField] private Image iconImage;

    private WeaponData weaponData;

    public void SetWeaponData(WeaponData weaponData)
    {
        this.weaponData = weaponData;

        iconImage.sprite = weaponData.Icon;
    }

    // Called from UnityEvent
    public void Click()
    {
        OnClick?.Invoke(weaponData);
    }
}
