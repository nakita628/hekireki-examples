package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable

object ProcessManagerInboxTable : IdTable<String>("\"ProcessManagerInbox\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val processName = text("processName")
    val projectId = text("projectId")
    val processKey = text("processKey")
    val tenantId = text("tenantId")
    val sourceEventId = text("sourceEventId")
    val sourceEventKey = text("sourceEventKey").nullable()
    val consumedAt = pgTimestamptz("consumedAt", 3)
    override val primaryKey = PrimaryKey(id, name = "\"ProcessManagerInbox_pkey\"")

    init {
        index("ProcessManagerInbox_processName_projectId_processKey_consum_idx", false, processName, projectId, processKey, consumedAt)
        index("\"ProcessManagerInbox_consumedAt_idx\"", false, consumedAt)
        uniqueIndex("\"ProcessManagerInbox_processName_projectId_sourceEventKey_key\"", processName, projectId, sourceEventKey)
    }
}
