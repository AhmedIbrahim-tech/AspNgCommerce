import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-pager',
  templateUrl: './pager.component.html',
  styleUrls: ['./pager.component.scss']
})
export class PagerComponent implements OnInit {

  @Input() TotalCount!: number;
  @Input() PageSize!: number;
  @Output() pageChanged = new EventEmitter<number>();
  constructor() { }

  ngOnInit(): void {
  }

  onPageChanged(event: any) {
    this.pageChanged.emit(event);
  }

}
