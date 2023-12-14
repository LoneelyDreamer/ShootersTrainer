using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private float speed = 0.1f;
    [SerializeField] Transform arrow;
    [SerializeField] private int scoreCost;
    [SerializeField] private float lifeTime;

    private int penaltyCost = 1;
    private float impactForse = 1f;
    private float lifeTimeAfterHit = 1;
    public static event Action<int> OnHited;
    public static event Action<int> NotHited;
    private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        StartCoroutine(LifeTimeCoroutine(lifeTime));
    }

    void FixedUpdate()
    {
        transform.position += Vector3.back * speed;
    }

    public void HitTarget(RaycastHit hit)
    {
        Instantiate(arrow, hit.point, Quaternion.identity, transform);
        OnHited?.Invoke(scoreCost);
        Destroy(gameObject, lifeTimeAfterHit);
    }

    IEnumerator LifeTimeCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        rigidbody.AddForce(Vector3.up * impactForse); 
        Destroy(gameObject, 0.01f);
        NotHited?.Invoke(penaltyCost);
    }
}
