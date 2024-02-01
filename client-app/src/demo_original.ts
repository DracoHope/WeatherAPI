const data = 6;
// Typescript can't allow a constant to be reassign
// data = 2;
console.log(data);

let numData = 8;
// Typescript know the previous assignment is a number therefore alswo prompt error when we reassign to a string or char
// numData = '8'
console.log(numData);

/*
    If we want to have a variable that can be either a number or later to be a string then we must do the following

    define the type as <variableaName: string | number>
*/
// Variable can be a string or number datatype
let randomTypeData: string | number = "88";
console.log(typeof randomTypeData + ": " + randomTypeData);

randomTypeData = 88;

console.log(typeof randomTypeData + ": " + randomTypeData);

/*
    We can actually specify a variable to be a specific type as follow
*/
// Variable of number only
const numberType = 86;

// Variable of string data type only
const stringType = "abc";

/*
    Using OOP in Typescript
    A list or disctionary are consider as an object in Typescript and  in Javascript.

    The <makeSound> is a function and the input parameter is "sound"
    The typescript need us to declare the datatype of the input parameter

    If we want the input parameter to be of specific data type, then we can use the keyword <string> or <number> or others object datatype
    For this case we define as a string data type
*/
const duck1 = {
  name: "huey",
  numLegs: 2,
  makeSound: (sound: string) => console.log(sound),
};
duck1.makeSound("miao");
console.log(duck1);

const duck2 = {
  name: "boy",
  numLegs: 2,
  makeQuack: () => console.log("quak"),
};
duck2.makeQuack();
duck2.name = "John";

console.log(duck1.makeSound("miao"));
console.log(duck2.makeQuack());

export const ducks = [duck1, duck2];
console.log(ducks);


/*
    Typescript also have <Interface> support
    such as to ensure an object of a specific data type
    create an Interface to ensure those object id of specific data type

    We create an object of <Dog> and specify the properties ans the data type as well.
    Any created object from this <Dog> interface must follow the specific properties name and data type
*/

interface Dog {
    breed: string,
    numLegs: number,
    makeSound: (sound: string) => void;
}

const dog1: Dog = {
    breed: "Chi Hua Hua",
    numLegs: 4,
    makeSound: (sound: string) => console.log(sound)
}
dog1.makeSound('wof wof');

const: dog2: Dog = {
    breed: "Sherphere",
    numLegs: 4,
    makeSound: (sound: string) => console.log(sound)
}
dog2.makeSound('band bang');