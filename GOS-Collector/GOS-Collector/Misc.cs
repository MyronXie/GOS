using GTA;

namespace GOSColl
{
    public class Misc
    {
        public static readonly string[] MENU_MSG = {
            "", //HOME
            "[1] Start/End\n" + "[2] Pause/Resume\n" + "[9] Read Place\n",  //Capture
            "[1] Single\n" + "[2] Auto Flag\n" + "[3] Auto Traverse\n" + "[7] Teleport\n" + "[8] Read Place\n" + "[9] Clear Place\n", //Record
            "[1] Change Time\n" + "[2] Change Weather\n" + "[3] Player invisible\n" + "[4] Change Camera\n" + "[5] Change Place\n", //World
        };

        public static readonly float[,] CAMERA_LIST = {
            // {Viewpoint, Distance, Height}
            {0, 0, 0},          // Default camera
            {0, 10, 0},
            {0, 10, 5},
            {-90, 10, 0},
        };

        public static Model[] MODEL_NEED_REMOVED = {
            //Streetlight
            new Model((int)EntityHash.prop_streetlight_01),
            new Model((int)EntityHash.prop_streetlight_01b),
            new Model((int)EntityHash.prop_streetlight_02),
            new Model((int)EntityHash.prop_streetlight_03),
            new Model((int)EntityHash.prop_streetlight_03b),
            new Model((int)EntityHash.prop_streetlight_03c),
            new Model((int)EntityHash.prop_streetlight_03d),
            new Model((int)EntityHash.prop_streetlight_03e),
            new Model((int)EntityHash.prop_streetlight_04),
            new Model((int)EntityHash.prop_streetlight_05),
            new Model((int)EntityHash.prop_streetlight_05),
            new Model((int)EntityHash.prop_streetlight_06),
            new Model((int)EntityHash.prop_streetlight_07a),
            new Model((int)EntityHash.prop_streetlight_07b),
            new Model((int)EntityHash.prop_streetlight_08),
            new Model((int)EntityHash.prop_streetlight_09),
            new Model((int)EntityHash.prop_streetlight_10),
            new Model((int)EntityHash.prop_streetlight_11a),
            new Model((int)EntityHash.prop_streetlight_11b),
            new Model((int)EntityHash.prop_streetlight_11c),
            new Model((int)EntityHash.prop_streetlight_12a),
            new Model((int)EntityHash.prop_streetlight_12b),
            new Model((int)EntityHash.prop_streetlight_14a),
            new Model((int)EntityHash.prop_streetlight_15a),
            new Model((int)EntityHash.prop_streetlight_16a),
            new Model((int)EntityHash.prop_oldlight_01a),
            new Model((int)EntityHash.prop_oldlight_01b),
            new Model((int)EntityHash.prop_oldlight_01c),
            //Traffic light
            new Model((int)EntityHash.prop_traffic_01a),
            new Model((int)EntityHash.prop_traffic_01b),
            new Model((int)EntityHash.prop_traffic_01d),
            new Model((int)EntityHash.prop_traffic_02a),
            new Model((int)EntityHash.prop_traffic_02b),
            new Model((int)EntityHash.prop_traffic_03a),
            new Model((int)EntityHash.prop_traffic_03b),
            new Model((int)EntityHash.prop_traffic_lightset_01),
            //bollard
            new Model((int)EntityHash.prop_bollard_01a),
            new Model((int)EntityHash.prop_bollard_01b),
            new Model((int)EntityHash.prop_bollard_01c),
            new Model((int)EntityHash.prop_bollard_02a),
            new Model((int)EntityHash.prop_bollard_02b),
            new Model((int)EntityHash.prop_bollard_02c),
            new Model((int)EntityHash.prop_bollard_03a),
            new Model((int)EntityHash.prop_bollard_04),
            new Model((int)EntityHash.prop_bollard_05),
            //Road sign
            new Model((int)EntityHash.prop_sign_road_01a),
            new Model((int)EntityHash.prop_sign_road_01b),
            new Model((int)EntityHash.prop_sign_road_01c),
            new Model((int)EntityHash.prop_sign_road_02a),
            new Model((int)EntityHash.prop_sign_road_03a),
            new Model((int)EntityHash.prop_sign_road_03b),
            new Model((int)EntityHash.prop_sign_road_03c),
            new Model((int)EntityHash.prop_sign_road_03d),
            new Model((int)EntityHash.prop_sign_road_03e),
            new Model((int)EntityHash.prop_sign_road_03f),
            new Model((int)EntityHash.prop_sign_road_03g),
            new Model((int)EntityHash.prop_sign_road_03h),
            new Model((int)EntityHash.prop_sign_road_03i),
            new Model((int)EntityHash.prop_sign_road_03j),
            new Model((int)EntityHash.prop_sign_road_03k),
            new Model((int)EntityHash.prop_sign_road_03l),
            new Model((int)EntityHash.prop_sign_road_03m),
            new Model((int)EntityHash.prop_sign_road_03n),
            new Model((int)EntityHash.prop_sign_road_03o),
            new Model((int)EntityHash.prop_sign_road_03p),
            new Model((int)EntityHash.prop_sign_road_03q),
            new Model((int)EntityHash.prop_sign_road_03r),
            new Model((int)EntityHash.prop_sign_road_03s),
            new Model((int)EntityHash.prop_sign_road_03t),
            new Model((int)EntityHash.prop_sign_road_03u),
            new Model((int)EntityHash.prop_sign_road_03v),
            new Model((int)EntityHash.prop_sign_road_03w),
            new Model((int)EntityHash.prop_sign_road_03x),
            new Model((int)EntityHash.prop_sign_road_03y),
            new Model((int)EntityHash.prop_sign_road_03z),
            new Model((int)EntityHash.prop_sign_road_04a),
            new Model((int)EntityHash.prop_sign_road_04b),
            new Model((int)EntityHash.prop_sign_road_04c),
            new Model((int)EntityHash.prop_sign_road_04d),
            new Model((int)EntityHash.prop_sign_road_04e),
            new Model((int)EntityHash.prop_sign_road_04f),
            new Model((int)EntityHash.prop_sign_road_04g),
            new Model((int)EntityHash.prop_sign_road_04g_l1),
            new Model((int)EntityHash.prop_sign_road_04h),
            new Model((int)EntityHash.prop_sign_road_04i),
            new Model((int)EntityHash.prop_sign_road_04j),
            new Model((int)EntityHash.prop_sign_road_04k),
            new Model((int)EntityHash.prop_sign_road_04l),
            new Model((int)EntityHash.prop_sign_road_04m),
            new Model((int)EntityHash.prop_sign_road_04n),
            new Model((int)EntityHash.prop_sign_road_04o),
            new Model((int)EntityHash.prop_sign_road_04p),
            new Model((int)EntityHash.prop_sign_road_04q),
            new Model((int)EntityHash.prop_sign_road_04r),
            new Model((int)EntityHash.prop_sign_road_04s),
            new Model((int)EntityHash.prop_sign_road_04t),
            new Model((int)EntityHash.prop_sign_road_04u),
            new Model((int)EntityHash.prop_sign_road_04v),
            new Model((int)EntityHash.prop_sign_road_04w),
            new Model((int)EntityHash.prop_sign_road_04x),
            new Model((int)EntityHash.prop_sign_road_04y),
            new Model((int)EntityHash.prop_sign_road_04z),
            new Model((int)EntityHash.prop_sign_road_04za),
            new Model((int)EntityHash.prop_sign_road_04zb),
            new Model((int)EntityHash.prop_sign_road_05a),
            new Model((int)EntityHash.prop_sign_road_05b),
            new Model((int)EntityHash.prop_sign_road_05c),
            new Model((int)EntityHash.prop_sign_road_05d),
            new Model((int)EntityHash.prop_sign_road_05e),
            new Model((int)EntityHash.prop_sign_road_05f),
            new Model((int)EntityHash.prop_sign_road_05g),
            new Model((int)EntityHash.prop_sign_road_05h),
            new Model((int)EntityHash.prop_sign_road_05i),
            new Model((int)EntityHash.prop_sign_road_05j),
            new Model((int)EntityHash.prop_sign_road_05k),
            new Model((int)EntityHash.prop_sign_road_05l),
            new Model((int)EntityHash.prop_sign_road_05m),
            new Model((int)EntityHash.prop_sign_road_05n),
            new Model((int)EntityHash.prop_sign_road_05o),
            new Model((int)EntityHash.prop_sign_road_05p),
            new Model((int)EntityHash.prop_sign_road_05q),
            new Model((int)EntityHash.prop_sign_road_05r),
            new Model((int)EntityHash.prop_sign_road_05s),
            new Model((int)EntityHash.prop_sign_road_05t),
            new Model((int)EntityHash.prop_sign_road_05u),
            new Model((int)EntityHash.prop_sign_road_05v),
            new Model((int)EntityHash.prop_sign_road_05w),
            new Model((int)EntityHash.prop_sign_road_05x),
            new Model((int)EntityHash.prop_sign_road_05y),
            new Model((int)EntityHash.prop_sign_road_05z),
            new Model((int)EntityHash.prop_sign_road_05za),
            new Model((int)EntityHash.prop_sign_road_06a),
            new Model((int)EntityHash.prop_sign_road_06b),
            new Model((int)EntityHash.prop_sign_road_06c),
            new Model((int)EntityHash.prop_sign_road_06d),
            new Model((int)EntityHash.prop_sign_road_06e),
            new Model((int)EntityHash.prop_sign_road_06f),
            new Model((int)EntityHash.prop_sign_road_06g),
            new Model((int)EntityHash.prop_sign_road_06h),
            new Model((int)EntityHash.prop_sign_road_06i),
            new Model((int)EntityHash.prop_sign_road_06j),
            new Model((int)EntityHash.prop_sign_road_06k),
            new Model((int)EntityHash.prop_sign_road_06l),
            new Model((int)EntityHash.prop_sign_road_06m),
            new Model((int)EntityHash.prop_sign_road_06n),
            new Model((int)EntityHash.prop_sign_road_06o),
            new Model((int)EntityHash.prop_sign_road_06p),
            new Model((int)EntityHash.prop_sign_road_06q),
            new Model((int)EntityHash.prop_sign_road_06r),
            new Model((int)EntityHash.prop_sign_road_06s),
            new Model((int)EntityHash.prop_sign_road_07a),
            new Model((int)EntityHash.prop_sign_road_07b),
            new Model((int)EntityHash.prop_sign_road_08a),
            new Model((int)EntityHash.prop_sign_road_08b),
            new Model((int)EntityHash.prop_sign_road_09a),
            new Model((int)EntityHash.prop_sign_road_09b),
            new Model((int)EntityHash.prop_sign_road_09c),
            new Model((int)EntityHash.prop_sign_road_09d),
            new Model((int)EntityHash.prop_sign_road_09e),
            new Model((int)EntityHash.prop_sign_road_09f),
            //electric box
            new Model((int)EntityHash.prop_elecbox_01a),
            new Model((int)EntityHash.prop_elecbox_01b),
            new Model((int)EntityHash.prop_elecbox_02a),
            new Model((int)EntityHash.prop_elecbox_02b),
            new Model((int)EntityHash.prop_elecbox_03a),
            new Model((int)EntityHash.prop_elecbox_04a),
            new Model((int)EntityHash.prop_elecbox_05a),
            new Model((int)EntityHash.prop_elecbox_06a),
            new Model((int)EntityHash.prop_elecbox_07a),
            new Model((int)EntityHash.prop_elecbox_08),
            new Model((int)EntityHash.prop_elecbox_08b),
            new Model((int)EntityHash.prop_elecbox_09),
            new Model((int)EntityHash.prop_elecbox_10),
            new Model((int)EntityHash.prop_elecbox_10_cr),
            new Model((int)EntityHash.prop_elecbox_11),
            new Model((int)EntityHash.prop_elecbox_12),
            new Model((int)EntityHash.prop_elecbox_13),
            new Model((int)EntityHash.prop_elecbox_14),
            new Model((int)EntityHash.prop_elecbox_15),
            new Model((int)EntityHash.prop_elecbox_15_cr),
            new Model((int)EntityHash.prop_elecbox_16),
            new Model((int)EntityHash.prop_elecbox_17),
            new Model((int)EntityHash.prop_elecbox_17_cr),
            new Model((int)EntityHash.prop_elecbox_18),
            new Model((int)EntityHash.prop_elecbox_19),
            new Model((int)EntityHash.prop_elecbox_20),
            new Model((int)EntityHash.prop_elecbox_21),
            new Model((int)EntityHash.prop_elecbox_22),
            new Model((int)EntityHash.prop_elecbox_23),
            new Model((int)EntityHash.prop_elecbox_24),
            new Model((int)EntityHash.prop_elecbox_24b),
            new Model((int)EntityHash.prop_elecbox_25),
            //Plant
            new Model((int)EntityHash.prop_plant_palm_01a),
            new Model((int)EntityHash.prop_plant_palm_01b),
            new Model((int)EntityHash.prop_plant_palm_01c),

        };

