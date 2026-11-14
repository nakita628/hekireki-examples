package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object SlackConnectionClaimTable : CompositeIdTable("slack_connection_claim") {
    val connectionId = text("connectionId").entityId()
    val claimantId = text("claimantId").entityId()
    val claimantLabel = text("claimantLabel")
    val organizationId = text("organizationId")
    val projectId = text("projectId")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(connectionId, claimantId, name = "slack_connection_claim_pkey")

    init {
        index("\"slack_connection_claim_organizationId_idx\"", false, organizationId)
    }
}
