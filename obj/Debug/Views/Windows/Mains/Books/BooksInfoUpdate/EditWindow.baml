   M S B A M L   `   `   `  ����I  EDigitalLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�Fclr-namespace:DigitalLibrary.Views.Windows.Mains.Books.BooksInfoUpdate8DigitalLibrary.Views.Windows.Mains.Books.BooksInfoUpdate  5   	   I    CDigitalLibrary.Views.Windows.Mains.Books.BooksInfoUpdate.EditWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   XlocalFclr-namespace:DigitalLibrary.Views.Windows.Mains.Books.BooksInfoUpdate     5   	   -   5          � Title$  
EditWindow��5   	   $	��450��5	   	   $	��450��5
   	    � WindowStartupLocation$ CenterScreen=�5   	   ���	#FFFFCC8F$��RightToLeft=�5   	    � WindowStyle$
 None=�5   	   $��Georgia Pro Black)�5   	   $y�Visible=�5   	   $	��450��5   	   $	��450��5   	   $	��450��5   	   $	��450��5   	    � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $E ?/DigitalLibrary;component/Views/Resources/HoverTransparent.xaml?�6%   %    5      .��5      � $��20q�5   	   .+�5   
   �� -   5         ��Name$ MainSP��5        �� Orientation$ Vertical=�5!      $��Center=�5"      $��Center=�5#      .+�5$      �� $ 
Horizontal=�5%      $
��0,10q�5&      .+�5'      �� -   5         ��Name$ 
L_BookName��5(      $��שם הספר:��5)      $��20(�5*      �� 5,      -   5        	 ��Name$	 TB_BookName��5-      $	��200��5.      $��25��5/      $��20(�50      $��Center=�51      $
��Left=�52      53      �� 55      $ 
Horizontal=�56      $
��0,10q�57      .+�58      �� -   5        $ L_BookGenre��59      $��
ז'אנר:��5:      $��0,0,51,0q�5;      $��20(�5<      �� 5>      -   5        
 ��Name$
 CB_BookGenre��5?      $��20(�5@      $	��200��5A      .@�5B      �� -   5         ��Name$ Novel��5B   #   $��רומן��62   �� 5C      -   5        $ Comics��5C   #   $��קומיקס��63   �� 5D      -	   5        $ Fantasy��5D   #   $��פנטנזיה��64   �� 5E      -
   5        $ Thriller��5E   #   $��
מותחן��65   �� 5F      -   5        $ History��5F   #   $��היסטוריה��64   �� 5G      -   5        $ ScienceFiction��5G   #   $��מדע בדיוני��6;   �� 5H      -   5        $
 Kids��5H   #   $��
ילדים��61   5I      5J      �� 5M      $ 
Horizontal=�5N      $
��0,10q�5O      .+�5P      �� -   5        $ L_Published��5Q      $��הוצל''א:��5R      $��0,0,60,0q�5S      $��20(�5T      (  "System.Windows.Controls.DatePicker  5U      -   5          Name$ DP_Published��5V      $��0q�5W      $��20(�5X      5Z      �� 5\      $ 
Horizontal=�5]      $
��0,10q�5^      .+�5_      �� -   5        $ L_Author��5`      $��	מחבר:��5a      $��20(�5b      $��0,0,28,0q�5c      �� 5d      -   5        $	 	TB_Author��5e      $	��200��5f      $��25��5g      $��20(�5h      $��Center=�5i      $
��Left=�5j      5k      �� 5m      $ 
Horizontal=�5n      $��Center=�5o      $
��0,10q�5p      .+�5q      �� -   5        $ L_Beforehand��5r      $��Center=�5s      $��תמונת ספר:��5t      $��20(�5u      $��0,0,85,0q�5v      �� 5x      -   5         ��Name$ BTN_FilePicker��5y      $��בחר כאן��5z      $	��100��5{      $��30��5|      $��20(�5}      5~      �� 5�      -   5         ��Name$ TB_FileImage��5�      $��50��5�      $��50��5�      $��Center=�5�      $��Center=�5�      5�      �� 5�      $ 
Horizontal=�5�      $
��0,10q�5�      .+�5�      �� -   5        $ L_BookQuantity��5�      $��כמות ספרים:��5�      $��20(�5�      $��	0,0,126,0q�5�      �� 5�      -   5        $	 TB_Quantity��5�      $��1��5�      $��60��5�      $��20(�5�      $��Center=�5�      5�      5�      �� 5�      -   5        $ BTN_EditBook��5�      $��עדכן��5�      $��80��5�      $��20(�5�       �� 	IsDefault . $��30��5�      5�      5�      �� 5�   
   -   5        $ BTN_Exit��5�      $��Bottom=�5�      $��Right=�5�      $��0,0,-20,-20q�5�         HoverTransparent#��[  5�      ��5�      �� 5�      .R�5�      �� $>��8/DigitalLibrary;component/Resources/Images/exit_icon.png��5�      $��35��5�      $��35��5�      5�      5�      5�      5�      