using System.Collections.Generic;
using UnityEngine;

public class MapGenerator2 : MonoBehaviour
{
    public Transform player; // Nhân vật
    public GameObject[] chunkPrefabs; // Các prefab chunks
    public int renderDistance = 3; // Số chunk giữ xung quanh nhân vật
    public float chunkSize = 50f; // Kích thước mỗi chunk

    private List<GameObject> activeChunks = new List<GameObject>();
    private Transform lastAnchorPoint; // Điểm neo của chunk cuối cùng
    public bool setZoneCenter;
    public bool setZoneRight;
    public bool setZoneLeft;
    public bool setZoneBack;

    void Start()
    {
        // Tạo chunk đầu tiên
        SpawnInitialChunks();
    }

    void Update()
    {
        // Kiểm tra nếu cần thêm hoặc hủy chunks
        UpdateChunks();
        if (setZoneCenter == true)
        {
            Debug.Log("Spaw");
            SpawnNextChunk();
            setZoneCenter = false;

        }else if (setZoneRight == true){
            SpawnNextChunkRight();
            setZoneRight = false;
        }else if(setZoneLeft == true)
        {
            SpawnNextChunkLetlf();
            setZoneLeft = false;

        }

    }

    void SpawnInitialChunks()
    {
        // Tạo chunk đầu tiên
        GameObject firstChunk = Instantiate(chunkPrefabs[0], new Vector3(player.position. x -50, player.position.y,player.position.z-50), player.rotation);
        activeChunks.Add(firstChunk);

        // Lấy anchor point cuối của chunk đầu tiên
        lastAnchorPoint = firstChunk.transform.Find("End Point");

        // Tạo các chunk tiếp theoi
       
       
    }

    void SpawnNextChunk()
    {
        // Chọn ngẫu nhiên một prefab chunk
        GameObject chunkPrefab = chunkPrefabs[Random.Range(0, chunkPrefabs.Length)];

        // Sinh chunk mới tại vị trí anchor point
        GameObject newChunk = Instantiate(chunkPrefab, new Vector3(player.position.x - 10,player.position.y, player.position.z -50), Quaternion.identity);

        // Cập nhật lastAnchorPoint với chunk vừa tạo
        //lastAnchorPoint = newChunk.transform.Find("End Point");

        // Thêm chunk vào danh sách
        activeChunks.Add(newChunk);
    }
    void SpawnNextChunkRight()
    {
        // Chọn ngẫu nhiên một prefab chunk
        GameObject chunkPrefab = chunkPrefabs[Random.Range(0, chunkPrefabs.Length)];
       
        // Sinh chunk mới tại vị trí anchor point
        GameObject newChunk = Instantiate(chunkPrefab, new Vector3(player.position.x- 50, player.position.y,player.position.z-50), Quaternion.identity);

        // Cập nhật lastAnchorPoint với chunk vừa tạo
       // lastAnchorPoint = newChunk.transform.Find("End Point");

        // Thêm chunk vào danh sách
        activeChunks.Add(newChunk);
    }
    void SpawnNextChunkLetlf()
    {
        // Chọn ngẫu nhiên một prefab chunk
        GameObject chunkPrefab = chunkPrefabs[Random.Range(0, chunkPrefabs.Length)];

        // Sinh chunk mới tại vị trí anchor point
        GameObject newChunk = Instantiate(chunkPrefab, new Vector3(player.position.x - 50, player.position.y, player.position.z ), Quaternion.identity);

        // Cập nhật lastAnchorPoint với chunk vừa tạo
       //lastAnchorPoint = newChunk.transform.Find("End Point");

        // Thêm chunk vào danh sách
        activeChunks.Add(newChunk);
    }
    void SpawnNextChunkBack()
    {
        // Chọn ngẫu nhiên một prefab chunk
        GameObject chunkPrefab = chunkPrefabs[Random.Range(0, chunkPrefabs.Length)];

        // Sinh chunk mới tại vị trí anchor point
        GameObject newChunk = Instantiate(chunkPrefab, new Vector3(player.position.x - 50, player.position.y, player.position.z-50

            ), Quaternion.identity);

        // Cập nhật lastAnchorPoint với chunk vừa tạo
       // lastAnchorPoint = newChunk.transform.Find("End Point");

        // Thêm chunk vào danh sách
        activeChunks.Add(newChunk);
    }


    void UpdateChunks()
    {
        // Hủy chunks quá xa
        if (activeChunks.Count > renderDistance)
        {
            GameObject oldChunk = activeChunks[0];
            activeChunks.RemoveAt(0);
            Destroy(oldChunk,5f);
        }

        // Tạo chunk mới nếu cần
       // if (Vector3.Distance(player.position, lastAnchorPoint.position) < chunkSize)
       // {
           // SpawnNextChunk();
       // }
    }
    public void SetZoneCenter(bool i)
    {
        setZoneCenter = i;
    }
    public void SetZoneRight(bool i ){
        setZoneRight = i;
    }
    public void SetZoneLeflt(bool i)
    {
        setZoneLeft = i;
    }
    public void SetZoneBack(bool i)
    {
        setZoneBack = i;
    }
}
