package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object AttributeSyncRuleTable : IdTable<String>("\"AttributeSyncRule\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val integrationAttributeSyncId =
        reference(
            "integrationAttributeSyncId",
            IntegrationAttributeSyncTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"AttributeSyncRule_integrationAttributeSyncId_fkey\"",
        ).uniqueIndex("\"AttributeSyncRule_integrationAttributeSyncId_key\"")
    val rule = jsonb<String>("rule", { it }, { it })
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"AttributeSyncRule_pkey\"")
}
