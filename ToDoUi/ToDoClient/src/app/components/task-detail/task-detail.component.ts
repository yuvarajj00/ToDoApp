import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { TaskService } from '../../services/task.service';
import { TodoService } from '../../services/todo.service';
import { TaskItem, ToDoItem, CreateToDoDto, UpdateToDoDto, TaskStatus } from '../../models/task.model';

@Component({
  selector: 'app-task-detail',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './task-detail.component.html',
  styleUrl: './task-detail.component.css'
})
export class TaskDetailComponent implements OnInit {
  task?: TaskItem;
  newTodoDescription: string = '';
  newTodoDueDate: string = '';
  editingTodoId: number | null = null;
  editTodoDescription: string = '';
  editTodoDueDate: string = '';
  editTodoStatus: string = '';

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private taskService: TaskService,
    private todoService: TodoService
  ) { }

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    if (id) {
      this.loadTask(id);
    }
  }

  loadTask(id: number): void {
    this.taskService.getTaskById(id).subscribe({
      next: (data) => {
        this.task = data;
      },
      error: (error) => console.error('Error loading task:', error)
    });
  }

  addTodo(): void {
    if (!this.task || !this.newTodoDescription.trim()) return;

    const newTodo: CreateToDoDto = {
      taskId: this.task.id,
      description: this.newTodoDescription,
      dueDate: this.newTodoDueDate || undefined,
      status: TaskStatus.New
    };

    this.todoService.createToDo(newTodo).subscribe({
      next: () => {
        this.newTodoDescription = '';
        this.newTodoDueDate = '';
        this.loadTask(this.task!.id);
      },
      error: (error) => console.error('Error creating todo:', error)
    });
  }

  startEditTodo(todo: ToDoItem): void {
    this.editingTodoId = todo.id;
    this.editTodoDescription = todo.description;
    this.editTodoDueDate = todo.dueDate ? todo.dueDate.split('T')[0] : '';
    this.editTodoStatus = todo.status;
  }

  saveEditTodo(): void {
    if (this.editingTodoId === null || !this.task) return;

    const updateTodo: UpdateToDoDto = {
      description: this.editTodoDescription,
      dueDate: this.editTodoDueDate || undefined,
      status: this.editTodoStatus as TaskStatus
    };

    this.todoService.updateToDo(this.editingTodoId, updateTodo).subscribe({
      next: () => {
        this.editingTodoId = null;
        this.loadTask(this.task!.id);
      },
      error: (error) => console.error('Error updating todo:', error)
    });
  }

  cancelEdit(): void {
    this.editingTodoId = null;
  }

  deleteTodo(id: number): void {
    if (!this.task || !confirm('Are you sure you want to delete this subtask?')) return;

    this.todoService.deleteToDo(id).subscribe({
      next: () => this.loadTask(this.task!.id),
      error: (error) => console.error('Error deleting todo:', error)
    });
  }

  toggleTodoStatus(todo: ToDoItem): void {
    if (!this.task) return;

    const newStatus = todo.status === 'Completed' ? TaskStatus.New : TaskStatus.Completed;
    const updateTodo: UpdateToDoDto = {
      description: todo.description,
      dueDate: todo.dueDate,
      status: newStatus
    };

    this.todoService.updateToDo(todo.id, updateTodo).subscribe({
      next: () => this.loadTask(this.task!.id),
      error: (error) => console.error('Error updating todo status:', error)
    });
  }

  goBack(): void {
    this.router.navigate(['/tasks']);
  }

  editTask(): void {
    if (this.task) {
      this.router.navigate(['/tasks/edit', this.task.id]);
    }
  }

  getStatusClass(status: string): string {
    switch (status) {
      case 'New':
        return 'status-new';
      case 'InProgress':
        return 'status-inprogress';
      case 'Completed':
        return 'status-completed';
      default:
        return '';
    }
  }

  formatDate(date?: string): string {
    if (!date) return 'No due date';
    return new Date(date).toLocaleDateString();
  }

  getCompletionPercentage(): number {
    if (!this.task || !this.task.toDoItems || this.task.toDoItems.length === 0) return 0;
    const completed = this.task.toDoItems.filter(t => t.status === 'Completed').length;
    return Math.round((completed / this.task.toDoItems.length) * 100);
  }
}
