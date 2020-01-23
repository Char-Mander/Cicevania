using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
    [SerializeField]
    private Transform posDisp;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float cadency;
    [SerializeField]
    private float dir;
    [SerializeField]
    float detectDist;
    [SerializeField]
    GameObject missile;


    private bool canLaunch = true;

    // Update is called once per frame
    void Update()
    {
        if (canLaunch && Vector2.Distance(this.transform.position, target.transform.position) <= detectDist)
        {
            canLaunch = false;
            GameObject go = Instantiate(missile, posDisp);
            go.GetComponent<Missile>().SetHorizontal(dir);
            Destroy(go, 10);
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(cadency);
        canLaunch = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, detectDist);
    }
}
