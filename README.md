# AcademyMVC
This is an ASP.NET MVC project. It was a university assignment. 

I've used HTML5/CSS3 , Bootstrap , jQuery to build the frontend part. It's tested on Visual Studio 2017 and SQL Server 2017.

The project is an Academy for online tutorials. You must create an account (ASP.NET Identity) in order to sign for a course, but it needs to has free spaces. After that you can start reading the lessons. 

If you are in Administrator role , you can manage users (make/remove admin rights) , add/remove disciplines and lessons.
Lessons are written in a .txt file (with StreamWriter) and read from there as well (StreamReader). Those files are saved in Views/Disciplines/Lessons.

  /Home/Index
![Index](https://github.com/BackNot/AcademyMVC/blob/master/PicturesOfProject/AcademyIndex.png)

footer:
![Footer](https://github.com/BackNot/AcademyMVC/blob/master/PicturesOfProject/AcademyFooter.png)

From 'Courses' tab you can choose , what courses you want to take.
/Home/Courses
![Footer](https://github.com/BackNot/AcademyMVC/blob/master/PicturesOfProject/AcademyCourses.png)

But you need to login first or register.
(Login tab)
![Login](https://github.com/BackNot/AcademyMVC/blob/master/PicturesOfProject/AcademyLogin.png)

You can see what courses you chose in 'My Courses' tab.
![My Courses](https://github.com/BackNot/AcademyMVC/blob/master/PicturesOfProject/AcademyMyCourses.png)

From the 'View lessons' button you can start learning!
![Lessons](https://github.com/BackNot/AcademyMVC/blob/master/PicturesOfProject/AcademyMyCourses2.png)

When you click the green button you can see the lesson (Don't mind that the lesson is lame, it's used as an example):
![Lesson](https://github.com/BackNot/AcademyMVC/blob/master/PicturesOfProject/AcademyLesson.png)


If you are an Administrator you have access to this panel (if you aren't you can't see the 'Admin panel' option)

![Admin Panel](https://github.com/BackNot/AcademyMVC/blob/master/PicturesOfProject/AcademyAdminPanel.png)


You can do a lot of things. For example you can add lessons:

Note: All html symbols that we enter are not going to be sanitized. This should never happen in real environment, but for the sake of testing I allow it here. We can escape < and > by using [[<]] and [[>]].
![Add lesson](https://github.com/BackNot/AcademyMVC/blob/master/PicturesOfProject/AcademyAddLesson.png)

Result:
![Add lesson2](https://github.com/BackNot/AcademyMVC/blob/master/PicturesOfProject/AcademyLesson2.png)


You can remove discipline:
![RemoveDiscipline](https://github.com/BackNot/AcademyMVC/blob/master/PicturesOfProject/RemoveDiscipline.png)


Or remove lessons:
![RemoveLessons](https://github.com/BackNot/AcademyMVC/blob/master/PicturesOfProject/RemoveLesson.png)


