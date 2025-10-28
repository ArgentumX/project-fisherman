using Domain.Models.Common;
using Presentation.Common;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicBox : OutlineInteractable
{
    [SerializeField] private AudioSource audioSource; 
    [SerializeField] private string onDescription = "Включить";
    [SerializeField] private string offDescription = "Выключить";
    private bool _isPlaying = false;

    public override void Interact<T>(IInteractor<T> interactor)
    {
        if (_isPlaying) {
            audioSource.Pause();
            _isPlaying = false;
        }
        else {
            audioSource.Play();
            _isPlaying = true;
        }
    }

    public override bool CanInteract<T>(IInteractor<T> interactor) => true;

    public override string GetDescription() {
        return _isPlaying ? offDescription : onDescription;
    }

    private void Reset() {
        ResetBase();
    }

    private void Awake() {
        audioSource ??= GetComponent<AudioSource>();
    }
}