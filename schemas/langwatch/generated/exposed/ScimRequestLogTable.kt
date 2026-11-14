package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ScimRequestLogTable : IdTable<String>("\"ScimRequestLog\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val organizationId = text("organizationId")
    val connectionId = text("connectionId").nullable()
    val method = text("method")
    val resource = text("resource")
    val status = integer("status")
    val reason = text("reason").nullable()
    val detail = text("detail").nullable()
    val occurredAt = pgTimestamp("occurredAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"ScimRequestLog_pkey\"")

    init {
        index("\"ScimRequestLog_organizationId_occurredAt_idx\"", false, organizationId, occurredAt)
        index("\"ScimRequestLog_connectionId_occurredAt_idx\"", false, connectionId, occurredAt)
        index("\"ScimRequestLog_occurredAt_idx\"", false, occurredAt)
    }
}
