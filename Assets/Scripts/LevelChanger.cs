using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour {
    public void changeSceneTo(int levelIndex) {
        SceneManager.LoadScene(levelIndex);
    }
}
