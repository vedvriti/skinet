import { Component } from '@angular/core';
import { MatButton } from '@angular/material/button';
import { MatBadge } from '@angular/material/badge';
import { MatIcon } from '@angular/material/icon';

@Component({
  selector: 'app-header', 
  standalone: true, 
  imports: [
    MatButton,
    MatIcon,
    MatBadge
  ],
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss' ]
})
export class HeaderComponent {}
