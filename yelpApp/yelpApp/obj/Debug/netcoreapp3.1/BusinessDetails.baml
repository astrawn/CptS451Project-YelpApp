   M S B A M L   `   `   `  ����B  >yelpApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:milestone1
milestone1  5   	   p lDotNetProjects.DataVisualization.Toolkit, Version=6.0.87.0, Culture=neutral, PublicKeyToken=79778c5f2eed289b�iclr-namespace:System.Windows.Controls.DataVisualization;assembly=DotNetProjects.DataVisualization.Toolkit)System.Windows.Controls.DataVisualization 5	   	   �rclr-namespace:System.Windows.Controls.DataVisualization.Charting;assembly=DotNetProjects.DataVisualization.Toolkit2System.Windows.Controls.DataVisualization.Charting 5
   	        milestone1.BusinessDetails   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:milestone1     5   	   xDViclr-namespace:System.Windows.Controls.DataVisualization;assembly=DotNetProjects.DataVisualization.Toolkit     5	   	   �DVCrclr-namespace:System.Windows.Controls.DataVisualization.Charting;assembly=DotNetProjects.DataVisualization.Toolkit     5
   	     � Title$  BusinessDetails��5   	   $	��439��6!   $	��900��6.   .��5      � $	��406��6   $	��Top=�6   .+�5   
   �� -   5         ��Name$ 
bnameLabel��5      $��Business Name��6$   $
��Left=�6<   $��70,8,0,0q�6W   $	��Top=�6i   $��14(�6�   �� 5   
   -   5        $ 
snameLabel��5      $��State��6$   $
��Left=�64   $��	88,39,0,0q�6O   $	��Top=�6b   $��14(�6z   �� 5   
   -   5        $ 
cnameLabel��5      $
��City��6$   $
��Left=�63   $��	91,70,0,0q�6N   $	��Top=�6a   $��14(�6y   �� 5   
   -   5         ��Name$ sname��5      $
��Left=�6!   $��
208,43,0,0q�6<   $�� ��6P    �� TextWrapping$
 Wrap=�6X   $	��Top=�6l   $	��203��6�   $��20��6�   $��14(�6�    �� 
IsReadOnly$
 True��6�   �� 5   
   -   5        $ cname��5      $
��Left=�6!   $��
208,74,0,0q�6<   $�� ��6P   $
 Wrap=�6X   $	��Top=�6l   $	��203��6�   $��20��6�   $��14(�6�   $
 True��6�   �� 5   
   -   5        $ bname��5      $
��Left=�6!   $��
208,16,0,0q�6<   $�� ��6P   $
 Wrap=�6X   $	��Top=�6l   $	��203��6�   $��20��6�   $��14(�6�   $
 True��6�   �� 5   
   -   5        $ inStateLabel��5      $(��"Number of businesses in same state��6&   $
��Left=�6S   $��
431,13,0,0q�6n   $	��Top=�6�   $��14(�6�   �� 5   
   -   5        $ inCityLabel��5      $'��!Number of businesses in same city��6%   $
��Left=�6Q   $��
431,40,0,0q�6l   $	��Top=�6�   $��14(�6�   �� 5   
   -	   5        $ inState��5      $
��Left=�6#   $��
686,18,0,0q�6>   $�� ��6R   $
 Wrap=�6Z   $	��Top=�6n   $��50��6�   $��20��6�   $��14(�6�   $
 True��6�   �� 5   
   -
   5        $ inCity��5      $
��Left=�6"   $��
686,49,0,0q�6=   $�� ��6Q   $
 Wrap=�6Y   $	��Top=�6m   $��50��6�   $��20��6�   $��14(�6�   $
 True��6�   �� 5   
   $��10,94,10,10q�6   .@�5      �� $
��Tips��6   .��5      &   System.Windows.Controls.DataGrid  -   5          Name$ tipGrid��5      $	��234��6,   $	��854��69      CanUserAddRows$ False��6E      CanUserDeleteRows$ False��6\      CanUserReorderColumns$ False��6v   	   CanUserSortColumns$
	 True��6�   
   
IsReadOnly$

 True��6�   $��Center=�6�      Columns 5      4  .System.Windows.Controls.DataGridTemplateColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Like Tip��61      CellTemplate 5      �� 5   "   .R�5   &   ��  ��Name$ 
likeButton��6-   -   5        $
��LIKE��5   A   6P   5   #   5!      5#      5$      �� 5%      $��Checkins��6   .��5&      >  8System.Windows.Controls.DataVisualization.Charting.Chart  -   5        $��mcChart��5&   =   
 �� Top$ 80��6    �� Left$ 10��6,   $	��261��6L   
����İ�$��	10,0,10,0q�6u      Series 5(      E  ?System.Windows.Controls.DataVisualization.Charting.ColumnSeries  ?  9System.Windows.Controls.DataVisualization.Charting.Series   Title$ Total Checkins Per Month��6+   H  BSystem.Windows.Controls.DataVisualization.Charting.DataPointSeries   IndependentValueBinding 5)   +   ��  �� Path$	 Key�   DependentValueBinding 5*   +   �� $ Value�5+      5-      5.      �� 5/      $��Friends' Reviews��6   .��50        -   5        $ tipGrid2��50      $	��234��6-   $	��854��6:   $ False��6F   $ False��6]   $ False��6w   $
	 True��6�   $

 True��6�   $��Center=�6�    51        52      $ Like Tip��61    53      �� 54   "   .R�55   &   �� $ 
likeButton��6-   -   5        $
��LIKE��55   A   6P   56   #   58      5:      5;      5<      �� 5=   
   -   5        $ refreshButton��5=      $��Refresh��6(   $
��Left=�6:   $��28��6U   $��
673,80,0,0q�6a   $	��Top=�6u   $	��106��6�   6�   �� 5>   
   -   5        $ checkinButton��5>      $��Check In��6(   $
��Left=�6;   $��28��6V   $��
441,80,0,0q�6b   $	��Top=�6v   $	��106��6�   6�   �� 5?   
   -   5        $ addTip��5?      $��Add Tip��6!   $
��Left=�63   $��28��6N   $��557,80,0,298q�6Z   $	��106��6p   6|   5B      5C      