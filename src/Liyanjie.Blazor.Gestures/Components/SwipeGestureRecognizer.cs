using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Liyanjie.Blazor.Gestures.Components;

public class SwipeGestureRecognizer : ComponentBase
{
    [CascadingParameter] public GestureRecognizer? GestureRecognizer { get; set; }

    [Parameter] public GestureDirection Direction { get; set; } = GestureDirection.Horizontal;
    [Parameter] public double MaxTime { get; set; } = 300;
    [Parameter] public double MinDistance { get; set; } = 20;
    [Parameter] public int Factor { get; set; } = 5;
    [Parameter] public EventCallback<GestureSwipeEventArgs> OnSwipe { get; set; }
    [Parameter] public EventCallback<GestureSwipeEventArgs> OnSwipeEnd { get; set; }
    [Parameter] public EventCallback<GestureSwipeEventArgs> OnSwipeLeft { get; set; }
    [Parameter] public EventCallback<GestureSwipeEventArgs> OnSwipeRight { get; set; }
    [Parameter] public EventCallback<GestureSwipeEventArgs> OnSwipeUp { get; set; }
    [Parameter] public EventCallback<GestureSwipeEventArgs> OnSwipeDown { get; set; }

    bool swipeStart;

    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (GestureRecognizer is not null)
        {
            GestureRecognizer.GestureStarted += GestureStarted;
            GestureRecognizer.GestureMoved += GestureMoved;
            GestureRecognizer.GestureEnded += GestureEnded;
        }
    }

    void GestureStarted(object? sender, TouchEventArgs e)
    {
    }
    void GestureMoved(object? sender, TouchEventArgs e)
    {
        if (!GestureRecognizer!.GestureStart)
            return;

        AwareSwipe(e);
    }
    void GestureEnded(object? sender, TouchEventArgs e)
    {
        if (!GestureRecognizer!.GestureStart)
            return;

        if (swipeStart)
            AwareSwipeEnd(e);

        swipeStart = false;
    }

    void AwareSwipe(TouchEventArgs e)
    {
        if (e.IsGestureMove())
        {
            swipeStart = true;

            var distanceX = GestureRecognizer!.CurrentPoints![0].ClientX - GestureRecognizer!.StartPoints![0].ClientX;
            var distanceY = GestureRecognizer!.CurrentPoints![0].ClientY - GestureRecognizer!.StartPoints![0].ClientY;
            var angle = GestureRecognizer.StartPoints[0].CalcAngle(GestureRecognizer.CurrentPoints[0]);
            var direction = angle.CalcDirectionFromAngle();
            var second = GestureRecognizer.GestureDuration / 1000;

            if (direction == 0 || direction != (Direction & direction))
                return;

            OnSwipe.InvokeAsync(CreateEventArgs("SWIPE",
                angle,
                direction,
                distanceX,
                distanceY,
                (10 - Factor) * 10 * second * second
            ));
        }
    }
    void AwareSwipeEnd(TouchEventArgs e)
    {
        if (e.IsGestureEnd())
        {
            var distanceX = GestureRecognizer!.CurrentPoints![0].ClientX - GestureRecognizer!.StartPoints![0].ClientX;
            var distanceY = GestureRecognizer!.CurrentPoints![0].ClientY - GestureRecognizer!.StartPoints![0].ClientY;
            var angle = GestureRecognizer.StartPoints[0].CalcAngle(GestureRecognizer.CurrentPoints[0]);
            var direction = angle.CalcDirectionFromAngle();
            var second = GestureRecognizer.GestureDuration / 1000;
            var factor = (10 - Factor) * 10 * second * second;

            if (direction == 0 || direction != (Direction & direction))
                return;

            OnSwipeEnd.InvokeAsync(CreateEventArgs("SWIPEEND",
                angle,
                direction,
                distanceX,
                distanceY,
                factor
            ));

            if (GestureRecognizer.GestureDuration < MaxTime && direction switch
            {
                GestureDirection.Up or GestureDirection.Down or GestureDirection.Vertical => Math.Abs(distanceY) > MinDistance,
                GestureDirection.Left or GestureDirection.Right or GestureDirection.Horizontal => Math.Abs(distanceX) > MinDistance,
                _ => false
            })
            {
                var eventArgs = CreateEventArgs(direction switch
                {
                    GestureDirection.Up => "SWIPE_UP",
                    GestureDirection.Down => "SWIPE_DOWN",
                    GestureDirection.Left => "SWIPE_LEFT",
                    GestureDirection.Right => "SWIPE_RIGHT",
                    _ => string.Empty,
                },
                    angle,
                    direction,
                    distanceX,
                    distanceY,
                    factor);
                _ = direction switch
                {
                    GestureDirection.Up => OnSwipeUp.InvokeAsync(eventArgs),
                    GestureDirection.Down => OnSwipeDown.InvokeAsync(eventArgs),
                    GestureDirection.Left => OnSwipeLeft.InvokeAsync(eventArgs),
                    GestureDirection.Right => OnSwipeRight.InvokeAsync(eventArgs),
                    _ => Task.CompletedTask,
                };
            }
        }
    }

    GestureSwipeEventArgs CreateEventArgs(
        string type,
        double? angle,
        GestureDirection direction,
        double distanceX,
        double distanceY,
        double factor)
    {
        return new()
        {
            Type = type,
            StartPoints = GestureRecognizer?.StartPoints,
            CurrentPoints = GestureRecognizer?.CurrentPoints,
            GestureCount = GestureRecognizer!.StartPoints!.Length,
            GestureDuration = GestureRecognizer.GestureDuration,
            Angle = angle,
            Direction = direction,
            DistanceX = distanceX,
            DistanceY = distanceY,
            Factor = factor
        };
    }
}