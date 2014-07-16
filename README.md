RUL
===

Version 1.0.0
This library is designed to simplify randomization and the creation of procedurally generated content. RUL is capable of generating 
pseudo-random numbers, vectors, colors and noise, as well as randomly modifying existing objects.

Rul is split into four modules :
Rul,
RulVec (for vector randomization), 
RulCol (for color randomization) and
RulNoise (for creating noise) .
All these modules are implemented as static classes in the RUL namespace.

Other versions
---------------------
This version of RUL comes with custom vector and color types, but ports for [Unity](https://github.com/CaptainBubbles/RUL_Unity) and [MonoGame](https://github.com/CaptainBubbles/RUL_MonoGame) exist.

Setting up RUL
----------------------
The easiest way to get started with RUL is to check out the tutorials in the [wiki](https://github.com/CaptainBubbles/RUL/wiki). They cover everything from downloading the source to generating Perlin noise.

Code samples : 
----------------------
    using RUL;
    ...
    long myLong = Rul.RandLong(700,1000); // Creates random long between 700 and 1000

    long myInt = Rul.RandInt(5,10,InclusionOptions.Upper); //Returns random int between 6 and 10

    string name = Rul.RandElement("Jon","Ned","Bran"); //Returns one of the given elements

    int probablyOne = Rul.RandElement(new int[] {1,2,3},0.9); //Returns 1 in nine out of ten cases

    Vec3 unitVec = RulVec.RandUnitVec3(); //Returns random 3D vector with length 1
    
    Col lightColor = RulCol.RandCol(200); //Returns light color
    
    Col darkRed = RulCol.RandCol(Hues.Red, LuminosityTypes.Dark); //Returns dark shade of red

    float[,] noise = RulNoise.RandPerlinNoise2(400,400); //Returns perlin noise 

License
-----------
This library is published under the very permissive MIT license. See [http://opensource.org/licenses/MIT](http://opensource.org/licenses/MIT) for information on what you can and cannot do with this software.
