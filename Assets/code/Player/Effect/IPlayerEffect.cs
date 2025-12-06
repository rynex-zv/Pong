using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public abstract class PlayerEffect
    {

        private PlayerObject _player;

        public PlayerEffect(PlayerObject player)
        {
            this._player = player;
        }

        public abstract float? onMove(float directionY, float yNew);
        public abstract void onEffectNew();
        public abstract void onEffectRemove();
        public abstract void onScoreUpdate();
    }