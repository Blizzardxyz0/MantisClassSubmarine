using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VehicleFramework;
using System.IO;
using System.Reflection;
using HarmonyLib;
using UnityEngine.U2D;
using VehicleFramework.VehicleParts;
using BepInEx;
using JetBrains.Annotations;
using HarmonyLib;




namespace PrecursorSub
{
    public class PrecursorSub : ModVehicle
    {
        public static GameObject model = null;

        
        

        public static void GetAssets()
        {
            // load the asset bundle
            string modPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var myLoadedAssetBundle = AssetBundle.LoadFromFile(Path.Combine(modPath, "Assets/precursorsub"));
            if (myLoadedAssetBundle == null)
            {
                return;
            }

            System.Object[] arr = myLoadedAssetBundle.LoadAllAssets();
            foreach (System.Object obj in arr)
            {
                if (obj.ToString().Contains("Vehicle"))
                {
                    model = (GameObject)obj;
                }
            }
        }

        public static Dictionary<TechType, int> GetRecipe()
        {
            Dictionary<TechType, int> recipe = new Dictionary<TechType, int>();
            recipe.Add(TechType.TitaniumIngot, 4);
            recipe.Add(TechType.PrecursorIonCrystal, 1);
            recipe.Add(TechType.PrecursorKey_Purple, 2);
            recipe.Add(TechType.AdvancedWiringKit, 3);
            
            
            return recipe;
        }
        public static IEnumerator Register()
        {
            GetAssets();
            ModVehicle PrecursorSub = model.EnsureComponent<PrecursorSub>() as ModVehicle;
            PrecursorSub.name = "MantisClass";
           
            yield return UWE.CoroutineHost.StartCoroutine(VehicleManager.RegisterVehicle(PrecursorSub, new PrecursorSubEngine(), GetRecipe(), (PingType)123, null, 2, 0, 2000, 2500, 10000));
        }
        
        
        public override string vehicleDefaultName
        {
            get
            {
                Language main = Language.main;
                if (!(main != null))
                {
                    return "Mantis";
                }
                return main.Get("Mantisdefaultname");
            }
        }
        public override string GetDescription()
        {
            return "A strange submarine with ankown qualities.";
        }

        public override string GetEncyEntry()
        {
            
            string ency = "The Mantis Clas Submarine Plans were picked up unexpectidly by your scanners";
            ency += "It is quite sturdy among submersibles, being constructed from an otherwise unkown material composition. \n";
            ency += "\nIt features:\n";

            ency += "- Great internal storage capacity\n";
            
            
            ency += "\nRatings:\n";
            ency += "- Top Speed (each axis): unkown \n";
            ency += "- Acceleration (each axis): unkown \n";
            ency += "- Distance per Power Cell: unkown \n";
            ency += "- Crush Depth: 2000 \n";
            ency += "- Upgrade Slots: 2 \n";
            ency += "- Dimensions: unkown \n";
            
            return ency;
        }

        public override GameObject VehicleModel
        {
            get
            {
                return model;
            }
        }


        public override GameObject StorageRootObject
        {
            get
            {
                return transform.Find("StorageRoot").gameObject;
            }
        }

        public override GameObject ModulesRootObject
        {
            get
            {
                return transform.Find("ModulesRoot").gameObject;
            }
        }

        /*public GameObject subRoot 
        {
            get
            {
                return transform.Find("SubRoot").gameObject;
            
            }
        
        
        }*/

        public override List<VehiclePilotSeat> PilotSeats
        {
            get
            {
                var list = new List<VehicleFramework.VehicleParts.VehiclePilotSeat>();
                VehicleFramework.VehicleParts.VehiclePilotSeat vps = new VehicleFramework.VehicleParts.VehiclePilotSeat();
                Transform mainSeat = transform.Find("PilotSeat");
                vps.Seat = mainSeat.gameObject;
                vps.SitLocation = mainSeat.Find("SitLocation").gameObject;
                vps.LeftHandLocation = mainSeat;
                vps.RightHandLocation = mainSeat;
                vps.ExitLocation = mainSeat.Find("ExitLocation");
                // TODO exit location
                list.Add(vps);
                return list;
            }
        }

        public override List<VehicleHatchStruct> Hatches
        {
            get
            {
                var list = new List<VehicleFramework.VehicleParts.VehicleHatchStruct>();

                VehicleFramework.VehicleParts.VehicleHatchStruct interior_vhs = new VehicleFramework.VehicleParts.VehicleHatchStruct();
                Transform intHatch = transform.Find("Hatches/Hatch/InsideHatch");
                interior_vhs.Hatch = intHatch.gameObject;
                interior_vhs.EntryLocation = intHatch.Find("Entry");
                interior_vhs.ExitLocation = intHatch.Find("Exit");
                interior_vhs.SurfaceExitLocation = intHatch.Find("SurfaceExit");

                VehicleFramework.VehicleParts.VehicleHatchStruct exterior_vhs = new VehicleFramework.VehicleParts.VehicleHatchStruct();
                Transform extHatch = transform.Find("Hatches/Hatch/OutsideHatch");
                exterior_vhs.Hatch = extHatch.gameObject;
                exterior_vhs.EntryLocation = interior_vhs.EntryLocation;
                exterior_vhs.ExitLocation = interior_vhs.ExitLocation;
                exterior_vhs.SurfaceExitLocation = interior_vhs.SurfaceExitLocation;

                list.Add(interior_vhs);
                list.Add(exterior_vhs);


                

                return list;
            }
        }
       

