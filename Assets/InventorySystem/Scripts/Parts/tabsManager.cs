using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class TabsManager : MonoBehaviour
{
    [SerializeField] private TabController mapTabIndicator;
    [SerializeField] private TabController inventoryTabIndicator;
    [SerializeField] private TabController journalTabIndicator;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    [Button]
    void ScrollLeft()
    {
        AdjustTab(mapTabIndicator,-1);
        AdjustTab(inventoryTabIndicator,-1);
        AdjustTab(journalTabIndicator,-1);
    }

    [Button]
    void ScrollRight()
    {
        AdjustTab(mapTabIndicator,1);
        AdjustTab(inventoryTabIndicator,1);
        AdjustTab(journalTabIndicator,1);
    }

    private void AdjustTab(TabController controller, int ammount)
    {
        controller.targetIndex += ammount;
    }
}
