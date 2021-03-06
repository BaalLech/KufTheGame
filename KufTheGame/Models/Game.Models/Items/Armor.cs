﻿using KufTheGame.Models.Abstracts;
using KufTheGame.Models.Enums;
using KufTheGame.Models.Game.Models.Characters;
using KufTheGame.Properties;

namespace KufTheGame.Models.Game.Models.Items
{
    public class Armor : Item
    {
        public Armor(int x, int y, int width, int height, Rarities rarity, Armors armorType, double defencePoints)
            : base(x, y, width, height, rarity)
        {
            this.ArmorType = armorType;
            this.DefencePoints = defencePoints;
        }

        public Armors ArmorType { get; set; }

        public double DefencePoints { get; set; }

        public override void Use(Player target)
        {
            target.SetArmor(this);
        }

        public override string GetTexturePath()
        {
            switch (this.ArmorType)
            {
                case Armors.Boots: return Resources.Armor_BootsArmorTexture;
                case Armors.Chest: return Resources.Armor_ChestArmorTexture;
                case Armors.Gloves: return Resources.Armor_GlovesArmorTexture;
                case Armors.Helmet: return Resources.Armor_HelmetArmorTexture;
            }

            return null;
        }
    }
}