        public override List<VehicleStorage> InnateStorages
        {
            get
            {
                var list = new List<VehicleFramework.VehicleParts.VehicleStorage>();

                Transform innate1 = transform.Find("Storage/Locker1");
                Transform innate2 = transform.Find("Storage/Locker2");
                Transform innate3 = transform.Find("Storage/Locker3");
                Transform innate4 = transform.Find("Storage/Locker4");
                Transform innate5 = transform.Find("Storage/Locker5");


                VehicleFramework.VehicleParts.VehicleStorage IS1 = new VehicleFramework.VehicleParts.VehicleStorage();
                IS1.Container = innate1.gameObject;
                IS1.Height = 10;
                IS1.Width = 8;
                list.Add(IS1);
                VehicleFramework.VehicleParts.VehicleStorage IS2 = new VehicleFramework.VehicleParts.VehicleStorage();
                IS2.Container = innate2.gameObject;
                IS2.Height = 10;
                IS2.Width = 8;
                list.Add(IS2);
                VehicleFramework.VehicleParts.VehicleStorage IS3 = new VehicleFramework.VehicleParts.VehicleStorage();
                IS3.Container = innate3.gameObject;
                IS3.Height = 10;
                IS3.Width = 8;
                list.Add(IS3);
                VehicleFramework.VehicleParts.VehicleStorage IS4 = new VehicleFramework.VehicleParts.VehicleStorage();
                IS4.Container = innate4.gameObject;
                IS4.Height = 10;
                IS4.Width = 8;
                list.Add(IS4);
                VehicleFramework.VehicleParts.VehicleStorage IS5 = new VehicleFramework.VehicleParts.VehicleStorage();
                IS5.Container = innate5.gameObject;
                IS5.Height = 10;
                IS5.Width = 8;
                list.Add(IS5);

                return list;
            }
        }

        public override List<VehicleStorage> ModularStorages
        {
            get
            {
                var list = new List<VehicleFramework.VehicleParts.VehicleStorage>();
                return list;
            }
        }

        

        public override List<VehicleBattery> Batteries
        {
            get
            {
                var list = new List<VehicleFramework.VehicleParts.VehicleBattery>();

                VehicleFramework.VehicleParts.VehicleBattery vb1 = new VehicleFramework.VehicleParts.VehicleBattery();
                vb1.BatterySlot = transform.Find("Batteries/Battery1").gameObject;
                vb1.BatteryProxy = null;
                list.Add(vb1);

                VehicleFramework.VehicleParts.VehicleBattery vb2 = new VehicleFramework.VehicleParts.VehicleBattery();
                vb2.BatterySlot = transform.Find("Batteries/Battery2").gameObject;
                vb2.BatteryProxy = null;
                list.Add(vb2);

                VehicleFramework.VehicleParts.VehicleBattery vb3 = new VehicleFramework.VehicleParts.VehicleBattery();
                vb3.BatterySlot = transform.Find("Batteries/Battery3").gameObject;
                vb3.BatteryProxy = null;
                list.Add(vb3);

                VehicleFramework.VehicleParts.VehicleBattery vb4 = new VehicleFramework.VehicleParts.VehicleBattery();
                vb4.BatterySlot = transform.Find("Batteries/Battery4").gameObject;
                vb4.BatteryProxy = null;
                list.Add(vb4);

                return list;
            }
        }

        public override List<VehicleBattery> BackupBatteries
        {
            get
            {
                var list = new List<VehicleFramework.VehicleParts.VehicleBattery>();
                return null;
            }
        }

        public override List<VehicleFloodLight> HeadLights
        {
            get
            {
                var list = new List<VehicleFramework.VehicleParts.VehicleFloodLight>();

                list.Add(new VehicleFramework.VehicleParts.VehicleFloodLight
                {
                    Light = transform.Find("lights_parent/headlights/headlight1").gameObject,
                    Angle = 70,
                    Color = Color.green,
                    Intensity = 1.3f,
                    Range = 90f
                });
                list.Add(new VehicleFramework.VehicleParts.VehicleFloodLight
                {
                    Light = transform.Find("lights_parent/headlights/headlight2").gameObject,
                    Angle = 70,
                    Color = Color.green,
                    Intensity = 1.3f,
                    Range = 90f
                });
                

                return list;
            }
        }
        public override GameObject SteeringWheelLeftHandTarget
        {
            get
            {
                return transform.Find("SteeringWheelLeftHandTarget").gameObject;
                
            }
        }
        public override GameObject SteeringWheelRightHandTarget
        {
            get
            {
                return transform.Find("SteeringWheelRightHandTarget").gameObject;
               
            }
        }

