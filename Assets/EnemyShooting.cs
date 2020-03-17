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
=======
        if(newProjectile != null)
        {
            newProjectile.GetComponent<Rigidbody2D>().velocity = -transform.right * Time.deltaTime * 50f;
        }

        StartCoroutine(cd());
>>>>>>> 3c4499b0d4c859d4ce41935d29c09ea996e2af10
    }
    IEnumerator cd()
    {
        isShooting = true;
        yield return new WaitForSeconds(2f);
        newProjectile = Instantiate(projectile);
        newProjectile.transform.position = new Vector2(transform.position.x, transform.position.y + Random.Range(-1, 2));
<<<<<<< HEAD
        newProjectile.GetComponent<Rigidbody2D>().velocity = -transform.right * Time.deltaTime * 50f;
        isShooting = false;
=======
        yield return new WaitForSeconds(2f);
>>>>>>> 3c4499b0d4c859d4ce41935d29c09ea996e2af10
    }
}