package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class LiteLLMShadowEvalJobEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMShadowEvalJobEntity>(LiteLLMShadowEvalJobTable)

    var groupId by LiteLLMShadowEvalJobTable.groupId
    var targetType by LiteLLMShadowEvalJobTable.targetType
    var targetId by LiteLLMShadowEvalJobTable.targetId
    var routerName by LiteLLMShadowEvalJobTable.routerName
    var routerNames by LiteLLMShadowEvalJobTable.routerNames
    var models by LiteLLMShadowEvalJobTable.models
    var direction by LiteLLMShadowEvalJobTable.direction
    var baselineModel by LiteLLMShadowEvalJobTable.baselineModel
    var judgeModel by LiteLLMShadowEvalJobTable.judgeModel
    var shadowPercentage by LiteLLMShadowEvalJobTable.shadowPercentage
    var maxTurns by LiteLLMShadowEvalJobTable.maxTurns
    var maxBudget by LiteLLMShadowEvalJobTable.maxBudget
    var createdAt by LiteLLMShadowEvalJobTable.createdAt
    var createdBy by LiteLLMShadowEvalJobTable.createdBy
    var endsAt by LiteLLMShadowEvalJobTable.endsAt
    var stoppedAt by LiteLLMShadowEvalJobTable.stoppedAt
    var stoppedBy by LiteLLMShadowEvalJobTable.stoppedBy
}
