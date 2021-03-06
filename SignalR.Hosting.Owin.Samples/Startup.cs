﻿using System;
using Gate;
using Gate.Middleware;
using Gate.Owin;
using SignalR.Hosting.Owin;

namespace SignalR.Hosting.Owin.Samples
{
    public class Startup
    {
        public static void Configuration(IAppBuilder builder)
        {
            builder
                .Use(LogToConsole)
                //.RescheduleCallbacks()
                .UseShowExceptions()
                .Map("/Raw", map => map.Chunked().MapConnection<Raw>())
                .Use(Alias, "/", "/index.html")
                .UseStatic("public");
        }

        public static AppDelegate Alias(AppDelegate app, string path, string alias)
        {
            return
                (env, result, fault) =>
                    {
                        var req = new Request(env);
                        if (req.Path == path)
                        {
                            req.Path = alias;
                        }
                        app(env, result, fault);
                    };
        }

        public static AppDelegate LogToConsole(AppDelegate app)
        {
            return
                (env, result, fault) =>
                    {
                        var req = new Request(env);
                        Console.WriteLine(req.Method + " " + req.PathBase + req.Path);
                        app(env, result, fault);
                    };
        }
    }
}