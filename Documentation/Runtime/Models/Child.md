[[Note permanente]] 
MOC : [[NCore]]
Source :  
Projets : 
Tags : #Core
Date : 07/10/2022

# Behaviour 
### Description :

**Typeof** : [[Child]]
**Type** : Abstract Class
**Inheritage** : None

**Description** : [[Child]] is an object who has a reference of another. 

**Parameters** : 
*T* : Parent of the child.

### Protected Methods :
* **virtual void Init**(T parent) : Execute TryMakeThisTheInstance.


### Public Methods : 
* **static void DestroyInstance()** : Will destroy the instance GameObject's if is existing.

***

# Properties
### Serialized Properties : 

##### **bool dontDestroyOnLoad** : 
Set : Protected
Get : Protected
Description : Define if the instance GameObject's will be destroy if the scene is unload.

### Other Properties : 

##### **T instance** :
**Set** : Protected
**Get** : Public
**Description** : Current instance.