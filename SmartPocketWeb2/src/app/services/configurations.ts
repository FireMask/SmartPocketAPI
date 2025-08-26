import { inject, Injectable } from '@angular/core';
import { AxiosService } from '../helpers/axios';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/auth/ApiResponse';
import { Configuration } from '../models/configuration/configuration';

@Injectable({
  providedIn: 'root'
})
export class ConfigurationsService {
  axiosInstance: AxiosService = inject(AxiosService);

    getAllConfigurations(): Observable<ApiResponse<Configuration[]>> {
        return this.axiosInstance.get<Configuration[]>("/configuration");
    }

    getConfigurationByKey(key: string): Observable<ApiResponse<Configuration>> {
        return this.axiosInstance.get<Configuration>(`/configuration/${key}`);
    }

    updateConfiguration(configuration: Configuration): Observable<ApiResponse<null>> {
        return this.axiosInstance.post<null>("/configuration", configuration);
    }


}
