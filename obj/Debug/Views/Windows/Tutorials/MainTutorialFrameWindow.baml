   M S B A M L   `   `   `  ����I  EDigitalLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_4clr-namespace:DigitalLibrary.Views.Windows.Tutorials&DigitalLibrary.Views.Windows.Tutorials  5   	   D    >DigitalLibrary.Views.Windows.Tutorials.MainTutorialFrameWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Flocal4clr-namespace:DigitalLibrary.Views.Windows.Tutorials     5   	   -   5          � Title$  MainTutorialFrameWindow��5   	   $	��450��5	   	   $	��450��5
   	   $	��800��5   	   $	��800��5   	   $	��800��5   	    � WindowStartupLocation$ CenterScreen=�5   	   ���	#FF98B2B9 � WindowStyle$
 None=�5   	   $��Georgia Pro Black)�5   	   $y�Visible=�5   	   $��20(�5   	    � Topmost .  � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $E ?/DigitalLibrary;component/Views/Resources/HoverTransparent.xaml?�6%   %    5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5       $��* �6   .+�5#   
   �� -   5         ��Name$ SP_Nav��5$      $��0��5%      	 �� Orientation$	 
Horizontal=�5&      $��Center=�5'      $��Center=�5(      .+�5)      �� -   5        
 ��Name$
 BTN_Back��5*      $
��10,0q�5+      $��50��5,      $��50��5-      {�.     HoverTransparent#��[  50      .��51      �� -   5         ��Name$ BTN_Back_Image��51      52      �� 53      -   5        $
 BTN_Next��54      $
��10,0q�55      $��50��56      $��50��57      {�.  #��[  5:      .��5;      �� -   5        $ BTN_Next_Image��5;      5<      5=      �� 5>   
   -   5         ��Name$ SV��5?      $
��Auto=�5@      $
��Auto=�5A      $��1��5B      $��5,10,5,0q�5C      .��5D       � -   5          �Name$ 	MainFrame��5E        � NavigationUIVisibility$ Hidden=�5F      5G      5H      5I      