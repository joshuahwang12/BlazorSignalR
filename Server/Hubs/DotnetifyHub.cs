using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;
using BlazorSignalRApp.Client.Models;
using BlazorSignalRApp.Server;
using Microsoft.AspNetCore.SignalR;

namespace Server.Hubs
{
    public class DotnetifyHub : Hub
    {
        //public async Task RecieveData(DotnetifyModel data)
        //{
        //    await Clients.All.SendAsync("Incoming Data: ", data);
        //}
    }
}