        public static readonly string[] SCENARIOS_NORMAL_VALUES = {
              "Standing",
              "WORLD_HUMAN_AA_COFFEE",
              "WORLD_HUMAN_AA_SMOKE",
              "WORLD_HUMAN_BINOCULARS",
              "WORLD_HUMAN_BUM_FREEWAY",
              "WORLD_HUMAN_BUM_SLUMPED",
              "WORLD_HUMAN_BUM_STANDING",
              "WORLD_HUMAN_BUM_WASH",
              "WORLD_HUMAN_CAR_PARK_ATTENDANT",
              "WORLD_HUMAN_CHEERING",
              "WORLD_HUMAN_CLIPBOARD",
              "WORLD_HUMAN_CLIPBOARD_FACILITY",
              "WORLD_HUMAN_CONST_DRILL",
              "WORLD_HUMAN_COP_IDLES",
              "WORLD_HUMAN_DRINKING",
              "WORLD_HUMAN_DRINKING_FACILITY",
              "WORLD_HUMAN_DRINKING_CASINO_TERRACE",
              "WORLD_HUMAN_DRUG_DEALER",
              "WORLD_HUMAN_DRUG_DEALER_HARD",
              "WORLD_HUMAN_DRUG_FIELD_WORKERS_RAKE",
              "WORLD_HUMAN_DRUG_FIELD_WORKERS_WEEDING",
              "WORLD_HUMAN_DRUG_PROCESSORS_COKE",
              "WORLD_HUMAN_DRUG_PROCESSORS_WEED",
              "WORLD_HUMAN_MOBILE_FILM_SHOCKING",
              "WORLD_HUMAN_GARDENER_LEAF_BLOWER",
              "WORLD_HUMAN_GARDENER_PLANT",
              "WORLD_HUMAN_GOLF_PLAYER",
              "WORLD_HUMAN_GUARD_PATROL",
              "WORLD_HUMAN_GUARD_STAND",
              "WORLD_HUMAN_GUARD_STAND_CASINO",
              "WORLD_HUMAN_GUARD_STAND_CLUBHOUSE",
              "WORLD_HUMAN_GUARD_STAND_FACILITY",
              "WORLD_HUMAN_GUARD_STAND_ARMY",
              "WORLD_HUMAN_HAMMERING",
              "WORLD_HUMAN_HANG_OUT_STREET",
              "WORLD_HUMAN_HANG_OUT_STREET_CLUBHOUSE",
              "WORLD_HUMAN_HIKER",
              "WORLD_HUMAN_HIKER_STANDING",
              "WORLD_HUMAN_HUMAN_STATUE",
              "WORLD_HUMAN_INSPECT_CROUCH",
              "WORLD_HUMAN_INSPECT_STAND",
              "WORLD_HUMAN_JANITOR",
              "WORLD_HUMAN_JOG",
              "WORLD_HUMAN_JOG_STANDING",
              "WORLD_HUMAN_LEANING",
              "WORLD_HUMAN_LEANING_CASINO_TERRACE",
              "WORLD_HUMAN_MAID_CLEAN",
              "WORLD_HUMAN_MUSCLE_FLEX",
              "WORLD_HUMAN_MUSCLE_FREE_WEIGHTS",
              "WORLD_HUMAN_MUSICIAN",
              "WORLD_HUMAN_PAPARAZZI",
              "WORLD_HUMAN_PARTYING",
              "WORLD_HUMAN_PICNIC",
              "WORLD_HUMAN_POWER_WALKER",
              "WORLD_HUMAN_PROSTITUTE_HIGH_CLASS",
              "WORLD_HUMAN_PROSTITUTE_LOW_CLASS",
              "WORLD_HUMAN_PUSH_UPS",
              "WORLD_HUMAN_SEAT_LEDGE",
              "WORLD_HUMAN_SEAT_LEDGE_EATING",
              "WORLD_HUMAN_SEAT_STEPS",
              "WORLD_HUMAN_SEAT_WALL",
              "WORLD_HUMAN_SEAT_WALL_EATING",
              "WORLD_HUMAN_SEAT_WALL_TABLET",
              "WORLD_HUMAN_SECURITY_SHINE_TORCH",
              "WORLD_HUMAN_SIT_UPS",
              "WORLD_HUMAN_SMOKING",
              "WORLD_HUMAN_SMOKING_CLUBHOUSE",
              "WORLD_HUMAN_SMOKING_POT",
              "WORLD_HUMAN_SMOKING_POT_CLUBHOUSE",
              "WORLD_HUMAN_STAND_FIRE",
              "WORLD_HUMAN_STAND_FISHING",
              "WORLD_HUMAN_STAND_IMPATIENT",
              "WORLD_HUMAN_STAND_IMPATIENT_CLUBHOUSE",
              "WORLD_HUMAN_STAND_IMPATIENT_FACILITY",
              "WORLD_HUMAN_STAND_IMPATIENT_UPRIGHT",
              "WORLD_HUMAN_STAND_IMPATIENT_UPRIGHT_FACILITY",
              "WORLD_HUMAN_STAND_MOBILE",
              "WORLD_HUMAN_STAND_MOBILE_CLUBHOUSE",
              "WORLD_HUMAN_STAND_MOBILE_FACILITY",
              "WORLD_HUMAN_STAND_MOBILE_UPRIGHT",
              "WORLD_HUMAN_STAND_MOBILE_UPRIGHT_CLUBHOUSE",
              "WORLD_HUMAN_STRIP_WATCH_STAND",
              "WORLD_HUMAN_STUPOR",
              "WORLD_HUMAN_STUPOR_CLUBHOUSE",
              "WORLD_HUMAN_SUNBATHE",
              "WORLD_HUMAN_SUNBATHE_BACK",
              "WORLD_HUMAN_SUPERHERO",
              "WORLD_HUMAN_SWIMMING",
              "WORLD_HUMAN_TENNIS_PLAYER",
              "WORLD_HUMAN_TOURIST_MAP",
              "WORLD_HUMAN_TOURIST_MOBILE",
              "WORLD_HUMAN_VALET",
              "WORLD_HUMAN_VEHICLE_MECHANIC",
              "WORLD_HUMAN_WELDING",
              "WORLD_HUMAN_WINDOW_SHOP_BROWSE",
              "WORLD_HUMAN_YOGA",
              "Walk",
              "Walk_Facility",
              "WORLD_BOAR_GRAZING",
              "WORLD_CAT_SLEEPING_GROUND",
              "WORLD_CAT_SLEEPING_LEDGE",
              "WORLD_COW_GRAZING",
              "WORLD_COYOTE_HOWL",
              "WORLD_COYOTE_REST",
              "WORLD_COYOTE_WANDER",
              "WORLD_COYOTE_WALK",
              "WORLD_CHICKENHAWK_FEEDING",
              "WORLD_CHICKENHAWK_STANDING",
              "WORLD_CORMORANT_STANDING",
              "WORLD_CROW_FEEDING",
              "WORLD_CROW_STANDING",
              "WORLD_DEER_GRAZING",
              "WORLD_DOG_BARKING_ROTTWEILER",
              "WORLD_DOG_BARKING_RETRIEVER",
              "WORLD_DOG_BARKING_SHEPHERD",
              "WORLD_DOG_SITTING_ROTTWEILER",
              "WORLD_DOG_SITTING_RETRIEVER",
              "WORLD_DOG_SITTING_SHEPHERD",
              "WORLD_DOG_BARKING_SMALL",
              "WORLD_DOG_SITTING_SMALL",
              "WORLD_DOLPHIN_SWIM",
              "WORLD_FISH_FLEE",
              "WORLD_FISH_IDLE",
              "WORLD_GULL_FEEDING",
              "WORLD_GULL_STANDING",
              "WORLD_HEN_FLEE",
              "WORLD_HEN_PECKING",
              "WORLD_HEN_STANDING",
              "WORLD_MOUNTAIN_LION_REST",
              "WORLD_MOUNTAIN_LION_WANDER",
              "WORLD_ORCA_SWIM",
              "WORLD_PIG_GRAZING",
              "WORLD_PIGEON_FEEDING",
              "WORLD_PIGEON_STANDING",
              "WORLD_RABBIT_EATING",
              "WORLD_RABBIT_FLEE",
              "WORLD_RATS_EATING",
              "WORLD_RATS_FLEEING",
              "WORLD_SHARK_SWIM",
              "WORLD_SHARK_HAMMERHEAD_SWIM",
              "WORLD_STINGRAY_SWIM",
              "WORLD_WHALE_SWIM",
              "DRIVE",
              "WORLD_VEHICLE_ATTRACTOR",
              "PARK_VEHICLE",
              "WORLD_VEHICLE_AMBULANCE",
              "WORLD_VEHICLE_BICYCLE_BMX",
              "WORLD_VEHICLE_BICYCLE_BMX_BALLAS",
              "WORLD_VEHICLE_BICYCLE_BMX_FAMILY",
              "WORLD_VEHICLE_BICYCLE_BMX_HARMONY",
              "WORLD_VEHICLE_BICYCLE_BMX_VAGOS",
              "WORLD_VEHICLE_BICYCLE_MOUNTAIN",
              "WORLD_VEHICLE_BICYCLE_ROAD",
              "WORLD_VEHICLE_BIKE_OFF_ROAD_RACE",
              "WORLD_VEHICLE_BIKER",
              "WORLD_VEHICLE_BOAT_IDLE",
              "WORLD_VEHICLE_BOAT_IDLE_ALAMO",
              "WORLD_VEHICLE_BOAT_IDLE_MARQUIS",
              "WORLD_VEHICLE_BROKEN_DOWN",
              "WORLD_VEHICLE_BUSINESSMEN",
              "WORLD_VEHICLE_HELI_LIFEGUARD",
              "WORLD_VEHICLE_CLUCKIN_BELL_TRAILER",
              "WORLD_VEHICLE_CONSTRUCTION_SOLO",
              "WORLD_VEHICLE_CONSTRUCTION_PASSENGERS",
              "WORLD_VEHICLE_DRIVE_PASSENGERS",
              "WORLD_VEHICLE_DRIVE_PASSENGERS_LIMITED",
              "WORLD_VEHICLE_DRIVE_SOLO",
              "WORLD_VEHICLE_FARM_WORKER",
              "WORLD_VEHICLE_FIRE_TRUCK",
              "WORLD_VEHICLE_EMPTY",
              "WORLD_VEHICLE_MARIACHI",
              "WORLD_VEHICLE_MECHANIC",
              "WORLD_VEHICLE_MILITARY_PLANES_BIG",
              "WORLD_VEHICLE_MILITARY_PLANES_SMALL",
              "WORLD_VEHICLE_PARK_PARALLEL",
              "WORLD_VEHICLE_PARK_PERPENDICULAR_NOSE_IN",
              "WORLD_VEHICLE_PASSENGER_EXIT",
              "WORLD_VEHICLE_POLICE_BIKE",
              "WORLD_VEHICLE_POLICE_CAR",
              "WORLD_VEHICLE_POLICE_NEXT_TO_CAR",
              "WORLD_VEHICLE_QUARRY",
              "WORLD_VEHICLE_SALTON",
              "WORLD_VEHICLE_SALTON_DIRT_BIKE",
              "WORLD_VEHICLE_SECURITY_CAR",
              "WORLD_VEHICLE_STREETRACE",
              "WORLD_VEHICLE_TOURBUS",
              "WORLD_VEHICLE_TOURIST",
              "WORLD_VEHICLE_TANDL",
              "WORLD_VEHICLE_TRACTOR",
              "WORLD_VEHICLE_TRACTOR_BEACH",
              "WORLD_VEHICLE_TRUCK_LOGS",
              "WORLD_VEHICLE_TRUCKS_TRAILERS",
              "PROP_BIRD_IN_TREE",
              "WORLD_VEHICLE_DISTANT_EMPTY_GROUND",
              "PROP_BIRD_TELEGRAPH_POLE",
              "PROP_HUMAN_ATM",
              "PROP_HUMAN_BBQ",
              "PROP_HUMAN_BUM_BIN",
              "PROP_HUMAN_BUM_SHOPPING_CART",
              "PROP_HUMAN_MUSCLE_CHIN_UPS",
              "PROP_HUMAN_MUSCLE_CHIN_UPS_ARMY",
              "PROP_HUMAN_MUSCLE_CHIN_UPS_PRISON",
              "PROP_HUMAN_PARKING_METER",
              "PROP_HUMAN_SEAT_ARMCHAIR",
              "PROP_HUMAN_SEAT_BAR",
              "PROP_HUMAN_SEAT_BENCH",
              "PROP_HUMAN_SEAT_BENCH_FACILITY",
              "PROP_HUMAN_SEAT_BENCH_DRINK",
              "PROP_HUMAN_SEAT_BENCH_DRINK_FACILITY",
              "PROP_HUMAN_SEAT_BENCH_DRINK_BEER",
              "PROP_HUMAN_SEAT_BENCH_FOOD",
              "PROP_HUMAN_SEAT_BENCH_FOOD_FACILITY",
              "PROP_HUMAN_SEAT_BUS_STOP_WAIT",
              "PROP_HUMAN_SEAT_CHAIR",
              "PROP_HUMAN_SEAT_CHAIR_DRINK",
              "PROP_HUMAN_SEAT_CHAIR_DRINK_BEER",
              "PROP_HUMAN_SEAT_CHAIR_FOOD",
              "PROP_HUMAN_SEAT_CHAIR_UPRIGHT",
              "PROP_HUMAN_SEAT_CHAIR_MP_PLAYER",
              "PROP_HUMAN_SEAT_COMPUTER",
              "PROP_HUMAN_SEAT_COMPUTER_LOW",
              "PROP_HUMAN_SEAT_DECKCHAIR",
              "PROP_HUMAN_SEAT_DECKCHAIR_DRINK",
              "PROP_HUMAN_SEAT_MUSCLE_BENCH_PRESS",
              "PROP_HUMAN_SEAT_MUSCLE_BENCH_PRESS_PRISON",
              "PROP_HUMAN_SEAT_SEWING",
              "PROP_HUMAN_SEAT_STRIP_WATCH",
              "PROP_HUMAN_SEAT_SUNLOUNGER",
              "PROP_HUMAN_STAND_IMPATIENT",
              "CODE_HUMAN_COWER",
              "CODE_HUMAN_CROSS_ROAD_WAIT",
              "CODE_HUMAN_PARK_CAR",
              "PROP_HUMAN_MOVIE_BULB",
              "PROP_HUMAN_MOVIE_STUDIO_LIGHT",
              "CODE_HUMAN_MEDIC_KNEEL",
              "CODE_HUMAN_MEDIC_TEND_TO_DEAD",
              "CODE_HUMAN_MEDIC_TIME_OF_DEATH",
              "CODE_HUMAN_POLICE_CROWD_CONTROL",
              "CODE_HUMAN_POLICE_INVESTIGATE",
              "CHAINING_ENTRY",
              "CHAINING_EXIT",
              "CODE_HUMAN_STAND_COWER",
              "EAR_TO_TEXT",
              "EAR_TO_TEXT_FAT",
              "WORLD_LOOKAT_POINT"
        };

