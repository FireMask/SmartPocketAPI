import { inject, Injectable } from "@angular/core";
import { AxiosService } from "../helpers/axios";
import { Observable } from "rxjs";
import { ApiResponse } from "../models/auth/ApiResponse";
import { Catalogs } from "../models/catalogs/catalogs";
import { Category } from "../models/movements/Category";

@Injectable({
    providedIn: 'root'
})
export class CatalogsService {

    axiosInstance: AxiosService = inject(AxiosService);

    getCatalogs(): Observable<ApiResponse<Catalogs>> {
        return this.axiosInstance.get<Catalogs>("/filterCatalogs");
    }

    getTopCategories(): Observable<ApiResponse<Partial<Category>[]>> {
        return this.axiosInstance.get<Partial<Category>[]>("/category/topCategories");
    }
}