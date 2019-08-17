[assembly: Rage.Attributes.Plugin("CarHUD", Description = "Displays a simple HUD showing speed", Author = "ZhivGaming", DefaultPedType = "CarHUD.MyPed", DefaultVehicleType = "CarHUD.MyVehicle")]


namespace CarHUD
{
    using System;
    using System.Drawing;
    
    using Rage;
   

    // pastebin
    //

    // pastebin--end

    internal static class EntryPoint
    {
        public static readonly Settings UserSettings = new Settings(@"Plugins\CarHUD.ini");

        private static void Main()
        {
            Game.LogTrivial("=============================");
            Game.LogTrivial("created by ZhivGaming");
            Game.LogTrivial($"Version: {typeof(String).Assembly.GetName().Version}");
            Game.LogTrivial("=============================");
            //GameFiber.StartNew(delegate
            //{
            //    Game.RawFrameRender += DrawSpeedo;
            //    // create UI if player is alive and in a vehicle
            //    GameFiber.Wait(250);
            //    GameFiber.Yield();
            //});
            // testing here
            while (true)
            {
                if (Game.LocalPlayer.Character.IsInAnyVehicle(false))
                {
                    if (UserSettings.ConversionType.Equals("MPH"))
                    {
                        int speedInMph = (int)MathHelper.ConvertMetersPerSecondToMilesPerHour(Game.LocalPlayer.Character.CurrentVehicle.Speed);
                        Game.DisplaySubtitle($"{speedInMph} mph", 100);
                    } else
                    {
                        Vector3 test = Game.LocalPlayer.Character.Position;
                        Console.WriteLine($"Player Position = {test}");
                        // assumes kph, but this is a catch all for anything not mph in the settings
                        int speedInKph = (int)MathHelper.ConvertMetersPerSecondToKilometersPerHour(Game.LocalPlayer.Character.CurrentVehicle.Speed);
                        Game.DisplaySubtitle($"{speedInKph} kph", 100);
                        
                    }
                }
                GameFiber.Yield();
            }
        }

        

        private static void DrawSpeedo(object sender, GraphicsEventArgs e)
        {
            // Get the player's ped.  The cast is fine as MyPed has been specified in the assembly
            MyPed playerPed = (MyPed)Game.LocalPlayer.Character;
            MyVehicle playerVeh = (MyVehicle)Game.LocalPlayer.Character.CurrentVehicle;


            int vehSpeedRaw = (int)Math.Ceiling(playerVeh.getSpeed());
            int vehSpeed = 0;
            string vehSuffix = UserSettings.ConversionType.ToUpper();

            // do a little waiting to not overload gamefiber
            
            if (playerPed.IsInAnyVehicle(false) && playerVeh.IsAlive)
            {
                // wait a second and get/display info
                if (UserSettings.ConversionType.ToUpper().Equals("MPH"))
                {
                    // mph
                    vehSpeed = (int)(vehSpeedRaw * 2.236936);
                }
                else
                {
                    // assumed kph or improper setting
                    vehSpeed = (int)(vehSpeedRaw * 3.6);
                }
                // display it

                Rectangle drawRect = new Rectangle(1, 250, 230, 117);
                e.Graphics.DrawRectangle(drawRect, Color.FromArgb(64, Color.Black));
                e.Graphics.DrawText(vehSpeed + " " + vehSuffix, "Arial Bold", 20.0f, new PointF(2f, 253f), Color.White, drawRect);
            }
            else
            {
                // todo: remove speedometer if player/vehicle aren't joined at hips

            }

        }

        

        internal struct Settings
        {
            public readonly InitializationFile INIFile;

            public readonly string ConversionType;


            /// <summary>
            ///  Used to read the config file for user settings.
            /// </summary>
            
            public Settings(string iniFilePath)
            {
                INIFile = new InitializationFile(iniFilePath, false);

                //reads the speedsetting
                ConversionType = INIFile.ReadString("Locale", "Units", "MPH").ToUpper();
            }
        }
    }
}
