using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ShapeView : View
{
    public GameObject block;
    private List<GameObject> blockList = new List<GameObject>();
    
    public void PlaceBlocks(List<Cell> blocks)
    {
        if (blockList.Count == 0) {
            blockList = CreateBlocks(blocks, block);
            return;
        }
        
        for (var i = 0; i < blockList.Count; i += 1) {
            var gmObj = blockList[i];
            gmObj.transform.position = GetObjectPos(blocks[i]);
        }
    }
}
