package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object LiteLLMDailyGatewayFailedRequestsTable : CompositeIdTable("\"LiteLLM_DailyGatewayFailedRequests\"") {
    val date = text("date").entityId()
    val category = text("category").entityId()
    val route = text("route").entityId()
    val statusCode = integer("status_code").entityId()
    val failedRequests = long("failed_requests").default(0L)
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(date, category, route, statusCode, name = "\"LiteLLM_DailyGatewayFailedRequests_pkey\"")

    init {
        index("\"LiteLLM_DailyGatewayFailedRequests_date_idx\"", false, date)
    }
}
