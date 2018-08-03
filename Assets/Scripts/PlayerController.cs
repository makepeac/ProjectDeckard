using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float walkSpeed = 1;
	private bool _isGrounded = true;

	Animator animator;

	bool _isPlaying_walk = false;

	const int STATE_IDLE = 0;
	const int STATE_WALK = 1;

	string _currentDirection = "right";
	int _currentAnimationState = STATE_IDLE;

	// Use this for initialization
	void Start () {

		animator = this.GetComponent<Animator>();
		
	}
	
	// Update is called once per frame
	void Update () {

		changeState (STATE_IDLE);

		if (Input.GetKey("left"))
		{
			changeState (STATE_WALK);
		}
		else if (Input.GetKey("right"))
			{
				changeState (STATE_WALK);
			}
		
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("android_walk"))
			_isPlaying_walk = true;
		else
			_isPlaying_walk = false;
		
	}

	void changeState(int state){
		Debug.Log(state);
		if (_currentAnimationState == state){
		return;}

		switch (state) {
			case STATE_WALK:
				animator.SetInteger ("state", STATE_WALK);
				break;

			case STATE_IDLE:
				animator.SetInteger ("state", STATE_IDLE);
				break;
		}

		_currentAnimationState = state;
	}

	void changeDirection(string direction)
	{
		if (_currentDirection != direction)
		{
			if (direction == "right")
			{
				transform.Rotate (0, 180, 0);
				_currentDirection = "right";
			}
			else if (direction == "left")
				{
					transform.Rotate (0, -180, 0);
					_currentDirection = "left";
				}
			}
		}
	}

