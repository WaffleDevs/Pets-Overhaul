using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using PetsOverhaul.Systems;

namespace PetsOverhaul.Systems
{
    public class FirstKillEoC : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !MasteryShardCheck.masteryShardObtained1;
        public bool CanShowItemDropInUI() => true;
        public string GetConditionDescription() => null;
    }
    public class FirstKillWoF : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !MasteryShardCheck.masteryShardObtained2;
        public bool CanShowItemDropInUI() => true;
        public string GetConditionDescription() => null;
    }
    public class FirstKillGolem : IItemDropRuleCondition, IProvideItemConditionDescription
    {
        public bool CanDrop(DropAttemptInfo info) => !MasteryShardCheck.masteryShardObtained3;
        public bool CanShowItemDropInUI() => true;
        public string GetConditionDescription() => null;
    }
}    
