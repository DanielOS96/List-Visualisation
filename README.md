
<h1>List Visualiser</h1>
This code is architected in the Model View Controller pattern. 
The idea behind this project is to create a way of visualising lists in 3d for use in VR. 
This could be a list of any kind be it a list of files or a list addresses from an excel document.
To change the data source the model just needs to be swapped out. In this project I have included a file browser model for demonstration purposes. 
</br></br></br>
The control panel allows for interaction with the list:

* Left arrow rotates the entire list left.
* Right arrow rotates the entire list right.
* Up arrow moves up a directory.
* The green ‘O’ button opens a file if contact with the list item is made. This button is designed for VR controls where ideally the user would grab a list item and just move it onto the open file button to open a file.
* Hover mouse over list item to show the name of the file.
* Click the list item to show more information (directory and file extension) as well as open the file. Currently there is only an image file viewer.
</br>
By default file explorer will start at C:\Users\danielos96\Downloads\TestFolder. Change the public ‘PathToSearch’ variable on the model to whatever path you want to start the explorer there.
</br>


<h2>Gameplay</h2>
https://youtu.be/ALe4umxwr_w
</br></br></br>


![](https://github.com/DanielOS96/List-Visualisation/blob/master/Screenshots/1.png)


