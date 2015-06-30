using KufTheGame.Models.Abstracts;
using KufTheGame.Models.Enums;
using KufTheGame.Models.Game.Models.Characters;
using KufTheGame.Properties;

namespace KufTheGame.Models.Game.Models.Items
{
    public class Weapon: Item
    {
        public const string ImagePath = "";

        public Weapon(int x, int y, int width, int height, Rarities rarity,
            Weapons weaponType, double attackPoints) : base(x, y,width,height, rarity)
        {
            this.WeaponType = weaponType;
            this.AttackPoints = attackPoints;
        }

        public Weapons WeaponType { get; set; }

        public double AttackPoints { get; set; }

        public override void Use(Player target)
        {
            target.SetWeapon(this);
        }

        public override string GetTexturePath()
        {
            switch (WeaponType)
            {
                case Weapons.Axe: return Resources.Weapon_AxeWeaponTexture;
                case Weapons.Dagger: return Resources.Weapon_DaggerWeaponTexture;
                case Weapons.Mace: return Resources.Weapon_MaceWeaponTexture;
                case Weapons.Sword: return Resources.Weapon_SwordWeaponTexture;
            }

            return null;
        }
    }
}