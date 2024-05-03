using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PhysicalMovement))]
[RequireComponent(typeof(Granada))]
public class Player : MonoBehaviour
{
    private PhysicalMovement physicalMovement;
    private Granada granada;

    [Header("Movimiento" /* Un movimiento sexy*/)]
    [SerializeField] private float speed = 15f;
    [SerializeField] private float jumpForce = 15f;

    [Header("Colisiones")]
    [SerializeField] private Vector3 size = Vector3.one;
    [SerializeField] private LayerMask empujable;

    [Header("Escena")]
    [SerializeField] private int sceneIndex;
    private bool canMove;
    private bool inVictory;
    void Start()
    {
        canMove = true;
        inVictory = false;
        physicalMovement = GetComponent<PhysicalMovement>();
        granada = GetComponent<Granada>();
    }

    // Update is called once per frame
    void Update()
    {
        #region Movement
        if(canMove)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");

            physicalMovement.Move(new Vector3(x, 0, z) * speed);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                physicalMovement.Jump(jumpForce);
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                physicalMovement.DisableJump();
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                granada.Explotion();
            }
        }
        #endregion

        if (inVictory)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(sceneIndex);
            }
        }

        Collider[] colliders = Physics.OverlapBox(transform.position, size, transform.rotation, empujable);

        foreach (Collider collider in colliders)
        {
            Debug.Log(collider.gameObject.name);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, size);
    }

    private void OnEnable()
    {
        GameManager.OnStateChanged += HandleStateChanged;
    }

    private void OnDisable()
    {
        GameManager.OnStateChanged -= HandleStateChanged;
    }
    private void HandleStateChanged(string state)
    {
        if (state == "Gameplay")
        {
            canMove = true;
            inVictory = false;
        }
        else if(state == "Victory")
        {
            canMove = false;
            inVictory = true;
        }
            

        Debug.Log(state);
    }
}
