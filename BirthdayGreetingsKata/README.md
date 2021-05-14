# The birthday greetings kata

### Purpose

To learn about the hexagonal architecture, which is a good way to structure an application, and how to shield your domain model from external apis and systems.

### Problem: write a program that

Loads a set of employee records from a flat file.
Sends a greetings email to all employees whose birthday is today.
The flat file is a sequence of records, separated by newlines (CSV).

This are the first few lines:

    last_name, first_name, date_of_birth, email
    Doe, John, 1982/10/08, john.doe@foobar.com
    Ann, Mary, 1975/09/11, mary.ann@foobar.com

The greetings email contains the following text:

    Subject: Happy birthday!
    Happy birthday, dear John!

with the first name of the employee substituted for “John”
