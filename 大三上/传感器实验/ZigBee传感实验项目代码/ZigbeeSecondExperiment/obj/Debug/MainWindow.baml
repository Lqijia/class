   M S B A M L   `   `   `  ����O  KZigbeeExperiment2.Uc, Version=0.0.0.5, Culture=neutral, PublicKeyToken=nullY@clr-namespace:ZigbeeExperiment2.Uc;assembly=ZigbeeExperiment2.UcZigbeeExperiment2.Uc  5   	   G7clr-namespace:OxyPlot.Wpf;assembly=ZigbeeExperiment2.UcOxyPlot.Wpf  5   
    ZigbeeSecondExperiment'   !ZigbeeSecondExperiment.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Rlocal@clr-namespace:ZigbeeExperiment2.Uc;assembly=ZigbeeExperiment2.Uc     5   	   Goxy7clr-namespace:OxyPlot.Wpf;assembly=ZigbeeExperiment2.Uc     5   
   -   5          � Title$  
MainWindow��5   	   $	��585��6   $
��1185��6)    � 	Resources 5      1   +ZigbeeExperiment2.Uc.CompareOutputConverter   c2c��%   &	          5   
   6'   5	      .��5
      �  � ColumnDefinitions 5   
   �� 5      $
��438* �6   �� 5      $
��145* �6   ��5   
   �� 5       �� ImageSource$ Images/bg_zigbee.png��6   .+�5   
   � $��0��6   $	��232��6   $	��295��6,   $��581,0,0,312q�68   ��5      �� 5      $ Images/bg_table.png��6   .+�5         OxyPlot.Wpf.PlotView  
������    Model 65   �� *	PlotModel+5      5      � 5   
   $��1��6    � RowDefinitions 5      �� 5       $
��200* �6    �� 5!      $
��150* �6    �� 5"      $
��115* �6     5$      �� 5%      $��1* �6#   �� 5&      $��1* �6#   .+�5(      �� $��1��6    �� TextAlignment$ Center=�6%   $��0��6<   
�������$��44,6,38,153q�6_   ��6t   ��  �� Path$ HresholdTemperature� �� StringFormat$ {0:f0}���� 5*      $
��Left=�6Q   	 � Maximum$	 70Q�6l   
 � Minimum$
 0Q�6y    �� Orientation$ Vertical=�6�   $	��130��6�   $��	64,22,0,0q�6�   $��1��6�   $	��Top=�6�    � Value 6   �� $ HresholdTemperature� �� Mode$ TwoWay=�5+      �� 5-      $��温度调节信号��6   
�������$��1��68   $��0��6E   $��32,151,29,0q�6U   �� 5.      $��1��6   $��1��6!   $��70��6�   $��27��6�   ��62   �� $ 
CompareOut� �� 	Converter# [   �� ConverterParameter$- 'Images/switch_1.png;Images/switch_0.png��6�   �� 5/      $��比较器输出��6   $��1��6$   
�������$��1��6D   $��39,151,38,0q�6T   �� 50      -   5         ��Name$ imageFan��50      $��2��6&   $��0��63   $��Images/fan_1.png��6D   $��64��6^   $��64��6j   �� 51      $��2��6   $��1��6!   $��64��6�   $��64��6�   ��61   �� $ 
IsOpenLamp�# [  $, &Images/lamp_on.png;Images/lamp_off.png��6�   54      57      58      