        public static readonly string[] PED_AVOID_LIST = {
            "JimmyBostonCutscene",
            "TracyDisantoCutscene",
            "FloydCutscene",
            "Pigeon",
            "TigerShark",
            "MaryannCutscene",
            "PestContGunman",
            "DaleCutscene",
            "GuadalopeCutscene",
            "MountainLion",
            "Chop",
            "DaveNorton",
            "Crow",
            "ShopKeep01",
            "PrologueSec02Cutscene",
            "OldMan1aCutscene",
            "Agent",
            "AshleyCutscene",
            "StripperLite",
            "SlodSmallQuadped",
            "DevinCutscene",
            "Fish",
            "JanetCutscene",
            "WeiChengCutscene",
            "Famdd01",
            "Retriever",
            "Stretch",
            "SteveHains",
            "Marston01",
            "LazlowCutscene",
            "TerryCutscene",
            "PestContDriver",
            "DreyfussCutscene",
            "HammerShark",
            "ExecutivePAMale01",
            "SlodHuman",
            "TanishaCutscene",
            "Poodle",
            "Shepherd",
            "ExecutivePAFemale01",
            "MartinMadrazoCutscene",
            "JewelassCutscene",
            "JoshCutscene",
            "LamarDavisCutscene",
            "MollyCutscene",
            "JosefCutscene",
            "RussianDrunkCutscene",
            "FabienCutscene",
            "Humpback",
            "Boar",
            "Cat",
            "ChickenHawk",
            "Chimp",
            "Chop",
            "Cormorant",
            "Cow",
            "Coyote",
            "Crow",
            "Deer",
            "Dolphin",
            "Fish",
            "Hen",
            "HammerShark",
            "Humpback",
            "Husky",
            "KillerWhale",
            "MountainLion",
            "Pig",
            "Pigeon",
            "Poodle",
            "Pug",
            "Rabbit",
            "Rat",
            "Retriever",
            "Rhesus",
            "Rottweiler",
            "Seagull",
            "Shepherd",
            "Stingray",
            "TigerShark",
            "Westy",
            "DomCutscene",
            "Andreas",
            "KarenDanielsCutscene",
            "MoviePremFemaleCutscene",
            "SiemonYetarian",
            "PriestCutscene",
            "LesterCrest",
            "NataliaCutscene",
            "MrsThornhillCutscene",
            "TaosTranslatorCutscene",
            "JimmyDisanto",
            "MarnieCutscene",
            "MagentaCutscene",
            "FbiSuit01Cutscene",
            "HunterCutscene",
            "TennisCoachCutscene",
            "FibSec01",
            "Omega",
            "Orleans",
            "LamarDavis",
            "BarryCutscene",
            "TomCutscene",
            "PaperCutscene",
            "AmandaTownley",
            "AmandaTownley",
            "Agent14Cutscene",
            "DeniseCutscene",
            "FreemodeMale01",
            "MichelleCutscene",
            "BradCadaverCutscene",
            "Lifeinvad01Cutscene",
            "DeadHooker",
            "Devin",
            "NervousRonCutscene",
            "JayNorris",
            "TaosTranslator",
            "Denise",
            "SlodLargeQuadped",
            "DaveNortonCutscene",
            "Solomon",
            "TaoChengCutscene",
            "StretchCutscene",
            "OmegaCutscene",
            "TomEpsilonCutscene",
            "CarBuyerCutscene",
            "MoviePremMaleCutscene",
            "Wade",
            "AmandaTownleyCutscene",
            "BankmanCutscene",
            "OldMan2Cutscene",
            "Hacker",
            "Dom",
            "FreemodeFemale01",
            "TennisCoach",
            "DrFriedlanderCutscene",
            "SteveHainsCutscene",
            "Ballasog",
            "Ballasog",
            "OrleansCutscene",
            "Molly",
            "Floyd",
            "BeverlyCutscene",
            "LesterCrestCutscene",
            "MiltonCutscene",
            "JimmyDisantoCutscene",
            "NervousRon",
            "Brad",
            "SiemonYetarianCutscene",
            "Claude01",
            "CrisFormageCutscene",
            "GurkCutscene",
            "GurkCutscene",
            "MrKCutscene",
            "Patricia",
            "Milton",
            "DrFriedlander",
            "MrsPhillipsCutscene",
            "Armoured01",
            "Fabien",
            "Misty01",
            "Misty01",
            "WadeCutscene",
            "ClayCutscene",
            "Pogo01",
            "TaoCheng",
            "TracyDisanto",
            "PatriciaCutscene",
            "PatriciaCutscene",
            "Lazlow",
            "NigelCutscene",
            "AndreasCutscene",
            "CaseyCutscene",
            "ZimborCutscene",
            "KarenDaniels",
            "DebraCutscene",
            "MrK",
            "Niko01",
            "BradCutscene",
            "JoeMinutemanCutscene",
            "SolomonCutscene",
            "JohnnyKlebitzCutscene",
            "ManuelCutscene",
            "Agent14",
        };

