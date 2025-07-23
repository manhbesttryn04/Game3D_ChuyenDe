using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] public GameObject thienThach;
    public Transform thienThachTransform;
    void Start()
    {
        thienThach.SetActive(false);
        thienThachTransform.position = thienThach.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)) {
            thienThach.SetActive(true);
            thienThach.transform.position = Vector3.down * 3 * Time.deltaTime;
            if(thienThach.transform.position == new Vector3(thienThach.transform.position.x, 1076f,thienThach.transform.position.z)) {

                Destroy(thienThach, 3f );
                thienThach.transform.position = thienThachTransform.position;
            }
        }
    }
}
