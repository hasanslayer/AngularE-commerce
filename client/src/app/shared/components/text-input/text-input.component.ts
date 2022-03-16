import {
  Component,
  ElementRef,
  Input,
  OnInit,
  Self,
  ViewChild,
} from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';

@Component({
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.scss'],
})
export class TextInputComponent implements OnInit, ControlValueAccessor {
  //ControlValueAccessor act as interface between angular forms and native element in DOM
  // because we need to access the form control values

  // we can access to the native element i.e 'input' by ViewChild()
  @ViewChild('input', { static: true }) input!: ElementRef; // static mean that we will not surround it with i.e '*ngIf'
  @Input() type = 'text';
  @Input() label!: string;

  // in order to access to validations we need to access to control itself
  // we specify public because we will use it in html template
  // @self is for dependancy injection in angular and we won't inject all the tree in the dependancies
  // but we need only the specified in the constructor
  constructor(@Self() public controlDir: NgControl) {
    this.controlDir.valueAccessor = this;
  }

  ngOnInit(): void {
    const control = this.controlDir.control;
    const validators = control?.validator ? [control.validator] : [];
    const asyncValidators = control?.asyncValidator ? [control.asyncValidator] : [];

    control?.setValidators(validators);
    control?.setAsyncValidators(asyncValidators);
    control?.updateValueAndValidity();
  }

  onChange(event: any) {}

  onTouched() {}

  writeValue(obj: any): void {
    this.input.nativeElement.value = obj || '';
  }
  registerOnChange(fn: any): void {
    this.onChange = fn;
  }
  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }
}