        public static readonly string[,] STREET_LIST_FULL = {
             {"ABA","Abattoir Ave",              "Abattoir Avenue",             },
             {"AMP","Abe Milton Parkway",        "Abe Milton Parkway",          },
             {"AJD","Ace Jones Dr",              "Ace Jones Drive",             },
             {"AAB","Adam's Apple Blvd",         "Adam's Apple Boulevard",      },
             {"AGS","Aguja St",                  "Aguja Street",                },
             {"ALB","Algonquin Blvd",            "Algonquin Boulevard",         },
             {"ALD","Alhambra Dr",               "Alhambra Drive",              },
             {"ALP","Alta Place",                "Alta Place",                  },
             {"ALS","Alta St",                   "Alta Street",                 },
             {"AMV","Amarillo Vista",            "Amarillo Vista",              },
             {"AMW","Amarillo Way",              "Amarillo Way",                },
             {"AOW","Americano Way",             "Americano Way",               },
             {"ARA","Armadillo Ave",             "Armadillo Avenue",            },
             {"ATS","Atlee St",                  "Atlee Street",                },
             {"AUP","Autopia Parkway",           "Autopia Parkway",             },
             {"BCD","Banham Canyon Dr",          "Banham Canyon Drive",         },
             {"BAR","Barbareno Rd",              "Barbareno Road",              },
             {"BCA","Bay City Ave",              "Bay City Avenue",             },
             {"BCI","Bay City Incline",          "Bay City Incline",            },
             {"BCR","Baytree Canyon Rd",         "Baytree Canyon Road",         },
             {"BDP","Blvd Del Perro",            "Boulevard Del Perro",         },
             {"BRS","Bridge St",                 "Bridge Street",               },
             {"BRA","Brouge Ave",                "Brouge Avenue",               },
             {"BUW","Buccaneer Way",             "Buccaneer Way",               },
             {"BVR","Buen Vino Rd",              "Buen Vino Road",              },
             {"CAP","Caesars Place",             "Caesars Place",               },
             {"CAR","Calafia Rd",                "Calafia Road",                },
             {"CAA","Calais Ave",                "Calais Avenue",               },
             {"CAB","Capital Blvd",              "Capital Boulevard",           },
             {"CAW","Carcer Way",                "Carcer Way",                  },
             {"CNA","Carson Ave",                "Carson Avenue",               },
             {"CLA","Cascabel Ave",              "Cascabel Avenue",             },
             {"CAT","Cassidy Trail",             "Cassidy Trail",               },
             {"CWA","Cat-Claw Ave",              "Cat-Claw Avenue",             },
             {"CAV","Catfish View",              "Catfish View",                },
             {"CYB","Cavalry Blvd",              "Cavalry Boulevard",           },
             {"CHP","Chianski Passage",          "Chianski Passage",            },
             {"CHR","Cholla Rd",                 "Cholla Road",                 },
             {"CSA","Cholla Springs Ave",        "Cholla Springs Avenue",       },
             {"CHS","Chum St",                   "Chum Street",                 },
             {"CAS","Chupacabra St",             "Chupacabra Street",           },
             {"CIA","Clinton Ave",               "Clinton Avenue",              },
             {"COD","Cockingend Dr",             "Cockingend Drive",            },
             {"COS","Conquistador St",           "Conquistador Street",         },
             {"CSS","Cortes St",                 "Cortes Street",               },
             {"COA","Cougar Ave",                "Cougar Avenue",               },
             {"CTA","Covenant Ave",              "Covenant Avenue",             },
             {"COW","Cox Way",                   "Cox Way",                     },
             {"CRR","Crusade Rd",                "Crusade Road",                },
             {"DAA","Davis Ave",                 "Davis Avenue",                },
             {"DES","Decker St",                 "Decker Street",               },
             {"DID","Didion Dr",                 "Didion Drive",                },
             {"DOD","Dorset Dr",                 "Dorset Drive",                },
             {"DOP","Dorset Pl",                 "Dorset Place",                },
             {"DDS","Dry Dock St",               "Dry Dock Street",             },
             {"DUA","Duluoz Ave",                "Duluoz Avenue",               },
             {"DUD","Dunstable Dr",              "Dunstable Drive",             },
             {"DUL","Dunstable Ln",              "Dunstable Lane",              },
             {"DLS","Dutch London St",           "Dutch London Street",         },
             {"DHL","Dutch London St Bridge",    "Dutch London Street Bridge",  },
             {"EGA","East Galileo Ave",          "East Galileo Avenue",         },
             {"EJR","East Joshua Road",          "East Joshua Road",            },
             {"EMD","East Mirror Dr",            "East Mirror Drive",           },
             {"EAW","Eastbourne Way",            "Eastbourne Way",              },
             {"ECB","Eclipse Blvd",              "Eclipse Boulevard",           },
             {"EDW","Edwood Way",                "Edwood Way",                  },
             {"EBB","El Burro Blvd",             "El Burro Boulevard",          },
             {"EGD","El Gordo Dr",               "El Gordo Drive",              },
             {"ERB","El Rancho Blvd",            "El Rancho Boulevard",         },
             {"ELA","Elgin Ave",                 "Elgin Avenue",                },
             {"EFB","Elysian Freeway Bridge",    "Elysian Freeway Bridge",      },
             {"EQW","Equality Way",              "Equality Way",                },
             {"EXW","Exceptionalists Way",       "Exceptionalists Way",         },
             {"FAP","Fantastic Pl",              "Fantastic Place",             },
             {"FEP","Fenwell Pl",                "Fenwell Place",               },
             {"FLP","Forced Labor Pl",           "Forced Labor Place",          },
             {"FOZ","Fort Zancudo Approach Rd",  "Fort Zancudo Approach Road",  },
             {"FOD","Forum Dr",                  "Forum Drive",                 },
             {"FUL","Fudge Ln",                  "Fudge Lane",                  },
             {"GAR","Galileo Rd",                "Galileo Road",                },
             {"GEL","Gentry Lane",               "Gentry Lane",                 },
             {"GIS","Ginger St",                 "Ginger Street",               },
             {"GLW","Glory Way",                 "Glory Way",                   },
             {"GOS","Goma St",                   "Goma Street",                 },
             {"GRA","Grapeseed Ave",             "Grapeseed Avenue",            },
             {"GMS","Grapeseed Main St",         "Grapeseed Main Street",       },
             {"GRP","Greenwich Parkway",         "Greenwich Parkway",           },
             {"GHP","Greenwich Pl",              "Greenwich Place",             },
             {"GRW","Greenwich Way",             "Greenwich Way",               },
             {"GRS","Grove St",                  "Grove Street",                },
             {"HAW","Hanger Way",                "Hanger Way",                  },
             {"HAA","Hangman Ave",               "Hangman Avenue",              },
             {"HYW","Hardy Way",                 "Hardy Way",                   },
             {"HKA","Hawick Ave",                "Hawick Avenue",               },
             {"HEW","Heritage Way",              "Heritage Way",                },
             {"HIA","Hillcrest Ave",             "Hillcrest Avenue",            },
             {"HIR","Hillcrest Ridge Access Rd", "Hillcrest Ridge Access Road", },
             {"IMC","Imagination Ct",            "Imagination Court",           },
             {"INP","Industry Passage",          "Industry Passage",            },
             {"INR","Ineseno Road",              "Ineseno Road",                },
             {"INB","Innocence Blvd",            "Innocence Boulevard",         },
             {"INW","Integrity Way",             "Integrity Way",               },
             {"INC","Invention Ct",              "Invention Court",             },
             {"JAS","Jamestown St",              "Jamestown Street",            },
             {"JOL","Joad Ln",                   "Joad Lane",                   },
             {"JOR","Joshua Rd",                 "Joshua Road",                 },
             {"KHD","Kimble Hill Dr",            "Kimble Hill Drive",           },
             {"KOD","Kortz Dr",                  "Kortz Drive",                 },
             {"LRP","Labor Pl",                  "Labor Place",                 },
             {"LAP","Laguna Pl",                 "Laguna Place",                },
             {"LVD","Lake Vinewood Dr",          "Lake Vinewood Drive",         },
             {"LVE","Lake Vinewood Est",         "Lake Vinewood Estates",       },
             {"LAD","Land Act Dam",              "Land Act Dam",                },
             {"LLB","Las Lagunas Blvd",          "Las Lagunas Boulevard",       },
             {"LEL","Lesbos Ln",                 "Lesbos Lane",                 },
             {"LIS","Liberty St",                "Liberty Street",              },
             {"LIC","Lindsay Circus",            "Lindsay Circus",              },
             {"LBA","Little Bighorn Ave",        "Little Bighorn Avenue",       },
             {"LOA","Lolita Ave",                "Lolita Avenue",               },
             {"LPS","Low Power St",              "Low Power Street",            },
             {"MAS","Macdonald St",              "Macdonald Street",            },
             {"MAW","Mad Wayne Thunder Dr",      "Mad Wayne Thunder Drive",     },
             {"MAA","Magellan Ave",              "Magellan Avenue",             },
             {"MNA","Marathon Ave",              "Marathon Avenue",             },
             {"MAD","Marina Dr",                 "Marina Drive",                },
             {"MED","Marlowe Dr",                "Marlowe Drive",               },
             {"MES","Melanoma St",               "Melanoma Street",             },
             {"MEL","Meringue Ln",               "Meringue Lane",               },
             {"MRS","Meteor St",                 "Meteor Street",               },
             {"MIR","Milton Rd",                 "Milton Road",                 },
             {"MPB","Mirror Park Blvd",          "Mirror Park Boulevard",       },
             {"MIP","Mirror Pl",                 "Mirror Place",                },
             {"MOB","Morningwood Blvd",          "Morningwood Boulevard",       },
             {"MHD","Mt Haan Dr",                "Mount Haan Drive",            },
             {"MHR","Mt Haan Rd",                "Mount Haan Road",             },
             {"MVD","Mt Vinewood Dr",            "Mount Vinewood Drive",        },
             {"MOD","Mountain View Dr",          "Mountain View Drive",         },
             {"MSW","Movie Star Way",            "Movie Star Way",              },
             {"MUR","Mutiny Rd",                 "Mutiny Road",                 },
             {"NEW","New Empire Way",            "New Empire Way",              },
             {"NIA","Nikola Ave",                "Nikola Avenue",               },
             {"NIP","Nikola Pl",                 "Nikola Place",                },
             {"NDA","Niland Ave",                "Niland Avenue",               },
             {"NOD","Normandy Dr",               "Normandy Drive",              },
             {"NAA","North Archer Ave",          "North Archer Avenue",         },
             {"NCW","North Calafia Way",         "North Calafia Way",           },
             {"NCA","North Conker Ave",          "North Conker Avenue",         },
             {"NRD","North Rockford Dr",         "North Rockford Drive",        },
             {"NSA","North Sheldon Ave",         "North Sheldon Avenue",        },
             {"NOR","Nowhere Rd",                "Nowhere Road",                },
             {"ONW","O'Neil Way",                "O'Neil Way",                  },
             {"OCA","Occupation Ave",            "Occupation Avenue",           },
             {"ORA","Orchardville Ave",          "Orchardville Avenue",         },
             {"PAB","Paleto Blvd",               "Paleto Boulevard",            },
             {"PAA","Palomino Ave",              "Palomino Avenue",             },
             {"PAD","Panorama Dr",               "Panorama Drive",              },
             {"PES","Peaceful St",               "Peaceful Street",             },
             {"PHS","Perth St",                  "Perth Street",                },
             {"PPD","Picture Perfect Drive",      "Picture Perfect Drive",       },
             {"PLP","Plaice Pl",                 "Plaice Place",                },
             {"PLV","Playa Vista",               "Playa Vista",                 },
             {"POS","Popular St",                "Popular Street",              },
             {"POD","Portola Dr",                "Portola Drive",               },
             {"PRS","Power St",                  "Power Street",                },
             {"PRD","Procopio Dr",               "Procopio Drive",              },
             {"PRP","Procopio Promenade",        "Procopio Promenade",          },
             {"PYS","Prosperity St",             "Prosperity Street",           },
             {"PYA","Pyrite Ave",                "Pyrite Avenue",               },
             {"RAP","Raton Pass",                "Raton Pass",                  },
             {"RDA","Red Desert Ave",            "Red Desert Avenue",           },
             {"RIS","Richman St",                "Richman Street",              },
             {"ROD","Rockford Dr",               "Rockford Drive",              },
             {"RSE","Route 68",                  "Route 68",                    },
             {"RSA","Route 68 Approach",         "Route 68 Approach",           },
             {"RLB","Roy Lowenstein Blvd",       "Roy Lowenstein Boulevard",    },
             {"RUS","Rub St",                    "Rub Street",                  },
             {"SAD","Sam Austin Dr",             "Sam Austin Drive",            },
             {"SAA","San Andreas Ave",           "San Andreas Avenue",          },
             {"SVB","San Vitus Blvd",            "San Vitus Boulevard",         },
             {"SAW","Sandcastle Way",            "Sandcastle Way",              },
             {"SER","Seaview Rd",                "Seaview Road",                },
             {"SAR","Senora Rd",                 "Senora Road",                 },
             {"SEW","Senora Way",                "Senora Way",                  },
             {"SHS","Shank St",                  "Shank Street",                },
             {"SIS","Signal St",                 "Signal Street",               },
             {"SRS","Sinner St",                 "Sinner Street",               },
             {"SIP","Sinners Passage",           "Sinners Passage",             },
             {"STR","Smoke Tree Rd",             "Smoke Tree Road",             },
             {"SAS","South Arsenal St",          "South Arsenal Street",        },
             {"SOB","South Boulevard Del Perro", "South Boulevard Del Perro",   },
             {"SOM","South Mo Milton Dr",        "South Mo Milton Drive",       },
             {"SRD","South Rockford Dr",         "South Rockford Drive",        },
             {"SSS","South Shambles St",         "South Shambles Street",       },
             {"SPA","Spanish Ave",               "Spanish Avenue",              },
             {"STW","Steele Way",                "Steele Way",                  },
             {"STD","Strangeways Dr",            "Strangeways Drive",           },
             {"STA","Strawberry Ave",            "Strawberry Avenue",           },
             {"SUS","Supply St",                 "Supply Street",               },
             {"SUR","Sustancia Rd",              "Sustancia Road",              },
             {"SWS","Swiss St",                  "Swiss Street",                },
             {"TAS","Tackle St",                 "Tackle Street",               },
             {"TES","Tangerine St",              "Tangerine Street",            },
             {"TOD","Tongva Dr",                 "Tongva Drive",                },
             {"TOW","Tower Way",                 "Tower Way",                   },
             {"TUS","Tug St",                    "Tug Street",                  },
             {"UNR","Union Rd",                  "Union Road",                  },
             {"UTG","Utopia Gardens",            "Utopia Gardens",              },
             {"VEB","Vespucci Blvd",             "Vespucci Boulevard",          },
             {"VIB","Vinewood Blvd",             "Vinewood Boulevard",          },
             {"VPD","Vinewood Park Dr",          "Vinewood Park Drive",         },
             {"VIS","Vitus St",                  "Vitus Street",                },
             {"VOP","Voodoo Place",              "Voodoo Place",                },
             {"WEB","West Eclipse Blvd",         "West Eclipse Boulevard",      },
             {"WGA","West Galileo Ave",          "West Galileo Avenue",         },
             {"WMD","West Mirror Drive",         "West Mirror Drive",           },
             {"WHD","Whispymound Dr",            "Whispymound Drive",           },
             {"WOD","Wild Oats Dr",              "Wild Oats Drive",             },
             {"YOS","York St",                   "York Street",                 },
             {"ZAA","Zancudo Ave",               "Zancudo Avenue",              },
             {"ZAB","Zancudo Barranca",          "Zancudo Barranca",            },
             {"ZGV","Zancudo Grande Valley",     "Zancudo Grande Valley",       },
             {"ZAR","Zancudo Rd",                "Zancudo Road",                },
             {"ZAT","Zancudo Trail",             "Zancudo Trail",               },
             // Freeway&Highway              
             {"LSF","Los Santos Freeway",        "Los Santos Freeway",          },
             {"DPF","Del Perro Fwy",             "Del Perro Freeway",           },
             {"OLF","Olympic Fwy",               "Olympic Freeway",             },
             {"LPF","La Puerta Fwy",             "La Puerta Freeway",           },
             {"GOH","Great Ocean Hwy",           "Great Ocean Highway",         },
             {"SEF","Senora Fwy",                "Senora Freeway",              },
             {"PAF","Palomino Fwy",              "Palomino Freeway",            },
             {"EFF","Elysian Fields Fwy",        "Elysian Fields Freeway",      },
             // Others
             {"MTO","Miriam Turner Overpass",    "Miriam Turner Overpass",      },
             {"GAO","Galileo Park",              "Galileo Park",                },
             {"RUW","Runway1",                   "Runway1",                     },

        };

