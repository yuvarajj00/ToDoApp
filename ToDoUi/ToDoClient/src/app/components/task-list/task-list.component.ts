import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { TaskService } from '../../services/task.service';
import { TaskItem, TaskStatus } from '../../models/task.model';

@Component({
  selector: 'app-task-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './task-list.component.html',
  styleUrl: './task-list.component.css'
})
export class TaskListComponent implements OnInit {
  tasks: TaskItem[] = [];
  filteredTasks: TaskItem[] = [];
  selectedStatus: string = 'All';
  taskStatuses = ['All', 'New', 'InProgress', 'Completed'];

  constructor(
    private taskService: TaskService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.loadTasks();
  }

  loadTasks(): void {
    this.taskService.getAllTasks().subscribe({
      next: (data) => {
        this.tasks = data;
        this.applyFilter();
      },
      error: (error) => console.error('Error loading tasks:', error)
    });
  }

  applyFilter(): void {
    if (this.selectedStatus === 'All') {
      this.filteredTasks = this.tasks;
    } else {
      this.filteredTasks = this.tasks.filter(task => task.status === this.selectedStatus);
    }
  }

  onFilterChange(status: string): void {
    this.selectedStatus = status;
    this.applyFilter();
  }

  viewTask(id: number): void {
    this.router.navigate(['/tasks', id]);
  }

  createTask(): void {
    this.router.navigate(['/tasks/new']);
  }

  editTask(id: number, event: Event): void {
    event.stopPropagation();
    this.router.navigate(['/tasks/edit', id]);
  }

  deleteTask(id: number, event: Event): void {
    event.stopPropagation();
    if (confirm('Are you sure you want to delete this task?')) {
      this.taskService.deleteTask(id).subscribe({
        next: () => this.loadTasks(),
        error: (error) => console.error('Error deleting task:', error)
      });
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

  getCompletionPercentage(task: TaskItem): number {
    if (!task.toDoItems || task.toDoItems.length === 0) return 0;
    const completed = task.toDoItems.filter(t => t.status === 'Completed').length;
    return Math.round((completed / task.toDoItems.length) * 100);
  }
}
