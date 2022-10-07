MOC : [[Library]]
Source :  
Projets : 
Tags : #Core
Date : 07/10/2022

# Behaviour 
### Description :

**Typeof** : [[NMath]]
**Type** : Abstract Class
**Inheritage** : Monobehaviour

**Description** : [[Singleton]] is a class that have only one instance. 

**Parameters** : 
*T* : [[Singleton]]<*T*>


### Protected Methods :
* **virtual void Awake**() : Execute TryMakeThisTheInstance.

* **void TryMakeThisTheInstance**() : Try make the object who is executing method the instance. If another instance already exist, the object will destroy itself.


### Public Methods : 
* **static void DestroyInstance**() : Will destroy the instance GameObject's if is existing.

***

# Properties
### Constant Properties : 

##### **bool dontDestroyOnLoad** : 
Set : Protected
Get : Protected
Description : Define if the instance GameObject's will be destroy if the scene is unload.