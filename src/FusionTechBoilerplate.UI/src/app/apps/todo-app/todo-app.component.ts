import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { BehaviorSubject, Observable } from 'rxjs';
import { TodoService } from './todo.service';

@Component({
  selector: 'app-todo-app',
  templateUrl: './todo-app.component.html',
  styleUrls: ['./todo-app.component.scss']
})
export class TodoAppComponent implements OnInit {

  items: any[] = [];
  filterItems: any[] = [];
  errors: any[] = [];

  itemForm: FormGroup;
  filterForm: FormGroup;
  
  new: number = 1;
  inProgress: number = 2;
  done: number = 3;
  
  filterValue: string = "";

  constructor(private formBuilder: FormBuilder,
    private itemService: TodoService) { }

  ngOnInit(): void {
    
    this.itemService.getAll().subscribe(() => {
      this.itemService.itemsChanged$
      .subscribe(elements => {
        this.items = elements;
        this.filterItems = this.items;
      });
    })
    this.filterData();

    this.itemForm = this.formBuilder.group({
      item: [ null, [ ]]
    });

    this.filterForm = this.formBuilder.group({
      filter: [ null, [ ]]
    });

    this.filterForm.controls['filter'].valueChanges
      .subscribe((value) => {
        if (value !== null) {
          setTimeout(() => {
            this.filterValue = value;
            this.filterData();
          }, 1000);  
        }
      });

    this.itemForm.controls['item'].valueChanges
      .subscribe(() => {
        this.errors = [];
      });
  }

  addItem() {
    let value = this.itemForm.getRawValue();    
    
    if (value.item !== null) {  
      this.itemService.post(value.item.trim()).subscribe(() => {
        this.itemForm.reset();
        this.itemService.getAll().subscribe();
      }, errors => {
        this.errors = errors.error.errors.Value;
      } );
    }
  }

  changeStatus(item: any) {
    this.itemService.put(item.id).subscribe(() => this.itemService.getAll().subscribe());
  }

  deleteItem(item: any) {
    this.itemService.delete(item.id).subscribe(() => this.itemService.getAll().subscribe());
  }

  filter(mode: number) {    
    if (mode === this.new) {
      this.filterItems = this.items.filter(i => i.status === 1);
    }
    else if (mode === this.inProgress) {
      this.filterItems = this.items.filter(i => i.status === 2);
    } else if (mode === this.done) {
      this.filterItems = this.items.filter(i => i.status === 3);
    } else {
      this.filterItems = this.items;
    }
  }

  filterData() {
    if (this.filterValue !== null && this.filterValue !== "") {
      this.filterItems = this.items.filter(i => i.value.toLowerCase().trim().indexOf(this.filterValue.toLowerCase().trim()) === 0);
    } else {
      this.filterItems = this.items;
    }
  }
}   
