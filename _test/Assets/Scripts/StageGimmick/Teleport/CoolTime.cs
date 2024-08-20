using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CoolTime : MonoBehaviour
{
    public DateTime cooltime;
    public DateTime cooltimetmp;

    public int cnt;

    // Start is called before the first frame update
    void Start()
    {
        cnt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        cooltime = DateTime.Now.AddSeconds(0.25);
    }
}
