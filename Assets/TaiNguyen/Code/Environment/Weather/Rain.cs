using System.Collections;
using UnityEngine;

public class Rain : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Light Light;
    [SerializeField] AudioSource AudioSourceRain;
    [SerializeField]public AudioSource AudioSourceThunder;
    [SerializeField] public AudioClip RainSource;
    [SerializeField] public Material RainImage;
    [SerializeField] public Material SunImage;
    [SerializeField] public AudioSource WindSource;
    [SerializeField] public Material ThunderImage;
     
     public bool StartRain = false;
     public bool StartsouRain = true;
     [SerializeField] GameObject rainNyTime;
    [SerializeField] GameObject lightNing;
    [SerializeField] Transform startLightNing1;
    [SerializeField] Transform startLightNing2;
    [SerializeField] Transform startLightNing3;

    void Start()
    {
        
        rainNyTime.SetActive(false);
        WindSource.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(StartRain == true )
        {
           
            WindSource.enabled = true;
            RenderSettings.skybox = RainImage;
            Light.intensity = 1;
           
            Rainn();
            if(StartsouRain == true )
            { 
                AudioSourceRain.PlayDelayed(0.3f);
                StartsouRain = false;
            }
            if(Random.Range(0,100) == 50)
            { 
                StartCoroutine(Thunder());
                var i1 = Instantiate(lightNing, startLightNing1.position, Quaternion.identity);
                var i2 = Instantiate(lightNing, startLightNing2.position, Quaternion.identity);
                var i3 = Instantiate(lightNing, startLightNing3.position, Quaternion.identity);
                Destroy(i1, 2f);
                Destroy(i2, 1f);
                Destroy(i3, 1.5f);

                AudioSourceThunder.PlayOneShot(RainSource);
            }
        }
        else {
            rainNyTime.SetActive(false);
            RenderSettings.skybox = SunImage;
            Light.intensity = 3;
           StartsouRain = true;
            AudioSourceRain.Stop();
            //lightTime.SetActive(true);
            WindSource.enabled = false ;
        } 
            
    }
    IEnumerator Thunder()
    {
        Light.enabled = false;
       
        yield return  new WaitForSeconds(0.1f);
        Light.enabled = true;
  
        Light.intensity = 24;
        RenderSettings.skybox= ThunderImage;
        yield return new WaitForSeconds(0.1f);
        Light.intensity = 1;
        RenderSettings.skybox = RainImage;
    }
    public void Rainn()
    {
        rainNyTime.SetActive(true);
        
    }
}
