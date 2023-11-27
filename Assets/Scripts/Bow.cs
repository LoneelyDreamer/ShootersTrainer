using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public event EventHandler OnShoot;

    public static Bow Instance { get; private set; }

    [SerializeField] private Camera fpsCamera;
    [SerializeField] private Transform arrow;
    [SerializeField] private float impactForse = 30f;
    private float timeBetwenShoots;
    private float timeBetwenShootsMax = 0.5f;
    private AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        TimetBeforeNextShoot();
        HandleShoot();
    }

    private void HandleShoot()
    {
        if (!GameManager.Instance.IsGamePlaying()) { return; } 

        if (Input.GetMouseButtonDown(0) && (timeBetwenShoots <= 0))
        {
            audioSource.Play();
            Shoot();
        }
    }

    private void Shoot()
    {        
        RaycastHit hit;
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit))
        {
            OnShoot?.Invoke(this, EventArgs.Empty);
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.HitTarget(hit); // передаю значения RaycastHit в Target чтобы прекрепить префаб стрелы в точке попадания
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(hit.point * impactForse);
            }

            timeBetwenShoots = timeBetwenShootsMax;
        }
    }

    private void TimetBeforeNextShoot()
    {
        timeBetwenShoots -= Time.deltaTime;
    }
}
