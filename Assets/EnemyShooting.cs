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
        { //Om han inte skjuter så börja "cd" Timer
            StartCoroutine(cd());
        }
    }
    IEnumerator cd()
    {
        isShooting = true;
        yield return new WaitForSeconds(2f); //Väntar 2 sekunder
        newProjectile = Instantiate(projectile); //Skapar en "Clone" av objectet "projectile" och sätter variabeln "newProjectile" till den klonen
        newProjectile.transform.position = new Vector2(transform.position.x, transform.position.y + Random.Range(-1, 2)); //Ändrar newProjectiles position till en av tre möjligheter på Y-axlen
        newProjectile.GetComponent<Rigidbody2D>().velocity = -transform.right * Time.deltaTime * 50f; //Adderar hastighet till newProjectile med 50 i sekunden
        isShooting = false;
    }
}