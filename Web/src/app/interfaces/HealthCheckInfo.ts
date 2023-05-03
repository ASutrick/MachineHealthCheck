export interface HealthCheckInfo {
    date: Date,
    cpuInfo: string [],
    memoryInfo: string [],
    sqlInfo: string [],
    diskInfo: string []
}