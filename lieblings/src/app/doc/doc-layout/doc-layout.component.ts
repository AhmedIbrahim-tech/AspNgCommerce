import { Component, OnInit, ViewChild } from '@angular/core';
import { BreakpointObserver } from '@angular/cdk/layout';
@Component({
  selector: 'll-doc-layout',
  templateUrl: './doc-layout.component.html',
  styleUrls: ['./doc-layout.component.scss']
})
export class DocLayoutComponent implements OnInit {
  @ViewChild('sidenav') sidenav;
  isSidenavExpand = true;
  isLessThenLargeDevice = true;
  
  constructor(private breakpointObserver: BreakpointObserver) {
    this.breakpointObserver.observe(['(max-width: 1199px)']).subscribe(({matches}) => {
      this.isLessThenLargeDevice = matches;
      if (matches) {
        this.isSidenavExpand = false;
      } else {
        this.isSidenavExpand = true;
      }
    });
  }

  ngOnInit(): void {}

  toggleSidenav(): void {
    this.sidenav.toggle();
    this.isSidenavExpand = this.sidenav.opened;
  }
  
}
