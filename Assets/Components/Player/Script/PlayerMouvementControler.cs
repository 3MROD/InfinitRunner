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
        // listen to OnStateChanged MegaCharge OnPlayerSlideDown and MegaCharge ready 
        EventSystem.OnStateChanged += HandleStateChanged;
        EventSystem.MegaCharge += HandleMegaCharge;
        EventSystem.OnPlayerSlideDown += HandleOnPlayerSlideDown;
        EventSystem.MegaChargeReady += HandleMegaChargeReady;
        //block the mouvement
        _locked = true;
    }

    private void Start()
    {
        _chargeReady = false;
    }

    private void HandleMegaChargeReady(bool megaChargeReady)
    {
       // linking MegaCharge Bool to _ChargeReady
        _chargeReady = megaChargeReady;
    }

    private void HandleOnPlayerSlideDown(bool charge)
    {
        // linking OnPlayerSlideDown Bool to _charge
        _charging = charge;
    }

    private void HandleMegaCharge(bool megaCharge)
    {
       
    }

    private void HandleStateChanged(State newState)
    {
        // if in GameState set animator to Running unlock the mouvement and listen to OnplayerLifeUpdate
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
        EventSystem.MegaCharge -= HandleMegaCharge;
        EventSystem.OnPlayerSlideDown -= HandleOnPlayerSlideDown;
    }

    private void HandlePlayerLifeUpted(int playerLife)
    {
        // when PlayerLifeupdate is called if player has no more life set Dead animation and lock mouvement, otherwise Damage animation
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
        //if the Space bar while activate Megacharge and Coroutine if _charge ready is true 
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
        // can't use the Keyboard 
        if (_locked)
        {
            return;
        }
        
       // Up Arrow will start the jump coroutine
        if (Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            if (_isJumping)
            {
                return;
            }
            StartCoroutine(JumpCoroutine());
        }
        // left arrow will mouve -1 on the lane index until no more left
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

        // left arrow will mouve +1 on the lane index until Max

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
        // down Arrow while start the SlideDown Coroutine 
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
        // Change animation to jumping
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
        // Change animation to not jumping
    }

    private IEnumerator MegaChargeCoroutine()
    {
        //MegaCharge
        // Lock Mouvement, player Scale Gameobject, wait ,then descale , reactivate mouvement, MegaCharge to False
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
        // Mouvement 
        _isSliding = true;
        var slideTimer = 0F;
        while (slideTimer < _slideDuration)
        {
            //smooth change of target during _SlideDuration Time  
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
        // Set animation to Is slidding down Invoke OnPlayerSlideDown bool true
        EventSystem.OnPlayerSlideDown?.Invoke(true);
        _isSlidingDown = true;
        _animator.SetBool("IsSlidingDown", true);
        
        // slide down during _slidedownDuration Time
        var slideTimer = 0f;

        while (slideTimer <= _slideDownDuration)
        {
            slideTimer += Time.deltaTime;
            yield return null;
        }
        // Stop SliddingDown animation and Invoke OnPlayerSlideDown bool false
        _isSlidingDown = false;
        _animator.SetBool("IsSlidingDown", false);
        EventSystem.OnPlayerSlideDown?.Invoke(false);
    }
}
