   M S B A M L   `   `   `  ����B  >Workout, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WorkoutWorkout  5   	       Workout.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Workout     5   	     � Icon$  /img/FBicon.png��5   	    � Title$ FatBurn��5	   	   $	��675��6   $
��1200��6&   
�������$��Center=�6F   $��Center=�6j   $��Center=�6�   $��Center=�6�   .��5
      �  � ColumnDefinitions 5   
   �� 5      $��2* �6   6,   �� 5      $��8* �6   6,   .+�5   
   �� $��0��6   $��0��6!   ���	#FF212121._�5      �  � RowDefinitions 5      �� 5      $
��2.5* �6$   64   �� 5      $��0.05* �6$   65   �� 5      $��2* �6$   62   �� 5      $��2* �6$   62   �� 5      $��2* �6$   62   �� 5      $��2* �6$   62   �� 5      $��3* �6$   62   .+�5      �� $��0��6   $��0��6)   ._�5      � .+�5      �� -   5         ��Name$ 
buttonHelp��5   !   
���������65   Z� ��6^   Z� .��5      �� -   5         ��Name$ 	helpImage��5   $   $��70��67   $��70��6C   $��/icons/help.png��6N   $��UniformToFill=�6g   5      5       5!      �� 5"      $��0��6   $��1��6)   
�������6K   �� 5#      $��0��6   $��2��6)   ._�5$      �� -   5        $ buttonPushups��5$      
���������64   Z� ��6]   Z� .��5%      �  5&   "   �� 5'   &   $��* �67   6C   �� 5(   &   $��2* �67   6D   .+�5*   "   �� $��0��6)   $��0��69   ._�5+   &   �� -   5        $ pushupsImage��5+   ,   $��50��6B   $��50��6N   $��/icons/push-up-light.png��6Y   $��UniformToFill=�6{   5,   #   �� 5-   "   $��1��6)   $��0��69   ._�5.   &   �� $��26(�6,   
�������.��6M   
100 pompek6Y   5/   #   50      51      52      �� 53      $��0��6   $��3��6)   ._�54      �� -   5        $ buttonWeider��54      
���������63   Z� ��6\   Z� .��55   "   �  56   &   �� 57   *   $��* �6;   6G   �� 58   *   $��2* �6;   6H   .+�5:   &   �� $��0��6-   $��0��6=   ._�5;   *   �� -   5        $ weiderImage��5;   0   $��50��6E   $��50��6Q   $!��/icons/abdominals-light.png��6\   $��UniformToFill=�6�   5<   '   �� 5=   &   $��1��6-   $��0��6=   ._�5>   *   �� $��26(�60   
�������.��6Q   	6 Weidera6\   5?   '   5@   #   5A      5B      �� 5C      $��0��6   $��4��6)   ._�5D      �� -   5        $ buttonSpartakus��5D      
���������66   Z� ��6_   Z� .��5E      �  5F      �� 5G   "   $��* �63   6?   �� 5H   "   $��2* �63   6@   .+�5J      �� $��0��6%   $��0��65   ._�5K   "   �� -   5        $ spartakusImage��5K   (   $��50��6@   $��50��6L   $��/icons/sweat-light.png��6W   $��UniformToFill=�6w   5L      �� 5M      $��1��6%   $��0��65   ._�5N   "   �� $��26(�6(   
�������.��6I   	Spartakus6T   5O      5P      5Q      5R      �� 5S      $��0��6   $��5��6)   ._�5T      �� -	   5        $ 	buttonGym��5T      
���������60   Z� ��6Y   Z� .��5U      �  5V      �� 5W   "   $��* �63   6?   �� 5X   "   $��2* �63   6@   .+�5Z      �� $��0��6%   $��0��65   ._�5[   "   �� -
   5        $ gymImage��5[   (   $��50��6:   $��50��6F   $$��/icons/weightlifting-light.png��6Q   $��UniformToFill=�6y   5\      �� 5]      $��1��6%   $��0��65   ._�5^   "   �� $��26(�6(   
�������.��6I   	Siłownia6S   5_      5`      5a      5b      �� 5c      $��0��6   $��6��6)   ._�5d      �  5e      �� 5f      $��* �6/   6;   �� 5g      $��* �6/   6;   .+�5i      �� $��0��6!   $��0��61   ._�5j      �� -   5        $ 	buttonMic��5j   %   
���������68   Z� ��6a   Z� .��5k   "   �� -   5        $ micImage��5k   (   $��50��6:   $��50��6F   $!��/icons/microphone-light.png��6Q   $��Uniform=�6v   $��Bottom=�6�   $��Center=�6�   $��LeftToRight=�6�   5l      5m      �� 5n      $��1��6!   $��0��61   ._�5o      �� -   5        $ buttonSpeaker��5o   %   
���������6<   Z� ��6e   Z� .��5p   "   �� -   5        $ speakerImage��5p   (   $��50��6>   $��50��6J   $��/icons/speaker-light.png��6U   $��Uniform=�6w   $��Bottom=�6�   $��Center=�6�   $��LeftToRight=�6�   5q      5r      5s      5t      5u      5w      �� 5x   
   $��1��6   $��0��6!   
���   �._�5y       � -   5          �Name$ 	mainFrame��5y        � NavigationUIVisibility$ Hidden=�6'   6I   5z      5{      5|      