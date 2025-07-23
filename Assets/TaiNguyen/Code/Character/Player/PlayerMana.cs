using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class PlayerMana : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Slider playerSlider;
    [SerializeField]  public CinemachineFollow CinemachineFollow;
    [SerializeField]  public MaNa MaNa;
    [SerializeField] public Manaing Manaing;
    [SerializeField] public Animator animator;
    public float chieu1 = 10f;
    public float chieu2 = 20f;
    public float chieu3 = 30f;
    public bool hoiMana= false;
    public float dameSkill = 50f;
   
    void Start()
    {
        playerSlider.maxValue = MaNa.GetManaMax();
            }

    // Update is called once per frame
    void Update()
    {playerSlider.value = MaNa.GetManaHienTai();
        if (MaNa.GetManaHienTai() < MaNa.GetManaMax())
        {
            hoiMana = true;
            Manaing.BatDauHoiMana(hoiMana);
        }

        if (Input.GetKeyDown(KeyCode.T) && MaNa.GetManaHienTai() >= chieu1 && MaNa.GetManaHienTai() > 0)
        {
            MaNa.TruMana(chieu1);
            Debug.Log("Chieu 1");
            animator.SetTrigger("Skill 1");
            StartCoroutine(SetDameSkill(10));


        }
        else if (Input.GetKeyDown(KeyCode.Y) && MaNa.GetManaHienTai() >= chieu2 && MaNa.GetManaHienTai() > 0)
        {
            MaNa.TruMana(chieu2);
            Debug.Log("Chieu 2");
            animator.SetTrigger("Skill 2");
            StartCoroutine(SetDameSkill(40));
        }
        else if (Input.GetKeyDown(KeyCode.U) && MaNa.GetManaHienTai() >= chieu3 && MaNa.GetManaHienTai() > 0)
        {
            MaNa.TruMana(chieu3);
            Debug.Log("Chieu 3");
            StartCoroutine(SetDameSkill(60));
        }
       
        else if(MaNa.GetManaHienTai() <= 0)
        {
            Debug.Log("HetMana");
        }
       
        
       
        //hoimana
        
        playerSlider.transform.rotation = CinemachineFollow.transform.rotation;
    }

    public IEnumerator SetDameSkill( float dame)
    {
        dameSkill = dame;
        yield return new WaitForSeconds(0.1f);
        dameSkill = 50f;
    }
    public float GetDameSkill() {  return dameSkill; }

}



