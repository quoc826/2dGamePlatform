using UnityEngine;
using UnityEngine.UI;

public class HealthBarEnemy : MonoBehaviour
{
    private Slider slider;

    // Tỷ lệ máu mục tiêu (từ 0 đến 1) mà thanh máu sẽ hướng tới
    private float targetHealthRatio;

    // Tốc độ thanh máu di chuyển đến giá trị mới
    [SerializeField] private float lerpSpeed = 5f;

    private void Awake()
    {
        // Tự động tìm component Slider
        slider = GetComponentInChildren<Slider>();

        if (slider == null)
        {
            Debug.LogError("HealthBar: Không tìm thấy component Slider trong đối tượng con!");
        }

        // Thiết lập giá trị ban đầu (Máu full)
        slider.value = 1f;
        targetHealthRatio = 1f;
    }

    // Hàm này được gọi từ Player.cs khi nhận sát thương
    public void updateHealthBar(float maxHealth, float currentHealth)
    {
        // Tính toán tỷ lệ máu mới (từ 0 đến 1)
        targetHealthRatio = currentHealth / maxHealth;
    }

    // Update chạy mỗi frame để làm thanh máu di chuyển mượt mà
    private void Update()
    {
        // Lerp di chuyển giá trị của Slider từ giá trị hiện tại đến giá trị mục tiêu
        // Time.deltaTime giúp quá trình này diễn ra mượt mà không phụ thuộc vào Frame Rate
        slider.value = Mathf.Lerp(slider.value, targetHealthRatio, Time.deltaTime * lerpSpeed);
    }
}
