using Godot;
using System;

internal partial class TransitionAnimation : CanvasLayer
{
    [Export]
    private AnimationPlayer _transitionPlayer;

    [Export]
    private string _fadeInAnimationName;
    [Export]
    private string _fadeOutAnimationName;

    [Export]
    private bool _playFadeInBackwards;
    [Export]
    private bool _playFadeOutBackwards;

    public void PlayFadeInAnimation(Action animationEnded = null)
    {
        if (_playFadeInBackwards == false)
            _transitionPlayer.Play(_fadeInAnimationName);
        
        if (_playFadeInBackwards == true)
            _transitionPlayer.PlayBackwards(_fadeInAnimationName);

		InvokeOnAnimationFinished(_ => animationEnded?.Invoke());
    }

    public void PlayFadeOutAnimation(Action animationEnded = null)
    {
        if (_playFadeOutBackwards == false)
            _transitionPlayer.Play(_fadeOutAnimationName);
        
        if (_playFadeOutBackwards == true)
            _transitionPlayer.PlayBackwards(_fadeOutAnimationName);

		InvokeOnAnimationFinished(_ => animationEnded?.Invoke());
	}

	private void InvokeOnAnimationFinished(Action<string> invokingAction)
	{
		string signalName = AnimationPlayer.SignalName.AnimationFinished;
		uint flags = (uint)ConnectFlags.OneShot;

        _transitionPlayer.Connect(signalName, Callable.From(invokingAction), flags);
	}
}