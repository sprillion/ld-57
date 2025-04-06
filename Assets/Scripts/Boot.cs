using items;
using UnityEngine;
using upgrades;
using vfx;

public class Boot : MonoBehaviour
{
    private ItemService _itemService;
    private UpgradeService _upgradeService;
    private VfxService _vfxService;

    public static bool HaveControl = true;
    
    private void Awake()
    {
        Application.targetFrameRate = 60;

        _itemService = new ItemService();
        _upgradeService = new UpgradeService();
        _vfxService = new VfxService();
    }
}