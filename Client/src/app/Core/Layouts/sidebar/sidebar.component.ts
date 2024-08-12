import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ICategorys } from '../../../shared/Models/Categorys';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {
  @Input() categories: ICategorys[] = [];
  @Input() selectedCategoryId: number = 0;
  @Output() categorySelected = new EventEmitter<number>();
  @Output() search = new EventEmitter<string>();

  searchValue: string = '';

  constructor() {}

  ngOnInit(): void {}

  onCategorySelect(id: number): void {
    this.categorySelected.emit(id);
  }

  onSearch(): void {
    this.search.emit(this.searchValue);
  }
}
