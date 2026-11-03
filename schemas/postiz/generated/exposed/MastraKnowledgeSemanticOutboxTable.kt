package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object MastraKnowledgeSemanticOutboxTable : IdTable<String>("mastra_knowledge_semantic_outbox") {
    override val id = text("id").entityId()
    val idempotencyKey = text("idempotencyKey").uniqueIndex("idx_knowledge_outbox_idempotency")
    val documentId = text("documentId")
    val documentType = text("documentType")
    val operation = text("operation")
    val scope = jsonb<String>("scope", { it }, { it })
    val scopeKey = text("scopeKey")
    val status = text("status")
    val attempts = integer("attempts")
    val availableAt = pgTimestamp("availableAt", 6)
    val claimedAt = pgTimestamp("claimedAt", 6).nullable()
    val claimedBy = text("claimedBy").nullable()
    val createdAt = pgTimestamp("createdAt", 6)
    val completedAt = pgTimestamp("completedAt", 6).nullable()
    val availableAtZ =
        pgTimestamptz("availableAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val claimedAtZ =
        pgTimestamptz("claimedAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdAtZ =
        pgTimestamptz("createdAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val completedAtZ =
        pgTimestamptz("completedAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "mastra_knowledge_semantic_outbox_pkey")

    init {
        index("idx_knowledge_outbox_claim", false, status, availableAt, createdAt)
    }
}
