package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable

object NurturingOrganizationCreatedTable : IdTable<String>("\"NurturingOrganizationCreated\"") {
    override val id = text("organizationId").entityId()
    val createdAt = pgTimestamp("createdAt", 3)
    override val primaryKey = PrimaryKey(id, name = "\"NurturingOrganizationCreated_pkey\"")

    init {
        index("\"NurturingOrganizationCreated_createdAt_idx\"", false, createdAt)
    }
}
