# CurveBased3DAnimator
A simple tool to make script based animation using animation curves
![Alt text](./ReadMeImages/AnimationTestGif.gif "Preview")
-Unity 2021.3.1f1

#HOW TO USE

**To Get Started You'll need to do 3 things**
* Create a Gameobject with the "Custom 3D Animator" component.
![Alt text](./ReadMeImages/Component.png "Component")
* Create a customAnimation scriptable object (in project window: right click -> Create -> Custom Animations -> Animation) and set the curves at will.
![Alt text](./ReadMeImages/Creation.png "ScriptableObject creation")
![Alt text](./ReadMeImages/Data.png "ScriptableObject")
* Press the "Start Animation" button and tweak the values if needed;

Now you should see your object animating, **GOOD JOB**!

To animate the object at runtime simply get a reference to the script and then call one of the "StartAnimation()" method's overloads, changing the parameters based on the situation.


#Known Issues

* The curves validation system (if the last keframe time isn't 1 a new keyframe is created to keep the curve normalized) works only when the scriptableObject's inspector gets repainted and not when a curve gets edited.



