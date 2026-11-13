package superproductivity.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object CaptureKeyTable : IdTable<Int>("capture_keys") {
    override val id =
        integer("user_id")
            .references(
                UserTable.id,
                onDelete = ReferenceOption.CASCADE,
                onUpdate = ReferenceOption.CASCADE,
                fkName = "capture_keys_user_id_fkey",
            ).entityId()
    val publicKey = text("public_key")
    val fingerprint = text("fingerprint")
    val createdAt = long("created_at")
    val disabledAt = long("disabled_at").nullable()
    override val primaryKey = PrimaryKey(id, name = "capture_keys_pkey")
}
