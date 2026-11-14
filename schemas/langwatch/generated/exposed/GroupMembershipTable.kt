package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object GroupMembershipTable : CompositeIdTable("\"GroupMembership\"") {
    val userId = text("userId").entityId()
    val groupId = text("groupId").entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(userId, groupId, name = "\"GroupMembership_pkey\"")

    init {
        index("\"GroupMembership_userId_idx\"", false, userId)
        index("\"GroupMembership_groupId_idx\"", false, groupId)
    }
}
