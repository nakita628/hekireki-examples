package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object MastraKnowledgeCursorsTable : CompositeIdTable("mastra_knowledge_cursors") {
    val sourceThreadId = text("sourceThreadId").entityId()
    val agent = text("agent").entityId()
    val lastKnowledgeId = text("lastKnowledgeId")
    val updatedAt = pgTimestamp("updatedAt", 6)
    val updatedAtZ =
        pgTimestamptz("updatedAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(sourceThreadId, agent, name = "mastra_knowledge_cursors_pkey")
}
