namespace CarHUD
{
    using Rage;

    public class MyVehicle : Vehicle 
    {
        public MyVehicle(Model model, Vector3 position, float heading) : base(model, position, heading)
        {

        }

        public MyVehicle(Model model, Vector3 position) : base(model, position)
        {

        }

        private MyVehicle(PoolHandle handle) : base(handle)
        {

        }

        /// <summary>
        /// Rigs this vehicle to explode when it reaches a specified speed
        /// </summary>
        /// <param name="targetSpeed">The target speed, in meters per second.</param>
        
        public float getSpeed()
        {
            Vehicle veh = this;
            Ped ped = Game.LocalPlayer.Character;

            // checks to see if there is a vehicle and if so, make sure ped is sitting in vehicle (not in state of entering)
            float speed = veh.Speed;
            return speed;
            
        }

        public float getHeading()
        {
            Vehicle veh = this;
            Ped ped = Game.LocalPlayer.Character;

            // checks to see that player is in a vehicle
            float heading = veh.Heading;
            return heading;
        }
    }
}
