namespace CarHUD
{
    using Rage;

    public class MyPed : Ped
    {
        public MyPed(Model model, Vector3 position, float heading) : base(model, position, heading)
        {

        }

        public MyPed(Vector3 position) : base(position)
        {

        }

        private MyPed(PoolHandle handle) : base(handle)
        {

        }

        /// <summary>
        /// Turns this <see cref="MyPed"/> into fireworks
        /// </summary>
        /// 
    }
}