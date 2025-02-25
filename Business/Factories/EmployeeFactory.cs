﻿using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class EmployeeFactory
{
    public static EmployeeEntity CreateEntity(EmployeeRegistrationForm form)
    {
        return new EmployeeEntity
        {
            EmploymentNumber = form.EmploymentNumber,
            FirstName = form.FirstName,
            LastName = form.LastName,
        };
    }

    public static Employee Create(EmployeeEntity entity)
    {
        return new Employee
        {
            Id = entity.Id,
            EmployementNumber = entity.EmploymentNumber,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
        };
    }

    public static EmployeeEntity CreateEntity(Employee employee)
    {
        return new EmployeeEntity
        {
            Id = employee.Id,
            EmploymentNumber = employee.EmployementNumber,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
        };
    }

    public static ProjectRegistrationForm CreateProjectForm(Employee employee)
    {
        return new ProjectRegistrationForm()
        {
            ProjectManager = new EmployeeRegistrationForm 
            { 
                Id = employee.Id, 
                EmploymentNumber = employee.EmployementNumber,
                FirstName = employee.FirstName, 
                LastName = employee.LastName  
            },
        };
    }

    public static EmployeeRegistrationForm CreateEmployeeForm(Employee employee)
    {
        return new EmployeeRegistrationForm
        {
            Id = employee.Id,
            EmploymentNumber = employee.EmployementNumber,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
        };
    }

    public static Employee Create(EmployeeRegistrationForm form)
    {
        return new Employee
        {
            Id= form.Id,
            EmployementNumber = form.EmploymentNumber,
            FirstName = form.FirstName,
            LastName = form.LastName,
        };
    }
}
