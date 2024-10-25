
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Font customFont; // Assign this in the Unity Inspector

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void LoadFirstLevel()
    {
        StartCoroutine(LoadSceneWithDelay(0.0f, "Level1"));
    }

    private IEnumerator LoadSceneWithDelay(float delay, string scene)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(scene);
    }

    public void LoadStartScene()
    {
        StartCoroutine(LoadSceneWithDelay(1.0f, "StartScene"));
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == SceneManager.GetSceneByName("Level1").buildIndex)
        {
            CreateCanvasesForGhosts();

            GameObject quitButton = GameObject.FindWithTag("QuitButton");
            if (quitButton != null)
            {
                Button button = quitButton.GetComponent<Button>();
                button.onClick.AddListener(LoadStartScene);
            }
        }
    }

    

    private void CreateCanvasesForGhosts()
    {
        for (int i = 1; i <= 4; i++)
        {
            // Find the ghost by tag
            GameObject ghost = GameObject.FindWithTag($"Ghost{i}");

            if (ghost != null)
            {
                // Find the existing canvas for the ghost
                GameObject canvas = GameObject.Find($"Ghost{i}Canvas");
                if (canvas != null)
                {
                    // Calculate the position above the ghost
                    Vector3 ghostPosition = ghost.transform.position;


                    // Find the TextMeshPro child in the canvas
                    RectTransform rect = canvas.GetComponentInChildren<RectTransform>();
                    rect.localPosition=  new Vector3(0, 0.2f, 0); 
                    TMP_Text labelText = canvas.GetComponentInChildren<TMP_Text>();
                    if (labelText != null)
                    {
                        labelText.text = i.ToString(); // Set the text to the corresponding ghost number

                        RectTransform textRect = labelText.GetComponent<RectTransform>();
                        textRect.localPosition=  new Vector3(0, 0.2f, 0); 
                    }
                    else
                    {
                        Debug.LogWarning($"TextMeshPro not found in {canvas.name}.");
                    }
                }
            }
        }
    }
    
}

