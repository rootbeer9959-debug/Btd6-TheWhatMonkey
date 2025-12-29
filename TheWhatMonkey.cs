using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Behaviors.Attack;
using Assets.Scripts.Models.Towers.Weapons;
using Assets.Scripts.Models.Towers.Projectiles;
using Assets.Scripts.Models.Towers.Behaviors;
using Assets.Scripts.Models.GenericBehaviors;
using Assets.Scripts.Unity;

namespace TemplateMod;

// ================= BASE TOWER =================
public class TheWhatMonkey : ModTower
{
    public override string BaseTower => "DartMonkey";
    public override string TowerSet => TowerSet.Primary;
    public override int Cost => 750;

    public override int TopPathUpgrades => 5;
    public override int MiddlePathUpgrades => 5;
    public override int BottomPathUpgrades => 5;

    public override string Description =>
        "Nobody knows how this tower works. The bloons fear it anyway.";

    public override void ModifyBaseTowerModel(TowerModel tower)
    {
        tower.range = 666;
        tower.GetAttackModel().range = 666;

        var weapon = tower.GetAttackModel().weapons[0];
        weapon.rate = 0.3f;

        var projectile = weapon.projectile;
        projectile.pierce = 13;
        projectile.GetDamageModel().damage = 2;

        // Random-ish targeting (Sniper style)
        tower.GetAttackModel().targetProvider =
            Game.instance.model.GetTowerFromId("SniperMonkey")
            .GetAttackModel().targetProvider.Duplicate();

        // Universal popping (meme)
        projectile.AddBehavior(new DamageModifierForTagModel(
            "LeadDamage", "Lead", 1, 999, false, false));

        projectile.AddBehavior(new DamageModifierForTagModel(
            "PurpleDamage", "Purple", 1, 999, false, false));
    }
}

//
// ================= TOP PATH =================
//

public class MoreWhat : ModUpgrade<TheWhatMonkey>
{
    public override int Path => TOP;
    public override int Tier => 1;
    public override int Cost => 450;

    public override void ApplyUpgrade(TowerModel tower)
    {
        tower.GetWeapon().projectile.pierce += 10;
    }
}

public class EvenMoreWhat : ModUpgrade<TheWhatMonkey>
{
    public override int Path => TOP;
    public override int Tier => 2;
    public override int Cost => 900;

    public override void ApplyUpgrade(TowerModel tower)
    {
        tower.GetWeapon().projectile.pierce += 50;
    }
}

public class WhyIsItExploding : ModUpgrade<TheWhatMonkey>
{
    public override int Path => TOP;
    public override int Tier => 3;
    public override int Cost => 2750;

    public override void ApplyUpgrade(TowerModel tower)
    {
        var explosion = Game.instance.model
            .GetTowerFromId("BombShooter-203")
            .GetWeapon().projectile
            .GetBehavior<CreateProjectileOnContactModel>()
            .Duplicate();

        tower.GetWeapon().projectile.AddBehavior(explosion);
    }
}

public class StopAskingQuestions : ModUpgrade<TheWhatMonkey>
{
    public override int Path => TOP;
    public override int Tier => 4;
    public override int Cost => 8500;

    public override void ApplyUpgrade(TowerModel tower)
    {
        tower.GetWeapon().rate *= 0.4f;
        tower.GetWeapon().projectile.GetDamageModel().damage += 5;
    }
}

public class TheAnswer : ModUpgrade<TheWhatMonkey>
{
    public override int Path => TOP;
    public override int Tier => 5;
    public override int Cost => 55000;

    public override void ApplyUpgrade(TowerModel tower)
    {
        var proj = tower.GetWeapon().projectile;
        proj.pierce = 500;
        proj.GetDamageModel().damage = 25;
    }
}

//
// ================= MIDDLE PATH =================
//

public class ButtonThatShouldntExist : ModUpgrade<TheWhatMonkey>
{
    public override int Path => MIDDLE;
    public override int Tier => 1;
    public override int Cost => 750;

    public override void ApplyUpgrade(TowerModel tower)
    {
        var ability = Game.instance.model
            .GetTowerFromId("BombShooter-040")
            .GetBehavior<AbilityModel>()
            .Duplicate();

        tower.AddBehavior(ability);
    }
}

public class PressToWin : ModUpgrade<TheWhatMonkey>
{
    public override int Path => MIDDLE;
    public override int Tier => 5;
    public override int Cost => 75000;

    public override void ApplyUpgrade(TowerModel tower)
    {
        foreach (var ability in tower.GetBehaviors<AbilityModel>())
        {
            ability.cooldown = 15;
        }
    }
}

//
// ================= BOTTOM PATH =================
//

public class HeCanSeeEverything : ModUpgrade<TheWhatMonkey>
{
    public override int Path => BOTTOM;
    public override int Tier => 1;
    public override int Cost => 350;

    public override void ApplyUpgrade(TowerModel tower)
    {
        tower.AddBehavior(new OverrideCamoDetectionModel("AllVision", true));
    }
}

public class GlobalMonkey : ModUpgrade<TheWhatMonkey>
{
    public override int Path => BOTTOM;
    public override int Tier => 4;
    public override int Cost => 18000;

    public override void ApplyUpgrade(TowerModel tower)
    {
        tower.range = 9999;
        tower.GetAttackModel().range = 9999;
    }
}

public class GameBalanceHasLeft : ModUpgrade<TheWhatMonkey>
{
    public override int Path => BOTTOM;
    public override int Tier => 5;
    public override int Cost => 65000;

    public override void ApplyUpgrade(TowerModel tower)
    {
        tower.GetWeapon().rate *= 0.1f;
        tower.GetWeapon().projectile.pierce = 9999;
        tower.GetWeapon().projectile.GetDamageModel().damage = 100;
    }
}
