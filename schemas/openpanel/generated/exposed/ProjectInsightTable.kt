package openpanel.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object ProjectInsightTable : IdTable<UUID>("project_insights") {
    override val id = javaUUID("id").databaseDefault("gen_random_uuid()").entityId()
    val projectId = text("projectId")
    val moduleKey = text("moduleKey")
    val dimensionKey = text("dimensionKey")
    val windowKind = text("windowKind")
    val state = pgEnum("state", "\"InsightState\"", InsightState.entries, InsightState::dbName).default(InsightState.ACTIVE)
    val title = text("title")
    val summary = text("summary").nullable()
    val displayName = text("displayName").default("")
    val payload = jsonb<String>("payload", { it }, { it }).default("{}")
    val direction = text("direction").nullable()
    val impactScore = double("impactScore").default(0.0)
    val severityBand = text("severityBand").nullable()
    val relevanceScore = double("relevanceScore").nullable()
    val aiSummary = text("aiSummary").nullable()
    val aiCategory = text("aiCategory").nullable()
    val emailWorthy = bool("emailWorthy").default(false)
    val referenceWorthy = bool("referenceWorthy").default(false)
    val enrichedAt = pgTimestamp("enrichedAt", 3).nullable()
    val enrichVersion = integer("enrichVersion").nullable()
    val version = integer("version").default(1)
    val threadId = javaUUID("threadId").databaseDefault("gen_random_uuid()")
    val windowStart = pgTimestamp("windowStart", 3).nullable()
    val windowEnd = pgTimestamp("windowEnd", 3).nullable()
    val firstDetectedAt =
        pgTimestamp("firstDetectedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastUpdatedAt =
        pgTimestamp("lastUpdatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastSeenAt = pgTimestamp("lastSeenAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "project_insights_pkey")

    init {
        index(
            "\"project_insights_projectId_impactScore_idx\"",
            false,
            functions = listOf(indexExpression("\"projectId\""), indexExpression("\"impactScore\" DESC")),
        )
        index("\"project_insights_projectId_moduleKey_windowKind_state_idx\"", false, projectId, moduleKey, windowKind, state)
        index(
            "\"project_insights_projectId_state_relevanceScore_idx\"",
            false,
            functions = listOf(indexExpression("\"projectId\""), indexExpression("\"state\""), indexExpression("\"relevanceScore\" DESC")),
        )
        uniqueIndex(
            "project_insights_projectId_moduleKey_dimensionKey_windowKin_key",
            projectId,
            moduleKey,
            dimensionKey,
            windowKind,
            state,
        )
    }
}
