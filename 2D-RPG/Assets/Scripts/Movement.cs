using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Toolbar_UI toolbarUI; // Toolbar_UI referansı
    public float speed;
    public Animator animator;

    private Vector3 direction;

private void Start()
{
    if (toolbarUI == null)
    {
        Debug.LogError("Toolbar UI is not assigned in Start!");
    }
}
    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        direction = new Vector3(horizontal, vertical).normalized;

        AnimateMovement(direction);
        
           if (Input.GetKeyDown(KeyCode.E))
        {
            SwingTool();
        }
    }

    private void FixedUpdate()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void AnimateMovement(Vector3 direction)
    {
        if (animator != null)
        {
            if (direction.magnitude > 0)
            {
                animator.SetBool("isMoving", true);

                animator.SetFloat("horizontal", direction.x);
                animator.SetFloat("vertical", direction.y);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }
        }
    }
private void SwingTool()
{
    if (toolbarUI == null)
    {
        Debug.LogError("Toolbar UI is not assigned!");
        return; // toolbarUI null ise metodu sonlandır
    }

    string itemName = toolbarUI.GetSelectedItemName();

    if (string.IsNullOrEmpty(itemName))
    {
        Debug.LogWarning("Item name is null or empty.");
        return; // itemName boşsa metodu sonlandır
    }

    if (animator == null)
    {
        Debug.LogError("Animator is not assigned!");
        return; // animator null ise metodu sonlandır
    }

    Debug.Log($"Swinging tool: {itemName}");

    animator.ResetTrigger("axe");
    animator.ResetTrigger("hammer");
    animator.ResetTrigger("hoe");
    animator.ResetTrigger("sword");
    animator.ResetTrigger("rod");

    animator.SetTrigger(itemName);
}
}
