package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable

object JoinRequestTable : IdTable<String>("\"JoinRequest\"") {
    override val id = text("id").entityId()
    val userId = text("userId")
    val organizationId = text("organizationId")
    val domain = text("domain")
    val state = text("state")
    val matchedVia = text("matchedVia")
    val origin = text("origin").default("web")
    val expiresAt = pgTimestamp("expiresAt", 3).nullable()
    val resolvedAt = pgTimestamp("resolvedAt", 3).nullable()
    val resolvedByType = text("resolvedByType").nullable()
    val resolvedById = text("resolvedById").nullable()
    val withdrawalCause = text("withdrawalCause").nullable()
    val connectionId = text("connectionId").nullable()
    val occurredAt = pgTimestamp("occurredAt", 3)
    val lastEventId = text("lastEventId")
    val acceptedAt = pgTimestamp("acceptedAt", 3)
    val projectionVersion = text("projectionVersion")
    val createdAt = pgTimestamp("createdAt", 3)
    val updatedAt = pgTimestamp("updatedAt", 3)
    override val primaryKey = PrimaryKey(id, name = "\"JoinRequest_pkey\"")

    init {
        index("\"JoinRequest_organizationId_state_idx\"", false, organizationId, state)
        index("\"JoinRequest_userId_state_idx\"", false, userId, state)
        index("\"JoinRequest_userId_organizationId_idx\"", false, userId, organizationId)
        index("\"JoinRequest_domain_idx\"", false, domain)
    }
}