        public static readonly string[] STREET_LIST = {
            //Street
            "Abattoir Avenue",              //"ABA"
            "Abe Milton Parkway",           //"AMP"
            "Ace Jones Drive",              //"AJD"
            "Adam's Apple Boulevard",       //"AAB"
            "Aguja Street",                 //"AGS"
            "Algonquin Boulevard",          //"ALB"
            "Alhambra Drive",               //"ALD"
            "Alta Place",                   //"ALP"
            "Alta Street",                  //"ALS"
            "Amarillo Vista",               //"AMV"
            "Amarillo Way",                 //"AMW"
            "Americano Way",                //"AOW"
            "Armadillo Avenue",             //"ARA"
            "Atlee Street",                 //"ATS"
            "Autopia Parkway",              //"AUP"
            "Banham Canyon Drive",          //"BCD"
            "Barbareno Road",               //"BAR"
            "Bay City Avenue",              //"BCA"
            "Bay City Incline",             //"BCI"
            "Baytree Canyon Road",          //"BCR"
            "Boulevard Del Perro",          //"BDP"
            "Bridge Street",                //"BRS"
            "Brouge Avenue",                //"BRA"
            "Buccaneer Way",                //"BUW"
            "Buen Vino Road",               //"BVR"
            "Caesars Place",                //"CAP"
            "Calafia Road",                 //"CAR"
            "Calais Avenue",                //"CAA"
            "Capital Boulevard",            //"CAB"
            "Carcer Way",                   //"CAW"
            "Carson Avenue",                //"CNA"
            "Cascabel Avenue",              //"CLA"
            "Cassidy Trail",                //"CAT"
            "Cat-Claw Avenue",              //"CWA"
            "Catfish View",                 //"CAV"
            "Cavalry Boulevard",            //"CYB"
            "Chianski Passage",             //"CHP"
            "Cholla Road",                  //"CHR"
            "Cholla Springs Avenue",        //"CSA"
            "Chum Street",                  //"CHS"
            "Chupacabra Street",            //"CAS"
            "Clinton Avenue",               //"CIA"
            "Cockingend Drive",             //"COD"
            "Conquistador Street",          //"COS"
            "Cortes Street",                //"CSS"
            "Cougar Avenue",                //"COA"
            "Covenant Avenue",              //"CTA"
            "Cox Way",                      //"COW"
            "Crusade Road",                 //"CRR"
            "Davis Avenue",                 //"DAA"
            "Decker Street",                //"DES"
            "Didion Drive",                 //"DID"
            "Dorset Drive",                 //"DOD"
            "Dorset Place",                 //"DOP"
            "Dry Dock Street",              //"DDS"
            "Duluoz Avenue",                //"DUA"
            "Dunstable Drive",              //"DUD"
            "Dunstable Lane",               //"DUL"
            "Dutch London Street",          //"DLS"
            "Dutch London Street Bridge",   //"DHL"
            "East Galileo Avenue",          //"EGA"
            "East Joshua Road",             //"EJR"
            "East Mirror Drive",            //"EMD"
            "Eastbourne Way",               //"EAW"
            "Eclipse Boulevard",            //"ECB"
            "Edwood Way",                   //"EDW"
            "El Burro Boulevard",           //"EBB"
            "El Gordo Drive",               //"EGD"
            "El Rancho Boulevard",          //"ERB"
            "Elgin Avenue",                 //"ELA"
            "Elysian Freeway Bridge",       //"EFB"
            "Equality Way",                 //"EQW"
            "Exceptionalists Way",          //"EXW"
            "Fantastic Place",              //"FAP"
            "Fenwell Place",                //"FEP"
            "Forced Labor Place",           //"FLP"
            "Fort Zancudo Approach Road",   //"FOZ"
            "Forum Drive",                  //"FOD"
            "Fudge Lane",                   //"FUL"
            "Galileo Road",                 //"GAR"
            "Gentry Lane",                  //"GEL"
            "Ginger Street",                //"GIS"
            "Glory Way",                    //"GLW"
            "Goma Street",                  //"GOS"
            "Grapeseed Avenue",             //"GRA"
            "Grapeseed Main Street",        //"GMS"
            "Greenwich Parkway",            //"GRP"
            "Greenwich Place",              //"GHP"
            "Greenwich Way",                //"GRW"
            "Grove Street",                 //"GRS"
            "Hanger Way",                   //"HAW"
            "Hangman Avenue",               //"HAA"
            "Hardy Way",                    //"HYW"
            "Hawick Avenue",                //"HKA"
            "Heritage Way",                 //"HEW"
            "Hillcrest Avenue",             //"HIA"
            "Hillcrest Ridge Access Road",  //"HIR"
            "Imagination Court",            //"IMC"
            "Industry Passage",             //"INP"
            "Ineseno Road",                 //"INR"
            "Innocence Boulevard",          //"INB"
            "Integrity Way",                //"INW"
            "Invention Court",              //"INC"
            "Jamestown Street",             //"JAS"
            "Joad Lane",                    //"JOL"
            "Joshua Road",                  //"JOR"
            "Kimble Hill Drive",            //"KHD"
            "Kortz Drive",                  //"KOD"
            "Labor Place",                  //"LRP"
            "Laguna Place",                 //"LAP"
            "Lake Vinewood Drive",          //"LVD"
            "Lake Vinewood Estates",        //"LVE"
            "Land Act Dam",                 //"LAD"
            "Las Lagunas Boulevard",        //"LLB"
            "Lesbos Lane",                  //"LEL"
            "Liberty Street",               //"LIS"
            "Lindsay Circus",               //"LIC"
            "Little Bighorn Avenue",        //"LBA"
            "Lolita Avenue",                //"LOA"
            "Low Power Street",             //"LPS"
            "Macdonald Street",             //"MAS"
            "Mad Wayne Thunder Drive",      //"MAW"
            "Magellan Avenue",              //"MAA"
            "Marathon Avenue",              //"MNA"
            "Marina Drive",                 //"MAD"
            "Marlowe Drive",                //"MED"
            "Melanoma Street",              //"MES"
            "Meringue Lane",                //"MEL"
            "Meteor Street",                //"MRS"
            "Milton Road",                  //"MIR"
            "Mirror Park Boulevard",        //"MPB"
            "Mirror Place",                 //"MIP"
            "Morningwood Boulevard",        //"MOB"
            "Mount Haan Drive",             //"MHD"
            "Mount Haan Road",              //"MHR"
            "Mount Vinewood Drive",         //"MVD"
            "Mountain View Drive",          //"MOD"
            "Movie Star Way",               //"MSW"
            "Mutiny Road",                  //"MUR"
            "New Empire Way",               //"NEW"
            "Nikola Avenue",                //"NIA"
            "Nikola Place",                 //"NIP"
            "Niland Avenue",                //"NDA"
            "Normandy Drive",               //"NOD"
            "North Archer Avenue",          //"NAA"
            "North Calafia Way",            //"NCW"
            "North Conker Avenue",          //"NCA"
            "North Rockford Drive",         //"NRD"
            "North Sheldon Avenue",         //"NSA"
            "Nowhere Road",                 //"NOR"
            "O'Neil Way",                   //"ONW"
            "Occupation Avenue",            //"OCA"
            "Orchardville Avenue",          //"ORA"
            "Paleto Boulevard",             //"PAB"
            "Palomino Avenue",              //"PAA"
            "Panorama Drive",               //"PAD"
            "Peaceful Street",              //"PES"
            "Perth Street",                 //"PHS"
            "Picture Perfect Drive",        //"PPD"
            "Plaice Place",                 //"PLP"
            "Playa Vista",                  //"PLV"
            "Popular Street",               //"POS"
            "Portola Drive",                //"POD"
            "Power Street",                 //"PRS"
            "Procopio Drive",               //"PRD"
            "Procopio Promenade",           //"PRP"
            "Prosperity Street",            //"PYS"
            "Pyrite Avenue",                //"PYA"
            "Raton Pass",                   //"RAP"
            "Red Desert Avenue",            //"RDA"
            "Richman Street",               //"RIS"
            "Rockford Drive",               //"ROD"
            "Route 68",                     //"RSE"
            "Route 68 Approach",            //"RSA"
            "Roy Lowenstein Boulevard",     //"RLB"
            "Rub Street",                   //"RUS"
            "Sam Austin Drive",             //"SAD"
            "San Andreas Avenue",           //"SAA"
            "San Vitus Boulevard",          //"SVB"
            "Sandcastle Way",               //"SAW"
            "Seaview Road",                 //"SER"
            "Senora Road",                  //"SAR"
            "Senora Way",                   //"SEW"
            "Shank Street",                 //"SHS"
            "Signal Street",                //"SIS"
            "Sinner Street",                //"SRS"
            "Sinners Passage",              //"SIP"
            "Smoke Tree Road",              //"STR"
            "South Arsenal Street",         //"SAS"
            "South Boulevard Del Perro",    //"SOB"
            "South Mo Milton Drive",        //"SOM"
            "South Rockford Drive",         //"SRD"
            "South Shambles Street",        //"SSS"
            "Spanish Avenue",               //"SPA"
            "Steele Way",                   //"STW"
            "Strangeways Drive",            //"STD"
            "Strawberry Avenue",            //"STA"
            "Supply Street",                //"SUS"
            "Sustancia Road",               //"SUR"
            "Swiss Street",                 //"SWS"
            "Tackle Street",                //"TAS"
            "Tangerine Street",             //"TES"
            "Tongva Drive",                 //"TOD"
            "Tower Way",                    //"TOW"
            "Tug Street",                   //"TUS"
            "Union Road",                   //"UNR"
            "Utopia Gardens",               //"UTG"
            "Vespucci Boulevard",           //"VEB"
            "Vinewood Boulevard",           //"VIB"
            "Vinewood Park Drive",          //"VPD"
            "Vitus Street",                 //"VIS"
            "Voodoo Place",                 //"VOP"
            "West Eclipse Boulevard",       //"WEB"
            "West Galileo Avenue",          //"WGA"
            "West Mirror Drive",            //"WMD"
            "Whispymound Drive",            //"WHD"
            "Wild Oats Drive",              //"WOD"
            "York Street",                  //"YOS"
            "Zancudo Avenue",               //"ZAA"
            "Zancudo Barranca",             //"ZAB"
            "Zancudo Grande Valley",        //"ZGV"
            "Zancudo Road",                 //"ZAR"
            "Zancudo Trail",                //"ZAT"
            //Freeway&Highway
            "Los Santos Freeway",           //"LSF"
            "Del Perro Freeway",            //"DPF"
            "Olympic Freeway",              //"OLF"
            "La Puerta Freeway",            //"LPF"
            "Great Ocean Highway",          //"GOH"
            "Senora Freeway",               //"SEF"
            "Palomino Freeway",             //"PAF"
            "Elysian Fields Freeway",       //"EFF"
            "Miriam Turner Overpass",       //"MTO"
            //
            "Miriam Turner Overpass",
            "Galileo Park",
            "Runway1",
        };

