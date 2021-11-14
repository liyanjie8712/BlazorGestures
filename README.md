# Liyanjie.Blazor.Gestures

Blazor����

- #### GestureArea
    ����������������ʶ������
  - Usage
    ```html
    <GestureArea AllowMouseSimulation="�Ƿ������ģ�⣬Ĭ��false">
        //ChildContent here
        //Recognizers here
    </GestureArea>
    ```
- #### LongPressGestureRecognizer
  - Usage
    ```html
    <GestureArea>
        //ChildContent here
        <LongPressGestureRecognizer MinTime="default 500"  //ʶ��ΪLongPress����Сmillionseconds
                                    MaxDistance="default 10"  //ʶ��ΪTap�����touchmove distance
                                    OnLongPress="Your callback" />
    </GestureArea>
    ```
- #### PanGestureRecognizer
  - Usage
    ```html
    <GestureArea>
        //ChildContent here
        <PanGestureRecognizer OnPan="Your callback"
                              OnPanEnd="Your callback" />
    </GestureArea>
    ```
- #### PinchGestureRecognizer
  - Usage
    ```html
    <GestureArea>
        //ChildContent here
        <PinchGestureRecognizer MinScale="default 0"  //����PinchIn��PinchOut����Сscale change
                                OnPinch="Your callback"
                                OnPinchEnd="Your callback"
                                OnPinchIn="Your callback"
                                OnPinchOut="Your callback" />
    </GestureArea>
    ```
- #### RotateGestureRecognizer
  - Usage
    ```html
    <GestureArea>
        //ChildContent here
        <RotateGestureRecognizer MinAngle="default 30"  //����RotateLeft��RotateRight����Сangle change
                                 OnRotate="Your callback"
                                 OnRotateEnd="Your callback"
                                 OnRotateLeft="Your callback"
                                 OnRotateRight="Your callback" />
    </GestureArea>
    ```
- #### SwipeGestureRecognizer
  - Usage
    ```html
    <GestureArea>
        //ChildContent here
        <SwipeGestureRecognizer Direction="default Horizontal"  //������ϣ�Up|Down==Vertical or Left|Right == Horizontal or Up|Down|Left|Right == Horizontal|Vertical
                                MaxTime="default 300"  //ʶ��SwipeUp��SwipeDown��SwipeLeft��SwipeRight�����millionseconds
                                MinDistance="default 20"  //ʶ��ΪTap�����touchmove distance
                                OnSwipe="Your callback"
                                OnSwipeEnd="default false"
                                OnSwipeUp="Your callback"
                                OnSwipeDown="Your callback"
                                OnSwipeLeft="Your callback"
                                OnSwipeRight="Your callback" />
    </GestureArea>
    ```
- #### TapGestureRecognizer
  - Usage
    ```html
    <GestureArea>
        //ChildContent here
        <TapGestureRecognizer MaxDistance="default 10"  //ʶ��ΪTap�����touchmove distance
                              MaxTime="default 300"  //ʶ��DoubleTap�����millionseconds
                              OnTap="Your callback"
                              AllowDoubleTap="default false"
                              MaxDoubleTapDistance="20"  ʶ��ΪDoubleTap�����touchstart distance
                              OnDoubleTap="Your callback" />
    </GestureArea>
    ```