package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object HostGroupTable : IdTable<String>("\"HostGroup\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val name = text("name")
    val eventTypeId =
        optReference(
            "eventTypeId",
            EventTypeTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"HostGroup_eventTypeId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"HostGroup_pkey\"")

    init {
        index("\"HostGroup_name_idx\"", false, name)
        index("\"HostGroup_eventTypeId_idx\"", false, eventTypeId)
    }
}
