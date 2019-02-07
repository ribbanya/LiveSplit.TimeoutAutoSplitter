using System;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using LiveSplit.Model;
using LiveSplit.Model.Comparisons;
using LiveSplit.UI;
using LiveSplit.UI.Components;

namespace LiveSplit.TimeoutAutoSplitter.UI {
    public partial class ComponentSettings : UserControl {
        public const string CurrentComparison = "Current Comparison";
        public const string Settings = "Settings";

        public string Comparison { get; set; } = CurrentComparison;
        public SplitBehavior SplitBehavior { get; set; }
        public bool ShouldPause { get; set; }


        public ComponentSettings(LiveSplitState state) {
            State = state;
            State.ComparisonRenamed += state_ComparisonRenamed;

            InitializeComponent();

            cmbComparison.DataBindings.Add("SelectedItem", this, nameof(Comparison), false,
                DataSourceUpdateMode.OnPropertyChanged);
            chkPause.DataBindings.Add("Checked", this, nameof(ShouldPause), false,
                DataSourceUpdateMode.OnPropertyChanged);
            AddRadioCheckedBinding(rbSplit, this, nameof(SplitBehavior), SplitBehavior.Split);
            AddRadioCheckedBinding(rbSkip, this, nameof(SplitBehavior), SplitBehavior.Skip);
        }


        private void ComponentSettings_Load(object sender, EventArgs e) {
            {
                cmbComparison.Items.Clear();
                cmbComparison.Items.Add(CurrentComparison);
                cmbComparison.Items.AddRange(State.Run.Comparisons.Where(x =>
                    x != null &&
                    x != BestSplitTimesComparisonGenerator.ComparisonName &&
                    x != NoneComparisonGenerator.ComparisonName).Select(s => (object) s).ToArray());
                if (!cmbComparison.Items.Contains(Comparison))
                    cmbComparison.Items.Add(Comparison);
            }
        }

        public LiveSplitState State { get; }

        //TODO: Version

        public void SetSettings(XmlNode node) {
            var element = (XmlElement) node;
            Comparison = SettingsHelper.ParseString(element[nameof(Comparison)], CurrentComparison);
            SplitBehavior = SettingsHelper.ParseEnum(element[nameof(SplitBehavior)], SplitBehavior.Split);
            ShouldPause = SettingsHelper.ParseBool(element[nameof(ShouldPause)], true);
        }

        public XmlNode GetSettings(XmlDocument document) {
            var parent = document.CreateElement(Settings);
            CreateSettingsNode(document, parent);
            return parent;
        }

        private int CreateSettingsNode(XmlDocument document, XmlElement parent) {
            return SettingsHelper.CreateSetting(document, parent, nameof(Comparison), Comparison) ^
                   SettingsHelper.CreateSetting(document, parent, nameof(SplitBehavior), SplitBehavior) ^
                   SettingsHelper.CreateSetting(document, parent, nameof(ShouldPause), ShouldPause);
        }

        public int GetSettingsHashCode() {
            return CreateSettingsNode(null, null);
        }

        void state_ComparisonRenamed(object sender, EventArgs e) {
            var args = (RenameEventArgs) e;
            if (Comparison == args.OldName) {
                Comparison = args.NewName;
            }
        }

//        private void rbSplit_CheckedChanged(object sender, EventArgs e) {
//            if (rbSplit.Checked) SplitBehavior = SplitBehavior.Split;
//        }
//
//        private void rbSkip_CheckedChanged(object sender, EventArgs e) {
//            if (rbSkip.Checked) SplitBehavior = SplitBehavior.Skip;
//        }

        private void AddRadioCheckedBinding<T>(IBindableComponent component, object dataSource, string dataMember,
            T trueValue) {
            var binding = new Binding(nameof(RadioButton.Checked), dataSource, dataMember, true,
                DataSourceUpdateMode.OnPropertyChanged);
            binding.Parse += (s, a) => {
                if ((bool) a.Value) a.Value = trueValue;
            };
            binding.Format += (s, a) => a.Value = ((T) a.Value).Equals(trueValue);
            component.DataBindings.Add(binding);
        }
    }
}