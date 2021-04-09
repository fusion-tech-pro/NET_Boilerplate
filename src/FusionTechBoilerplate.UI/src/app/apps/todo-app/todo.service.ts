      import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})

export class TodoService {

  baseUrl: string = "https://localhost:44334/api/";

  itemsChanged$: BehaviorSubject<any[]> = new BehaviorSubject([]);
  
  constructor(private httpClient: HttpClient) { }

  post(value: string): Observable<any> {
    let body = {
      value: value
    };

    return this.httpClient.post(`${this.baseUrl}item/post`, body);
  }  

  getAll(): Observable<any>  {    
    return this.httpClient.get(`${this.baseUrl}item/get`)
      .pipe(map((items: any) => {        
        this.itemsChanged$.next(items);
      }));
  }  

  get(id: number): Observable<any> {
    console.log(`${this.baseUrl}item/get/${id}`);
    
    return this.httpClient.get(`${this.baseUrl}item/get/${id}`);
  }  

  update(value: string, status: number, id: number): Observable<any> {
    let body = {
      value: value,
      status: status,
      id: id
    };

    return this.httpClient.post(`${this.baseUrl}item/update`, body);
  }  

  put(id: number): Observable<any> {
    return this.httpClient.put(`${this.baseUrl}item/put/${id}`, null);
  }  

  delete(id: number): Observable<any> {
    return this.httpClient.delete(`${this.baseUrl}item/delete/${id}`);
  }  
}
