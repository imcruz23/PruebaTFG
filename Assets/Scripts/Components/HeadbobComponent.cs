using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadbobComponent : MonoBehaviour
{
    [SerializeField] private bool enable_ = true;

    [SerializeField, Range(0, 0.1f)] private float amplitude_ = 0.015f; // Tamaño del paso
    [SerializeField, Range(0, 30)] private float frequency_ = 10.0f; // Velocidad del paso

    [SerializeField] private Transform camera_;
    [SerializeField] private Transform cameraHolder_;

    private float toggleSpeed_ = 3.0f;
    private Vector3 startPos_;
    private CharacterController controller_;

    private void Awake()
    {
        TryGetComponent(out controller_);
        startPos_ = camera_.localPosition;
    }

    private Vector3 FootStepMotion()
    {
        Vector3 pos = Vector3.zero;

        pos.y += Mathf.Sin(Time.time * frequency_) * amplitude_;
        pos.x += Mathf.Cos(Time.time * frequency_ / 2) * amplitude_ * 2;

        return pos;
    }

    private void CheckMotion()
    {
        float speed = new Vector3(controller_.velocity.x, 0, controller_.velocity.z).magnitude;

        if (speed < toggleSpeed_) return;
        if (!controller_.isGrounded) return;

        PlayMotion(FootStepMotion());
        ResetPosition();
    }

    private void ResetPosition()
    {
        if (camera_.localPosition == startPos_) return;
        camera_.localPosition = Vector3.Lerp(camera_.localPosition, startPos_, 1 * Time.deltaTime);
    }

    private Vector3 FocusTarget()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y * cameraHolder_.localPosition.y, transform.position.z);
        pos += cameraHolder_.forward * 15.0f;
        return pos;
    }

    private void PlayMotion(Vector3 motion)
    {
        camera_.localPosition += motion;
    }

    // Update is called once per frame
    void Update()
    {
        if (!enable_) return;

        CheckMotion();
        camera_.LookAt(FocusTarget());
    }
}
