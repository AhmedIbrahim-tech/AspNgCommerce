import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'll-product-hero',
  templateUrl: './product-hero.component.html',
  styleUrls: ['./product-hero.component.scss']
})
export class ProductHeroComponent implements OnInit {
  particlesOptions = {
    particles: {
      color: {
        value: [ '#ffffff', '#ffffff' ]
      },
      size: {
        value: 1
      },
      lineLinked: {
        enable: true,
        color: 'random'
      },
      move: {
        enable: true,
        speed: 1
      }
    }
  };
  constructor() { }

  ngOnInit(): void {
  }

}
