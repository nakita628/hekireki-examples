package openpanel.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ShareWidgetEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ShareWidgetEntity>(ShareWidgetTable)

    var projectId by ShareWidgetTable.projectId
    var project by ProjectEntity referencedOn ShareWidgetTable.projectId
    var organization by OrganizationEntity referencedOn ShareWidgetTable.organizationId
    var organizationId by ShareWidgetTable.organizationId
    var public by ShareWidgetTable.public
    var options by ShareWidgetTable.options1
    var createdAt by ShareWidgetTable.createdAt
    var updatedAt by ShareWidgetTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ShareWidgetTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
