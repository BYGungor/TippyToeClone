using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;
using DG.Tweening;

public class GirlController : MonoBehaviour
{
    public enum FootOrder
    {
        RIGHTFOOT,
        LEFTFOOT,
    }

    private FootOrder _footOrder;
   [SerializeField] private FullBodyBipedIK _bipedIK;

    private Vector3 _targetPos;
    private Vector3 _velocity;

    private bool _mouseFree;
    private bool _mouseDownOnce;
    private bool _mouseDownStill;

    private Camera _camera;

  
    private void Awake()
    {

        _camera=Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _mouseDownOnce = true;
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            _mouseDownStill = true;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _mouseDownStill = false;
            _mouseFree = true;

        }

    }

    void FixedUpdate()
    {
        
        if (_mouseDownOnce)
        {
            _mouseFree = false;
            _mouseDownOnce = false;
            if (_footOrder==FootOrder.LEFTFOOT)
            {
                _targetPos = new Vector3(-0.1f, _bipedIK.solver.leftFootEffector.position.y + 0.55f, _bipedIK.solver.leftFootEffector.position.z + 1.4f);
                _footOrder = FootOrder.RIGHTFOOT;
            }
            else
            {
                _targetPos = new Vector3(0.1f, _bipedIK.solver.rightFootEffector.position.y + 0.55f, _bipedIK.solver.rightFootEffector.position.z + 1.4f);
                _footOrder = FootOrder.LEFTFOOT;
            }
            
            
        }
        if (_mouseDownStill)
        {
            
            if (_footOrder==FootOrder.LEFTFOOT)
            {
                _bipedIK.solver.leftFootEffector.position = Vector3.Lerp(_bipedIK.solver.leftFootEffector.position, _targetPos, 0.008f);
                
            }
            else
            {
                _bipedIK.solver.rightFootEffector.position = Vector3.Lerp(_bipedIK.solver.rightFootEffector.position, _targetPos, 0.008f);

            }

            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, Mathf.Abs(Mathf.Min(_bipedIK.solver.rightFootEffector.position.z, _bipedIK.solver.leftFootEffector.position.z) + (Mathf.Max(_bipedIK.solver.rightFootEffector.position.z, _bipedIK.solver.leftFootEffector.position.z) - Mathf.Min(_bipedIK.solver.rightFootEffector.position.z, _bipedIK.solver.leftFootEffector.position.z)) / 2)), 0.15f);
            _camera.transform.localPosition = Vector3.Lerp(_camera.transform.localPosition, new Vector3(_camera.transform.localPosition.x, _camera.transform.localPosition.y, Mathf.Abs(Mathf.Min(_bipedIK.solver.rightFootEffector.position.z, _bipedIK.solver.leftFootEffector.position.z) + (Mathf.Max(_bipedIK.solver.rightFootEffector.position.z, _bipedIK.solver.leftFootEffector.position.z) - Mathf.Min(_bipedIK.solver.rightFootEffector.position.z, _bipedIK.solver.leftFootEffector.position.z)) / 2) + 0.5f), 0.09f);

        }
       
        if (_mouseFree)
        {
            if (_footOrder == FootOrder.LEFTFOOT)
            {
                _bipedIK.solver.leftFootEffector.position = Vector3.Lerp(_bipedIK.solver.leftFootEffector.position, new Vector3(-0.1f, 0.1f, _bipedIK.solver.leftFootEffector.position.z), 0.2f);
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, Mathf.Abs(_bipedIK.solver.leftFootEffector.position.z + (_bipedIK.solver.rightFootEffector.position.z - _bipedIK.solver.leftFootEffector.position.z) / 2)), 0.1f);
            }
            else
            {
                _bipedIK.solver.rightFootEffector.position = Vector3.Lerp(_bipedIK.solver.rightFootEffector.position, new Vector3(0.1f, 0.1f, _bipedIK.solver.rightFootEffector.position.z), 0.2f);
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, Mathf.Abs(_bipedIK.solver.rightFootEffector.position.z + (_bipedIK.solver.leftFootEffector.position.z - _bipedIK.solver.rightFootEffector.position.z) / 2)), 0.1f);
            }
        }
        
    }
}
