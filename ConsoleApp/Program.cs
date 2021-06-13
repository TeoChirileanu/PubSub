using Messages;
using Pub;
using Services;
using Sub;
using static Broker.Topic;

var amd = new Publisher("Advanced Micro Devices");
var nvidia = new Publisher("Nvidia");
amd.PublishOn(GpuListing);
nvidia.PublishOn(GpuListing);
var emag = MakeSubscriber("eMag");
var cel = MakeSubscriber("Cel");
var altex = MakeSubscriber("Altex");
emag.Subscribe(GpuListing);
cel.Subscribe(GpuListing);
altex.Subscribe(GpuListing);
var rtx3070 = new Gpu("RTX 3070", 499);
var rx5500 = new Gpu("RX 5500", 169);
amd.Publish(rtx3070);
nvidia.Publish(rx5500);

// todo: check console to see emag getting notified about the new gpu

static Subscriber MakeSubscriber(string name)
{
    return new(name, new GpuBuyer(), new GpuPriceModifier(), new GpuReseller());
}