using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    GameObject projectile;
    GameObject canonBall;
    Bullet canon;
    [SerializeField]
    int speed = 1;
    void Start()
    {
        canonBall = Instantiate(projectile, transform.position, transform.rotation) as GameObject;
        canon = canonBall.GetComponent<Bullet>();
        canon.speed = speed;
    }
    public void Fire()
    {
        if (canonBall.activeSelf == false)
        {
            canonBall.transform.position = transform.position;
            canonBall.transform.rotation = transform.rotation;
            canonBall.SetActive(true);
        }
    }
    IEnumerator ShowFire()
    {
        yield return new WaitForSeconds(0.3f);
    }
    private void OnDestroy()
    {
        if (canonBall != null) canon.DestroyProjectile();
    }
}
