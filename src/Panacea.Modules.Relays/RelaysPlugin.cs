using Panacea.Core;
using Panacea.Modularity.Relays;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.Modules.Relays
{
    public class RelaysPlugin : IRelayManagerPlugin
    {
        public RelaysPlugin(PanaceaServices core)
        {
            _core = core;
        }

        public Task BeginInit()
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {

        }

        public Task EndInit()
        {
            return Task.CompletedTask;
        }

        IRelayManager _manager;
        private readonly PanaceaServices _core;

        public IRelayManager GetRelayManager()
        {
            if (_manager == null)
            {
                _manager = new RelayManager(_core);
            }
            return _manager;
        }

        public Task Shutdown()
        {
            return Task.CompletedTask;
        }
    }
}
