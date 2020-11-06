using BlazorSignalRApp.Client.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Server.Hubs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorSignalRApp.Server
{
    public class LiveView : IHostedService
    {
        private Timer _timer;
        private readonly IHubContext<DotnetifyHub> _hubContext;

        public LiveView(IHubContext<DotnetifyHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(UpdateLiveView, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(2));

            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        private void UpdateLiveView(object state)
        {
            var random = new Random();
            var data = new DotnetifyModel()
            {
                Waveform = Enumerable.Range(1, 30).Select(x => new string[] { $"{x}", $"{Math.Sin(x / Math.PI)}" }).ToArray(),
                Bar = Enumerable.Range(1, 8).Select(_ => random.Next(500, 1000)).ToArray(),
                Pie = Enumerable.Range(1, 3).Select(_ => random.NextDouble()).ToArray()
            };
            var timer = Observable.Interval(TimeSpan.FromSeconds(1));
            timer.Subscribe(x =>
            {
                x += 31;
                //WaveForm function that doesnt translate well
                //string[] vs = new string[] { $"{x}", $"{Math.Sin(x / Math.PI)}" }.ToArray();
                data.Bar = Enumerable.Range(1, 12).Select(_ => random.Next(500, 1000)).ToArray();
                data.Pie = Enumerable.Range(1, 3).Select(_ => random.NextDouble()).ToArray();
                _hubContext.Clients.All.SendAsync("Send Data: ", data);
            });

        }
    }
}
