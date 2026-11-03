package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object SecretStoreTable : IdTable<String>("\"SecretStore\"") {
    override val id = text("key").entityId()
    val value = jsonb<String>("value", { it }, { it })
    val version = text("version").default("1")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }

    init {
        uniqueIndex("\"SecretStore_key_key\"", id)
        index("\"SecretStore_key_idx\"", false, functions = listOf(indexExpression("\"key\" text_pattern_ops")))
    }
}
