   M S B A M L   `   `   `  ���� F  BLab2-WpfApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Lab2_WpfAppLab2_WpfApp      Lab2_WpfApp.MainWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:Lab2_WpfApp       � WindowStartupLocation$  CenterScreen=� � Title$ 
MainWindow��$	��450��$	��350��.��� .+�� 
����ح� � RowDefinitions �� $
��Auto ��� $��* ��� $
��Auto �.+��� $��Stretch=�$��Stretch=� �� Orientation$ Vertical=�.+��� -   $��lblURL��$	��URL���� -   $��txtURL��$��http://www.contoso.com��$��25���� -   $��bContent��$��HTML Source���� -   $��
txtContent��$��1�� �� TextWrapping$
 Wrap=�$ �� HorizontalScrollBarVisibility$
 Auto=� �� AcceptsReturn$
 True��" �� VerticalScrollBarVisibility$ Visible=��� $��2��$ 
Horizontal=�$��Center=�$	��Top=�.+��� -    ��Name$ btnViewHTML��$
��25,5q�$��80��$��	View HTML���� -   $ btnClear��$
��25,5q�$��80��$��Clear���� -   $ btnClose��$
��25,5q�$��80��$��Close��