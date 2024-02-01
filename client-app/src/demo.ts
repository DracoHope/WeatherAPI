/*
    We need to export this "interface" for other function to implement this object as data 

    



*/

export interface Duck {
  name: string;
  numLegs: number;
  makeSound: (sound: string) => void;
}

const duck1: Duck = {
  name: "huey",
  numLegs: 2,
  makeSound: (sound: string) => console.log(sound),
};
// duck1.makeSound("miao");
// console.log(duck1);

const duck2: Duck = {
  name: "boy",
  numLegs: 2,
  makeSound: () => console.log("quak"),
};
// duck2.makeQuack();
// duck2.name = "John";

// console.log(duck1.makeSound("miao"));
// console.log(duck2.makeQuack());

// export const ducks = [duck1, duck2];
// console.log(ducks);
export const ducks = [duck1, duck2];
