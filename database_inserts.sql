INSERT INTO Modules (Module_name)
VALUES 
('Module 1: Variables'), -- id 1
('Module 2: Arithmetic and logic'),
('Module 3: Classes and objects'),
('Module 4: Arrays'),
('Module 5: Methods');

INSERT INTO Questions (ModulesId, Question_text)
VALUES 
(1, 'What is the variable type String surrounded by?'), -- id 1
(1, 'Which variable type is the value 35?'), -- id 2
(1, 'Which variable type would you use to save grades?'),
(1, 'String name = "Trine"; int age = BLANK; System.out.println(age)'),
(1, 'double price = BLANK;'),
(1, 'Which of the following statements is true about variable names in Java?'),
(1, 'What is the standard value of an uninitialized int-variable in a class? (e.g: int x;)'),
(1, 'How many bytes does an int use in Java?'),
(1, 'Which of the following statements is the correct method to declare several variables of the same type?'),
(1, 'What value can the data type boolean have?'),

(2, 'If you take the number modulus 5, which answer can you get?'),
(2, 'Which number is being divided by 2? 4+(3+3)/2'),
(2, 'Which operator is used for equality comparison in Java?'),
(2, 'Which operator is used to show that two variables are not equal?'),
(2, 'Which operator is used for arithmetic addition in Java?'),
(2, 'Which operator is used as logical AND in Java?'),
(2, 'What is the output of the following code? int x = 5; int y = 2; int result = x%y; System.out.println(result);'),

(3, 'Which of the following is the correct definition of a class?'),
(3, 'What is an object in Java?'),
(3, 'Which keyword is used to create a new instance of a class?'),
(3, 'What are the main components of a class?'),
(3, 'Which keyword is used to refer to the current instance of an object within a class?'),
(3, 'When a class inherits a property from another class, what do we call it?'),
(3, 'What do we mean by the "state" of an object?'),
(3, 'Fill in the correct keyword to declare the class: public BLANK main { int x = 5; }'),
(3, 'Which of the following is NOT and OOP concept in Java?'),
(3, 'What is the correct way to define a class in Java?'),

(4, 'What is an Array in Java?'),
(4, 'What is an index range of elements in an array in Java?'),
(4, 'How do you access an element in an array in Java?'),
(4, 'What happens if you try to access an array element with an index that is out of bounds?'),
(4, 'Can the length of an array be changed after its creation in Java?'),
(4, 'What symbol is used for a varargs parameter?'),
(4, 'Which animal is declared illegaly? []double lion; double[] tiger; double bear[];'),
(4, 'How do you determine the number of elements in an array? int example[] = new int[5];'),
(4, 'Which is not a true statement about an array?'),
(4, 'How can you declare an array of strings?'),

(5, 'What is the default return type of a method in Java if none is specified?'),
(5, 'Which statement about instance methods is correct?'),
(5, 'What happens if we declare a method both static and final?'),
(5, 'Which of the following will cause a compile-time error in method overriding?'),
(5, 'What is method overloading?'),
(5, 'What is the correct syntax to call a method named display() from the same name class?'),
(5, 'What is the use of varargs in Java?'),
(5, 'Which method is called when a Java program starts execution?'),
(5, 'What is the return type of a method that does not return any value?'),
(5, 'Which of the following is a correct method declaration in Java?');

INSERT INTO Answers (QuestionsId, Is_correct, Answer_text)
VALUES 
(1, 0, '()'),
(1, 0, '**'),
(1, 1, '""'),
(1, 0, '^^'),

(2, 1, 'int'),
(2, 0, 'float'),
(2, 0, 'String'),
(2, 0, 'boolean'),

(3, 0, 'String'),
(3, 0, 'boolean'),
(3, 0, 'int'),
(3, 1, 'char'),

(4, 1, '25'),
(4, 0, '"25"'),
(4, 0, 'name'),
(4, 0, 'int'),

(5, 0, '"29.99"'),
(5, 0, '30'),
(5, 0, '"30"'),
(5, 1, '29.99'),

(6, 0, 'Variable names can be a Java keyword.'),
(6, 1, 'Variable names are case sensitive.'),
(6, 0, 'Variable names can contain special signs such as $ or @.'),
(6, 0, 'Variable names can start with a number.'),

(7, 1, '0'),
(7, 0, 'null'),
(7, 0, '1'),
(7, 0, '""'),

(8, 1, '4'),
(8, 0, '8'),
(8, 0, '2'),
(8, 0, '1'),

(9, 1, 'All are correct.'),
(9, 1, 'int a; int b; int c;'),
(9, 1, 'int a, b, c;'),
(9, 1, 'int a, b = 5, c;'),

(10, 0, '1 or 0'),
(10, 1, 'true or false'),
(10, 0, 'yes or no'),
(10, 0, 'on or off'),

(11, 1, '0-4'),
(11, 0, '0-5'),
(11, 0, '6-10'),
(11, 0, 'It depends on the number.'),

(12, 1, '6'),
(12, 0, '10'),
(12, 0, '7'),
(12, 0, '3'),

(13, 1, '=='),
(13, 0, '='),
(13, 0, '-'),
(13, 0, '"="'),

(14, 1, '!='),
(14, 0, '=!'),
(14, 0, '≠'),
(14, 0, '!=='),

(15, 1, '+'),
(15, 0, '"plus"'),
(15, 0, 'plus'),
(15, 0, '&'),

(16, 1, '&&'),
(16, 0, '||'),
(16, 0, '&'),
(16, 0, '!'),

