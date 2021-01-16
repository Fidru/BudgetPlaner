using System.Windows;

namespace UI.Wpf.Animations
{
    public class AnimationElement
    {
        public AnimationElement(AnimationTag tag, FrameworkElement frameworkElement)
        {
            Tag = tag;
            FrameworkElement = frameworkElement;
        }

        public AnimationTag Tag { get; set; }
        public FrameworkElement FrameworkElement { get; set; }
    }
}