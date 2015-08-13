namespace Invasion.Renderers
{
    using System;

    public class KeyDownEventArgs : EventArgs
    {
        public KeyDownEventArgs(Command command)
        {
            this.Command = command;
        }

        public Command Command { get; private set; }
    }
}