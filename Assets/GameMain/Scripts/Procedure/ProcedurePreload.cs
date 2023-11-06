using GameFramework.Event;
using System;
using System.Collections.Generic;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace Suture
{
    /// <summary>
    /// 预加载流程，这一流程已经属于游戏业务层，但大部分游戏其实都需要这么一个流程，
    /// 所以这里也把他规划到通用流程中，流程中主要负责设置框架的功能模块，预加载数据表，
    /// 初始化游戏中的功能系统等。
    /// </summary>
    public class ProcedurePreload : ProcedureBase
    {
        public static readonly string[] DataTableNames = new string[]
        {
        "Scene",
        "UIForm"
        };

        /// <summary>
        /// 用来标记是否已经加载
        /// </summary>
        private Dictionary<string, bool> m_LoadedFlag = new Dictionary<string, bool>();

        public override bool UseNativeDialog
        {
            get
            {
                return true;
            }
        }

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);

            GameEntry.Event.Subscribe(LoadConfigSuccessEventArgs.EventId, OnLoadConfigSuccess);
            GameEntry.Event.Subscribe(LoadConfigFailureEventArgs.EventId, OnLoadConfigFailure);
            GameEntry.Event.Subscribe(LoadDataTableSuccessEventArgs.EventId, OnLoadDataTableSuccess);
            GameEntry.Event.Subscribe(LoadDataTableFailureEventArgs.EventId, OnLoadDataTableFailure);


            m_LoadedFlag.Clear();

            PreloadResources();
        }


        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            foreach (KeyValuePair<string, bool> loadedFlag in m_LoadedFlag)
            {
                if (!loadedFlag.Value)
                {
                    return;
                }
            }

            procedureOwner.SetData<VarInt32>("NextSceneId", GameEntry.Config.GetInt("Scene.Menu"));
            ChangeState<ProcedureChangeScene>(procedureOwner);
        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            GameEntry.Event.Unsubscribe(LoadConfigSuccessEventArgs.EventId, OnLoadConfigSuccess);
            GameEntry.Event.Unsubscribe(LoadConfigFailureEventArgs.EventId, OnLoadConfigFailure);
            GameEntry.Event.Unsubscribe(LoadDataTableSuccessEventArgs.EventId, OnLoadDataTableSuccess);
            GameEntry.Event.Unsubscribe(LoadDataTableFailureEventArgs.EventId, OnLoadDataTableFailure);
            base.OnLeave(procedureOwner, isShutdown);
        }

        /// <summary>
        /// 预加载资源
        /// </summary>
        private void PreloadResources()
        {
            //预加载配置
            LoadConfig("DefaultConfig");

            //预加载数据表
            foreach (string dataTableName in DataTableNames)
            {
                LoadDataTable(dataTableName);
            }


        }

        /// <summary>
        /// 加载数据表
        /// </summary>
        /// <param name="dataTableName"></param>
        private void LoadDataTable(string dataTableName)
        {
            string dataTableAssetName = AssetUtility.GetDataTableAsset(dataTableName, false);
            m_LoadedFlag.Add(dataTableAssetName, false);
            GameEntry.DataTable.LoadDataTable(dataTableName, dataTableAssetName, this);
        }

        private void LoadConfig(string configName)
        {
            string configAssetName = AssetUtility.GetConfigAsset(configName, false);
            m_LoadedFlag.Add(configAssetName, false);
            GameEntry.Config.ReadData(configAssetName, this);
        }

        private void OnLoadConfigSuccess(object sender, GameEventArgs e)
        {
            LoadConfigSuccessEventArgs args = (LoadConfigSuccessEventArgs)e;
            if (args.UserData != this)
                return;

            m_LoadedFlag[args.ConfigAssetName] = true;
            Log.Info("Load config '{0}' OK.", args.ConfigAssetName);
        }

        private void OnLoadConfigFailure(object sender, GameEventArgs e)
        {
            LoadConfigFailureEventArgs args = (LoadConfigFailureEventArgs)e;
            if (args.UserData != this)
                return;

            Log.Error("Can not load config '{0}' from '{1}' with error message '{2}'.", args.ConfigAssetName, args.ConfigAssetName, args.ErrorMessage);
        }

        private void OnLoadDataTableFailure(object sender, GameEventArgs e)
        {
            LoadDataTableFailureEventArgs ne = (LoadDataTableFailureEventArgs)e;
            if (ne.UserData != this)
                return;

            Log.Error("Can not load data table '{0}' from '{1}' with error message '{2}'.", ne.DataTableAssetName, ne.DataTableAssetName, ne.ErrorMessage);
        }

        private void OnLoadDataTableSuccess(object sender, GameEventArgs e)
        {
           LoadDataTableSuccessEventArgs ne = (LoadDataTableSuccessEventArgs)e;
            if (ne.UserData != this)
                return;

            m_LoadedFlag[ne.DataTableAssetName] = true;
            Log.Info("Load data table '{0}' OK.", ne.DataTableAssetName);
        }
    }
}