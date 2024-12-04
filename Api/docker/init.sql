\c database;

-- Create the student table
DROP TABLE IF EXISTS students;
CREATE TABLE IF NOT EXISTS students (
    Id SERIAL PRIMARY KEY,
    Firstname VARCHAR(255) NOT NULL,
    Lastname VARCHAR(255) NOT NULL,
    Email VARCHAR(255) NOT NULL
);

-- Insert starting data if the table is empty
DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM students LIMIT 1) THEN
        INSERT INTO students (Firstname, Lastname, Email)
        VALUES
            ('adrien', 'Montreer', 'test@gmail.Com'),
            ('ad', 'Mon', 't@gmail.Com'),
            ('a', 'b', 'test3@gmail.Com');
    END IF;
END $$;
