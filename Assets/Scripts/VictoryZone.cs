using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryZone : MonoBehaviour
{
    [SerializeField] private LayerMask empujable;
    [SerializeField] private Vector3 size = Vector3.one;
    [SerializeField] private GameObject winPanel;
    // Start is called before the first frame update
    void Start()
    {
        winPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, size, transform.rotation, empujable);

        if(colliders.Length > 0)
        {
            GameManager.State = "Victory";
            winPanel.SetActive(true);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, size);
    }
}
