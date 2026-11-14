package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ScimTokenTable : IdTable<String>("\"ScimToken\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val organizationId = text("organizationId")
    val connectionId = text("connectionId").nullable()
    val hashedToken = text("hashedToken").uniqueIndex("\"ScimToken_hashedToken_key\"")
    val hashScheme = text("hashScheme").default("sha256")
    val description = text("description").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastUsedAt = pgTimestamp("lastUsedAt", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"ScimToken_pkey\"")

    init {
        index("\"ScimToken_organizationId_idx\"", false, organizationId)
        index("\"ScimToken_connectionId_idx\"", false, connectionId)
    }
}
