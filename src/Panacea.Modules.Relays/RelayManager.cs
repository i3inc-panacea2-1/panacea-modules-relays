using Panacea.Core;
using Panacea.Modularity.Relays;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.Modules.Relays
{
    class RelayManager : IRelayManager
    {
        private readonly PanaceaServices _core;
        IRelayModule _blindsModule;

        public RelayManager(PanaceaServices core)
        {
            _core = core;
            _core.PluginLoader.PluginLoaded += PluginLoader_PluginLoaded;
            _core.PluginLoader.PluginUnloaded += PluginLoader_PluginUnloaded;
            BlindsAttached = _core.PluginLoader.GetPlugins<IRelayModulePlugin>().Any(p => p.GetType().Name == "FtdiPlugin");
            if (BlindsAttached)
            {
                _core.PluginLoader.GetPlugins<IRelayModulePlugin>().First(p => p.GetType().Name == "FtdiPlugin").GetModuleAsync()
                    .ContinueWith(task => _blindsModule = task.Result);
            }
        }

        private async void PluginLoader_PluginUnloaded(object sender, Modularity.IPlugin e)
        {
            BlindsAttached = _core.PluginLoader.GetPlugins<IRelayModulePlugin>().Any(p => p.GetType().Name == "FtdiPlugin");
            if (BlindsAttached)
            {
                _blindsModule = await _core.PluginLoader.GetPlugins<IRelayModulePlugin>().First(p => p.GetType().Name == "FtdiPlugin").GetModuleAsync();
            }
            //NurseCallAttached = _core.PluginLoader.GetPlugins<IRelayModulePlugin>().Any(p => p.GetType().Name == "FtdiPlugin");
        }

        private async void PluginLoader_PluginLoaded(object sender, Modularity.IPlugin e)
        {
            BlindsAttached = _core.PluginLoader.GetPlugins<IRelayModulePlugin>().Any(p => p.GetType().Name == "FtdiPlugin");
            if (BlindsAttached)
            {
                _blindsModule = await _core.PluginLoader.GetPlugins<IRelayModulePlugin>().First(p => p.GetType().Name == "FtdiPlugin").GetModuleAsync();
            }
            //NurseCallAttached = _core.PluginLoader.GetPlugins<IRelayModulePlugin>().Any(p => p.GetType().Name == "FtdiPlugin");
        }

        public bool BlindsAttached { get; set; }

        public bool NurseCallAttached { get; set; }

        public Task<bool> SetBlindsDownAsync(bool on)
        {
            return _blindsModule.SetStatusAsync(on, 3);
        }

        public Task<bool> SetBlindsUpAsync(bool on)
        {
            return _blindsModule.SetStatusAsync(on, 4);
        }

        public Task<bool> SetNurseCallAsync(bool on)
        {
            return Task.FromResult(true);
        }
    }
}
