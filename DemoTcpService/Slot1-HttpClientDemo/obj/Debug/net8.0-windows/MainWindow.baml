   M S B A M L   `   `   `  ����O  KSlot1-HttpClientDemo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Slot1_HttpClientDemoSlot1_HttpClientDemo  5   	   %    Slot1_HttpClientDemo.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:Slot1_HttpClientDemo     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      � .+�5
   
   � 
����ح� � RowDefinitions 5      �� 5      $
��Auto �6    60   �� 5      $��* �6    6-   �� 5      $
��Auto �6    60   .+�5      �� $��Stretch=�6   $��Stretch=�5       �� Orientation$ Vertical=�5      .+�5      �� -   5        $��lblURL��5      $	��URL��6&   �� 5      -   5        $��txtURL��5      $��http://www.contoso.com��6(   $��25��6F   �� 5      -   5        $��bContent��5      $��HTML Source��6(   5      �� 5      -   5        $��
txtContent��5   #   $��1��6    �� TextWrapping$
 Wrap=�65   $ �� HorizontalScrollBarVisibility$
 Auto=�6I    �� AcceptsReturn$
 True��6n   " �� VerticalScrollBarVisibility$ Visible=�6�   �� 5      $��2��6   $ 
Horizontal=�6&   $��Center=�6?   $	��Top=�6\   .+�5      �� -   5         ��Name$ btnViewHTML��5      $
��25,5q�6.   $��80��6<   $��	View HTML��6G   6[   �� 5      -   5        $ btnClear��5      $
��25,5q�6+   $��80��69   $��Clear��6D   6T   �� 5      -   5        $ btnClose��5      $
��25,5q�6+   $��80��69   $��Close��6D   6T   5      5      5      5       