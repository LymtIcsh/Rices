using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suture
{
    public abstract class GameBase
    {
        public GameMode GameMode
        {
            get;
        }

        /// <summary>
        /// 游戏是否结束
        /// </summary>
        public bool GameOver
        {
            get;
            protected set;
        }

        
    }
}
