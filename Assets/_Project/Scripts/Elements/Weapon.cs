using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    public Bullet bulletPrefab;
    public Transform bulletPlaceHolder;

    public float machinegunAttackCooldown;
    public float shotgunAttackCooldown;
    public int shotgunBulletCount;
    public float shotgunSpread;

    private WeaponType _weaponType;

    private float _lastShootTimeMachinegun;
    private float _lastShootTimeShotgun;

    private void Update()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            _weaponType = WeaponType.Machinegun;
        }
        else if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            _weaponType = WeaponType.Shotgun;
        }

        if (Mouse.current.leftButton.isPressed 
            && _weaponType == WeaponType.Machinegun
            && GetTimeSinceLastShootMachinegun() > machinegunAttackCooldown)
        {
            ShootForMachinegun();
            _lastShootTimeMachinegun = Time.time;
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame 
            && _weaponType == WeaponType.Shotgun
            && GetTimeSinceLastShootShotgun() > shotgunAttackCooldown)
        {
            ShootForShotgun();
            _lastShootTimeShotgun = Time.time;
        }
    }

    float GetTimeSinceLastShootMachinegun()
    {
        return Time.time - _lastShootTimeMachinegun;
    }
    float GetTimeSinceLastShootShotgun()
    {
        return Time.time - _lastShootTimeShotgun;
    }

    void ShootForMachinegun()
    {
        SpawnBullet(0);
    }

    void ShootForShotgun()
    {
        for (int i = 0; i < shotgunBulletCount; i++)
        {
            SpawnBullet(shotgunSpread);
        }
    }

    void SpawnBullet(float spread)
    {
        var newBullet = Instantiate(bulletPrefab, GameDirector.instance.levelManager.GetCurrentLevel().transform);
        newBullet.transform.position = bulletPlaceHolder.position;

        var xSpread = Random.Range(-spread, spread);
        var ySpread = Random.Range(-spread, spread);
        var zSpread = Random.Range(-spread, spread);

        newBullet.transform.LookAt(transform.position + transform.forward * 10 + new Vector3(xSpread, ySpread, zSpread));
    }
}

public enum WeaponType
{
    Machinegun,
    Shotgun,
}