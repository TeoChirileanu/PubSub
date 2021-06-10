using System.Threading.Tasks;
using Pub;
using Sub;

var publisher = new Publisher("Nvidia");
var subscriber = new Subscriber("Teodor");
subscriber.Subscribe(publisher);

await Task.Run(async () => await publisher.Publish());