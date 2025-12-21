using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string weaponName;
    public int damage = 20;
    public float fireRate = 0.3f;
    public float range = 100f;

    [Header("Патроны")]
    public int maxAmmo = 10; // макс. патронов
    public int currentAmmo; // текущие патроны
    public float reloadTime = 1.5f;
    private bool isReloading = false;

    private float nextFireTime = 0f;

    void Start()
    {
        currentAmmo = maxAmmo; // при старте полная обойма
    }

    public void Shoot()
    {
        if (isReloading) return;
        if (currentAmmo <= 0)
        {
            Debug.Log($"{weaponName}: Нет патронов!");
            return;
        }

        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            currentAmmo--;

            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            if (Physics.Raycast(ray, out RaycastHit hit, range))
            {
                Debug.Log($"{weaponName} попал в {hit.collider.name}");

                /*Health target = hit.collider.GetComponent<Health>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }*/
            }
        }
    }

    public void Reload()
    {
        if (isReloading) return;
        isReloading = true;
        Debug.Log($"{weaponName}: Перезарядка...");

        // Через reloadTime пополняем патроны
        Invoke(nameof(FinishReload), reloadTime);
    }

    private void FinishReload()
    {
        currentAmmo = maxAmmo;
        isReloading = false;
        Debug.Log($"{weaponName}: Перезарядка завершена!");
    }
}
