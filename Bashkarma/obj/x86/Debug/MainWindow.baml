   M S B A M L   `   `   `  ����D  @Bashkarma, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Bashkarma	Bashkarma  5   	       Bashkarma.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   )localclr-namespace:Bashkarma     5   	   -   5          � Title$  
MainWindow��5   	   $	��474��6   $	��681��6)    � WindowStartupLocation$ CenterScreen=�65    � WindowStyle$
 None=�6Z    � AllowsTransparency . ��5      �� 5   
    � 	Resources 5
         Bashkarma.ImageConverter   ImageConverter  DataTemplateItemConverter �� Left
 �� Top �� Path �� Mode	 �� 	Converter  DataTemplateItemDirect  DataTemplateItemStyle
 �� Triggers �� Value �� Binding �� 
TargetName �� Property �� Value��%  &	        &	      7   &	 F    &	 �      5   
   6   �� 5   
   .R�5      �� $	��236��6   $��48��6!   .+�5      �� $��	ImageName��6r   $��32��6�   $��32��6�   $ 8��6�   $ 8��6�   ��6   �� $ 	PictureID�$ TwoWay=�8	   6�   �� 5      $ 56��62   $ 8��6C   $	��172��6R   ��6   �� *Name+6R   5      5      �� 5   
   .R�5      �� $	��236��6   $��48��6!   .+�5      �� $��	ImageName��6L   $��32��6]   $��32��6i   $ 8��6t   $ 8��6�   ��6   �� $ PictureString�$ TwoWay=�6�   �� 5      $ 56��6E   $ 8��6V   $	��172��6e   $��14(�6q   $��Arial)�6   $
��Bold"�6�   ��6   �� $
 Name�$ TwoWay=�6�   5      5      �� 5   
   .R�5      �� $	��236��6   $��48��6!   .+�5      �� $��	ImageName��6   $��32��6)   $��32��65   $ 8��6@   $ 8��6P   6a   �� 5      $ 56��62   $ 8��6C   $	��172��6R   ��6   �� *Name+6R   5       
 5!      �� 5"      $ 0��6O    6   �� $ 	PictureID�$ TwoWay=�.a�5#      �� $ 	ImageName��6    � ��$ Images\0.png��6F   5%      �� 5&      $ 1��6P    6   �� $ 	PictureID�$ TwoWay=�.a�5'      �� $ 	ImageName��6    � ��$ Images\1.png��6F   5)      5+      5-      .��5.      � .+�5/   
   �� -   5        $��
rectangle1��5/   O   $	��384��6   $
��Left=�6!   $��	27,28,0,0q�6<   ���	#FF1A7603$	��Top=�6t   $	��606��6�    �� RadiusX$ 10��6�    �� RadiusY$ 10��6�   ���	#9D006A00 J� Effect 50      3  -System.Windows.Media.Effects.DropShadowEffect  51         
BlurRadius$ 30Q�6#      Color$ 	DarkGreen��63      	Direction$ 0Q�6E      ShadowDepth$ 0Q�6S   53      �� 55   
   -   5        $��listBox1��56      $	��297��55      $
��Left=�6   $��	61,90,0,0q�6:   $	��Top=�56   "   $	��377��6:   $ �� IsSynchronizedWithCurrentItem$
 TrueY�57      #���  58      ��59      �� 5:       �� ImageSource$* $/Bashkarma;component/Images/key1.png��6    j� Stretch$
 None=�6P    j� TileMode$
 None=�6_    j� 
AlignmentY$ Bottom=�6o    j� 
AlignmentX$ Right=�6�    �� Opacity$	 0.2Q�6�   5<      �� 5=   
   -   5        $��button1��5=   a   $��Подписать��6   $��43��6&   $
��Left=�62   $��
470,90,0,0q�6M   $	��Top=�6p   $	��126��6�   {�. 5?      �� 5@   
   -   5        $��label1��5@   h   $*��$Выберите адресатов:��6   $��28��6.   $
��Left=�6:   $��	61,56,0,0q�6U   $	��Top=�6v   ���	#FFF9F3F3$��Expanded&�6�   $
��Bold"�6�   �� 5A   
   -   5        $��button2��5A   ]   $��
Выход��6   $��43��6!   $
��Left=�6-   $��470,147,0,0q�6H   $	��Top=�6l   $	��126��6�   5B      5C      