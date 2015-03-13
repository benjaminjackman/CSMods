using ColossalFramework;
using ICities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

//namespace FooBar
//{
//	public class AutobulldozeMod : IUserMod
//	{
//		public static GameObject modObject;
//
//		public string Name { 
//			get { return "Automatic Bulldoze"; } 
//		} 
//		public string Description { 
//			get { return "Automatically destroys abandoned and burned buildings"; } 
//		}
//
//	}
//
//	public class ThreadingTestMod : ThreadingExtensionBase
//	{
//		public static AudioGroup nullAudioGroup;
//
//		public override void OnCreated(IThreading threading)
//		{
//			UnityEngine.Debug.LogWarning("Autobulldoze initialized.");
//			nullAudioGroup = new AudioGroup(0, new SavedFloat("NOTEXISTINGELEMENT", Settings.gameSettingsFile, 0, false));
//		}
//
//		public override void OnUpdate(float realTimeDelta, float simulationTimeDelta)
//		{
//
//
//
//		}
//
//		private int GetBuildingRefundAmount(ushort building)
//		{
//			BuildingManager instance = Singleton<BuildingManager>.instance;
//			if (Singleton<SimulationManager>.instance.IsRecentBuildIndex(instance.m_buildings.m_buffer[(int) building].m_buildIndex))
//				return instance.m_buildings.m_buffer[(int) building].Info.m_buildingAI.GetRefundAmount(building, ref instance.m_buildings.m_buffer[(int) building]);
//			else
//				return 0;
//		}
//
//
//		public static void DispatchAutobulldozeEffect(BuildingInfo info, Vector3 pos, float angle, int length)
//		{
//			EffectInfo effect = Singleton<BuildingManager>.instance.m_properties.m_bulldozeEffect;
//			if (effect == null) return;
//			InstanceID instance = new InstanceID();
//			EffectInfo.SpawnArea spawnArea = new EffectInfo.SpawnArea(Matrix4x4.TRS(Building.CalculateMeshPosition(info, pos, angle, length), Building.CalculateMeshRotation(angle), Vector3.one), info.m_lodMeshData);
//			Singleton<EffectManager>.instance.DispatchEffect(effect, instance, spawnArea, Vector3.zero, 0.0f, 1f, nullAudioGroup);
//		}
//
//
//		private void DeleteBuildingImpl(ushort building)
//		{
//			if (Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int) building].m_flags != Building.Flags.None)
//			{
//				BuildingManager instance = Singleton<BuildingManager>.instance;
//				BuildingInfo info = instance.m_buildings.m_buffer[(int) building].Info;
//				if (info.m_buildingAI.CheckBulldozing(building, ref instance.m_buildings.m_buffer[(int) building]) == ToolBase.ToolErrors.None)
//				{
//					if (info.m_placementStyle == ItemClass.Placement.Automatic)
//					{
//					}
//					int buildingRefundAmount = this.GetBuildingRefundAmount(building);
//					if (buildingRefundAmount != 0)
//						Singleton<EconomyManager>.instance.AddResource(EconomyManager.Resource.RefundAmount, buildingRefundAmount, info.m_class);
//					Vector3 pos = instance.m_buildings.m_buffer[(int) building].m_position;
//					float angle = instance.m_buildings.m_buffer[(int) building].m_angle;
//					int length = instance.m_buildings.m_buffer[(int) building].Length;
//					instance.ReleaseBuilding(building);
//					if (info.m_class.m_service > ItemClass.Service.Office)
//						Singleton<CoverageManager>.instance.CoverageUpdated(info.m_class.m_service, info.m_class.m_subService, info.m_class.m_level);
//					DispatchAutobulldozeEffect(info, pos, angle, length);
//				}
//			}
//		}
//
//
//
// 		public override void OnAfterSimulationTick()
//		{
//			SimulationManager simManager = Singleton<SimulationManager>.instance;
//			BuildingManager buildManager = Singleton<BuildingManager>.instance;
//
//			for (ushort i = (ushort)(simManager.m_currentTickIndex%1000); i < buildManager.m_buildings.m_buffer.Length; i+=1000)
//			{
//				Building build = buildManager.m_buildings.m_buffer[i];
//
//				if ((build.m_flags & (Building.Flags.BurnedDown | Building.Flags.Abandoned)) != Building.Flags.None)
//				{
//					UnityEngine.Debug.LogWarning("Autobulldozed #" + i);
//					DeleteBuildingImpl(i);
//					return;
//				}
//
//			}
//		}
//
//
//	}
//}
