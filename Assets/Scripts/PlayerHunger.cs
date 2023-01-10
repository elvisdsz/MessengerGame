using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHunger : MonoBehaviour {

    public Slider energySlider;
    public int food;
    public TMP_Text foodText;
    public GameObject pressEText;
    public float hungerSpeed;
    public float playerSpeed;

    void Update() {

        playerSpeed = energySlider.value / 100;
        energySlider.value -= hungerSpeed * Time.deltaTime;
        foodText.text = food.ToString();

        if (energySlider.value < 10) {
            pressEText.SetActive(true);
        } else {
            pressEText.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.E) && food >= 5) {
            food -= 5;
            energySlider.value += 20;
        }
    }
}
