using System.IO.Compression;
using Unity.VisualScripting;
using UnityEngine;

public class HP : MonoBehaviour
{

     [SerializeField] private float Maucaonhat = 100f;
    [SerializeField] private float Mauhientai;
    void Start()
    {
        Mauhientai = Maucaonhat;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int damge)
    {
        Mauhientai -= damge;
        if(Mauhientai<=0)
        {
            Debug.Log("Chet");
        }
       
    }
    public float GetHPHienTai()
    {
        return Mauhientai;
    }
    public float GetHpMax()
    {
        return Maucaonhat;
    }
    public void SetMauHienTai(float hp)
    { Mauhientai += hp;
           
        if(Mauhientai >= Maucaonhat) {
            Mauhientai = Maucaonhat;

        }
    }
    

    }
        
    

