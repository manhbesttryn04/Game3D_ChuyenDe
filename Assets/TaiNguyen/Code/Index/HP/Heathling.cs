using System.Collections;
using UnityEngine;

public class Heathling : MonoBehaviour
{
    [SerializeField] private float tyLeHoiMau;
    [SerializeField] private float thoiGianHoiMau;
    [SerializeField] private HP HP;
    public bool hoiMau = false;
    void Start()
    {
        
    }
    
    public void BatDauHoiMau(bool hoiMau)
    {
        if ((hoiMau == true))
        {
          
            StartCoroutine(HoiMau());
        }
    }
    private IEnumerator HoiMau()
    {if(HP.GetHPHienTai() < HP.GetHpMax())
        {
            hoiMau=true;
        }

        while ( hoiMau == true)
        {
            yield return new WaitForSeconds(thoiGianHoiMau);
            if (HP.GetHPHienTai() < HP.GetHpMax())
            {
                HP.SetMauHienTai(tyLeHoiMau);
            }
            if (HP.GetHPHienTai() >= HP.GetHpMax())
            {
                hoiMau = false;
            }
        }
    }
}
