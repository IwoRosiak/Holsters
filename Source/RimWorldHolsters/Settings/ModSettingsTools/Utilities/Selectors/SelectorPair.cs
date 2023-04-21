namespace Holsters.Settings.Drawing.Utilities
{
    public sealed class SelectorPair<T>
    {
        public bool IsSelected;

        public SelectorPair(T selected, string name)
        {
            Selected = selected;
            Name = name;
        }

        public T Selected { get; }
        public string Name { get; }
    }
}