        public override void PlayerEntry()
        {
            base.PlayerEntry();
            


        }

        public override void BeginPiloting()
        {
            base.EnterVehicle(Player.main, true);
            Player.main.EnterSittingMode();
            StartCoroutine(SitDownInChair());
            StartCoroutine(TryStandUpFromChair());
            isPilotSeated = true;
            uGUI.main.quickSlots.SetTarget(this);
            Player.main.armsController.ikToggleTime = 0;
            Player.main.armsController.SetWorldIKTarget(SteeringWheelLeftHandTarget.transform, SteeringWheelRightHandTarget.transform);
            
            NotifyStatus(PlayerStatus.OnPilotBegin);
        }
        public override void StopPiloting()
        {
            // this function
            // called by Player.ExitLockedMode()
            // which is triggered on button press
            StartCoroutine(StandUpFromChair());
            isPilotSeated = false;
            Player.main.transform.SetParent(transform);
            if (thisStopPilotingLocation == null)
            {
                
                Player.main.transform.position = TetherSources[0].transform.position;
            }
            else
            {
                Player.main.transform.position = thisStopPilotingLocation.position;
            }
            Player.main.SetScubaMaskActive(false);
            uGUI.main.quickSlots.SetTarget(null);
            Player.main.armsController.ikToggleTime = 0.5f;
            Player.main.armsController.SetWorldIKTarget(null, null);
            NotifyStatus(PlayerStatus.OnPilotEnd);
        }
        public override List<VehicleFloodLight> FloodLights
        {
            get
            {
               
                

                return null;
            }
        }

        public override List<GameObject> NavigationPortLights
        {
            get
            {
                return null;
            }
        }

        public override List<GameObject> NavigationStarboardLights
        {
            get
            {
                return null;
            }
        }

        public override List<GameObject> NavigationPositionLights
        {
            get
            {
                return null;
            }
        }

        public override List<GameObject> NavigationWhiteStrobeLights
        {
            get
            {
                return null;
            }
        }

        public override List<GameObject> NavigationRedStrobeLights
        {
            get
            {
                return null;
            }
        }

        public override List<GameObject> WaterClipProxies
        {
            get
            {
                var list = new List<GameObject>();
                foreach (Transform child in transform.Find("WaterClipProxies"))
                {
                    list.Add(child.gameObject);
                }
                return list;
            }
        }

        public override List<GameObject> CanopyWindows
        {
            get
            {
                //var list = new List<GameObject>();
                //list.Add(transform.Find("Model/PrecursorSub/Hull.001/CanopyOutside").gameObject);
                //return list;
                return null;
            }
        }
     

        public override List<GameObject> TetherSources
        {
            get
            {
                var list = new List<GameObject>();
                foreach (Transform child in transform.Find("TetherSources"))
                {
                    list.Add(child.gameObject);
                }
                return list;
            }
        }
        public override GameObject ColorPicker
        {
            get
            {
                return null;
            }
        }
        public override GameObject Fabricator
        {
            get
            {
                return transform.Find("Fabricator-Location").gameObject;
            }
        }

        public override GameObject BoundingBox
        {
            get
            {
                return transform.Find("BoundingBox").gameObject;
            }
        }

        public override GameObject ControlPanel
        {
            get
            {
                return null;
            }
        }

        public override GameObject CollisionModel
        {
            get
            {
                return transform.Find("Collider").gameObject;
                
            }
        }



        public override List<VehicleUpgrades> Upgrades
        {
            get
            {
                var list = new List<VehicleFramework.VehicleParts.VehicleUpgrades>();
                VehicleFramework.VehicleParts.VehicleUpgrades vu = new VehicleFramework.VehicleParts.VehicleUpgrades();
                vu.Interface = transform.Find("UpgradesInterface").gameObject;
                vu.Flap = vu.Interface;
                vu.AnglesClosed = Vector3.zero;
                vu.AnglesOpened = new Vector3(0,0,0);

                vu.ModuleProxies = null;

                list.Add(vu);
                return list;
            }
        }



        public override void Awake()
        {
            
            OGVehicleName = "MNT-" + Mathf.RoundToInt(UnityEngine.Random.value * 10000).ToString();
            vehicleName = OGVehicleName;
            NowVehicleName = OGVehicleName;

            // ModVehicle.Awake
            base.Awake();
        }
        public override void Start()
        {
            base.Start();
            



        }

        public override void Update()
        {
            base.Update();

            

            
        }
        public void maybeboost()
        {
            /*if (GameInput.GetButtonDown(GameInput.Button.LeftHand)==true)
            {
                this.boost();


            }*/
            }

        public void boost() 
        {
            //this.PrecursorSubEngine.Boost(1000);
            
        
        }
        public SubRoot subRoot;
    }

    



}


    
        


       
        
    
;