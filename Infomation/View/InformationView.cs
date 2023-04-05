using UniqNumbers.ValueType;

namespace UniqNumbers.Infomation.View
{
    class InformationView
    {
        public TextView PathView { get; } = new TextView(new Vector2(1, 10));
        public TextView FileNamesView { get; } = new TextView(new Vector2(100, 1));
        public TextView MessageView { get; } = new TextView(new Vector2(1, 13));
        public TextView DiagnosticView { get; } = new TextView(new Vector2(35, 0));

        public InformationView()
        {
            Update();
        }

        public void Update()
        {
            PathView.Update();
            MessageView.Update();
            FileNamesView.Update();
            DiagnosticView.Update();
        }
    }
}
