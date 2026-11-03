package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object AttributeSyncFieldMappingTable : IdTable<String>("\"AttributeSyncFieldMapping\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val integrationFieldName = text("integrationFieldName")
    val attributeId =
        reference(
            "attributeId",
            AttributeTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"AttributeSyncFieldMapping_attributeId_fkey\"",
        )
    val enabled = bool("enabled")
    val integrationAttributeSyncId =
        reference(
            "integrationAttributeSyncId",
            IntegrationAttributeSyncTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"AttributeSyncFieldMapping_integrationAttributeSyncId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"AttributeSyncFieldMapping_pkey\"")

    init {
        index("\"AttributeSyncFieldMapping_integrationAttributeSyncId_idx\"", false, integrationAttributeSyncId)
        uniqueIndex("AttributeSyncFieldMapping_integrationAttributeSyncId_attrib_key", integrationAttributeSyncId, attributeId)
    }
}
