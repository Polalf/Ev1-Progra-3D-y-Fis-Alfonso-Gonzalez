using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granada : MonoBehaviour
{
    [SerializeField] private float explotionForce = 50;
    [SerializeField] private float radius = 10f;
    [SerializeField] private LayerMask afectable;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;

        Gizmos.DrawWireSphere(transform.position, radius);
    }
    public void Explotion()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius,afectable);

        foreach (Collider collider in colliders)
        {
            Vector3 dir = (collider.transform.position - transform.position).normalized * explotionForce;
            collider.GetComponent<Rigidbody>().AddForce(dir);
        }
    }
}
