using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Weapon[] weapons;
    private int currentWeaponIndex = 0;

    void Start()
    {
        SelectWeapon(currentWeaponIndex);
    }

    void Update()
    {
        // Стрельба
        if (Input.GetMouseButton(0))
        {
            weapons[currentWeaponIndex].Shoot();
        }

        // Перезарядка (клавиша R)
        if (Input.GetKeyDown(KeyCode.R))
        {
            weapons[currentWeaponIndex].Reload();
        }

        // Переключение колёсиком мыши
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
        {
            currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Length;
            SelectWeapon(currentWeaponIndex);
        }
        else if (scroll < 0f)
        {
            currentWeaponIndex--;
            if (currentWeaponIndex < 0) currentWeaponIndex = weapons.Length - 1;
            SelectWeapon(currentWeaponIndex);
        }

        // Переключение по цифрам
        for (int i = 0; i < weapons.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SelectWeapon(i);
            }
        }
    }

    void SelectWeapon(int index)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(i == index);
        }
        Debug.Log("Выбрано оружие: " + weapons[index].weaponName);
    }
}
