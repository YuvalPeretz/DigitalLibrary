   M S B A M L   `   `   `  ����I  EDigitalLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulli9clr-namespace:DigitalLibrary.Views.Windows.Mains.Students+DigitalLibrary.Views.Windows.Mains.Students  5   	   D    >DigitalLibrary.Views.Windows.Mains.Students.StudentsReturnBook   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Klocal9clr-namespace:DigitalLibrary.Views.Windows.Mains.Students     5   	   -   5          � Title$  StudentsReturnBook��5   	   $	��720��5	   	   $	��720��5
   	   $	��720��5   	   $	��300��5   	   $	��300��5   	   $	��300��5   	    � WindowStartupLocation$ CenterScreen=�5   	   ���	#FF5EAEFF$��RightToLeft=�5   	    � WindowStyle$
 None=�5   	   $��Georgia Pro Black)�5   	   $y�Visible=�5   	    � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $E ?/DigitalLibrary;component/Views/Resources/HoverTransparent.xaml?�6%   %    5      .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5       $��* �6   .+�5#   
   �� $��0��5$      $��
-32,30,0,0q�5%       �� Orientation$ 
Horizontal=�5&      $��Center=�5'      .+�5)      �� $��חיפוש תלמיד:��5*      $��20(�5+      �� 5-      -   5         ��Name$ 	CB_Filter��5.      $��	10,0,10,0q�5/      $	��100��50      $��15(�51      {�.  	 �� SelectedIndex$	 0��53      .@�54      �� $��15(�6   $
��שם��6-   �� 55      $��15(�6   $��
טלפון��6-   56      �� 57      -   5        
 ��Name$
 TB_SearchBox��58      $	��250��59      $��15(�5:      {�.  $
��Left=�5<      $��Center=�5=      5>      5?      �� 5A   
   $��1��5B      $��
66,30,19,0q�5C      $ 
Horizontal=�5D      $��Center=�5E      .+�5G      &   System.Windows.Controls.DataGrid  -   5          Name$ DG_StudentsList��5H      $��20(�5I      $��Center=�5J      $��Center=�5K         AutoGenerateColumns .  $	��170��5M      $	��170��5N      $	��470��5O      $	��470��5P      $   HorizontalScrollBarVisibility$
 Auto=�5Q         Columns 5R      0  *System.Windows.Controls.DataGridTextColumn  5S      ,  &System.Windows.Controls.DataGridColumn   Header$ מ''ס��6@   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6)   �� *Id+6@     5T      $
 שם��6B    6)   �� *Name+6B     5U      $ 
טלפון��6F    6)   �� *
PhoneNum+6F   4  .System.Windows.Controls.DataGridTemplateColumn  5V      $ ספרים מושאלים��6-      CellTemplate 5W      �� 5X      .R�5Y   "   �� $ BN��6+   -   5         �� DisplayMemberPath$ BookName��5Y   ]   ��67   �� *BorrowedBooks+6z   5Z      5\        5]      $ תאריך השאלה��6-   -   5        BTN_ShowDate��   
Visibility$ Hidden=�5]   X    5^      �� 5_      .R�5`   "   �� -   5        $"��הצג תאריך השאלה��5a   )   5b   )   5c      5e      5g      �� 5i      -   5         ��Name$ BTN_ReturnBook��5j      $��30��5l       �� 	IsDefault . $��5,0,0,0q�5n      $�� החזר ספר ��5o      $��20(�5p      5q      �� 5s   
   -	   5        $ BTN_Exit��5t      $��1��5u      $��Bottom=�5w      $��Right=�5x         HoverTransparent#��[  5y      ��5z      �� 5{      .R�5|      �� $>��8/DigitalLibrary;component/Resources/Images/exit_icon.png��5}      $��35��5~      $��35��5      5�      5�      5�      5�      