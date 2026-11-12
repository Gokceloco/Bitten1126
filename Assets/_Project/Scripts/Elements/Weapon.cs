using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    public Bullet bulletPrefab;
    public Transform bulletPlaceHolder;

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            SpawnBullet();
        }
    }

    void SpawnBullet()
    {
        var newBullet = Instantiate(bulletPrefab);
        newBullet.transform.position = bulletPlaceHolder.position;
        newBullet.transform.LookAt(transform.position + transform.forward * 10);
    }
}
