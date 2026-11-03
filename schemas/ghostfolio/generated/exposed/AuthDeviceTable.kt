package ghostfolio.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object AuthDeviceTable : IdTable<String>("\"AuthDevice\"") {
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val credentialId = binary("credentialId")
    val credentialPublicKey = binary("credentialPublicKey")
    val counter = integer("counter")
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"AuthDevice_userId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"AuthDevice_pkey\"")

    init {
        index("\"AuthDevice_userId_idx\"", false, userId)
    }
}
