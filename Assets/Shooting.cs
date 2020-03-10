using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    bool canShoot = true;

    float xaim, yaim, fireRate = .15f;
    GameObject currentPrefab;
    public GameObject prefab;
    public float speed = 10;

    public KeyCode shootKey;
    void Update()
    {
        xaim = Input.GetAxisRaw("Horizontal");
        yaim = Input.GetAxisRaw("Vertical");
        #region Shoot
        if (Input.GetKey(KeyCode.Space) && canShoot)
        {
            Shoot();
            //prefab.transform.position = transform.position;
        }
        #endregion
    }
    void Shoot()
    {
        Debug.Log("SHot");
        StartCoroutine(FireRate());
        currentPrefab = Instantiate(prefab, transform.position, transform.rotation);
        Rigidbody2D cprb = currentPrefab.GetComponent<Rigidbody2D>();
        cprb.velocity = new Vector2(xaim * speed, yaim * speed);
        if (yaim == 0 && xaim == 0)
        {
            cprb.velocity = new Vector2(1 * speed, 0 * speed);
        }
    }
    IEnumerator FireRate()
    {
        canShoot = false;
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }
}
