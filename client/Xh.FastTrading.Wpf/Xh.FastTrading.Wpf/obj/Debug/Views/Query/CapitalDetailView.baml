   M S B A M L   `   `   `  ����D  @Sup-Trade, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Xh.FastTrading.Wpf.ViewsXh.FastTrading.Wpf.Views  5      0    *Xh.FastTrading.Wpf.Views.CapitalDetailView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8local&clr-namespace:Xh.FastTrading.Wpf.Views     5         UserControl_Shadow#���   5        � DataContext  5
      ��   Locator �� Source# [  �� Path$ CapitalFlow�.��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   $	��Top=�6;   $��10q�6S   .+�5      	� -   5         	�Name$ unitId��5   <   $��股票资金账号��6   $	��200��6L   ��6+   Z� 6L   	� 5      $��保证金账号��6   $	��200��6<   ��6*   Z� 6<   5      �� 5   
   
������ $��0q�5        for_scrollviewer#���  5      $
��Auto=�5      .��5      �� $��1��6   $
��Left=�6&   $ 
Horizontal=�6B   .+�5      &   System.Windows.Controls.DataGrid  -   5          Name$ dataGridFert��5         AutoGenerateColumns .     GridLinesVisibility$
 None=�5      $
��Left=�5      $��Stretch=�5       	   
IsReadOnly	 . 
   RowHeaderWidth$
 0Q�5"         CanUserDeleteRows .     CanUserAddRows .  $	��900��5%      $
��2000��5&      ��5'      �� *List+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=�   Columns 5(      0  *System.Windows.Controls.DataGridTextColumn  5)      ,  &System.Windows.Controls.DataGridColumn   Header$ 发生日期��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$
 auto 6;   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6H   �� *HappenDateTime+  5*      $ 发生时间��6-   $
 auto 6;    6H   �� *
HappenTime+  5+      $ 业务名称��6-   $
 auto 6;    6H   �� *BusinessStr+  5,      $ 发生金额��6-   $
 auto 6;    6H   �� *HappenAmount+  5-      $ 备注��6-   $
 auto 69    6F   �� *	Remarks+5/      50      51      52      53      