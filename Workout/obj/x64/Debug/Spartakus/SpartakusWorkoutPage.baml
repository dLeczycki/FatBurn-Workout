   M S B A M L   `   `   `  ����B  >Workout, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Workout.SpartakusWorkout.Spartakus  5      ,    &Workout.Spartakus.SpartakusWorkoutPage   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:Workout.Spartakus     5        N� Title$  SpartakusWorkoutPage��5	      .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   6)   �� 5      $
��0.1* �6   6,   �� 5      $��6* �6   6*   �� 5      $��* �6   6)   .+�5   
   �� $��0��6   $��0��6!   ._�5      �� $��	SPARTAKUS��6   $
��34px(�6(   $
��Bold"�68   $��Center=�6J   $��Center=�6g   
�������6�   5      �� 5   
   $��0��6   $��1��6!   ._�5      �� $��/icons/bar-light.png��6   5      �� 5   
   $��0��6   $��2��6!   ._�5      �  � ColumnDefinitions 5      �� 5      $��* �6'   63   �� 5      $��* �6'   63   .+�5      �� $��0��6   $��0��6&   ._�5      � .+�5       �� $��0��6!   $��0��61   ._�5!      �� -   5         ��Name$ labelCounter��5!   $   $��0��6:   $	��300(�6F   
�������$��Stretch=�6h   $��Center=�6�   5"      5#      5$      �� 5%      $��0��6   $��1��6&   ._�5&      �  5'      �� 5(      $��* �6,   69   �� 5)      $��5* �6,   6:   �� 5*      $��* �6,   69   .+�5,      �� $��0��6!   $��0��61   ._�5-      �� -   5        $ labelExName��5-   $   $��Nazwa ćwiczenia��69   $��Center=�6S   $��Center=�6w   $��24(�6�   $��0,10,0,0q�6�   $
��Bold"�6�   
�������6�   5.      �� 5/      $��0��6!   $��1��61   ._�50      �� -   5         ��Name$ imageEx��50   $   $��/img/example.jpg��65   $��10,10,10,10q�6O   51      �� 52      $��0��6!   $��2��61   ._�53      �  54   "   �� 55   &   $��* �67   6C   �� 56   &   $��* �67   6C   .+�58   "   �� $��0��6)   $��0��66   ._�59   &   �� -   5        $ labelEx��59   ,   $��Ćwiczenie: 0/10��6=   $��Center=�6W   $��Center=�6t   $��24(�6�   
�������6�   5:   #   �� 5;   "   $��0��6)   $��1��66   ._�5<   &   �� -   5        $ labelSeries��5<   ,   $��
Seria: 0/3��6A   $��24(�6V   $��Center=�6d   $��Center=�6�   
�������6�   5=   #   5>      5?      5@      5A      5B      5C      �� 5D   
   $��0��6   $��3��6!   ._�5E      �  5F      �� 5G      $��2* �6'   64   �� 5H      $��* �6'   63   �� 5I      $��* �6'   63   �� 5J      $��2* �6'   64   .+�5L      �� $��0��6   $��0��6)   ._�5M      �� -   5        $��
backButton��5M      ���	#FF2094FF
�������$��Center=�6p   $��24(�6�   $��Wstecz��6�   {�.  ��6/   Z� 6�   5N      �� 5O      $��1��6   $��0��6)   ._�5P      �� -   5        $��pauseButton��5P      ��60   Z� .��5Q      �� -   5        $��
pauseImage��5Q       $��/icons/pause.png��62   $��5q�6L   6Y   5R      5S      �� 5T      $��2��6   $��0��6)   ._�5U      �� -	   5        $��
playButton��5U      {�.  ��6/   Z� .��5V      �� -
   5        $��	playImage��5V       $��/icons/play-dark.png��61   $��5q�6O   {�.  6n   5W      5X      �� 5Y      $��3��6   $��0��6)   ._�5Z      �� -   5        $��
nextButton��5Z      ���	#FF2094FF
�������$��Center=�6p   $��24(�6�   $��Przerwij trening��6�   ��6/   Z� 6�   5[      5\      5]      5^      5_      