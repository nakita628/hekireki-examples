package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable

object NurturingPersonalProjectTable : IdTable<String>("\"NurturingPersonalProject\"") {
    override val id = text("projectId").entityId()
    val organizationId = text("organizationId")
    val ownerUserId = text("ownerUserId")
    override val primaryKey = PrimaryKey(id, name = "\"NurturingPersonalProject_pkey\"")
}
