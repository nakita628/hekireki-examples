package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object AgentTable : IdTable<String>("\"Agent\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val name = text("name")
    val userId =
        optReference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Agent_userId_fkey\"",
        )
    val teamId =
        optReference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Agent_teamId_fkey\"",
        )
    val providerAgentId = text("providerAgentId").uniqueIndex("\"Agent_providerAgentId_key\"")
    val inboundEventTypeId = integer("inboundEventTypeId").nullable()
    val outboundEventTypeId = integer("outboundEventTypeId").nullable()
    val enabled = bool("enabled").default(true)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"Agent_pkey\"")

    init {
        index("\"Agent_userId_idx\"", false, userId)
        index("\"Agent_teamId_idx\"", false, teamId)
        index("\"Agent_inboundEventTypeId_idx\"", false, inboundEventTypeId)
        index("\"Agent_outboundEventTypeId_idx\"", false, outboundEventTypeId)
    }
}
