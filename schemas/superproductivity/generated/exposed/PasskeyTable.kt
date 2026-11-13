package superproductivity.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object PasskeyTable : IdTable<String>("passkeys") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val credentialId = binary("credential_id").uniqueIndex("passkeys_credential_id_key")
    val publicKey = binary("public_key")
    val counter = long("counter").default(0L)
    val transports = text("transports").nullable()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastUsedAt = pgTimestamp("last_used_at", 3).nullable()
    val userId =
        reference(
            "user_id",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "passkeys_user_id_fkey",
        )
    override val primaryKey = PrimaryKey(id, name = "passkeys_pkey")

    init {
        index("passkeys_user_id_idx", false, userId)
    }
}
