using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Suture
{
    //StructLayout特性允许我们控制Structure语句块的元素在内存中的排列方式
    //,以及当这些元素被传递给外部DLL时，运行库排列这些元素的方式
    [StructLayout(LayoutKind.Auto)]
    public struct ImpactData
    {
        // readonly 在字段声明中，readonly 指示只能在声明期间或在同一个类的构造函数中向字段赋值
        // 。 可以在字段声明和构造函数中多次分配和重新分配只读字段。
        private readonly CampType m_Camp;
        // private readonly int m_HP;
        //
        //
        // private readonly int m_Attack;
        //
        // private readonly int m_Defense;
        
        public ImpactData(CampType camp/*, int hp, int attack, int defense*/)
        {
            m_Camp = camp;
            // m_HP = hp;
            // m_Attack = attack;
            // m_Defense = defense;
        }

        public CampType Camp
        {
            get
            {
                return m_Camp;
            }
        }

        // public int HP
        // {
        //     get
        //     {
        //         return m_HP;
        //     }
        // }
        //
        // /// <summary>
        // /// 攻击
        // /// </summary>
        // public int Attack
        // {
        //     get
        //     {
        //         return m_Attack;
        //     }
        // }
        // /// <summary>
        // /// 防御
        // /// </summary>
        // public int Defense
        // {
        //     get
        //     {
        //         return m_Defense;
        //     }
        // }
    }

}
