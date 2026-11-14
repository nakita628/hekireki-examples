package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb

object ProcessManagerInstanceTable : IdTable<String>("\"ProcessManagerInstance\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val processName = text("processName")
    val projectId = text("projectId")
    val processKey = text("processKey")
    val tenantId = text("tenantId")
    val userId = text("userId").nullable()
    val state = jsonb<String>("state", { it }, { it })
    val revision = integer("revision")
    val nextWakeAt = pgTimestamptz("nextWakeAt", 3).nullable()
    val updatedAt = pgTimestamptz("updatedAt", 3)
    override val primaryKey = PrimaryKey(id, name = "\"ProcessManagerInstance_pkey\"")

    init {
        index("\"ProcessManagerInstance_nextWakeAt_idx\"", false, nextWakeAt)
        uniqueIndex("\"ProcessManagerInstance_processName_projectId_processKey_key\"", processName, projectId, processKey)
    }
}
