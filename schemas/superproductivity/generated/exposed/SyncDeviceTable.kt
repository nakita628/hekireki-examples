package superproductivity.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable

object SyncDeviceTable : CompositeIdTable("sync_devices") {
    val clientId = text("client_id").entityId()
    val userId =
        reference(
            "user_id",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "sync_devices_user_id_fkey",
        )
    val deviceName = text("device_name").nullable()
    val userAgent = text("user_agent").nullable()
    val appVersion = text("app_version").nullable()
    val lastSeenAt = long("last_seen_at")
    val lastAckedSeq = integer("last_acked_seq").default(0)
    val createdAt = long("created_at")
    override val primaryKey = PrimaryKey(userId, clientId, name = "sync_devices_pkey")

    init {
        addIdColumn(userId)
    }
}
