import React, { Component } from "react";
import { Link } from 'react-router-dom';

class Class extends Component {
    constructor(props) {
        super(props) //since we are extending class Table so we have to use super in order to override Component class constructor
        this.state = { //state is by default an object
            students: [
                { id: 1, name: 'Мельничук Станіслав', age: 21, email: 'wasif@email.com' },
                { id: 2, name: 'Орелецький Валентин', age: 19, email: 'ali@email.com' },
                { id: 3, name: 'Продан Анатолій', age: 16, email: 'saad@email.com' },
                { id: 4, name: 'Роєк Анастасія', age: 25, email: 'asad@email.com' },
                { id: 5, name: 'Стрільчук Вадим', age: 25, email: 'asad@email.com' },
                { id: 6, name: 'Романовський Михайло', age: 25, email: 'asad@email.com' },
                { id: 7, name: 'Щур Олександр', age: 25, email: 'asad@email.com' },
                { id: 8, name: 'Чебан Владислав', age: 25, email: 'asad@email.com' },
                { id: 9, name: 'Тихович Михайло', age: 25, email: 'asad@email.com' },
                { id: 10, name: 'Тарица Олександр', age: 25, email: 'asad@email.com' },
                { id: 11, name: 'Сілімір Руслан', age: 25, email: 'asad@email.com' },
            ]
        }
    }

    renderTableData() {
        return this.state.students.map((student, index) => {
            const { id, name, age, email } = student //destructuring
            return (
                <tr key={id}>
                    <td>{id}</td>
                    <Link to="/userProfile"><td>{name}</td></Link>
                    <td>{age}</td>
                    <td>{email}</td>
                </tr>
            )
        })
    }
    renderTableHeader() {
        let header = Object.keys(this.state.students[0])
        return header.map((key, index) => {
            return <th key={index}>{key.toUpperCase()}</th>
        })
    }

    render() {
        return (
            <div>
                <h1 id='title'>Список студентів</h1>
                <table id='students'>
                    <tbody>
                        <tr>{this.renderTableHeader()}</tr>
                        {this.renderTableData()}
                    </tbody>
                </table>
            </div>
        )
    }
}

export default Class;