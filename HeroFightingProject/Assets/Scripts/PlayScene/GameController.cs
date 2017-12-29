using UnityEngine;
using System.Collections;
public enum GameState
{
    Start,
    End,
    Pause
}
public class GameController : MonoBehaviour {
    public static GameController _instance;
    public GameState gameState;
    public PhotonEngine photoEngine;
    void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
        gameState = GameState.Pause;
        photoEngine = new PhotonEngine();
        UIManager._Instnace.PushPanel(UiPanelType.Start);
    }
    void Start()
    {
    }
    void Update()
    {
        photoEngine.peer.Service();
    }
}
