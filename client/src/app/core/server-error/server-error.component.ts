import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-server-error',
  templateUrl: './server-error.component.html',
  styleUrls: ['./server-error.component.scss']
})
export class ServerErrorComponent implements OnInit {
  error:any;

  constructor(private router: Router) {
    const naviation = this.router.getCurrentNavigation();
    this.error = naviation?.extras?.state?.['error'];
    console.log(this.error);
  }

  ngOnInit(): void {

  }

}
