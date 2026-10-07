using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;

[System.Serializable]
public class GameData
{
    public string lastSavedDate;
    public int currentLevel;
}

public class MainMenuManager : MonoBehaviour
{
    [Header("Giao diện Panels")]
    public GameObject panelHome;
    public GameObject panelSaveSlots;
    public GameObject panelConfirmDelete;

    [Header("Chữ hiển thị của 4 Slot")]
    public TextMeshProUGUI[] slotTexts; 

    [Header("4 Nút Xóa (Nhỏ)")]
    public GameObject[] deleteButtons; // <-- THÊM MẢNG NÀY ĐỂ QUẢN LÝ NÚT XÓA

    private string savePath;
    private int currentSlotToDelete = -1; 

    void Start()
    {
        savePath = Application.persistentDataPath;
        ShowPanel(panelHome);
    }

    public void ShowPanel(GameObject panelToShow)
    {
        panelHome.SetActive(false);
        panelSaveSlots.SetActive(false);
        panelConfirmDelete.SetActive(false);
        
        panelToShow.SetActive(true);
    }

    public void OpenSaveSlotsMenu()
    {
        ShowPanel(panelSaveSlots);
        RefreshSlotsUI(); 
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void RefreshSlotsUI()
    {
        for (int i = 0; i < 4; i++)
        {
            string filePath = savePath + "/saveSlot_" + i + ".json";
            if (File.Exists(filePath))
            {
                slotTexts[i].text = "Slot " + (i + 1) + " - Đã có dữ liệu";
                deleteButtons[i].SetActive(true); // <-- HIỆN NÚT XÓA NẾU CÓ DỮ LIỆU
            }
            else
            {
                slotTexts[i].text = "Slot " + (i + 1);
                deleteButtons[i].SetActive(false); // <-- ẨN NÚT XÓA NẾU SLOT TRỐNG
            }
        }
    }

    public void OnSlotClicked(int slotIndex)
    {
        string filePath = savePath + "/saveSlot_" + slotIndex + ".json";

        if (File.Exists(filePath))
        {
            Debug.Log("Đang tải dữ liệu từ Slot " + slotIndex);
            SceneManager.LoadScene("DemoGame");
        }
        else
        {
            Debug.Log("Slot trống. Đang tạo file JSON mới cho Slot " + slotIndex);
            
            GameData newData = new GameData { 
                lastSavedDate = System.DateTime.Now.ToString("dd/MM/yyyy HH:mm"), 
                currentLevel = 1 
            };
            
            string json = JsonUtility.ToJson(newData);
            File.WriteAllText(filePath, json);
            
            SceneManager.LoadScene("DemoGame"); 
        }
    }

    public void OnClickDeleteButton(int slotIndex)
    {
        string filePath = savePath + "/saveSlot_" + slotIndex + ".json";
        
        if (File.Exists(filePath))
        {
            currentSlotToDelete = slotIndex;
            panelConfirmDelete.SetActive(true); 
        }
    }

    public void ConfirmDelete()
    {
        string filePath = savePath + "/saveSlot_" + currentSlotToDelete + ".json";
        
        if (File.Exists(filePath))
        {
            File.Delete(filePath); 
            Debug.Log("Đã xóa vĩnh viễn dữ liệu tại Slot " + currentSlotToDelete);
        }
        
        panelConfirmDelete.SetActive(false); 
        currentSlotToDelete = -1;            
        RefreshSlotsUI(); // <-- Hàm này sẽ quét lại và lập tức ẩn nút Xóa đi                   
    }

    public void CancelDelete()
    {
        panelConfirmDelete.SetActive(false); 
        currentSlotToDelete = -1;            
    }
}