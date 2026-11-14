package langwatch.models

import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb

object ScimSsoConnectionViewTable : IdTable<String>("\"ScimSsoConnectionView\"") {
    override val id = text("id").entityId()
    val organizationId = text("organizationId")
    val folded = jsonb<String>("folded", { it }, { it })
    val appliedEventIds = array("appliedEventIds", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val projectionVersion = text("projectionVersion")
    val createdAt = pgTimestamp("createdAt", 3)
    val updatedAt = pgTimestamp("updatedAt", 3)
    override val primaryKey = PrimaryKey(id, name = "\"ScimSsoConnectionView_pkey\"")

    init {
        index("\"ScimSsoConnectionView_organizationId_createdAt_idx\"", false, organizationId, createdAt)
    }
}
