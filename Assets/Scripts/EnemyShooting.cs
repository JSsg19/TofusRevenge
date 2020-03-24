using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyShooting : MonoBehaviour
{
    public GameObject projectile;
    GameObject newProjectile;
    bool isShooting = false;
    void Update()
    {
        if (!isShooting)
        {
            StartCoroutine(cd());
        }
    }
    IEnumerator cd()
    {
        isShooting = true;
        yield return new WaitForSeconds(2f);
        newProjectile = Instantiate(projectile);
        newProjectile.transform.position = new Vector2(transform.position.x, transform.position.y + Random.Range(-1, 2));
        newProjectile.GetComponent<Rigidbody2D>().velocity = -transform.right * Time.deltaTime * 50f;
        isShooting = false;
    }
}