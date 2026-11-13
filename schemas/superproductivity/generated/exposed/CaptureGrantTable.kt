package superproductivity.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object CaptureGrantTable : IdTable<String>("capture_grants") {
    override val id = text("id").entityId()
    val userId =
        reference(
            "user_id",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "capture_grants_user_id_fkey",
        )
    val kind = text("kind")
    val clientName = text("client_name")
    val secretHash = text("secret_hash")
    val createdAt = long("created_at")
    val expiresAt = long("expires_at")
    val lastUsedAt = long("last_used_at").nullable()
    val revokedAt = long("revoked_at").nullable()
    val clientId = text("client_id").nullable()
    val resource = text("resource").nullable()
    val refreshHash = text("refresh_hash").nullable()
    val prevRefreshHash = text("prev_refresh_hash").nullable()
    val rotatedAt = long("rotated_at").nullable()
    val refreshExpiresAt = long("refresh_expires_at").nullable()
    override val primaryKey = PrimaryKey(id, name = "capture_grants_pkey")

    init {
        index("capture_grants_user_id_idx", false, userId)
    }
}
