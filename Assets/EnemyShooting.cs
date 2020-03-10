using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyShooting : MonoBehaviour
{
    public GameObject projectile;
    GameObject newProjectile;
    void Update()
    {
        if(newProjectile != null)
        {
            newProjectile.GetComponent<Rigidbody2D>().velocity = -transform.right * Time.deltaTime * 50f;
        }

        StartCoroutine(cd());
    }
    IEnumerator cd()
    {
        yield return new WaitForSeconds(2f);
        newProjectile = Instantiate(projectile);
        newProjectile.transform.position = new Vector2(transform.position.x, transform.position.y + Random.Range(-1, 2));
        yield return new WaitForSeconds(2f);
    }
}