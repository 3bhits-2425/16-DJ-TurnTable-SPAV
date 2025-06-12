using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DJController : MonoBehaviour
{
    [Header("AudioSources")]
    public AudioSource leftSource;
    public AudioSource rightSource;

    [Header("Clips")]
    public AudioClip leftClip;
    public AudioClip rightClip;

    [Header("UI Elemente")]
    public Slider leftSpeedSlider;
    public Slider leftVolumeSlider;
    public Slider rightSpeedSlider;
    public Slider rightVolumeSlider;
    public Slider fadeSlider;
    public TMPro.TextMeshProUGUI songTitleText;
    public TMPro.TextMeshProUGUI songTitleText2;


    void Start()
    {
        // Clips zuweisen
        leftSource.clip = leftClip;
        rightSource.clip = rightClip;

        // Initiale Werte setzen
        UpdateLeftAudioSettings();
        UpdateRightAudioSettings();
        UpdateFade();
    }

    void Update()
    {
        UpdateFade();
        UpdateSongTitle();
    }

    public void PlayPauseLeft()
    {
        if (leftSource.isPlaying)
            leftSource.Pause();
        else
            leftSource.Play();
    }

    public void StopLeft()
    {
        leftSource.Stop();
    }

    public void PlayPauseRight()
    {
        if (rightSource.isPlaying)
            rightSource.Pause();
        else
            rightSource.Play();
    }

    public void StopRight()
    {
        rightSource.Stop();
    }

    public void UpdateLeftAudioSettings()
    {
        leftSource.volume = leftVolumeSlider.value;
        leftSource.pitch = Mathf.Lerp(0.5f, 2f, leftSpeedSlider.value); // Pitch 0.5x–2x
    }

    public void UpdateRightAudioSettings()
    {
        rightSource.volume = rightVolumeSlider.value;
        rightSource.pitch = Mathf.Lerp(0.5f, 2f, rightSpeedSlider.value);
    }

    public void UpdateFade()
    {
        float fadeValue = fadeSlider.value; // 0 = nur links, 1 = nur rechts
        leftSource.panStereo = -1f;
        rightSource.panStereo = 1f;

        leftSource.volume = (1f - fadeValue) * leftVolumeSlider.value;
        rightSource.volume = fadeValue * rightVolumeSlider.value;
    }

    private void UpdateSongTitle()
    {
        if (leftSource.isPlaying)
            songTitleText.text = "Aktueller Song: " + leftClip.name;
        else if (rightSource.isPlaying)
            songTitleText2.text = "Aktueller Song: " + rightClip.name;
        else
            songTitleText.text = "Kein Song spielt.";
    }
}
