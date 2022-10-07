MOC : [[Spline]]
Dependancies : 
Tags : #Core
Date : 07/10/2022

# Behaviour 
### Description :

**Typeof** : [[NCurve]]
**Type** : Sealed Class
**Inheritage** : 

**Description** : [[NCurve]] is a class that define a curve for a [[Spline]]. 

### Public Methods : 
* **Vector3 ComputePoint** : Return position depending on a t value.
* **Vector3 ComputePointDistance** : Return position depending on a value.
* **float DistanceToT** : Convert t value to distance in units.
* **Vector3 ComputePointUniform** : Return position depending on a t value in using parameterization.
* **Vector3 Direction** : Output all local direction of curvePoint computed in using t value.
* **Vector3 DirectionUniform** : Output all local direction of curvePoint computed in using t value with uniform method.
* **void Update** : Update the curve.
* **Vector3 Forward** : Return the forward direction.
* **Vector3 Up** : Return the up direction.

# Properties

### Serialized Properties : 

##### **public CurveType type** :
**Description** : Type of the curve.

##### **public bool loop** :
**Description** : Define if the curve is a loop.

##### **public List<[[NHandle]]> handles** :
**Description** : List of all the hanldes of the curve.


### Other Properties : 

##### **public float curveLength** :
**Description** : Total length of the curve in unit.
**Set** : Private
**Get** : Public