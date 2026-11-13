package superproductivity.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object CaptureAuthTable : IdTable<String>("capture_auth") {
    override val id = text("id").entityId()
    val userId =
        optReference(
            "user_id",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "capture_auth_user_id_fkey",
        )
    val clientId = text("client_id")
    val clientName = text("client_name")
    val redirectUri = text("redirect_uri")
    val codeChallenge = text("code_challenge")
    val resource = text("resource")
    val state = text("state")
    val scope = text("scope")
    val codeHash = text("code_hash").nullable().uniqueIndex("capture_auth_code_hash_key")
    val tokenVersion = integer("token_version").nullable()
    val expiresAt = long("expires_at")
    override val primaryKey = PrimaryKey(id, name = "capture_auth_pkey")

    init {
        index("capture_auth_user_id_idx", false, userId)
        index("capture_auth_expires_at_idx", false, expiresAt)
    }
}
