package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class InstantEvalJudgeProjectEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, InstantEvalJudgeProjectEntity>(InstantEvalJudgeProjectTable)

    var organizationId by InstantEvalJudgeProjectTable.organizationId
    var createdAt by InstantEvalJudgeProjectTable.createdAt
}
