using UnityEngine.UI;
using UnityEngine;
using TMPro;

/// <summary>
/// Handles the muffin button clicking
/// </summary>
public class MuffinClicker : MonoBehaviour
{
    public GameObject muffinTextRewardPrefab;
    public MiniMuffin miniMuffinPrefab;
    public RectTransform muffinTransformParent;
    public Vector2 minimumXandY;
    public Vector2 maximumXandY;
    public AudioClip[] muffinClips;

    [SerializeField]
    private UIHandler uiHandler;
    private AudioSource muffinAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello world!");
        muffinAudioSource = GetComponent<AudioSource>();
    }

    public void OnMuffinButtonClicked()
    {
        // notify the GameManager that the muffin button was clicked, add muffins
        GameManager.instance.AddMuffins(GameManager.instance.muffinsPerClick);

        // notify the ui handler that the muffin button was clicked
        uiHandler.UpdateMuffinAmountText();

        // creating the floating text reward 
        CreateTextReward();

        // playing a sound on muffin clicked
        PlayMuffinSound();

        // create the mini muffin
        CreateLittleMiniMuffin();
    }

    private void CreateLittleMiniMuffin()
    {
        MiniMuffin miniMuffin = Instantiate(miniMuffinPrefab, muffinTransformParent);
        miniMuffin.SetUpVelocities(GetRandomPosition());
    }

    private void CreateTextReward()
    {
        // create the text reward
        GameObject newTextReward = Instantiate(muffinTextRewardPrefab, muffinTransformParent);

        // get a random position on screen, set the muffin position to it
        Vector2 randomPosition = GetRandomPosition();
        newTextReward.transform.localPosition = randomPosition;

        // set the text to be the actual muffins per click (+1 or +n etc.)
        newTextReward.GetComponent<TMP_Text>().text = $"+ {GameManager.instance.muffinsPerClick}";
    }

    private Vector2 GetRandomPosition()
    {
        float x = Random.Range(minimumXandY.x, maximumXandY.x);
        float y = Random.Range(minimumXandY.y, maximumXandY.y);
        return new Vector2(x, y);
    }

    private void PlayMuffinSound()
    {
        int index = Random.Range(0, muffinClips.Length);
        muffinAudioSource.PlayOneShot(muffinClips[index]);
    }
}
