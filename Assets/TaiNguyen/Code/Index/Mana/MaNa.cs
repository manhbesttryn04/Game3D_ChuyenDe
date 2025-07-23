using UnityEngine;

public class MaNa : MonoBehaviour
{
    public float maNaMax = 100f;
    public float maNaHienTai;
    void Start()
    {
        maNaHienTai = maNaMax;
    }

    public void TruMana(float value) {
        maNaHienTai -= value;
        if( maNaHienTai <= 0)
        {
            Debug.Log("Het mana");
        }
    }
   public float GetManaMax()
    {
        return maNaMax;
    }
    public float GetManaHienTai()
    {
        return maNaHienTai;
    }
    public void SetManaHienTai(float value)
    {
        maNaHienTai += value;
        if(maNaHienTai == maNaMax)
        {
            maNaHienTai = maNaMax;
        }
    }
}
