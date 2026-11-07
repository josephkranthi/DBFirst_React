import { useEffect, useState } from 'react';
import './App.css';

function App() {
    const [employees, setEmployees] = useState();
    const [name, setName] = useState('');
    const [salary, setSalary] = useState('');

    useEffect(() => {
        fetchEmployees();
    }, []);

    async function fetchEmployees() {
        const res = await fetch('/api/Employee');
        if (res.ok) {
            const data = await res.json();
            setEmployees(data);
        }
    }

    async function createEmployee(e) {
        e.preventDefault();
        const emp = { name, salary: parseFloat(salary || 0) };

        const res = await fetch('/api/Employee', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(emp)
        });

        if (res.ok) {
            setName('');
            setSalary('');
            fetchEmployees();
        }
    }

    async function deleteEmployee(id) {
        const res = await fetch(`/api/Employee/${id}`, { method: 'DELETE' });
        if (res.ok) fetchEmployees();
    }

    const contents = employees === undefined
        ? <p><em>Loading employees... Please start the ASP.NET backend.</em></p>
        : (
            <table className="table table-striped" aria-labelledby="tableLabel">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Name</th>
                        <th>Salary</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {employees.map(emp =>
                        <tr key={emp.id}>
                            <td>{emp.id}</td>
                            <td>{emp.name}</td>
                            <td>{emp.salary}</td>
                            <td>
                                <button onClick={() => deleteEmployee(emp.id)}>Delete</button>
                            </td>
                        </tr>
                    )}
                </tbody>
            </table>
        );

    return (
        <div>
            <h1 id="tableLabel">Employees</h1>
            <p>Simple CRUD demo using the Employee API.</p>

            <form onSubmit={createEmployee} style={{ marginBottom: 20 }}>
                <input placeholder="Name" value={name} onChange={e => setName(e.target.value)} />
                <input placeholder="Salary" value={salary} onChange={e => setSalary(e.target.value)} />
                <button type="submit">Add</button>
            </form>

            {contents}
        </div>
    );
}

export default App;
