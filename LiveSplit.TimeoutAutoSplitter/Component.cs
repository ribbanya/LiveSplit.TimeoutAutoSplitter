using System;
using System.Windows.Forms;
using System.Xml;
using LiveSplit.Model;
using LiveSplit.TimeoutAutoSplitter;
using LiveSplit.TimeoutAutoSplitter.UI;

// ReSharper disable once CheckNamespace
namespace LiveSplit.UI.Components {
    public class Component : LogicComponent {
        public ComponentSettings Settings { get; }

        private PreciseSplitTimerModel Model { get; }

        public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height,
            LayoutMode mode) {
            if (state.CurrentPhase != TimerPhase.Running) return;

            var method = state.CurrentTimingMethod;
            var index = state.CurrentSplitIndex;

            var comparisonKey = Settings.Comparison == ComponentSettings.CurrentComparison
                ? state.CurrentComparison
                : Settings.Comparison;

            var curSegment = state.Run[index];
            var curSplit = state.CurrentTime[method];
            var curComparison = curSegment.Comparisons[comparisonKey][method];
            TimeSpan? prevSplit = null, comparisonDuration = null;
            //TODO: Option to use segment duration or not

            bool durationExceeded;
            if (index <= 0) {
                durationExceeded = false;
            }
            else {
                var prevSegment = state.Run[index - 1];
                var prevComparison = prevSegment.Comparisons[comparisonKey][method];
                prevSplit = prevSegment.SplitTime[method];

                var splitDuration = curSplit - prevSplit;
                comparisonDuration = curComparison - prevComparison;

                durationExceeded = splitDuration >= comparisonDuration;
            }


            if (!(durationExceeded || curSplit >= curComparison)) return;

            switch (Settings.SplitBehavior) {
                case SplitBehavior.Split:
                    var curTime = state.CurrentTime;
                    var targetTime = durationExceeded ? prevSplit + comparisonDuration : curComparison;
                    if (curTime[method] > targetTime)
                        curTime[method] = targetTime;
                    Model.SplitAt(curTime);
                    break;
                case SplitBehavior.Skip:
                    Model.SkipSplit();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (Settings.ShouldPause) Model.Pause();
        }

        public override void Dispose() { }

        public override string ComponentName => "Timeout Auto Splitter";

        public Component(LiveSplitState state) {
            Settings = new ComponentSettings(state);
            Model = new PreciseSplitTimerModel {CurrentState = state};
        }


        public override Control GetSettingsControl(LayoutMode mode) => Settings;

        public override XmlNode GetSettings(XmlDocument document) => Settings.GetSettings(document);

        public override void SetSettings(XmlNode settings) => Settings.SetSettings(settings);


        // ReSharper disable once UnusedMember.Global
        public int GetSettingsHashCode() => Settings.GetSettingsHashCode();
    }
}