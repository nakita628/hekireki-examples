package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object LiteLLMManagedFileContentTableTable : IdTable<String>("\"LiteLLM_ManagedFileContentTable\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val content = binary("content")
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_ManagedFileContentTable_pkey\"")
}
