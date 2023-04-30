using UnityEngine;
using UnityEngine.SceneManagement;
using EnumLibrary;

public enum GameFinish
{
    Win,
    Lose
}
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Game Manager is NULL");
            }
            return _instance;
        }
    }
    public delegate void StartGame();
    public event StartGame OnStartGame;
    private Escenas _scene;
    private bool _calledStartGame;
    private GameObject _playerData;
    //public delegate void ChangeScene(string scene);
    //public event ChangeScene OnChangeScene;
    private void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);
        else
        {
            DontDestroyOnLoad(gameObject);
            _instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        _calledStartGame = false;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeBetweenScene();
        if (_scene == Escenas.GameScreen)
        {

            if (!_calledStartGame)
            {
                _playerData = GameObject.Find("Player");
                UIManager.Instance.AtStartGameScene();
                _calledStartGame = true;
               // OnStartGame();
            }
        }
    }
        void ChangeBetweenScene()
        {
            switch (SceneManager.GetActiveScene().name)
            {
                case "GameScreen":
                    _scene = Escenas.GameScreen;
                    break;
                case "GameStart":
                    _scene = Escenas.StartScreen;
                    break;
                default:
                    break;
            }
        }
        public void EventSubscriberForUsableItem(UsableItemBehaivour usableItemBehaivour)
        {
        GameObject[] eventors = GameObject.FindGameObjectsWithTag("Event");
        foreach (GameObject eventor in eventors)
            {
             if (eventor.TryGetComponent(out IWaitTillUsableItem gameObjectWithEvent))
                {
                    if (gameObjectWithEvent.GetId() == usableItemBehaivour.UsableItem.Id)
                         {
                            usableItemBehaivour.usableItemEvent += gameObjectWithEvent.ItemInteractionEvent;
                         }
                }
            }    
        }

        public void GameOver(bool death)
        {
             UIManager.Instance.ShowGameOverText(death ? "You lose" : "You win");
            Cursor.lockState = CursorLockMode.None;
             Cursor.visible = true;
         }

        public void LoadScene(Escenas escena)
        {
        switch (escena)
        {
            case Escenas.GameScreen:
                _calledStartGame = false;
                SceneManager.LoadScene("GameScreen");
                break;
            case Escenas.StartScreen:
                SceneManager.LoadScene("StartScreen");
                break;
            default:
                break;
        }

         }
        
        public void ApplyAudioClipForInteraction(AudioClip audioClip)
        {
            _playerData.GetComponent<AudioSource>().clip = audioClip;
        }
    }
