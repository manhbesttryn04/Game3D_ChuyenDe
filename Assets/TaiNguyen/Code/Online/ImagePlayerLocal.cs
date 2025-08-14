using UnityEngine;
using UnityEngine.UIElements;

public class ImagePlayerLocal : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]public GameObject ImageCharacterObject;


    public void SetImage(Sprite sprite)
    {
        ImageCharacterObject.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
    }
}
