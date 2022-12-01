using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] Movement _movement;
    [SerializeField] Combat _combat;
    [SerializeField] EntityBase _entity;
    [SerializeField] Inventory _inventory;
    public bool enableControls;
    HUDActivator HUDActivator;
    Vector2 dir;
    //Quaternion GetMouseRot()
    //{
    //    var mouse_pos = Input.mousePosition;
    //    var object_pos = Camera.main.WorldToScreenPoint(transform.position);
    //    mouse_pos.x = mouse_pos.x - object_pos.x;
    //    mouse_pos.y = mouse_pos.y - object_pos.y;
    //    var angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
    //    return Quaternion.Euler(new Vector3(0, 0, angle));
    //}
    private void Start()
    {
        HUDActivator = FindObjectOfType<HUDActivator>();
    }
    Vector2 GetMouseDirection(Vector3 from)
    {
        var mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return (mouse_pos - from).normalized;
    }
    void Update()
    {
        if (_entity.Dead) return;
        dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        DetectInput();
    }
    private void DetectInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _inventory.ActivateArtifact();
        }
        if (Input.GetButton("Fire1") && _inventory.GetWeapon().Auto)
        {
            _combat.StartAttack(GetMouseDirection(_combat.RangedAttackSpawn.position), _inventory.GetWeapon());
        }
        else if(Input.GetButtonDown("Fire1"))
        {
            _combat.StartAttack(GetMouseDirection(_combat.RangedAttackSpawn.position), _inventory.GetWeapon());
        }
        //else if (Input.GetButton("Fire2") && _inventory.GetWeapon().Auto)
        //{
        //    _combat.StartAltAttack(GetMouseDirection(_combat.RangedAttackSpawn.position), _inventory.GetWeapon());
        //}
        //else if (Input.GetButtonDown("Fire2"))
        //{
        //    _combat.StartAltAttack(GetMouseDirection(_combat.RangedAttackSpawn.position), _inventory.GetWeapon());
        //}
    }
    private void FixedUpdate()
    {
        _movement.Move(dir);
    }
    public EntityBase TargetSelect()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        EntityBase[] foundEnts = FindObjectsOfType<EntityBase>();
        EntityBase nearestEnt = null;
        for (int i = 0; i < foundEnts.Length; i++)
        {
            if (foundEnts[i] != _entity)
            {
                if (!nearestEnt)
                {
                    nearestEnt = foundEnts[i];
                }
                float nearest = Vector3.Distance(nearestEnt.transform.position, mousePos);
                float dist = Vector3.Distance(foundEnts[i].transform.position, mousePos);
                if(dist < nearest)
                {
                    nearestEnt = foundEnts[i];
                }
            }
        }
        return nearestEnt;
    }
}