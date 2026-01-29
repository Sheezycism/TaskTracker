export enum TaskStatus {
  New = 0,
  InProgress = 1,
  Completed = 2 
}

export enum TaskPriority {
  Low = 0,
  Medium = 1,
  High = 2
}

export interface ToDoTask {
  id?: number;
  title: string;
  description: string;
  status: TaskStatus;
  priority: TaskPriority;
  dueDate?: string;
  createdAt?: string;
}