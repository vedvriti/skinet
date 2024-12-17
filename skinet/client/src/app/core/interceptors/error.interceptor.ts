import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';
import { SnackbarService } from '../services/snackbar.service';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
  const snackbar = inject(SnackbarService)
  
  
  //if we does not want to subscribe to observable and want to do something with it we need to use pipe
  return next(req).pipe(
    catchError((err:HttpErrorResponse) => {
         if(err.status === 400){
          if(err.error.errors){
            const modelStateErrors = [];
            for(const key in err.error.errors){
              if(err.error.errors[key]){
                modelStateErrors.push(err.error.errors[key])
              }
            }
            //single list of array to diaplay in browser - flat function
            throw modelStateErrors.flat();
          }
          // alert(err.error.title || err.error)
          //snackbar.error(err.error.title || err.error)
         }
         else{
          snackbar.error(err.error.title || err.error)
         }
         if(err.status === 401){
          // alert(err.error.title || err.error )
          snackbar.error(err.error.title || err.error )
         }
         if(err.status === 404){
            router.navigateByUrl('/not-found')
         }
         if(err.status == 500){
          const navigationExtras: NavigationExtras = {state: { error:err.error}}
          //navigationExtras contains the error response from the 500 internal server error
          router.navigateByUrl('/server-error',navigationExtras);
          //navigationExtras ensures the error details are sent along with the navigation.
         }
         
         return throwError(() => err)
    })
  )
};
