package documenso.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object PasskeyTable : IdTable<String>("\"Passkey\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Passkey_userId_fkey\"",
        )
    val name = text("name")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastUsedAt = pgTimestamp("lastUsedAt", 3).nullable()
    val credentialId = binary("credentialId")
    val credentialPublicKey = binary("credentialPublicKey")
    val counter = long("counter")
    val credentialDeviceType = text("credentialDeviceType")
    val credentialBackedUp = bool("credentialBackedUp")
    val transports = array("transports", TextColumnType()).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Passkey_pkey\"")
}
