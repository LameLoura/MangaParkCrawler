   M S B A M L   `   `   `  ����  WPFWebCrawler    WPFWebCrawler.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   AtoolKit-http://schemas.microsoft.com/wpf/2008/toolkit     5   	     � Title$  
MainWindow��5   	   $	��350��6   $	��598��6)    � 	Resources 5         ItemCollectionViewSource �� CollectionViewType��%"   &	        �� 5   
   " ��6A   5	      .��5      � .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��dataGridMangaList��5       � DataContext# [  5         AutoGenerateColumns .     CanUserAddRows .  $	��141��5   (   $
��Left=�5      $��	20,25,0,0q�6.   $	��Top=�5   ,   $	��544��6D   ��5      ��  5      )  #System.Windows.Controls.DataGridRow �� 
TargetType��%E   '        �� 5      "  6   .�5      -   5        5      5         Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Manga��6C   	   
FontWeight$
	 Bold"�6R   
   
IsReadOnly
 . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   ��  �� Path$
 Name�6d     5      $ Last Read Chapter��6J    6%   �� $ ReadChapter�6J     5      $ Latest Chapter��6L   
 .  6%   �� $ LatestChapter�6d     5      $ Unread Chapter��6L   
 .  6%   �� $ UnreadChapter�6d   5(      � 5*   	   -   5        $��pgbUpdateData��5*   /    � Minimum$ 0Q�6    � Maximum$	 100Q�6!   $��21,182,158,109q�6D   �� 5,   
   -   5        $��btnSave��5,   \   $
��Save��6   $��35��6    $
��Left=�6,   $��351,264,0,0q�6G   $	��Top=�6k   $	��140��6�   6�   �� 5-   
   -   5        $��btnAddManga��5-   a   $��	Add Manga��6   $��35��6%   $
��Left=�61   $��205,264,0,0q�6L   $	��Top=�6t   $	��140��6�   6�   �� 5.   
   -   5        $��btnUpdateLatestCh��5.   c   $��Update Data��6   $��59��6'   $
��Left=�63   $��424,180,0,0q�6N   $	��Top=�6|   $	��140��6�   6�   �� 5/   
   -   5        $��lblUpdateProgress��5/   V   $�� ��6   $��28��6   $
��Left=�6'   $��
20,211,0,0q�6B   $	��Top=�6o   50      51      