(17, 1, '1'),
(17, 0, '2.5'),
(17, 0, '2'),
(17, 0, '10'),

(18, 1, 'The template or prototype to create an object with.'),
(18, 0, 'An instance of an object.'),
(18, 0, 'A method in Java.'),
(18, 0, 'A package in Java.'),

(19, 1, 'An instance of a class.'),
(19, 0, 'A method.'),
(19, 0, 'A class template.'),
(19, 0, 'A function.'),

(20, 1, 'New'),
(20, 0, 'This'),
(20, 0, 'Instance'),
(20, 0, 'Object'),

(21, 1, 'Methods and attributes'),
(21, 0, 'Attributes and packages'),
(21, 0, 'Objects and references'),
(21, 0, 'Methods and objects'),

(22, 1, 'This'),
(22, 0, 'Object'),
(22, 0, 'New'),
(22, 0, 'Class'),

(23, 1, 'Inheritance'),
(23, 0, 'Encapsulation'),
(23, 0, 'Polymorphism'),
(23, 0, 'Instantiation'),

(24, 1, 'The value assigned to its instance variables.'),
(24, 0, 'Its behavior'),
(24, 0, 'Its methods'),
(24, 0, 'Its interface'),

(25, 1, 'class'),
(25, 0, 'object'),
(25, 0, 'new'),
(25, 0, 'Main'),

(26, 1, 'Compilation'),
(26, 0, 'Encapsulation'),
(26, 0, 'Inheritance'),
(26, 0, 'Polymorphism'),

(27, 1, 'class Example{}'),
(27, 0, 'public Example{}'),
(27, 0, 'void Example{}'),
(27, 0, 'object Example{}'),

(28, 1, 'A collection of elements with the same type.'),
(28, 0, 'A collection of elements with different types.'),
(28, 0, 'A resizable data structure.'),
(28, 0, 'A container for storing key-value pairs.'),

(29, 1, '0 to length - 1'),
(29, 0, '1 to length'),
(29, 0, '-1 to length - 1'),
(29, 0, '0 to length'),

(30, 1, 'By using the elements index.'),
(30, 0, 'By using the elements value.'),
(30, 0, 'By using the elements key.'),
(30, 0, 'By using the elements label.'),

(31, 1, 'A runtime exception is thrown.'),
(31, 0, 'The program terminates abruptly.'),
(31, 0, 'The element value is set to null.'),
(31, 0, 'The element value is set to 0.'),

(32, 1, 'No, the length of an array is fixed after creation.'),
(32, 0, 'Yes, by using the resize() method.'),
(32, 0, 'Yes, by using the length property.'),
(32, 0, 'No, arrays in Java have a fixed length.'),

(33, 1, '...'),
(33, 0, '..'),
(33, 0, '--'),
(33, 0, '---'),

(34, 1, 'Lion'),
(34, 0, 'Tiger'),
(34, 0, 'Bear'),
(34, 0, 'All options are illegal.'),

(35, 1, 'example.length'),
(35, 0, 'example.length()'),
(35, 0, 'example.size'),
(35, 0, 'example.size()'),

(36, 1, 'An array expands automatically when it is full.'),
(36, 0, 'An array is allowed to contain duplicate values.'),
(36, 0, 'An array understands the concept of ordered elements.'),
(36, 0, 'An array used a zero index to reference the first element.'),

(37, 1, 'String[] myText;'),
(37, 0, 'str[] myText;'),
(37, 0, 'string = myText;'),
(37, 0, 'string[] myText;'),

(38, 1, 'No return type being present produces a compilation error.'),
(38, 0, 'Object'),
(38, 0, 'int'),
(38, 0, 'void'),

(39, 1, 'They require an object to be called.'),
(39, 0, 'They can only be accessed from static methods.'),
(39, 0, 'They are automatically synchronized.'),
(39, 0, 'They must be declared final.'),

(40, 1, 'The method cannot be overridden.'),
(40, 0, 'The method can be overridden.'),
(40, 0, 'Compilation error.'),
(40, 0, 'Runtime error.'),

(41, 1, 'Overriding a method and narrowing its access modifier.'),
(41, 0, 'Overriding a method with the same return type.'),
(41, 0, 'Overriding a method using @Override annotation.'),
(41, 0, 'Overriding a method and throwing fewer checked exceptions.'),

(42, 1, 'Two methods with the same name but different parameters.'),
(42, 0, 'Two methods with the same name and parameters.'),
(42, 0, 'Two methods with the same name but different return types.'),
(42, 0, 'Defining methods inside a loop.'),

(43, 1, 'display();'),
(43, 0, 'class.display();'),
(43, 0, 'void display();'),
(43, 0, 'call display();'),

(44, 1, 'To accept a variable number of arguments.'),
(44, 0, 'To return multiple values.'),
(44, 0, 'To call a method multiple times.'),
(44, 0, 'To pass a list.'),

(45, 1, 'main()'),
(45, 0, 'execute()'),
(45, 0, 'run()'),
(45, 0, 'start()'),

(46, 1, 'void'),
(46, 0, 'null'),
(46, 0, '0'),
(46, 0, 'empty'),

(47, 1, 'public int myMethod(int a, int b)'),
(47, 0, 'public myMethod(int a, int b): int'),
(47, 0, 'myMethod(int a, int b) -> int'),
(47, 0, 'myMethod: int(a,b)');


-- text = 'dette er en text'
-- tall = 30
-- boolean = 0 for false, 1 for true
-- husk semikolon etter alle inserts
-- avslutt med denne: 
-- sqlite3 database.db < DO_NOT_COMMIT.sql
