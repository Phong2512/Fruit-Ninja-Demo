using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitController : MonoBehaviour
{
    public GameObject whole;
    public GameObject sliced;

    private Rigidbody fruitRigidbody;
    private Collider fruitCollider;
    private ParticleSystem juiceParticleEffect;

    public int points = 1;

    [SerializeField] float maxLifetime;

    [SerializeField] float minForce;
    [SerializeField] float maxForce;
    private Rigidbody rb;

    private void Awake()
    {
        fruitRigidbody = GetComponent<Rigidbody>();
        fruitCollider = GetComponent<Collider>();
        juiceParticleEffect = GetComponentInChildren<ParticleSystem>();
    }

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
    private void Slice(Vector3 direction, Vector3 postion, float force)
    {
        FindObjectOfType<GameManager>().IncreaseScore(points);
        whole.SetActive(false);
        sliced.SetActive(true);

        fruitCollider.enabled = false;
        juiceParticleEffect.Play();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        sliced.transform.rotation =Quaternion.Euler(0f,0f,angle);

        Rigidbody[] slices = sliced.GetComponentsInChildren<Rigidbody>();
        
        foreach (Rigidbody slice in slices)
        {
            slice.velocity = fruitRigidbody.velocity;
            slice.AddForceAtPosition(direction * force, postion, ForceMode.Impulse);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Blade blade = other.GetComponent<Blade>();
            Slice(blade.direction, blade.transform.position, blade.sliceForce);

        }
    }
}
