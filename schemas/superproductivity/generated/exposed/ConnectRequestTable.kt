package superproductivity.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object ConnectRequestTable : IdTable<String>("connect_requests") {
    override val id = text("id").entityId()
    val pollSecretHash = text("poll_secret_hash")
    val matchCode = text("match_code")
    val deviceName = text("device_name")
    val platform = text("platform")
    val status = text("status").default("pending")
    val userId =
        optReference(
            "user_id",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "connect_requests_user_id_fkey",
        )
    val tokenVersion = integer("token_version").nullable()
    val expiresAt = long("expires_at")
    override val primaryKey = PrimaryKey(id, name = "connect_requests_pkey")

    init {
        index("connect_requests_user_id_idx", false, userId)
        index("connect_requests_expires_at_idx", false, expiresAt)
    }
}
