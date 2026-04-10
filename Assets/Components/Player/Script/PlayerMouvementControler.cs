using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private float _jumpDuration = 1f ;
    [SerializeField] private float _jumpHeight = 3f;
    [SerializeField] private AnimationCurve _jumpCurve;
    [SerializeField] private AnimationCurve _fallCurve;

    [Header("Slide parameters")] 
    [SerializeField] private float _slideDuration;
    [SerializeField] private Transform[] _slideTarget;
    [SerializeField] private float _slideDownDuration = 1.5f;
	[Header("component")]
    [SerializeField] private Animator _animator;

    [Header("Debug")] 
    [SerializeField]private int _currentLaneIndex = 2;
    [SerializeField]private bool _isSliding;
    [SerializeField]private bool _isJumping; 
    [SerializeField]private bool _isSlidingDown;
    [SerializeField] private bool _locked;
    private bool _charging;
    private bool _chargeReady;
    private void Awake()
    {
        EventSystem.OnStateChanged += HandleStateChanged;
        EventSystem.MegaCharge += HandleMegaCharge;
        EventSystem.OnPlayerSlideDown += HandleOnPlayerSlideDown;
        EventSystem.MegaChargeReady += HandleMegaChargeReady;
        _locked = true;
    }

    private void Start()
    {
        _chargeReady = false;
    }

    private void HandleMegaChargeReady(bool megaChargeReady)
    {
        _chargeReady = megaChargeReady;
    }

    private void HandleOnPlayerSlideDown(bool charge)
    {
        _charging = charge;
    }

    private void HandleMegaCharge(bool megaCharge)
    {
       
    }

    private void HandleStateChanged(State newState)
    {
       if (newState is not GameState)
       {
           _locked = true;
           StopAllCoroutines();
           EventSystem.OnPlayerLifeUpdate -= HandlePlayerLifeUpted;
           return;
       }
       _animator.SetTrigger("Running");
       EventSystem.OnPlayerLifeUpdate += HandlePlayerLifeUpted;
       _locked = false;
    }

   

    private void OnDestroy()
    {
        EventSystem.OnPlayerLifeUpdate -= HandlePlayerLifeUpted; 
        EventSystem.OnStateChanged -= HandleStateChanged;
    }

    private void HandlePlayerLifeUpted(int playerLife)
    {
        if (playerLife > 0)
        {
            _animator.SetTrigger("TakeDamage");
            return;
        }
        _animator.SetTrigger("Dead");
        _locked = true;
    }

    public void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (_chargeReady == true)
            {
                EventSystem.MegaCharge?.Invoke(true);
                StartCoroutine(MegaChargeCoroutine());
                Debug.Log("megacharge");
                _chargeReady = false;
            }
            Debug.Log("megacharge not possible");
        }
        
        if (_locked)
        {
            return;
        }
        
       
        if (Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            if (_isJumping)
            {
                return;
            }
            StartCoroutine(JumpCoroutine());
        }

        if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            if (_isSliding)
            {
                return;
            }
            if (_currentLaneIndex == 0)
            {
                return;
            }
            _currentLaneIndex--;
            StartCoroutine(SlideCoroutine(_slideTarget[_currentLaneIndex])); 
        }

        if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            if (_isSliding)
            {
                return;
            }
            if (_currentLaneIndex == _slideTarget.Length - 1)
            {
                return; 
            }
            _currentLaneIndex++;
            StartCoroutine(SlideCoroutine(_slideTarget[_currentLaneIndex])); 
        }
        if (Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            if (_isSlidingDown || _isJumping)
            {
                return;
            }
            
            StartCoroutine(SlideDownCoroutine());
        }
    }

    private IEnumerator JumpCoroutine()
    {
        _isJumping = true;
        _animator.SetBool("IsJumping", true);
        float jumpTimer =0f;
        float halfJumpDuration =_jumpDuration/2f;
        //jump
        while (jumpTimer < halfJumpDuration)
        {
            jumpTimer += Time.deltaTime;

            var normalizedTime = jumpTimer / halfJumpDuration;
            
            var targetHeight =_jumpCurve.Evaluate(normalizedTime)* _jumpHeight ;
            var targetPosition = new Vector3(transform.position.x, targetHeight, transform.position.z);
            
            transform.position = targetPosition;
            
            yield return null;
        }
        //Fall
       jumpTimer = 0f;
       
        while (jumpTimer < halfJumpDuration)
        {
            jumpTimer += Time.deltaTime;
           
            
            var normalizedTime = jumpTimer / halfJumpDuration;
            var targetHeight =_fallCurve.Evaluate(normalizedTime)* _jumpHeight ;
            var targetPosition = new Vector3(transform.position.x, targetHeight, transform.position.z);
            transform.position = targetPosition;
            yield return null;
        }
        _isJumping = false;
        _animator.SetBool("IsJumping", false);
    }

    private IEnumerator MegaChargeCoroutine()
    {
        _currentLaneIndex = 2;
        _locked = true;
        transform.localScale = new Vector3(2, 2, 2) ;
        yield return new WaitForSeconds(5F);
        transform.localScale = new Vector3(1, 1, 1) ;
        _locked = false;
        EventSystem.MegaCharge?.Invoke(false);
        yield return null;
    }


    private IEnumerator SlideCoroutine(Transform target)
    {
        _isSliding = true;
        var slideTimer = 0F;
        while (slideTimer < _slideDuration)
        {
            slideTimer += Time.deltaTime;
            var normalizedTime = slideTimer / _slideDuration;
            var targetPosition = new Vector3(target.position.x,transform.position.y, target.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, normalizedTime);
            yield return null; 
        }
        _isSliding = false;
    }

    private IEnumerator SlideDownCoroutine()
    {
        EventSystem.OnPlayerSlideDown?.Invoke(true);
        _isSlidingDown = true;
        _animator.SetBool("IsSlidingDown", true);
        
        var slideTimer = 0f;

        while (slideTimer <= _slideDownDuration)
        {
            slideTimer += Time.deltaTime;
            yield return null;
        }
        
        _isSlidingDown = false;
        _animator.SetBool("IsSlidingDown", false);
        EventSystem.OnPlayerSlideDown?.Invoke(false);
    }
}
