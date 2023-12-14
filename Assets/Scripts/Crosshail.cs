using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshail : MonoBehaviour
{
    [SerializeField] private FPSCamera fPSCamera;
    [SerializeField] private float sizeState;
    [SerializeField] private float sizeAfterShoot;
    [SerializeField] private float speedOfChangingSize;
    private float sizeCurrent;
    private RectTransform crosshail;

    private void Start()
    {
        crosshail = gameObject.GetComponent<RectTransform>();
        Bow.Instance.OnShoot += Bow_OnShoot;
    }

    private void Bow_OnShoot(object sender, System.EventArgs e)
    {
        sizeCurrent = sizeAfterShoot; 
    }

    private void Update()
    {
        sizeCurrent = Mathf.Lerp(sizeCurrent, sizeState, Time.deltaTime * speedOfChangingSize); 
        crosshail.sizeDelta = new Vector2(sizeCurrent, sizeCurrent);
    }

}

