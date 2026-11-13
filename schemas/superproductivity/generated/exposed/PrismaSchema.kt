package superproductivity.models

import org.jetbrains.exposed.v1.core.Schema
import org.jetbrains.exposed.v1.core.Table

object PrismaSchema {
    val schemas: List<Schema> = emptyList()
    val enumTypes: List<String> = emptyList()
    val tables: List<Table> =
        listOf(
            UserTable,
            PasskeyTable,
            PendingPasskeyRegistrationTable,
            OperationTable,
            UserSyncStateTable,
            SyncDeviceTable,
            CaptureKeyTable,
            CaptureGrantTable,
            CaptureItemTable,
            CaptureConnectCodeTable,
            CaptureAuthTable,
            ConnectRequestTable,
        )
}
