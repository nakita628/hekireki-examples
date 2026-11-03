package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object HashedLinkTable : IdTable<Int>("\"HashedLink\"") {
    override val id = integer("id").autoIncrement().entityId()
    val link = text("link").uniqueIndex("\"HashedLink_link_key\"")
    val eventTypeId =
        reference(
            "eventTypeId",
            EventTypeTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"HashedLink_eventTypeId_fkey\"",
        )
    val expiresAt = pgTimestamp("expiresAt", 3).nullable()
    val maxUsageCount = integer("maxUsageCount").default(1)
    val usageCount = integer("usageCount").default(0)
    override val primaryKey = PrimaryKey(id, name = "\"HashedLink_pkey\"")

    init {
        index("\"HashedLink_eventTypeId_idx\"", false, eventTypeId)
    }
}
