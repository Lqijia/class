   M S B A M L   `   `   `  ����C  ?Light.Uc, Version=0.0.0.4, Culture=neutral, PublicKeyToken=null;+clr-namespace:OxyPlot.Wpf;assembly=Light.UcOxyPlot.Wpf  5      	 Light   Light.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   ;oxy+clr-namespace:OxyPlot.Wpf;assembly=Light.Uc     5        � Title$  
MainWindow��5   	   $	��550��6   $
��1166��6+   .��5      � -   5         �Name$ mainGrid��5      ��5   
   �� 5       �� ImageSource$ Images/bg_light.png��6   6=   .+�5
   
   �  � ColumnDefinitions 5      �� 5      $
��413* �6#   �� 5      $
��491* �6#   �� 5      $
��262* �6#   .+�5         OxyPlot.Wpf.PlotView  -   5          Name$ pv��5      $��1��6H   
������ $��3,10,4,0q�6q   $	��490��6�   $	��Top=�6�      Model 6(   ��  �� Path$ MyModel�6�   �� 5      $��电   阻：��6   
�������$
��Left=�6<   $��	25,23,0,0q�6W   $	��Top=�6j    J� RenderTransformOrigin$ 0.125,-0.16&�6�   $��2��6�   $��25��6�   $��51��6�   �� 5      -   5        $��tbResistance��5   �   $��25��6   $
��Left=�6"   ���#4F5A95
������ $��0,4,0,0q�6m   $��2��6�   $��	82,23,0,0q�6�   
�������$	��0Ω��6�   $	��Top=�6�   $ 1.197,0.913&�6�   $��64��6  �� 5      $��光照度：��6   
�������
������ $
��Left=�6S   $��	25,58,0,0q�6n   $	��Top=�6�   $ 0.125,-0.16&�6�   $��2��6�   $��25��6�   $��52��6�   �� 5      -   5        $��tbIll��5   �   $��23��6   $
��Left=�6"   
�������
������ ���#4F5A95$��0,4,0,0q�6   $��	82,60,0,0q�6�   $
��0Lux��6�   $	��Top=�6�   $��2��6�   $��64��6�   $ 0.509,-0.087&�6�   �� 5      $��比较器输出：��6   
�������
������ $
��Left=�6U   $��	25,88,0,0q�6p   $	��Top=�6�   $ 0.125,-0.16&�6�   $��2��6�   $��25��6�   $��81��6�   �� 5      -   5        $��isOpenLampImage��5   @   $��2��6   $
��Left=�6$   $��30��6X   $��
31,118,0,0q�6d   $	��Top=�6x   $��70��6�   $��Images/switch_0.png��6�   �� 5      -   5        $��2��5      $
��Left=�6(   
������ $��
31,120,0,0q�6\   $
��Wrap=�6p   $��0��6�    �� Padding$ 0,5,0,0q�6�   	 �� TextAlignment$	 Center=�6�   $	��Top=�6�   
�������$��34��6�   $��25��6�   6�   �� 5      -   5        $��2��5      $
��Left=�6(   
������ $��
65,120,0,0q�6\   $
��Wrap=�6p   $��1��6�   $ 0,5,0,0q�6�   $	 Center=�6�   $	��Top=�6�   
�������$��34��6�   $��25��6�   6�   �� 5      -   5        $��tbAd1��5   S   $��16��6   $
��Left=�6$   $��
160,16,0,0q�6?   $��0��6`   
�������$	��Top=�6|   $��2��6�   $��22��6�   �� 5      -	   5        $��tbAd2��5   S   $��17��6   $
��Left=�6$   $��
219,15,0,0q�6?   $��0��6`   
�������$	��Top=�6|   $��2��6�   $��24��6�   �� 5      
�������$��60��6)   
������ $
��Left=�6Q   $��138,148,0,0q�6l   $	��Top=�6�   $ 0.125,-0.16&�6�   $��2��6�   .��5       �� $
��Wrap=�6   $	 Center=�60   $��HTC电阻AD值��6G   5!      � 5"      -
   5        
 �Name$
 sensorAdProgressBar��5"      $��2��67   ���#49981E���#C8C8C8 � Orientation$ Vertical=�6q   $
��Left=�6�   $	��120��6�   $��
167,30,0,0q�6�   $	��Top=�6�   $��10��6�   5#      �� 5%      
�������$��60��6)   
������ $
��Left=�6Q   $��207,148,0,0q�6l   $	��Top=�6�   $ 0.125,-0.16&�6�   $��2��6�   .��5&      �� $
��Wrap=�6   $	 Center=�60   $��电位器AD值��6G   5'      � 5(      -   5        $ 
 potentiometerAdProgressBar��5(      $��2��6>   ���#2189ff���#C8C8C8$ Vertical=�6x   $
��Left=�6�   $	��120��6�   $��
226,30,0,0q�6�   $	��Top=�6�   $��10��6�   5)      �� 5*      $��电阻式传感器模块��6   
�������$��2��6;   $
��Left=�6K   $��
77,337,0,0q�6f   $	��Top=�6z   �� 5,      -   5         ��Name$ imgSensorLamp��5,       � Cursor$
 Hand��6d   
 � Tag$	 123��6r   $��45,397,207,93q�6|   $��Images/point_normal.png��6�   $��2��6�   �� 5-      $��	传感器��6   ���#6372BA$��2��69   $��29,416,187,59q�6I   $��50��6`   �� 5/      -   5        $ imgDigitalOutputLamp��5/      $
 Hand��6r   $��202,393,50,97q�6�   $��Images/point_normal.png��6�   $��2��6�   �� 50      $��数字量输出��6   ���#6372BA$��2��6:   $��172,411,20,64q�6J   $��70��6a   �� 52      -   5        $ imgPotentiometerLamp��52      $
 Hand��6q   $��45,450,207,40q�6   $��Images/point_normal.png��6�   $��2��6�   $ -5.4,0.7&�6�   �� 53      $��	电位器��6   ���#6372BA$��2��68   $��29,467,181,0q�6H   $	��Top=�6^   $��50��6v   �� 55      -   5        $ imgAnalogOutputLamp��55      $
 Hand��6o   $��204,450,48,40q�6}   $��Images/point_normal.png��6�   $��2��6�   �� 56      $��模拟量输出��6   ���#6372BA$��2��6:   $��172,467,18,8q�6J   $��70��6`   58      5;      5<      