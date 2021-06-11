using System;
using Messages;
using Pub;
using Services;
using Sub;
using static Broker.Topic;

Console.WriteLine("Hello World!");

var amd = new Publisher("Advanced Micro Devices", GpuListing);

var emag = new Subscriber(new GpuBuyer(), new GpuPriceModifier(), new GpuPoster());
emag.Subscribe(GpuListing);

var gpu = new Gpu("RTX 3070", 499);

amd.Publish(gpu);