package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMDailyGatewayFailedRequestsEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<LiteLLMDailyGatewayFailedRequestsEntity>(LiteLLMDailyGatewayFailedRequestsTable)

    val date by LiteLLMDailyGatewayFailedRequestsTable.date
    val category by LiteLLMDailyGatewayFailedRequestsTable.category
    val route by LiteLLMDailyGatewayFailedRequestsTable.route
    val statusCode by LiteLLMDailyGatewayFailedRequestsTable.statusCode
    var failedRequests by LiteLLMDailyGatewayFailedRequestsTable.failedRequests
    var createdAt by LiteLLMDailyGatewayFailedRequestsTable.createdAt
    var updatedAt by LiteLLMDailyGatewayFailedRequestsTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMDailyGatewayFailedRequestsTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
