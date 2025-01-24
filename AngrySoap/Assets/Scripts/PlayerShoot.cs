using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{

    [SerializeField]
    private Transform BulletSpawn;

    [SerializeField]
    private GameObject Bullet;

    [SerializeField]
    private float baseCoolDown = 0.5f;

    [SerializeField]
    private float attackSpeed = 1.0f;

    [SerializeField]
    private bool canShoot = true;

    [SerializeField]
    private bool isShooting = false;

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {   
            isShooting = true;

            StartCoroutine(ShootingLoop());
        }

        if (context.canceled)
        {
            isShooting = false;
        }
        
    }


    public void Shoot(){
        GameObject bullet = Instantiate(Bullet, BulletSpawn.position, BulletSpawn.rotation) as GameObject;
        bullet.GetComponent<Rigidbody>().AddForce(BulletSpawn.forward * 1000);
    }

    public float CalculateCoolDown(){
        return baseCoolDown / attackSpeed;
    }

    public IEnumerator ShootCooldown(){
        canShoot = false;
        yield return new WaitForSeconds(CalculateCoolDown());
        canShoot = true;
    }

    private IEnumerator ShootingLoop()
    {
        while (isShooting) // Continue while the button is held down
        {
            if (canShoot)
            {
                Shoot();
                StartCoroutine(ShootCooldown()); // Start the cooldown
            }

            yield return new WaitForSeconds(0.1f); // Add a small wait to prevent constant firing
        }
    }

    public void UpgradeAttackSpeed(float amount)
    {
        attackSpeed += amount;
    }

    public void ResetAttackSpeed()
    {
        attackSpeed = 1.0f;
    }
}
