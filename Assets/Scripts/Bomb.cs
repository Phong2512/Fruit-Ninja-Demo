using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    [SerializeField] float maxLifetime;

    [SerializeField] float minForce;
    [SerializeField] float maxForce;
    private Rigidbody rb;

    private void Start()
    {
        AddForce();
        Destroy(this.gameObject, maxLifetime);
    }
    public void AddForce()
    {
        rb = GetComponent<Rigidbody>();

        float force = Random.Range(minForce, maxForce);
        rb.AddForce(this.transform.up * force, ForceMode.Impulse);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<GameManager>().Explode();

        }
    }
}
