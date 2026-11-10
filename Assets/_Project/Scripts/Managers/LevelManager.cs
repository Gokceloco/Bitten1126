using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int levelNo;

    private Level _currentLevel;

    public Level levelPrefab;

    public void RestartLevelManager()
    {
        DestroyOldLevel();
        CreateNewLevel();
    }

    private void CreateNewLevel()
    {
        _currentLevel = Instantiate(levelPrefab, transform);
        _currentLevel.StartLevel();
    }

    public void DestroyOldLevel()
    {
        if (_currentLevel != null)
        {
            Destroy(_currentLevel.gameObject);
        }
    }
}
