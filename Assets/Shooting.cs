using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float damage = 1f;
    bool canShoot = true;

    float xaim, yaim, fireRate = .15f;
    GameObject currentPrefab;
    public GameObject prefab;
    public float speed = 10;

    public KeyCode shootKey;
    void Update()
    {
        xaim = Input.GetAxisRaw("Horizontal"); //Sätter Horizontala siknings riktningen till plus eller minus beroende på vilken knapp man trycker ned (A eller D)
        yaim = Input.GetAxisRaw("Vertical"); //Sätter den Verticala siknings rikningen till plus eller minus beroende på vilken knapp man trycker ned (W eller S)
        #region Shoot
        if (Input.GetKey(shootKey) && canShoot)
        {//Om den angivna knappen hålls ned och variablen "canShoot" är lika med sant så startas funktionen shoot
            Shoot();
        }
        #endregion
    }
    void Shoot()
    {
        Debug.Log("SHot");
        StartCoroutine(FireRate()); //startar timer funktionen "FireRate"
        currentPrefab = Instantiate(prefab, transform.position, transform.rotation); //skapar ett nytt object som en klon av objectet "prefab" och sätter variabeln currentPrefab till den variablen
        Rigidbody2D cprb = currentPrefab.GetComponent<Rigidbody2D>(); //Sätter variabeln cprb till currentPrefabs Rigidbody2D
        cprb.velocity = new Vector2(xaim * speed, yaim * speed); //Adderar hastighet i dom aktiva riktningarna beroende på till vilka knappar som trycks ned
        if (yaim == 0 && xaim == 0)
        { // Om ingen riktning är aktiv så sätts den aktiva riktningen till höger
            cprb.velocity = new Vector2(1 * speed, 0 * speed);
        }
    }
    IEnumerator FireRate()
    { //Timer som hanterar hur snappt man kan skjuta
        canShoot = false;
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }
}
