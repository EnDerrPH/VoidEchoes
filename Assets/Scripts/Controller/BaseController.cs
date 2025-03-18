using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Animator _animator;
    protected string _currentAnimation;
    protected AudioManager _audioManager;

    public virtual void Start()
    {
        InitializeComponents();
        Physics.SyncTransforms();
    }

    public virtual void InitializeComponents()
    {
        _animator = GetComponent<Animator>();
        _audioManager = GameManager.instance.GetAudioManager();
    }

    public void ChangeAnimation(string animation , float crossFadeTime)
    {
        if(_currentAnimation == animation)
        {
            return;
        }
        _currentAnimation = animation;
        _animator.CrossFade(animation , crossFadeTime);
    }

    public void PlayAnimation(string animation)
    {
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName(animation)) 
        {
            _animator.Play(animation, 0, 0f);
        }
    }
}
