package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object GatewayConnectUpstreamTable : IdTable<String>("\"GatewayConnectUpstream\"") {
    override val id = text("organizationId").entityId()
    val baseUrl = text("baseUrl")
    val encryptedToken = text("encryptedToken")
    val instanceId = text("instanceId")
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"GatewayConnectUpstream_pkey\"")
}
