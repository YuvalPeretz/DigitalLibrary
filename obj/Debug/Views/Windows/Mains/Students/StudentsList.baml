   M S B A M L   `   `   `  ����I  EDigitalLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulli9clr-namespace:DigitalLibrary.Views.Windows.Mains.Students+DigitalLibrary.Views.Windows.Mains.Students  5   	   >    8DigitalLibrary.Views.Windows.Mains.Students.StudentsList   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Klocal9clr-namespace:DigitalLibrary.Views.Windows.Mains.Students     5   	   -   5          � Title$  StudentsList��5   	   $	��450��5	   	   $	��450��5
   	   $	��450��5   	   $	��760��5   	   $	��570��5   	   $
��1000��5   	    � WindowStartupLocation$ CenterScreen=�5   	   ���	#FF5EAEFF$��RightToLeft=�5   	    � WindowStyle$
 None=�5   	   $��Georgia Pro Black)�5   	   $y�Visible=�5   	    � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $E ?/DigitalLibrary;component/Views/Resources/HoverTransparent.xaml?�6%   %    5      .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5       $��* �6   .+�5"   
   �� $��	50,20,0,0q�5#      $��0��5$       �� Orientation$ 
Horizontal=�5%      $��Bottom=�5&      $
��Left=�5'      .+�5*      �� $��חיפוש:��5+      $��20(�5,      �� 5.      -   5         ��Name$ 	CB_Filter��5/      $��	10,0,10,0q�50      $	��100��51      $��15(�52      {�.  .@�55      �� $��15(�6   $
��שם��6-   �� 56      $��15(�6   $��
טלפון��6-   57      �� 58      -   5        	 ��Name$	 TB_SearchBox��59      {�.  $	��200��5;      $��15(�5<      $
��Left=�5=      $��Center=�5>      5?      5@      � 5A   
   $��50q�5B      $��1��5C      .+�5D      � -   5        
 �Name$
 
MainSPGrid��5E      .+�5G      &   System.Windows.Controls.DataGrid  -   5          Name$ DG_StudentsList��5H      $��20(�5I      $��Center=�5J      $��Center=�5K         AutoGenerateColumns .     Columns 5M      0  *System.Windows.Controls.DataGridTextColumn  5N      ,  &System.Windows.Controls.DataGridColumn   Header$ מ''ס��6D   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6-   �� *Id+6D     5O      $
 שם��6F    6-   �� *Name+6F     5P      $ 
טלפון��6J    6-   �� *
PhoneNum+6J   4  .System.Windows.Controls.DataGridTemplateColumn  5Q      $ ספרים מושאלים��61      CellTemplate 5R      �� 5S   "   .R�5T   &   �� $ BN��6/   -   5         �� DisplayMemberPath$ BookName��5T   a   ��6;   �� *BorrowedBooks+6~   5U   #   5W        5X      $ תאריך השאלה��61   -   5        BTN_ShowDate��   
Visibility$ Hidden=�5X   \    5Y      �� 5Z   "   .R�5[   &   �� -   5        $"��הצג תאריך השאלה��5\   )   5]   )   5^   #   5`      5b      5c      5d      �� 5e   
   -	   5         ��Name$ BTN_Exit��5f      $��1��5g      $��Bottom=�5i      $��Right=�5j         HoverTransparent#��[  5k      ��5l      �� 5m      .R�5n      �� $>��8/DigitalLibrary;component/Resources/Images/exit_icon.png��5o      $��35��5p      $��35��5q      5r      5t      5u      5v      