import { Injectable, signal, inject } from '@angular/core';
import { ConfigurationsService } from '../services/configurations';
import { Configuration } from '../models/configuration/configuration';
import { ConfigEnum } from '../helpers/enums/config';

@Injectable({ providedIn: 'root' })
export class ConfigurationStore {

    private configurationService = inject(ConfigurationsService);

    private _configurations = signal<Configuration[]>([]);

    readonly select = {
        configurations: this._configurations.asReadonly()
    };

    constructor(){
        this.getAllConfigurations();
    }

    get isDarkMode(): boolean {
        return this.select?.configurations().find(c => c.key === ConfigEnum.DarkMode)?.value === 'true';
    }

    getAllConfigurations() {
        this.configurationService.getAllConfigurations().subscribe({
            next: (response) => {
                this._configurations.set(response.data)
            }
        });
    }

    setConfiguration(key: string, value: string) {
        const config = this._configurations().find(c => c.key === key);
        if (config) {
            config.value = value;
            this.configurationService.updateConfiguration(config).subscribe({
                next: (response) => {
                    this.getAllConfigurations();
                }
            });
        }
    }

}
