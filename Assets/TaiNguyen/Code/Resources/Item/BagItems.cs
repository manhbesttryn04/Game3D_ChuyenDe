using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BagItems : MonoBehaviour
{
    [SerializeField] private GameObject bagItems;
    [SerializeField] private GameObject panelInventory;
    [SerializeField] private GameObject panelSlot;
    public  List<GameObject> _slots;
    public List<PlayerItems> _items; // danh sách các item trong inventory
    public  int _numberOfSlots = 65;
    public bool batTatBagItems = true;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _slots = new List<GameObject>();
        _items = new List<PlayerItems>();



        // gi? s? nhân v?t có 3 item
        // item 1: id = 1, name = "Sword", img = "sword.png", amount = 1
        // item 2: id = 2, name = "Shield", img = "shield.png", amount = 2
        // item 3: id = 3, name = "Potion", img = "potion.png", amount = 10
        var item1 = new PlayerItems()
        {
            value = 3,
            id = 1,
            icon = null,
            name = "Sword"
        };
        var item2 = new PlayerItems()
        {
            value = 2,
            id = 2,
            icon = null,
            name = "Shield"
        };
        var item3 = new PlayerItems()
        {
            value = 2,
            id = 3,
            icon = null,
            name = "Shield"
        };

        _items.Add(item1);
        _items.Add(item2);
        _items.Add(item3); 


        RenderInventory();
        bagItems.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.B) && batTatBagItems == true)
        {
            bagItems.SetActive(true);
            batTatBagItems = false;
        }else if (Input.GetKeyDown(KeyCode.B) && batTatBagItems==false)
        {
            bagItems.SetActive(false) ;
            batTatBagItems = true;
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Mana"))
        {
            // l?y thông tin c?a item
            var item = other.gameObject.GetComponent<Items>();
            Debug.Log(item.id);
            // thêm item vào inventory
            AddItem(new PlayerItems()
            {
                value = 1,
                id = item.id,
                icon = item.icon,
                name = item.name
            });
            // ?n item
            other.gameObject.SetActive(false);
        }
    }
    public void AddItem(PlayerItems item)
    {
        // ki?m tra xem item ?ã có trong inventory ch?a
        var existingItem = _items.Find(i => i.id == item.id);
        if (existingItem != null)
        {
            existingItem.value += item.value;
            Hienthivatpham();
        }
        else
        {
            _items.Add(item);
            Hienthivatpham();
        }
        

        
    }

    void RenderInventory()
    {
        // t?o 84 slot, sau ?ó g?n vào inventory
        for (int i = 0; i < _numberOfSlots; i++)
        {
            GameObject slot = Instantiate(panelSlot, panelInventory.transform);
            slot.SetActive(true);
            slot.name = "Slot" + i;
            _slots.Add(slot);
        }
        Hienthivatpham();

        // hi?n th? item lên inventory
        

    }
    public void Hienthivatpham()
    {
        for (int i = 0; i < _items.Count; i++)
        {
            //_slots[i].transform.GetChild(0).GetComponent<Image>().sprite = _items[i].icon;
            // _slots[i].transform.GetChild(0).GetComponent<Image>().gameObject.SetActive(true);
            _slots[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _items[i].value.ToString();
            _slots[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().gameObject.SetActive(true);

        }
        
    }
    
}