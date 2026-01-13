export enum TaskStatus {
  New = 'New',
  InProgress = 'InProgress',
  Completed = 'Completed'
}

export interface ToDoItem {
  id: number;
  taskId: number;
  description: string;
  dueDate?: string;
  status: TaskStatus;
}

export interface TaskItem {
  id: number;
  title: string;
  description: string;
  dueDate?: string;
  status: TaskStatus;
  toDoItems: ToDoItem[];
}

export interface CreateTaskDto {
  title: string;
  description: string;
  dueDate?: string;
  status: TaskStatus;
}

export interface UpdateTaskDto {
  title: string;
  description: string;
  dueDate?: string;
  status: TaskStatus;
}

export interface CreateToDoDto {
  taskId: number;
  description: string;
  dueDate?: string;
  status: TaskStatus;
}

export interface UpdateToDoDto {
  description: string;
  dueDate?: string;
  status: TaskStatus;
}
