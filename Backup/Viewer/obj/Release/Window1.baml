   M S B A M L   `   `   `  ����   	ULSViewer    Viewer.Window1   \ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    8x,http://schemas.microsoft.com/winfx/2006/xaml      � Title$+  %Stefan Gordon's SharePoint ULS Viewer��$	��411��$	��840�� � Icon$2 ,/ULSViewer;component/toolbargraphics/see.png�� � 	Resources    
BlueHeader �� Orientation  HeaderStyle �� 
TargetType �� Property �� Value �� EndPoint �� 
StartPoint	 � Color
 � Offset��%�   &	        &	 G     �� .R��� $ 
Horizontal=�.+��� $��Center=�
����������� �� " ��.���  � �� �� $ 0.5,1&�$ 0.5,0&�.��� $	 	#FF223B84��$
 1Q�� $	 	#FF57A0F4��$	
 0.5Q�� $	 	#FF4B94EC��$	
 0.5Q�.���  � RowDefinitions �� $
��298* ��� $��75 �.+��� -   $��dataList��$��
2,27.5,0,0q�$y�Hidden=� �� View ��  �� ColumnHeaderTemplate# [  ! �� ColumnHeaderContainerStyle# [ -   	gview��.P��� $
��Time�� �� HeaderTemplate# [   �� DisplayMemberBinding ��  �� Path$ 	Timestamp��� $��Process��# [   �� $ Process��� $	��TID��# [   �� $	 TID��� $
��Area��# [   �� $
 Area��� $��Category��# [   �� $ Category��� $��EventID��# [   �� $ EventID��� $��Level��# [   �� $ Level��� $��Message��# [   �� $ Message��� $��Correlation��# [   �� $ Correlation�c� -   $��toolBar1��$��26��$��2,0,0,0q�$	��Top=�
�������.@��� -   .���� $��20��$��20��$��toolbargraphics\open.jpg�� � ToolTip$ Open Log File���� $
��Auto��$��10q��� $��Stretch=�$��2q�.��	Severity:�� -   $��severityCombo��$��85��$	��3,0q��� $��5q��� $��Stretch=�$��2q�.��
Process:�� -   $��processCombo��$��85��$	��3,0q�
��������� $��5q��� $��Stretch=�$��2q�.��	Category:�� -   $��categoryCombo��$��85��$	��3,0q�
��������� $��5q��� $��Stretch=�$��2q�.��Text Filter:�� -   $��textFind��$��85��$	��3,0q�
�������.���� $��10q��� -	   .���� $��toolbargraphics\help.jpg��$ Open Log File���� -
   $��
detailText��$��2,0,0,0q�$��1�� �� 
IsReadOnly .  �� TextWrapping$ WrapWithOverflow=�" �� VerticalScrollBarVisibility$
 Auto=