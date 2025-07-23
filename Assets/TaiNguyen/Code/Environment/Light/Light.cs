using System.Collections;
using UnityEngine;

public class Lightt: MonoBehaviour
{
    [SerializeField] public Light sun;
    public AnimationCurve lightIntensity;
    public float Timee = 0f;
    [SerializeField] public Material sun6time;
    [SerializeField] public Material sun12time;
    [SerializeField] public Material sun16time;
    [SerializeField] public Material darktime;
    public float TimeSunMaterDay = 0f;
    public float TimeDarkMaterDay = 0f;

   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        sun.intensity = Timee;
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(RunLIgt());
        sun.intensity = Timee;
        
        if (Timee >= 24)
        {
            Timee = 0;
        }
        if (TimeSunMaterDay >= 350f) {
            TimeSunMaterDay = 0;
        }
        float sunAngle = (Timee / 24f) * 360 - 90;

        // Cập nhật hướng ánh sáng (mặt trời mọc từ -90°, lặn ở 90°)
        sun.transform.rotation = Quaternion.Euler(sunAngle, 180f, 0f);
        if (Timee >= 7.37f && Timee < 12f)
        {
            RenderSettings.skybox = sun6time;
            RenderSettings.fogDensity = 0.001f;
            sun.colorTemperature = 5500f;
        }

        else if (Timee >= 12f && Timee < 15f)
        {
            RenderSettings.skybox = sun12time;
            sun.colorTemperature = 6500f;
            RenderSettings.fogDensity = 0f;
        }

        else if (Timee >= 15f && Timee < 18f)
        {
            
            if (Timee >= 15f && Timee < 17)
            {
                sun.colorTemperature = 4000;
                RenderSettings.fogDensity = 0.005f;
            }
            else if (Timee >= 17 && Timee < 18.5)
            {
                sun.colorTemperature = 1500;
                RenderSettings.skybox = sun16time;
                RenderSettings.fogDensity = 0.026f;

            }

        }

        else
        {
            RenderSettings.skybox = darktime;
            if (Timee >= 19 && Timee < 20)
            {
                sun.colorTemperature = 7000;
                RenderSettings.fogDensity = 0.027f;

            }
            else if (Timee >= 20 || Timee <= 4)
            {
                sun.colorTemperature = 10000;
                RenderSettings.fogDensity = 0.029f;
            }



        }
    }
    public IEnumerator RunLIgt()
    {
        yield return new WaitForSeconds(0.01f);
        Timee += 0.01f;
        TimeSunMaterDay += 0.01f;
    }
    

}
