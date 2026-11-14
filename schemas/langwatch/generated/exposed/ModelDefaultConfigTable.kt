package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object ModelDefaultConfigTable : IdTable<String>("\"ModelDefaultConfig\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val config = jsonb<String>("config", { it }, { it })
    val authorId = text("authorId").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val organizationId = text("organizationId")
    override val primaryKey = PrimaryKey(id, name = "\"ModelDefaultConfig_pkey\"")

    init {
        index("\"ModelDefaultConfig_organizationId_idx\"", false, organizationId)
    }
}
