package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable

object NurturingProjectTable : IdTable<String>("\"NurturingProject\"") {
    override val id = text("projectId").entityId()
    val organizationId = text("organizationId")
    override val primaryKey = PrimaryKey(id, name = "\"NurturingProject_pkey\"")

    init {
        index("\"NurturingProject_organizationId_idx\"", false, organizationId)
    }
}