        public static readonly string[] STREET_LIST_GAME = {
            "Abattoir Ave",                 //"ABA"
            "Abe Milton Pkwy",              //"AMP"
            "Ace Jones Dr",                 //"AJD"
            "Adam's Apple Blvd",            //"AAB"
            "Aguja St",                     //"AGS"
            "Algonquin Blvd",               //"ALB"
            "Alhambra Dr",                  //"ALD"
            "Alta Pl",                      //"ALP"
            "Alta St",                      //"ALS"
            "Amarillo Vista",               //"AMV"
            "Amarillo Way",                 //"AMW"
            "Americano Way",                //"AOW"
            "Armadillo Ave",                //"ARA"
            "Atlee St",                     //"ATS"
            "Autopia Pkwy",                 //"AUP"
            "Banham Canyon Dr",             //"BCD"
            "Barbareno Rd",                 //"BAR"
            "Bay City Ave",                 //"BCA"
            "Bay City Incline",             //"BCI"
            "Baytree Canyon Rd",            //"BCR"
            "Boulevard Del Perro",          //"BDP"
            "Bridge St",                    //"BRS"
            "Brouge Ave",                   //"BRA"
            "Buccaneer Way",                //"BUW"
            "Buen Vino Rd",                 //"BVR"
            "Caesars Place",                //"CAP"
            "Calafia Rd",                   //"CAR"
            "Calais Ave",                   //"CAA"
            "Capital Blvd",                 //"CAB"
            "Carcer Way",                   //"CAW"
            "Carson Ave",                   //"CNA"
            "Cascabel Ave",                 //"CLA"
            "Cassidy Trail",                //"CAT"
            "Cat-Claw Ave",                 //"CWA"
            "Catfish View",                 //"CAV"
            "Cavalry Blvd",                 //"CYB"
            "Chianski Passage",             //"CHP"
            "Cholla Rd",                    //"CHR"
            "Cholla Springs Ave",           //"CSA"
            "Chum St",                      //"CHS"
            "Chupacabra St",                //"CAS"
            "Clinton Ave",                  //"CIA"
            "Cockingend Dr",                //"COD"
            "Conquistador St",              //"COS"
            "Cortes St",                    //"CSS"
            "Cougar Ave",                   //"COA"
            "Covenant Ave",                 //"CTA"
            "Cox Way",                      //"COW"
            "Crusade Rd",                   //"CRR"
            "Davis Ave",                    //"DAA"
            "Decker St",                    //"DES"
            "Didion Dr",                    //"DID"
            "Dorset Dr",                    //"DOD"
            "Dorset Pl",                    //"DOP"
            "Dry Dock St",                  //"DDS"
            "Duluoz Ave",                   //"DUA"
            "Dunstable Dr",                 //"DUD"
            "Dunstable Ln",                 //"DUL"
            "Dutch London St",              //"DLS"
            "Dutch London St Bridge",       //"DHL"
            "East Galileo Ave",             //"EGA"
            "East Joshua Road",             //"EJR"
            "East Mirror Dr",               //"EMD"
            "Eastbourne Way",               //"EAW"
            "Eclipse Blvd",                 //"ECB"
            "Edwood Way",                   //"EDW"
            "El Burro Blvd",                //"EBB"
            "El Gordo Drive",               //"EGD"
            "El Rancho Blvd",               //"ERB"
            "Elgin Ave",                    //"ELA"
            "Elysian Freeway Bridge",       //"EFB"
            "Equality Way",                 //"EQW"
            "Exceptionalists Way",          //"EXW"
            "Fantastic Pl",                 //"FAP"
            "Fenwell Pl",                   //"FEP"
            "Forced Labor Pl",              //"FLP"
            "Fort Zancudo Approach Rd",     //"FOZ"
            "Forum Dr",                     //"FOD"
            "Fudge Ln",                     //"FUL"
            "Galileo Rd",                   //"GAR"
            "Gentry Lane",                  //"GEL"
            "Ginger St",                    //"GIS"
            "Glory Way",                    //"GLW"
            "Goma St",                      //"GOS"
            "Grapeseed Ave",                //"GRA"
            "Grapeseed Main St",            //"GMS"
            "Greenwich Pkwy",               //"GRP"
            "Greenwich Pl",                 //"GHP"
            "Greenwich Way",                //"GRW"
            "Grove St",                     //"GRS"
            "Hanger Way",                   //"HAW"
            "Hangman Ave",                  //"HAA"
            "Hardy Way",                    //"HYW"
            "Hawick Ave",                   //"HKA"
            "Heritage Way",                 //"HEW"
            "Hillcrest Ave",                //"HIA"
            "Hillcrest Ridge Access Rd",    //"HIR"
            "Imagination Ct",               //"IMC"
            "Industry Passage",             //"INP"
            "Ineseno Road",                 //"INR"
            "Innocence Blvd",               //"INB"
            "Integrity Way",                //"INW"
            "Invention Ct",                 //"INC"
            "Jamestown St",                 //"JAS"
            "Joad Ln",                      //"JOL"
            "Joshua Rd",                    //"JOR"
            "Kimble Hill Dr",               //"KHD"
            "Kortz Dr",                     //"KOD"
            "Labor Pl",                     //"LRP"
            "Laguna Pl",                    //"LAP"
            "Lake Vinewood Dr",             //"LVD"
            "Lake Vinewood Est",            //"LVE"
            "Land Act Dam",                 //"LAD"
            "Las Lagunas Blvd",             //"LLB"
            "Lesbos Ln",                    //"LEL"
            "Liberty St",                   //"LIS"
            "Lindsay Circus",               //"LIC"
            "Little Bighorn Ave",           //"LBA"
            "Lolita Ave",                   //"LOA"
            "Low Power St",                 //"LPS"
            "Macdonald St",                 //"MAS"
            "Mad Wayne Thunder Dr",         //"MAW"
            "Magellan Ave",                 //"MAA"
            "Marathon Ave",                 //"MNA"
            "Marina Dr",                    //"MAD"
            "Marlowe Dr",                   //"MED"
            "Melanoma St",                  //"MES"
            "Meringue Ln",                  //"MEL"
            "Meteor St",                    //"MRS"
            "Milton Rd",                    //"MIR"
            "Mirror Park Blvd",             //"MPB"
            "Mirror Pl",                    //"MIP"
            "Morningwood Blvd",             //"MOB"
            "Mt Haan Dr",                   //"MHD"
            "Mt Haan Rd",                   //"MHR"
            "Mt Vinewood Dr",               //"MVD"
            "Mountain View Dr",             //"MOD"
            "Movie Star Way",               //"MSW"
            "Mutiny Rd",      //?           //"MUR"
            "New Empire Way",               //"NEW"
            "Nikola Ave",                   //"NIA"
            "Nikola Pl",                    //"NIP"
            "Niland Ave",                   //"NDA"
            "Normandy Dr",                  //"NOD"
            "North Archer Ave",             //"NAA"
            "North Calafia Way",            //"NCW"
            "North Conker Ave",             //"NCA"
            "North Rockford Dr",            //"NRD"
            "North Sheldon Ave",            //"NSA"
            "Nowhere Rd",                   //"NOR"
            "O'Neil Way",                   //"ONW"
            "Occupation Ave",               //"OCA"
            "Orchardville Ave",             //"ORA"
            "Paleto Blvd",                  //"PAB"
            "Palomino Ave",                 //"PAA"
            "Panorama Dr",                  //"PAD"
            "Peaceful St",                  //"PES"
            "Perth St",                     //"PHS"
            "Picture Perfect Drive",        //"PPD"
            "Plaice Pl",                    //"PLP"
            "Playa Vista",                  //"PLV"
            "Popular St",                   //"POS"
            "Portola Dr",                   //"POD"
            "Power St",                     //"PRS"
            "Procopio Dr",                  //"PRD"
            "Procopio Promenade",           //"PRP"
            "Prosperity St",                //"PYS"
            "Pyrite Ave",                   //"PYA"
            "Raton Pass",                   //"RAP"
            "Red Desert Ave",               //"RDA"
            "Richman St",                   //"RIS"
            "Rockford Dr",                  //"ROD"
            "Route 68",                     //"RSE"
            "Route 68 Approach",            //"RSA"
            "Roy Lowenstein Blvd",          //"RLB"
            "Rub St",                       //"RUS"
            "Sam Austin Dr",                //"SAD"
            "San Andreas Ave",              //"SAA"
            "San Vitus Blvd",               //"SVB"
            "Sandcastle Way",               //"SAW"
            "Seaview Rd",                   //"SER"
            "Senora Rd",                    //"SAR"
            "Senora Way",                   //"SEW"
            "Shank St",                     //"SHS"
            "Signal St",                    //"SIS"
            "Sinner St",                    //"SRS"
            "Sinners Passage",              //"SIP"
            "Smoke Tree Rd",                //"STR"
            "South Arsenal St",             //"SAS"
            "South Boulevard Del Perro",    //"SOB"
            "South Mo Milton Dr",           //"SOM"
            "South Rockford Dr",            //"SRD"
            "South Shambles St",            //"SSS"
            "Spanish Ave",                  //"SPA"
            "Steele Way",                   //"STW"
            "Strangeways Dr",               //"STD"
            "Strawberry Ave",               //"STA"
            "Supply St",                    //"SUS"
            "Sustancia Rd",                 //"SUR"
            "Swiss St",                     //"SWS"
            "Tackle St",                    //"TAS"
            "Tangerine St",                 //"TES"
            "Tongva Dr",                    //"TOD"
            "Tower Way",                    //"TOW"
            "Tug St",                       //"TUS"
            "Union Rd",                     //"UNR"
            "Utopia Gardens",               //"UTG"
            "Vespucci Blvd",                //"VEB"
            "Vinewood Blvd",                //"VIB"
            "Vinewood Park Dr",             //"VPD"
            "Vitus St",                     //"VIS"
            "Voodoo Place",                 //"VOP"
            "West Eclipse Blvd",            //"WEB"
            "West Galileo Ave",             //"WGA"
            "West Mirror Drive",            //"WMD"
            "Whispymound Dr",               //"WHD"
            "Wild Oats Dr",                 //"WOD"
            "York St",                      //"YOS"
            "Zancudo Ave",                  //"ZAA"
            "Zancudo Barranca",             //"ZAB"
            "Zancudo Grande Valley",        //"ZGV"
            "Zancudo Rd",                   //"ZAR"
            "Zancudo Trail",                //"ZAT"
         
            "Los Santos Freeway",           //"LSF"
            "Del Perro Fwy",                //"DPF"
            "Olympic Fwy",                  //"OLF"
            "La Puerta Fwy",                //"LPF"
            "Great Ocean Hwy",              //"GOH"
            "Senora Fwy",                   //"SEF"
            "Palomino Fwy",                 //"PAF"
            "Elysian Fields Fwy",           //"EFF"
                                            
            "Miriam Turner Overpass",       //"MTO"
            "Galileo Park",
            "Runway1",
        };

