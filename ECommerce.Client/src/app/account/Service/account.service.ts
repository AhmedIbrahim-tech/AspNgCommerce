import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, ReplaySubject, map, of } from 'rxjs';
import { Address, User } from 'src/app/shared/Models/user';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private currentUserSource = new ReplaySubject<User | null>(1);
  currentUser$ = this.currentUserSource.asObservable();
  constructor(private http: HttpClient, private router: Router) { }

  //#region Get Cruuent User
  loadCurrentUser(token: string | null) {
    if (token === null) {
      this.currentUserSource.next(null);
      return of(null);
    }

    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);

    return this.http.get<User>(environment.APIURL + 'Account', { headers }).pipe(
      map((user) => {
        if (user) {
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
          return user;
        }else{
          return null;
        }
      })
    );
  }
  //#endregion

  //#region Register
  /**
   * The register function sends a POST request to the '/account/register' endpoint with the provided
   * values, and upon success, stores the returned user token in local storage and updates the current
   * user source.
   * @param {any} values - The "values" parameter is an object that contains the data needed for user
   * registration. It could include properties such as username, email, password, and any other required
   * information for creating a new user account.
   * @returns The `register` function is returning an Observable.
   */
  register(values: any) {
    return this.http.post<User>(environment.APIURL + '/Account/register', values).pipe(
      map(user => {
        localStorage.setItem('token', user.token);
        this.currentUserSource.next(user);
      })
    )
  }
  //#endregion

  //#region login
  /**
   * The login function sends a POST request to the server with user login credentials, saves the
   * returned token in local storage, and updates the current user source.
   * @param {any} values - The "values" parameter is an object that contains the data needed for the
   * login request. It could include properties such as username and password.
   * @returns The login function is returning an Observable of type User.
   */
  login(values: any) {
    return this.http.post<User>(environment.APIURL + '/Account/login', values).pipe(
      map(user => {
        localStorage.setItem('token', user.token);
        this.currentUserSource.next(user);
        return user
      })
    )
  }
  //#endregion

  //#region logout
  logout() {
    localStorage.removeItem("token");
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/');
  }
  //#endregion

  //#region check Email Exists
  checkEmailExists(email: string) {
    return this.http.get(environment.APIURL + '/Account/emailexists?email=' + email);
  }
  //#endregion

  //#region get Address
  getUserAddress() {
    return this.http.get<Address>(environment.APIURL + '/Account/address');
  }
  //#endregion

  //#region update Address
  updateUserAddress(address: Address) {
    return this.http.put<Address>(environment.APIURL + '/Account/address', address);
  }
  //#endregion


}
