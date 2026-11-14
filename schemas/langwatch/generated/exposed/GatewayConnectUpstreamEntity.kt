package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class GatewayConnectUpstreamEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, GatewayConnectUpstreamEntity>(GatewayConnectUpstreamTable)

    var baseUrl by GatewayConnectUpstreamTable.baseUrl
    var encryptedToken by GatewayConnectUpstreamTable.encryptedToken
    var instanceId by GatewayConnectUpstreamTable.instanceId
    var updatedAt by GatewayConnectUpstreamTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(GatewayConnectUpstreamTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
