package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object LangyTurnRequestTable : IdTable<String>("\"LangyTurnRequest\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val projectId = text("projectId")
    val userId = text("userId")
    val requestId = text("requestId")
    val conversationId = text("conversationId")
    val turnId = text("turnId")
    val status = text("status")
    val leaseOwner = text("leaseOwner")
    val leaseExpiresAt = pgTimestamp("leaseExpiresAt", 3)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"LangyTurnRequest_pkey\"")

    init {
        index("\"LangyTurnRequest_projectId_conversationId_idx\"", false, projectId, conversationId)
        uniqueIndex("\"LangyTurnRequest_projectId_userId_requestId_key\"", projectId, userId, requestId)
    }
}
