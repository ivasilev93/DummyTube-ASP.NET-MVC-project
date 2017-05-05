using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace DummyTube.Hubs
{
    public class ActivityHub : Hub
    {
        public void WatchedVideo(string videoTitle, string username)
        {
            Clients.All.logWatchedVideo(videoTitle, username);
        }

        public void UploadedVideo(string videoTitle, string username)
        {
            Clients.All.logActivity(videoTitle, username);
        }
    }
}