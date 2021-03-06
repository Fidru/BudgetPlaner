﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace UI.Wpf.Animations
{
    public class CustomAnimations
    {
        private PaymentAnimations _nextAnimations { get; set; }
        private PaymentAnimations _middleToLeft { get; set; }
        private PaymentAnimations _rightToLeft { get; set; }
        private PaymentAnimations _rightBottomToTopLeft { get; set; }

        private PaymentAnimations _paymentView { get; set; }
        private PaymentAnimations _hide { get; set; }
        public bool PaymentIsActive { get; private set; }

        public CustomAnimations(Rect leftCordination, Rect middleCordination, Rect rightTop, Rect rightBottom)
        {
            DisplayedAnimations = new List<AnimationElement>();

            _nextAnimations = new PaymentAnimations
            {
                Start = CreateStoryboard(1200, 0, "RenderTransform", 0, 0, "Opacity", 0, 1, 800),
                Revert = CreateStoryboard(-1200, 0, "RenderTransform", 0, 0, "Opacity", 0, 1, 800)
            };

            var middleToLeftX = GetXvalue(leftCordination, middleCordination);

            _middleToLeft = new PaymentAnimations
            {
                Start = CreateStoryboard(0, middleToLeftX, "RenderTransform", 0, 0, "Opacity", 0, 1, 800),
                Revert = CreateStoryboard(middleToLeftX, 0, "RenderTransform", 0, 0, "Opacity", 0, 1, 800)
            };

            var rightToLeftX = GetXvalue(leftCordination, rightTop);
            _rightToLeft = new PaymentAnimations
            {
                Start = CreateStoryboard(0, rightToLeftX, "RenderTransform", 0, 0, "Opacity", 0, 1, 800),
                //Revert = CreateStoryboard(1280, 0, "RenderTransform", 0, 0, "Opacity", 0, 1, 800)
                Revert = CreateStoryboard(-rightToLeftX, 0, "RenderTransform", 0, 0, "Opacity", 0, 1, 800)
            };

            var rightBottomToTopLeftX = GetXvalue(leftCordination, rightBottom);
            var rightBottomToTopLeftY = GetYvalue(leftCordination, rightBottom);
            _rightBottomToTopLeft = new PaymentAnimations
            {
                Start = CreateStoryboard(0, rightBottomToTopLeftX, "RenderTransform", 0, rightBottomToTopLeftY, "Opacity", 0, 1, 800),
                //Revert = CreateStoryboard(1280, 0, "RenderTransform", -449, 0, "Opacity", 0, 1, 800)
                Revert = CreateStoryboard(rightBottomToTopLeftX, 0, "RenderTransform", rightBottomToTopLeftY, 0, "Opacity", 0, 1, 800)
            };

            _paymentView = new PaymentAnimations
            {
                Start = CreateStoryboard(0, 0, "RenderTransform", 1000, 0, "Opacity", 0, 1, 800),
                Revert = CreateStoryboard(0, 0, "RenderTransform", 0, 1000, "Opacity", 1, 0, 800)
            };

            _hide = new PaymentAnimations
            {
                Start = CreateStoryboard(0, 0, "RenderTransform", 0, -1200, "Opacity", 1, 0, 800),
                Revert = CreateStoryboard(0, 0, "RenderTransform", -1200, 0, "Opacity", 0, 1, 800)
            };
        }

        private double GetXvalue(Rect leftCordination, Rect middleCordination)
        {
            return leftCordination.X - middleCordination.X;
        }

        private double GetYvalue(Rect leftCordination, Rect middleCordination)
        {
            return leftCordination.Y - middleCordination.Y;
        }

        private Storyboard CreateStoryboard(double fromX, double toX, string render, double fromY, double toY, string opacity, int opacityFrom, int opacityTo, int maxTimer)
        {
            Storyboard sb = new Storyboard();
            sb.FillBehavior = FillBehavior.HoldEnd;

            var fadeIn = new DoubleAnimation(opacityFrom, opacityTo, new Duration(TimeSpan.FromMilliseconds(maxTimer)));
            Storyboard.SetTargetProperty(fadeIn, new PropertyPath(opacity));
            sb.Children.Add(fadeIn);

            DoubleAnimation moveIn = new DoubleAnimation(fromX, toX, new Duration(TimeSpan.FromMilliseconds(maxTimer)));
            Storyboard.SetTargetProperty(moveIn, new PropertyPath($"{render}.{nameof(TranslateTransform.X)}"));
            sb.Children.Add(moveIn);

            DoubleAnimation moveInY = new DoubleAnimation(fromY, toY, new Duration(TimeSpan.FromMilliseconds(maxTimer)));
            Storyboard.SetTargetProperty(moveInY, new PropertyPath($"{render}.{nameof(TranslateTransform.Y)}"));
            sb.Children.Add(moveInY);

            return sb;
        }

        private List<AnimationElement> DisplayedAnimations { get; set; }

        public void StartAnimation(AnimationTag tag, FrameworkElement element, FrameworkElement[] hiddenElements = null)
        {
            if (hiddenElements != null)
            {
                foreach (var hiddenElement in hiddenElements)
                {
                    StartAnimation(AnimationTag.Hide, hiddenElement);
                }
            }

            switch (tag)
            {
                case AnimationTag.Next:
                    {
                        _nextAnimations.Start.Begin(element);
                        return;
                    }
                case AnimationTag.Previous:
                    {
                        _nextAnimations.Revert.Begin(element);
                        return;
                    }
                case AnimationTag.MiddleToLeft:
                    {
                        _middleToLeft.Start.Begin(element);
                        DisplayedAnimations.Add(new AnimationElement(AnimationTag.MiddleToLeftRevert, element));
                        return;
                    }
                case AnimationTag.MiddleToLeftRevert:
                    {
                        _middleToLeft.Revert.Begin(element);
                        return;
                    }
                case AnimationTag.RightToLeft:
                    {
                        _rightToLeft.Start.Begin(element);
                        DisplayedAnimations.Add(new AnimationElement(AnimationTag.RightToLeftRevert, element));
                        return;
                    }
                case AnimationTag.RightToLeftRevert:
                    {
                        _rightToLeft.Revert.Begin(element);
                        return;
                    }
                case AnimationTag.RightToTopLeft:
                    {
                        _rightBottomToTopLeft.Start.Begin(element);
                        DisplayedAnimations.Add(new AnimationElement(AnimationTag.RightToTopLeftRevert, element));
                        return;
                    }
                case AnimationTag.RightToTopLeftRevert:
                    {
                        _rightBottomToTopLeft.Revert.Begin(element);
                        return;
                    }
                case AnimationTag.Payment:
                    {
                        _paymentView.Start.Begin(element);
                        DisplayedAnimations.Add(new AnimationElement(AnimationTag.PaymentRevert, element));
                        PaymentIsActive = true;

                        return;
                    }
                case AnimationTag.PaymentRevert:
                    {
                        _paymentView.Revert.Begin(element);
                        PaymentIsActive = false;

                        return;
                    }
                case AnimationTag.Hide:
                    {
                        _hide.Start.Begin(element);
                        DisplayedAnimations.Add(new AnimationElement(AnimationTag.HideRevert, element));
                        return;
                    }
                case AnimationTag.HideRevert:
                    {
                        _hide.Revert.Begin(element);
                        return;
                    }
                default:
                    {
                        return;
                    }
            }
        }

        public void ResetAnimations()
        {
            while (DisplayedAnimations.Any())
            {
                var animation = DisplayedAnimations[0];

                StartAnimation(animation.Tag, animation.FrameworkElement);
                DisplayedAnimations.Remove(animation);
            }
        }
    }
}