        public static readonly string[] STREET_LIST_ABBR =
            {
                "ABA",
                "AMP",
                "AJD",
                "AAB",
                "AGS",
                "ALB",
                "ALD",
                "ALP",
                "ALS",
                "AMV",
                "AMW",
                "AOW",
                "ARA",
                "ATS",
                "AUP",
                "BCD",
                "BAR",
                "BCA",
                "BCI",
                "BCR",
                "BDP",
                "BRS",
                "BRA",
                "BUW",
                "BVR",
                "CAP",
                "CAR",
                "CAA",
                "CAB",
                "CAW",
                "CNA",
                "CLA",
                "CAT",
                "CWA",
                "CAV",
                "CYB",
                "CHP",
                "CHR",
                "CSA",
                "CHS",
                "CAS",
                "CIA",
                "COD",
                "COS",
                "CSS",
                "COA",
                "CTA",
                "COW",
                "CRR",
                "DAA",
                "DES",
                "DID",
                "DOD",
                "DOP",
                "DDS",
                "DUA",
                "DUD",
                "DUL",
                "DLS",
                "DHL",
                "EGA",
                "EJR",
                "EMD",
                "EAW",
                "ECB",
                "EDW",
                "EBB",
                "EGD",
                "ERB",
                "ELA",
                "EFB",
                "EQW",
                "EXW",
                "FAP",
                "FEP",
                "FLP",
                "FOZ",
                "FOD",
                "FUL",
                "GAR",
                "GEL",
                "GIS",
                "GLW",
                "GOS",
                "GRA",
                "GMS",
                "GRP",
                "GHP",
                "GRW",
                "GRS",
                "HAW",
                "HAA",
                "HYW",
                "HKA",
                "HEW",
                "HIA",
                "HIR",
                "IMC",
                "INP",
                "INR",
                "INB",
                "INW",
                "INC",
                "JAS",
                "JOL",
                "JOR",
                "KHD",
                "KOD",
                "LRP",
                "LAP",
                "LVD",
                "LVE",
                "LAD",
                "LLB",
                "LEL",
                "LIS",
                "LIC",
                "LBA",
                "LOA",
                "LPS",
                "MAS",
                "MAW",
                "MAA",
                "MNA",
                "MAD",
                "MED",
                "MES",
                "MEL",
                "MRS",
                "MIR",
                "MPB",
                "MIP",
                "MOB",
                "MHD",
                "MHR",
                "MVD",
                "MOD",
                "MSW",
                "MUR",
                "NEW",
                "NIA",
                "NIP",
                "NDA",
                "NOD",
                "NAA",
                "NCW",
                "NCA",
                "NRD",
                "NSA",
                "NOR",
                "ONW",
                "OCA",
                "ORA",
                "PAB",
                "PAA",
                "PAD",
                "PES",
                "PHS",
                "PPD",
                "PLP",
                "PLV",
                "POS",
                "POD",
                "PRS",
                "PRD",
                "PRP",
                "PYS",
                "PYA",
                "RAP",
                "RDA",
                "RIS",
                "ROD",
                "RSE",
                "RSA",
                "RLB",
                "RUS",
                "SAD",
                "SAA",
                "SVB",
                "SAW",
                "SER",
                "SAR",
                "SEW",
                "SHS",
                "SIS",
                "SRS",
                "SIP",
                "STR",
                "SAS",
                "SOB",
                "SOM",
                "SRD",
                "SSS",
                "SPA",
                "STW",
                "STD",
                "STA",
                "SUS",
                "SUR",
                "SWS",
                "TAS",
                "TES",
                "TOD",
                "TOW",
                "TUS",
                "UNR",
                "UTG",
                "VEB",
                "VIB",
                "VPD",
                "VIS",
                "VOP",
                "WEB",
                "WGA",
                "WMD",
                "WHD",
                "WOD",
                "YOS",
                "ZAA",
                "ZAB",
                "ZGV",
                "ZAR",
                "ZAT",
                //
                "LSF",
                "DPF",
                "OLF",
                "LPF",
                "GOH",
                "SEF",
                "PAF",
                "EFF",
                //
                "MTO",
                "GAO",
                "RUW",
        };
    }
}
