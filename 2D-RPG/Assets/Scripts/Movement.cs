using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Toolbar_UI toolbarUI; // Toolbar_UI referansı
    public float speed;
    public Animator animator;

    private Vector3 direction;
    private Vector3Int characterPosition;
    private TileManager tileManager; // TileManager referansı

    private void Start()
    {
        if (toolbarUI == null)
        {
            Debug.LogError("Toolbar UI is not assigned in Start!");
        }
        tileManager = GameManager.instance.tileManager; // TileManager'ı al

        if (tileManager == null)
        {
            Debug.LogError("tileManager is not assigned!");
        }    
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        direction = new Vector3(horizontal, vertical).normalized;

        AnimateMovement(direction);

        // Mouse tıklama kontrolü
        if (Input.GetMouseButtonDown(0)) // Sol tık kontrolü
        {
            // Mouse pozisyonunu al ve dünya koordinatlarına çevir
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z * -1));
            Vector3Int gridPosition = new Vector3Int(Mathf.RoundToInt(mouseWorldPosition.x), Mathf.RoundToInt(mouseWorldPosition.y), 0);

            // Karakter pozisyonunu hesapla
            characterPosition = new Vector3Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0);

            // Mesafe kontrolü: sadece 3x3'lük alan içindeki tile'lar ile etkileşim
            if (gridPosition.x >= characterPosition.x - 1 && gridPosition.x <= characterPosition.x + 1 &&
                gridPosition.y >= characterPosition.y - 1 && gridPosition.y <= characterPosition.y + 1)
            {
                // Tile etkileşim kontrolü
                    if (tileManager.IsDiggable(gridPosition))
                {   
                    StartCoroutine(Dig(gridPosition));
                }
                    if (tileManager.IsSeed(gridPosition))
                {   
                    StartCoroutine(Seed(gridPosition));
                }

            }
            else
            {
                Debug.Log("Tile is out of range");
            }
            Debug.Log($"Character Position: {characterPosition}, Mouse Position: {gridPosition}");
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
        private IEnumerator Dig(Vector3Int gridPosition)
    {
        if (toolbarUI == null)
        {
            Debug.LogError("Toolbar UI is not assigned!");
            yield break; // toolbarUI null ise metodu sonlandır
        }

        string itemName = toolbarUI.GetSelectedItemName();

        if (string.IsNullOrEmpty(itemName))
        {
            Debug.LogWarning("Item name is null or empty.");
            yield break; // itemName boşsa metodu sonlandır
        }

        if (animator == null)
        {
            Debug.LogError("Animator is not assigned!");
            yield break; // animator null ise metodu sonlandır
        }

        Debug.Log($"Swinging tool: {itemName}");

        // Animasyon parametrelerini sıfırla
        animator.ResetTrigger("axe");
        animator.ResetTrigger("hammer");
        animator.ResetTrigger("hoe");
        animator.ResetTrigger("sword");
        animator.ResetTrigger("rod");
        animator.ResetTrigger("seed");
        
        

        // 0.2 saniye bekle
        yield return new WaitForSeconds(0.2f);

            if (itemName == "hoe") // Animasyon adı "hoe" ise
        {
            tileManager.SetDiggable(gridPosition);
            animator.SetTrigger(itemName);
        }

    }
    private IEnumerator Seed(Vector3Int gridPosition)
    {
        if (toolbarUI == null)
        {
            Debug.LogError("Toolbar UI is not assigned!");
            yield break; // toolbarUI null ise metodu sonlandır
        }

        string itemName = toolbarUI.GetSelectedItemName();

        if (string.IsNullOrEmpty(itemName))
        {
            Debug.LogWarning("Item name is null or empty.");
            yield break; // itemName boşsa metodu sonlandır
        }

        if (animator == null)
        {
            Debug.LogError("Animator is not assigned!");
            yield break; // animator null ise metodu sonlandır
        }

        Debug.Log($"Swinging tool: {itemName}");

        // Animasyon parametrelerini sıfırla
        animator.ResetTrigger("axe");
        animator.ResetTrigger("hammer");
        animator.ResetTrigger("hoe");
        animator.ResetTrigger("sword");
        animator.ResetTrigger("rod");
        animator.ResetTrigger("seed");
        
        

        // 0.2 saniye bekle
        yield return new WaitForSeconds(0.2f);

            if (itemName == "seed") // Animasyon adı "hoe" ise
        {
            tileManager.SetSeed(gridPosition);
            animator.SetTrigger(itemName);
        }
    }
}
