package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable

object InstantEvalJudgeProjectTable : IdTable<String>("\"InstantEvalJudgeProject\"") {
    override val id = text("projectId").entityId()
    val organizationId = text("organizationId")
    val createdAt = pgTimestamp("createdAt", 3)
    override val primaryKey = PrimaryKey(id, name = "\"InstantEvalJudgeProject_pkey\"")
}
