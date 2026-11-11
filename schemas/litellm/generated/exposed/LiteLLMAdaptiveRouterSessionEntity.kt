package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMAdaptiveRouterSessionEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<LiteLLMAdaptiveRouterSessionEntity>(LiteLLMAdaptiveRouterSessionTable)

    val sessionId by LiteLLMAdaptiveRouterSessionTable.sessionId
    val routerName by LiteLLMAdaptiveRouterSessionTable.routerName
    val modelName by LiteLLMAdaptiveRouterSessionTable.modelName
    var classifiedType by LiteLLMAdaptiveRouterSessionTable.classifiedType
    var misalignmentCount by LiteLLMAdaptiveRouterSessionTable.misalignmentCount
    var stagnationCount by LiteLLMAdaptiveRouterSessionTable.stagnationCount
    var disengagementCount by LiteLLMAdaptiveRouterSessionTable.disengagementCount
    var satisfactionCount by LiteLLMAdaptiveRouterSessionTable.satisfactionCount
    var failureCount by LiteLLMAdaptiveRouterSessionTable.failureCount
    var loopCount by LiteLLMAdaptiveRouterSessionTable.loopCount
    var exhaustionCount by LiteLLMAdaptiveRouterSessionTable.exhaustionCount
    var lastUserContent by LiteLLMAdaptiveRouterSessionTable.lastUserContent
    var lastAssistantContent by LiteLLMAdaptiveRouterSessionTable.lastAssistantContent
    var toolCallHistory by LiteLLMAdaptiveRouterSessionTable.toolCallHistory
    var pendingToolCalls by LiteLLMAdaptiveRouterSessionTable.pendingToolCalls
    var turnCount by LiteLLMAdaptiveRouterSessionTable.turnCount
    var lastProcessedTurn by LiteLLMAdaptiveRouterSessionTable.lastProcessedTurn
    var cleanCreditAwarded by LiteLLMAdaptiveRouterSessionTable.cleanCreditAwarded
    var terminalStatus by LiteLLMAdaptiveRouterSessionTable.terminalStatus
    var lastActivityAt by LiteLLMAdaptiveRouterSessionTable.lastActivityAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMAdaptiveRouterSessionTable.lastActivityAt)) {
                lastActivityAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
