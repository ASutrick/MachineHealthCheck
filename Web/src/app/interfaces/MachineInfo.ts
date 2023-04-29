export interface MachineInfo {
    Id: string;
    Name: string;
    Machine: string;
    Key: string;
    LastChecked?: Date;
    HealthChecks: number;
    IsVerified: boolean;
}
