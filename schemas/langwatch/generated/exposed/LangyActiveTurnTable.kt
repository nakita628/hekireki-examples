package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object LangyActiveTurnTable : IdTable<String>("\"LangyActiveTurn\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val projectId = text("projectId")
    val conversationId = text("conversationId")
    val turnId = text("turnId")
    val requestId = text("requestId")
    val userId = text("userId")
    val status = text("status")
    val leaseOwner = text("leaseOwner")
    val leaseExpiresAt = pgTimestamp("leaseExpiresAt", 3)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"LangyActiveTurn_pkey\"")

    init {
        index("\"LangyActiveTurn_projectId_userId_idx\"", false, projectId, userId)
        uniqueIndex("\"LangyActiveTurn_projectId_conversationId_key\"", projectId, conversationId)
    }
}
