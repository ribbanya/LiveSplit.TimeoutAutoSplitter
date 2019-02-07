using System;
using LiveSplit.Model;
using LiveSplit.Model.Input;

namespace LiveSplit.TimeoutAutoSplitter {
    internal class PreciseSplitTimerModel : ITimerModel {
        private LiveSplitState _currentState;

        public void Start() => throw new NotSupportedException();
        public void InitializeGameTime() => throw new NotSupportedException();

        public void Split() => SplitAt(_currentState.CurrentTime);
        public void SplitAt(Time time) {
            if (CurrentState.CurrentPhase != TimerPhase.Running) return;
            if (CurrentState.CurrentTime.RealTime <= TimeSpan.Zero) return;

            CurrentState.CurrentSplit.SplitTime = time;
            CurrentState.CurrentSplitIndex++;
            if (CurrentState.Run.Count == CurrentState.CurrentSplitIndex) {
                CurrentState.CurrentPhase = TimerPhase.Ended;

                //TODO? (See LiveSplitState.CurrentTime.get
                CurrentState.AttemptEnded = TimeStamp.CurrentDateTime;
            }

            CurrentState.Run.HasChanged = true;

            OnSplit?.Invoke(this, null);
        }

        public void SkipSplit() {
            switch (CurrentState.CurrentPhase) {
                case TimerPhase.Running:
                case TimerPhase.Paused:
                    if (CurrentState.CurrentSplitIndex >= CurrentState.Run.Count - 1)
                        return;
                    CurrentState.CurrentSplit.SplitTime = default(Time);
                    CurrentState.CurrentSplitIndex++;
                    CurrentState.Run.HasChanged = true;

                    OnSkipSplit?.Invoke(this, null);
                    break;
                default:
                    return;
            }
        }

        public void UndoSplit() => throw new NotSupportedException();
        public void Reset() => throw new NotSupportedException();
        public void Reset(bool updateSplits) => throw new NotSupportedException();
        public void ResetAndSetAttemptAsPB() => throw new NotSupportedException();

        public void Pause() {
            switch (CurrentState.CurrentPhase) {
                case TimerPhase.Running:
                    var realTime = CurrentState.CurrentTime.RealTime;
                    if (realTime == null) break;
                    CurrentState.TimePausedAt = realTime.Value;
                    CurrentState.CurrentPhase = TimerPhase.Paused;
                    OnPause?.Invoke(this, null);
                    break;
                case TimerPhase.Paused:
                    CurrentState.AdjustedStartTime = TimeStamp.Now - CurrentState.TimePausedAt;
                    CurrentState.CurrentPhase = TimerPhase.Running;
                    OnResume?.Invoke(this, null);
                    break;
                case TimerPhase.NotRunning:
                    Start();
                    break;
                case TimerPhase.Ended:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void UndoAllPauses() => throw new NotSupportedException();
        public void ScrollUp() => throw new NotSupportedException();
        public void ScrollDown() => throw new NotSupportedException();
        public void SwitchComparisonPrevious() => throw new NotSupportedException();
        public void SwitchComparisonNext() => throw new NotSupportedException();

        public LiveSplitState CurrentState {
            get => _currentState;
            set {
                _currentState = value;
                value?.RegisterTimerModel(this);
            }
        }

        public event EventHandler OnSplit;
        public event EventHandler OnUndoSplit;
        public event EventHandler OnSkipSplit;
        public event EventHandler OnStart;
        public event EventHandlerT<TimerPhase> OnReset;
        public event EventHandler OnPause;
        public event EventHandler OnUndoAllPauses;
        public event EventHandler OnResume;
        public event EventHandler OnScrollUp;
        public event EventHandler OnScrollDown;
        public event EventHandler OnSwitchComparisonPrevious;
        public event EventHandler OnSwitchComparisonNext;
    }
}