using System;
using System.Reflection;
using LiveSplit.Model;
using LiveSplit.TimeoutAutoSplitter;
using LiveSplit.UI.Components;


[assembly: ComponentFactory(typeof(Factory))]

namespace LiveSplit.TimeoutAutoSplitter {
    // ReSharper disable once UnusedMember.Global
    public class Factory : IComponentFactory {
        public string ComponentName => "Timeout Auto Splitter";

        public string Description => "An Auto Splitter that splits at the times of a specified Comparison.";
        public ComponentCategory Category => ComponentCategory.Control;

        public IComponent Create(LiveSplitState state) {
            return new Component(state);
        }

        public string UpdateName => ComponentName;

        public string XMLURL => ""; //TODO

        public string UpdateURL => ""; //TODO;

        public Version Version => Assembly.GetExecutingAssembly().GetName().Version;
    }
}