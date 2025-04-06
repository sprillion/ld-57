using items;
using level;
using sfx;
using UnityEngine;
using upgrades;
using vfx;

public class Boot : MonoBehaviour
{
    [SerializeField] private LevelService _levelService;
    private ItemService _itemService;
    private UpgradeService _upgradeService;
    private VfxService _vfxService;
    private SoundService _soundService;
    
    public static bool HaveControl = true;
    
    private void Awake()
    {
        Application.targetFrameRate = 60;

        _levelService.Initialize();
        _itemService = new ItemService();
        _upgradeService = new UpgradeService();
        _vfxService = new VfxService();
        _soundService = new SoundService();
    }
}