using UnityEngine;
using UnityEngine.Events;

public class WeaponUiItemsDisplayer : MonoBehaviour
{
    [SerializeField] private Transform itemsContainer;
    [SerializeField] private WeaponDataUiItem weaponDataUiItemPrefab;

    [SerializeField] private WeaponData[] WeaponData;

    public UnityEvent<WeaponData> OnWeaponDataClicked;

    void Start()
    {
        foreach (WeaponData weaponData in WeaponData)
        {
            WeaponDataUiItem weaponDataUiItem = Instantiate(weaponDataUiItemPrefab, itemsContainer);
            weaponDataUiItem.SetWeaponData(weaponData);
            weaponDataUiItem.OnClick += HandleWeaponClicked;
        }
    }

    private void HandleWeaponClicked(WeaponData weaponData)
    {
        OnWeaponDataClicked?.Invoke(weaponData);
    }
}
