using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotPuzzle : MonoBehaviour
{
    public List<RectTransform> circleDials;

    public Color normalColor;
    public Color selectedColor;

    private int maxParts = 16;
    private int selectedIndex = 0;

    [SerializeField] private bool solved = false;

    // Start is called before the first frame update
    void Start()
    {
        foreach(RectTransform rectTransform in circleDials) {
            rectTransform.Rotate(new Vector3(0f, 0f, ((float)Random.Range(1, maxParts)/maxParts)*360f));
        }

        selectedIndex = circleDials.Count-1;
        circleDials[selectedIndex].gameObject.GetComponent<Image>().color = selectedColor;
    }

    // Update is called once per frame
    void Update()
    {
        if(solved || circleDials.Count==0)
            return;

        int yDirection = 0;
        if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            yDirection = 1;
        else if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            yDirection = -1;

        if(yDirection != 0) {
            circleDials[selectedIndex].gameObject.GetComponent<Image>().color = normalColor;
            selectedIndex += yDirection;
            if(selectedIndex > circleDials.Count-1)
                selectedIndex = circleDials.Count-1;
            if(selectedIndex < 0)
                selectedIndex = 0;
            circleDials[selectedIndex].gameObject.GetComponent<Image>().color = selectedColor;
        }

        int xDirection = 0;
        if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            xDirection = 1;
        else if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            xDirection = -1;

        if(xDirection != 0) {
            RectTransform selectedDial = circleDials[selectedIndex];
            selectedDial.Rotate(new Vector3(0f, 0f, ((float)xDirection/maxParts)*360f));
            CheckSolution();
        }
    }

    private void CheckSolution() {
        bool allSet = true;
        foreach(RectTransform rectTransform in circleDials) {
            if(rectTransform.eulerAngles.z > 0.1f) {
                allSet = false;
                break;
            }
        }

        if(allSet) {
            Debug.Log("Solved!!");
            this.solved = true;
            StartCoroutine(PlaySolvedAnim());
        }
    }

    IEnumerator PlaySolvedAnim() {
        foreach(RectTransform rectTransform in circleDials) {
            rectTransform.gameObject.GetComponent<Image>().color = Color.yellow;
        }
        yield return new WaitForSeconds(0.2f);
    }
}
