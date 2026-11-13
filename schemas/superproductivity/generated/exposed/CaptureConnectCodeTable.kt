package superproductivity.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object CaptureConnectCodeTable : IdTable<Int>("capture_connect_codes") {
    override val id =
        integer("user_id")
            .references(
                UserTable.id,
                onDelete = ReferenceOption.CASCADE,
                onUpdate = ReferenceOption.CASCADE,
                fkName = "capture_connect_codes_user_id_fkey",
            ).entityId()
    val codeHash = text("code_hash").uniqueIndex("capture_connect_codes_code_hash_key")
    val tokenVersion = integer("token_version")
    val expiresAt = long("expires_at")
    override val primaryKey = PrimaryKey(id, name = "capture_connect_codes_pkey")
}
