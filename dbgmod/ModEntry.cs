using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
//
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
//
using StardewValley.Menus;
using System.Reflection;
using Microsoft.Xna.Framework.Audio;

namespace volcontrolmod
{

    class ModConfig
    {
        public bool mutemusic { get; set; } = true;
        public bool muteambient { get; set; } = true;
        public bool mutefootsteps { get; set; } = true;
    }

    public class ModEntry : Mod
    {

        internal static ModConfig Config;
        public string modversion = "0.0.1";
        public bool audiotogglestate;

        public float stored_ambient_vol;
        public float stored_sound_vol;
        public float stored_music_vol;

        private void HandleDebugHelp(object sender, EventArgsCommand e)
        {
            ///
            // Currently this mod does the following :
            // Press numpad 0 to toggle between MUSIC and AMBIENT sounds being ON or OFF.
            ///

            this.Monitor.Log("=========================================", LogLevel.Info);
            this.Monitor.Log("How to use the volcontrolmod version " + modversion , LogLevel.Info);
            this.Monitor.Log("Get ingame and press your hotkey to toggle sounds on or off.", LogLevel.Info);
            this.Monitor.Log("=========================================", LogLevel.Info);
        }

        private void backupVolume()
        {
            

            
        }

        private void enableSounds()
        {
            this.Monitor.Log("enabling sounds", LogLevel.Info);
            
            try
            {
                //Game1.musicPlayerVolume = Math.Max(0f, Game1.musicPlayerVolume - 0.01f);
                //Game1.ambientPlayerVolume = Math.Max(0f, Game1.ambientPlayerVolume - 0.01f);
                Game1.musicCategory.SetVolume(5f);
                Game1.ambientCategory.SetVolume(5f);
                Game1.soundCategory.SetVolume(5f);

                Game1.currentSong.Resume();
            }
            catch (Exception e)
            {
                this.Monitor.Log(e.ToString(), LogLevel.Error);
            }
            
        }

        private void disableSounds()
        {
            this.Monitor.Log("disabling sounds", LogLevel.Info);
            
            try
            {
                Game1.musicCategory.SetVolume(0f);
                Game1.ambientCategory.SetVolume(0f);
                Game1.soundCategory.SetVolume(0f);
                Game1.currentSong.Pause();
            }
            catch (Exception e)
            {
                this.Monitor.Log(e.ToString(), LogLevel.Error);
            }
            
        }

        private void ReceiveKeyPress(object sender, EventArgsKeyPressed e)
        {
            if (e.KeyPressed == Keys.NumPad0)
            {

                if (!audiotogglestate)
                {
                    audiotogglestate = true;
                    enableSounds();
                }
                else if (audiotogglestate)
                {
                    audiotogglestate = false;
                    disableSounds();
                }
                
                this.Monitor.Log("key pressed!", LogLevel.Info);
            }
        }

        public override void Entry(IModHelper helper)
        {
            Command.RegisterCommand("help_vctrl", "Shows volcontrolmod  infos | volcontrolmod").CommandFired += this.HandleDebugHelp;
            Config = helper.ReadConfig<ModConfig>();
            ControlEvents.KeyPressed += this.ReceiveKeyPress;
        }
    }
}
