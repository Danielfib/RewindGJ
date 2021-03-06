﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinAnim : MonoBehaviour
{
    void Update()
    {
        if(Time.timeScale > 0)
            transform.position += new Vector3(0f, Mathf.Sin(Time.time) * 0.005f, 0f);
    }
}
