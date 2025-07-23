using System.Collections;
using UnityEngine;

public class Manaing : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float tyLeHoiMana;
    public float thoiGianHoiMana;
    [SerializeField ] MaNa MaNa;
    public bool hoiMana = false;

    // Update is called once per frame
   public void BatDauHoiMana(bool hoi)
    {
        if(hoi == true ) {
            StartCoroutine(HoiMaNa());
        }
    }
     public IEnumerator HoiMaNa()
    {
        if(MaNa.GetManaHienTai() < MaNa.GetManaMax()) {
            hoiMana=true;
        }
        while(hoiMana == true)
        {
            yield return new WaitForSeconds(thoiGianHoiMana);
            MaNa.SetManaHienTai(tyLeHoiMana);
        }
        if(MaNa.GetManaHienTai() >= MaNa.GetManaMax()){
            hoiMana= false;

        }
    }
   
}
