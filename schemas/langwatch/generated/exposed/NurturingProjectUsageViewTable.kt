package langwatch.models

import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import org.jetbrains.exposed.v1.json.jsonb

object NurturingProjectUsageViewTable : CompositeIdTable("\"NurturingProjectUsageView\"") {
    val projectId = text("projectId").entityId()
    val owner = text("owner").entityId()
    val folded = jsonb<String>("folded", { it }, { it })
    val appliedEventIds = array("appliedEventIds", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val projectionVersion = text("projectionVersion")
    val updatedAt = pgTimestamp("updatedAt", 3)
    override val primaryKey = PrimaryKey(projectId, owner, name = "\"NurturingProjectUsageView_pkey\"")
}
