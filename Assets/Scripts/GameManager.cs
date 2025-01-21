using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] UtilitiesSO _utilities;

    public static GameManager instance;
    private void Awake()
    {

        if (instance != null && instance != this)
        {
            // Destroy this object because it is a duplicate
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public UtilitiesSO GetUtilities()
    {
        return _utilities;
    }
}
