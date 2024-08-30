using System;
using System.Collections.Generic;

namespace TestToDoList
{
    public class Main_Task
    {
        public string Description { get; set; } // Define la descripción de la tarea 
        public DateTime? Deadline { get; set; } // Define la fecha limite de cada tarea y la almacena.
        public bool IsCompleted { get; private set; } // Indica si la tarea está completa o no.
        public Main_Task(string description, DateTime? deadline = null)
        {
            Description = description;
            Deadline = deadline;
            IsCompleted = false;
        }

        public void MarkAsCompleted()  // Metodo que realiza la marcación de la tarea como completada
        {
            IsCompleted = true;
        }

        public override string ToString()  // Hacemos la representación en cadena de manera personalizada de una tarea de manera legible en la consola.
        {
            // Valida si la tarea está marcada como completada o no y tiene fecha limite y la imprime en formato corto, en caso de que no, se ignora la fecha límite.
            string status = IsCompleted ? "[X]" : "[ ]";
            string deadline = Deadline.HasValue ? $" - Fecha límite: {Deadline.Value.ToShortDateString()}" : ""; 
            return $"{status} {Description}{deadline}";
        }
    }
    class Program
    {
        static List<Main_Task> tasks = new List<Main_Task>();

        static void Main(string[] args)
        {
            while (true)
            {
                // Con el ciclo while se crea la información de la lista a mostrar en consola en el menú para que se muestre la información de manera amena con el usuario.
                Console.Clear();
                Console.WriteLine("To-Do List:");
                Console.WriteLine("1. Agregar tarea");
                Console.WriteLine("2. Listar tareas");
                Console.WriteLine("3. Marcar tarea como completada");
                Console.WriteLine("4. Eliminar tarea");
                Console.WriteLine("5. Salir");
                Console.Write("Selecciona una opción: ");

                string option = Console.ReadLine(); //se lee la información digitada por el usuario 

                //se crea el vinculador de la información digitada por el usuario con cada metodo correspondiente según lo que se quiera realizar.

                switch (option)
                {
                    case "1":
                        AddTask();
                        break;
                    case "2":
                        ListTasks();
                        break;
                    case "3":
                        MarkTaskAsCompleted();
                        break;
                    case "4":
                        DeleteTask();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Opción no válida, intenta de nuevo."); 
                        break;
                        //en caso tal de que la opción digitada por el usuario no corresponda a las opciones disponibles se le genera nuevamente el menú.
                }
            }
        }

        static void AddTask()
        {
            Console.Write("Descripción de la tarea: ");
            string description = Console.ReadLine();

            Console.Write("Fecha límite (dd/MM/yyyy): ");
            string deadlineInput = Console.ReadLine();
            DateTime? deadline = null;

            if (!string.IsNullOrWhiteSpace(deadlineInput))  
                //con este metodo lo que realizamos es que se verifica si la información es null o tiene espacios para poder o guardar la información o pedir se ingrese nuevamente
            {
                if (DateTime.TryParse(deadlineInput, out DateTime parsedDate)) //convierte la información dada en la fecha al formato de fecha indicado previamente.
                {
                    deadline = parsedDate;
                }
                else
                {
                    Console.WriteLine("Formato de fecha no valida. Tarea no creada.");
                    Console.WriteLine("Presiona cualquier tecla para continuar...");
                    Console.ReadKey();
                    return;
                }
            }

            tasks.Add(new Main_Task(description, deadline));
            // se agrega la tarea a la lista correctamente.
            Console.WriteLine("Tarea agregada exitosamente.");
            Console.WriteLine("Presiona cualquier tecla para continuar...");
            Console.ReadKey();
        }

        static void ListTasks()
        {
            
            Console.Clear();
            // Se verifica si hay o no una tarea agregado a la lista tasks
            if (tasks.Count == 0)
            {
                Console.WriteLine("No hay tareas en la lista.");
            }
            else
            // Si hay una tarea agregada en la lista tasks se muestra por medio de validación de ciclo for enumerando las tareas
            //mostrando también la descripción y si posee o no una fecha de finalización.
            {
                for (int i = 0; i < tasks.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {tasks[i]}");
                }
            }
            Console.WriteLine("Presiona cualquier tecla para continuar...");
            Console.ReadKey();
        }

        static void MarkTaskAsCompleted()
        {   
            // Con este metodo se valida si hay tareas en la lista que se puedan marcar o si ya está marcada
            ListTasks(); 
            Console.Write("Selecciona el número de la tarea que quieres marcar como completada: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= tasks.Count)
            // Se llama al metodo ListTask para realizar la consulta de las tareas que hayan
            // Con el if se valida si la información que digite el usuario es mayor a 0 en la lista y si es menor  o igual que las tareas en la lista.
            {
                var task = tasks[index - 1];

                if (task.IsCompleted)
                {
                    Console.WriteLine("Ésta tarea ya está marcada como completada.");
                }
                else
                {
                    task.MarkAsCompleted();
                    Console.WriteLine("Tarea marcada como completada exitosamente.");
                }
            }
            else
            {
                Console.WriteLine("Entrada no válida.");
            }

            Console.WriteLine("Presiona cualquier tecla para continuar...");
            Console.ReadKey();
        }

        static void DeleteTask()
        {
            // Con este metodo vamos a validar lo mismo que con la marcación de tareas MarkAsCompleted pero para eliminar la tarea que selecccione
            ListTasks();
            Console.Write("Selecciona el número de la tarea que quieres eliminar: ");

            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= tasks.Count)
            {
                // En esta línea vamos a confirmar con el usuario si está seguro de eliminar la tarea seleccionada de la lista tasks.
                // Dependiendo de la respuesta del usuario se va a borrar o no dicha tarea quitandola de la lista.
                Console.WriteLine($"¿Estás seguro que deseas eliminar la tarea: \"{tasks[index - 1].Description}\"? (s/n)");
                string confirmation = Console.ReadLine();

                if (confirmation.ToLower() == "s")// con esto hacemos que el string digitado se convierta a minuscula
                {
                    tasks.RemoveAt(index - 1);
                    Console.WriteLine("Tarea eliminada exitosamente.");
                }
                else
                {
                    Console.WriteLine("Eliminación cancelada.");
                }
            }
            else
            {
                Console.WriteLine("Entrada no válida. Por favor, selecciona un número de tarea existente.");
            }

            Console.WriteLine("Presiona cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}