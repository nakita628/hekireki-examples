package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object CalendarCacheTable : CompositeIdTable("\"CalendarCache\"") {
    val id1 = text("id").nullable().clientDefault { UUID.randomUUID().toString() }
    val key = text("key").entityId()
    val value = jsonb<String>("value", { it }, { it })
    val expiresAt = pgTimestamp("expiresAt", 3)
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val credentialId =
        reference(
            "credentialId",
            CredentialTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"CalendarCache_credentialId_fkey\"",
        )
    val userId = integer("userId").nullable()
    override val primaryKey = PrimaryKey(credentialId, key, name = "\"CalendarCache_pkey\"")

    init {
        addIdColumn(credentialId)
        index("\"CalendarCache_userId_key_idx\"", false, userId, key)
        uniqueIndex("\"CalendarCache_credentialId_key_key\"", credentialId, key)
    }
}
