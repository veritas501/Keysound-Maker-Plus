using System;
using Stylet;
using StyletIoC;
using KeySoundMaker.Pages;

namespace KeySoundMaker
{
    public class Bootstrapper : Bootstrapper<MainViewModel>
    {
        protected override void ConfigureIoC(IStyletIoCBuilder builder)
        {
            // Configure the IoC container in here
        }

        protected override void Configure()
        {
            // Perform any other configuration before the application starts
        }
    }
}
