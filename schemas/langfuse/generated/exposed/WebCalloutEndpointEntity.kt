package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class WebCalloutEndpointEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, WebCalloutEndpointEntity>(WebCalloutEndpointTable)

    var createdAt by WebCalloutEndpointTable.createdAt
    var updatedAt by WebCalloutEndpointTable.updatedAt
    var projectId by WebCalloutEndpointTable.projectId
    var project by ProjectEntity referencedOn WebCalloutEndpointTable.projectId
    var name by WebCalloutEndpointTable.name
    var url by WebCalloutEndpointTable.url
    var enabled by WebCalloutEndpointTable.enabled
    var toastMessage by WebCalloutEndpointTable.toastMessage
    var requestHeaders by WebCalloutEndpointTable.requestHeaders
    var requestHeaderKeys by WebCalloutEndpointTable.requestHeaderKeys

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(WebCalloutEndpointTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
