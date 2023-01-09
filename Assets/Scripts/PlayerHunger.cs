using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHunger : MonoBehaviour {

    public Slider healthSlider;
    public Slider foodSlider;
    public TMP_Text foodText;
    public int speed;
    public float playerSpeed;

    void Update() {

        playerSpeed = foodSlider.value / 100;
        foodSlider.value -= speed * Time.deltaTime;
        foodText.text = ((int)(foodSlider.value)).ToString();

        /*
        if (foodSlider.value < 25) {
            healthSlider.value -= speed * Time.deltaTime;
        }
        if (foodSlider.value == 0) {
            healthSlider.value -= 2 * (speed * Time.deltaTime);
        }
        if (foodSlider.value > 75) {
            healthSlider.value += speed * Time.deltaTime;
        }
        */
    }
}
