using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; // Thư viện bắt buộc cho Input System mới

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenuCanvas;
    private bool isPaused = false;

    void Start()
    {
        // Khi mới vào game: Ẩn menu và cho thời gian chạy bình thường
        pauseMenuCanvas.SetActive(false);
        Time.timeScale = 1f;
    }

    void Update()
    {
        // Nhấn phím ESC để bật/tắt Menu
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        pauseMenuCanvas.SetActive(true); // Hiện Menu
        Time.timeScale = 0f; // Đóng băng thời gian

        // Bắt buộc: Hiện chuột và mở khóa để người chơi có thể bấm nút
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenuCanvas.SetActive(false); // Ẩn Menu
        Time.timeScale = 1f; // Trả lại thời gian

        // Bắt buộc: Ẩn chuột và khóa lại vào giữa màn hình để điều khiển góc nhìn
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OpenSettings()
    {
        // Nút này tạm thời chỉ in ra thông báo, phát triển sau
        Debug.Log("Tính năng Cài đặt sẽ được cập nhật sau!");
    }

    public void SaveAndExit()
    {
        // Bắt buộc: Trả thời gian về 1 trước khi chuyển Cảnh, nếu không Cảnh mới cũng bị đóng băng
        Time.timeScale = 1f; 
        
        Debug.Log("Đã lưu dữ liệu game!"); // Code lưu trữ sẽ viết tại đây sau

        // Thay chữ "MainMenu" bằng đúng tên cái Scene menu chính của bạn
        SceneManager.LoadScene("MainMenu"); 
    }
}