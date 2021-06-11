using Messages;
using Sub.GpuServiceInterfaces;

namespace Services
{
    public class GpuPriceModifier : IGpuPriceModifier
    {
        public Gpu ModifyPrice(Gpu gpu)
        {
            return gpu with{Price = gpu.Price * 2};
        }
    }
}