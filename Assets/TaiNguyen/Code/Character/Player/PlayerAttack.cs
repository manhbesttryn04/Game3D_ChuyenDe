using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Animator Animatorr ;
    [SerializeField ] HP hP ;
    [SerializeField] private AudioSource AudioSource ;
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.R)) {
            Animatorr.SetTrigger("Tan cong");
            AudioSource.Play();
        }
    }
    

    
    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Dragon"))
        {
            Debug.Log(" Cham voi con rong.....");
            hP.TakeDamage(10);
          
        };
        

    }
}
