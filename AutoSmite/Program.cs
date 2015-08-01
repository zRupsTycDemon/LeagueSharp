using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;

namespace AutoSmite
{
    class Program
    {
        private static readonly int[] SmiteAzul = { 3706, 3710, 3709, 3708, 3707 };
        private static readonly int[] SmiteMorado = { 3713, 3723, 3725, 3726, 3724 };
        private static readonly int[] SmiteGris = { 3711, 3722, 3721, 3720, 3719 };
        private static readonly int[] SmiteRojo = { 3715, 3718, 3717, 3716, 3714 };
        private static Menu MainMenu;
        private static SpellSlot _Smite;
        public static int SmiteDamage;
        static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Setup;
        }

        private static void Setup(EventArgs args)
        {
            MainMenu = new Menu("AutoSmite","AutoSmite",true);
            MainMenu.AddItem(new MenuItem("Smite", "Habilitar Smite").SetValue(true));
            MainMenu.AddItem(new MenuItem("SmiteR", "Smite Red").SetValue(false));
            MainMenu.AddItem(new MenuItem("SmiteB", "Smite Blue").SetValue(false));
            MainMenu.AddItem(new MenuItem("SmiteD", "Smite Dragon").SetValue(true));
            MainMenu.AddItem(new MenuItem("SmiteBa", "Smite Baron").SetValue(true));
            MainMenu.AddToMainMenu();
            Game.OnUpdate += Game_OnUpdate;
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            if (!MainMenu.Item("AutoSmite").GetValue<bool>()) return;
            DetectarSmite();
            foreach (var M in MinionManager.GetMinions(510,MinionTypes.All,MinionTeam.Neutral).Where(minion => minion.Health <= SmiteDamage))
            {
                switch (M.SkinName)
                {
                    case "SRU_Baron":
                        if (MainMenu.Item("SmiteBa").GetValue<bool>()) Smite(M);
                        break;
                    case "SRU_Dragon":
                        if (MainMenu.Item("SmiteD").GetValue<bool>()) Smite(M);
                        break;
                    case "SRU_Blue":
                        if (MainMenu.Item("SmiteB").GetValue<bool>()) Smite(M);
                        break;
                    case "SRU_Red":
                        if (MainMenu.Item("SmiteR").GetValue<bool>()) Smite(M);
                        break;
                }
            }
        }

        private static void Smite(Obj_AI_Base Minion)
        {
            ObjectManager.Player.Spellbook.CastSpell(_Smite, Minion);
        }
        private static void DetectarSmite()
        {
            #region SmiteDamage
            switch (ObjectManager.Player.Level)
            {
                case 1:
                    SmiteDamage =  390;
                    break;
                case 2:
                    SmiteDamage =  410;
                    break;
                case 3:
                    SmiteDamage =  430;
                    break;
                case 4:
                    SmiteDamage =  450;
                    break;
                case 5:
                    SmiteDamage =  480;
                    break;
                case 6:
                    SmiteDamage =  510;
                    break;
                case 7:
                    SmiteDamage =  540;
                    break;
                case 8:
                    SmiteDamage =  570;
                    break;
                case 9:
                    SmiteDamage =  600;
                    break;
                case 10:
                    SmiteDamage =  640;
                    break;
                case 11:
                    SmiteDamage =  680;
                    break;
                case 12:
                    SmiteDamage =  720;
                    break;
                case 13:
                    SmiteDamage =  760;
                    break;
                case 14:
                    SmiteDamage =  800;
                    break;
                case 15:
                    SmiteDamage =  850;
                    break;
                case 16:
                    SmiteDamage =  900;
                    break;
                case 17:
                    SmiteDamage =  950;
                    break;
                case 18:
                    SmiteDamage =  1000;
                    break;
            }
            #endregion
            if (SmiteAzul.Any(x => Items.HasItem(x)))
            {
                _Smite = ObjectManager.Player.GetSpellSlot("s5_summonersmiteplayerganker");
            }
            else if (SmiteMorado.Any(x => Items.HasItem(x)))
            {
                _Smite = ObjectManager.Player.GetSpellSlot("itemsmiteaoe");
            }
            else if (SmiteGris.Any(x => Items.HasItem(x)))
            {
                _Smite = ObjectManager.Player.GetSpellSlot("s5_summonersmitequick");
            }
            else if (SmiteRojo.Any(x => Items.HasItem(x)))
            {
                _Smite = ObjectManager.Player.GetSpellSlot("s5_summonersmiteduel");
            }
            else
            {
                _Smite = ObjectManager.Player.GetSpellSlot("SummonerSmite");
            }
        }
    }
}
