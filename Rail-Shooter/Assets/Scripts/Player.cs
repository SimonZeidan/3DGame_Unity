﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Tooltip("In ms^-1")][SerializeField] public float Speed = 20f;
    [Tooltip("In ms")] [SerializeField] public float xRange = 5f;
    [Tooltip("In ms")] [SerializeField] public float yRange = 3f;

    [SerializeField] public float positionPitchFactor = -5f;
    [SerializeField] public float controlPitchFactor = -20f;
    [SerializeField] public float positionYawFactor = 5f;
    [SerializeField] public float controllRollFactor = -20f;
    
    float xThrow, yThrow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        float pitchDuetoPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControl = transform.localPosition.y * positionPitchFactor;
        float pitch = pitchDuetoPosition + pitchDueToControl;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = yThrow * controllRollFactor ;

        transform.localRotation = Quaternion.Euler(pitch,yaw,roll);
    }

    private void ProcessTranslation()
    {
       xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
       yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * Speed * Time.deltaTime;
        float yOffset = yThrow * Speed * Time.deltaTime;

        float rawXPos = transform.localPosition.x + xOffset;
        float rawYPos = transform.localPosition.y + yOffset;

        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }
}
