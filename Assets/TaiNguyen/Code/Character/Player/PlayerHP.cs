using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    
    [SerializeField] private Slider playerSlider;
    [SerializeField] private CinemachineFollow CinemachineFollow;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject canvas;
    [SerializeField] private HP playerHP;
    [SerializeField] private Heathling Heathling;
    public bool hoiMau = false;
    void Start()
    {
        playerSlider.maxValue = playerHP.GetHpMax();
    }

    // Update is called once per frame
    void Update()
    {
        playerSlider.value = playerHP.GetHPHienTai();
        playerSlider.transform.rotation = CinemachineFollow.transform.rotation;
        if(playerHP.GetHPHienTai() < playerHP.GetHpMax())
        {
            hoiMau=true;
            Heathling.BatDauHoiMau(hoiMau);
        }
        else hoiMau =false;
        if(playerSlider.value <=0)
            {
                animator.SetBool("Die", true);
                
                StartCoroutine(LoadSceneAfterDelay(3));
        }
    }
    private IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(2f);
        canvas.SetActive(true);
        yield return new WaitForSeconds(delay);
        canvas.SetActive(true); SceneManager.LoadScene(0);
    }

}

