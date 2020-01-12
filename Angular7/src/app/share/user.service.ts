import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private fb: FormBuilder, private http: HttpClient) { }
  readonly BaseURI = 'http://localhost:50761/api/';
  

  formModel = this.fb.group({
    Name: [''],
    Email: ['', Validators.email],
    Surname: [''],
    Passwords: this.fb.group({
      Password: ['', [Validators.required, Validators.minLength(4)]]
      })

  });

  
  

  register() {
    var body = {
      Email: this.formModel.value.Email,
      Password: this.formModel.value.Passwords.Password,
      Name: this.formModel.value.Name,
     
      Surname: this.formModel.value.Surname
      
    };
    return this.http.post("http://localhost:50761/api/Account/Register",body);
  }
}