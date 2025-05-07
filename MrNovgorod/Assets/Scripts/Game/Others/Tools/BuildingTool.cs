using Game.Buildings;

namespace Game.Others.Tools
{
    public abstract class BuildingTool
    {
        public static Ebuildings GetEbuildings(string buildingName)
        {
            return buildingName switch
            {
                "yuriev_monastery" => Ebuildings.YurievMonastery,
                "vitosavlitsy" => Ebuildings.Vitosavlitsy,
                "varlaamo_khutyn" => Ebuildings.VarlaamoKhutyn,
                "tank_t34" => Ebuildings.TankT34,
                "pankratov" => Ebuildings.Pankratov,
                "spass_nereditsa" => Ebuildings.SpassNereditsa,
                "flagstaff" => Ebuildings.Flagstaff,
                "bridge_piers" => Ebuildings.BridgePiers,
                "ryurikovo_gorodische" => Ebuildings.RyurikovoGorodische,
                "spass_kovalevo" => Ebuildings.SpassKovalevo,
                "spass_iline" => Ebuildings.SpassIline,
                "znamensky_cathedral" => Ebuildings.ZnamenskyCathedral,
                "alexeevskaya_bashnya" => Ebuildings.AlexeevskayaBashnya,
                "victory_monument" => Ebuildings.VictoryMonument,
                "novgorod_kremlin" => Ebuildings.NovgorodKremlin,
                _ => Ebuildings.Empty
            };
        }
    }
}