package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object LiteLLMDailyGatewayRequestsTable : CompositeIdTable("\"LiteLLM_DailyGatewayRequests\"") {
    val date = text("date").entityId()
    val category = text("category").entityId()
    val route = text("route").entityId()
    val successfulRequests = long("successful_requests").default(0L)
    val failedRequests = long("failed_requests").default(0L)
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(date, category, route, name = "\"LiteLLM_DailyGatewayRequests_pkey\"")

    init {
        index("\"LiteLLM_DailyGatewayRequests_date_idx\"", false, date)
    }
}
