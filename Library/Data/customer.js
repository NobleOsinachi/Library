class Customer {
    constructor(firstName, lastName, dateOfBirth) {
        this.FirstName = firstName;
        this.LastName = lastName;
        this.DateOfBirth = new Date(dateOfBirth);
        this.Age = new Date().getFullYear() - this.DateOfBirth.getFullYear();
    }

    main() {
        console.log(`${this.FirstName}, you are ${this.Age} years old since ${this.DateOfBirth}`);
    }
}

//console.log(
new Customer("Noble", "Osinachi", "1995-12-25").main()
    //);
