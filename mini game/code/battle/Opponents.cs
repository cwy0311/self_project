using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace esports
{
    public class Opponents
    {
        public SimpleBattleStatus thirdSeed;
        public SimpleBattleStatus secondSeed;
        public SimpleBattleStatus firstSeed;
        public int thirdSeedPlayerPrefabId;
        public int secondSeedPlayerPrefabId;
        public int firstSeedPlayerPrefabId;

        public Opponents(SimpleBattleStatus thirdSeed, SimpleBattleStatus secondSeed, SimpleBattleStatus firstSeed, int thirdSeedPlayerPrefabId,int secondSeedPlayerPrefabId, int firstSeedPlayerPrefabId)
        {
            this.thirdSeed = thirdSeed;
            this.secondSeed = secondSeed;
            this.firstSeed = firstSeed;
            this.thirdSeedPlayerPrefabId = thirdSeedPlayerPrefabId;
            this.secondSeedPlayerPrefabId = secondSeedPlayerPrefabId;
            this.firstSeedPlayerPrefabId = firstSeedPlayerPrefabId;
        }

        public Opponents DeepClone()
        {
            return new Opponents(thirdSeed.DeepClone(), secondSeed.DeepClone(), firstSeed.DeepClone(), thirdSeedPlayerPrefabId, secondSeedPlayerPrefabId, firstSeedPlayerPrefabId);
        }
    }
}