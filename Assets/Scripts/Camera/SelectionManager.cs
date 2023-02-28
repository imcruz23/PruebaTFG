using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ESTO HAY QUE REFACTORIZARLO
public class SelectionManager : MonoBehaviour
{
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private PlayerComponent player;

    private Transform selection_; // Trackeo de la seleccion
    // Update is called once per frame
    void Update()
    {
        if (selection_ != null) // Si existe algo que esté en el raycast
        {
            var selectionRenderer = selection_.GetComponent<Renderer>();

            if (selectionRenderer != null)
            {
                selectionRenderer.material = defaultMaterial;
            }
        }

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f)); // El rayo.Apunta desde el mouse
        RaycastHit hit; // El hit del raycast

        if (Physics.Raycast(ray, out hit, (int) player.playerRange))
        {
            var selection = hit.transform;
            var selectionRenderer = selection.GetComponent<Renderer>();

            if(selectionRenderer != null && selection.gameObject.layer == (int) Layers.Interactable)
            {
                selectionRenderer.material = highlightMaterial;
                selection_ = selection; // Seleccion local
            }
        }
    }
}
