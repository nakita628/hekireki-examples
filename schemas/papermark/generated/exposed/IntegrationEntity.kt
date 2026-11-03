package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class IntegrationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, IntegrationEntity>(IntegrationTable)

    var name by IntegrationTable.name
    var slug by IntegrationTable.slug
    var description by IntegrationTable.description
    var readme by IntegrationTable.readme
    var developer by IntegrationTable.developer
    var website by IntegrationTable.website
    var logo by IntegrationTable.logo
    var screenshots by IntegrationTable.screenshots
    var verified by IntegrationTable.verified
    var installUrl by IntegrationTable.installUrl
    var category by IntegrationTable.category
    var comingSoon by IntegrationTable.comingSoon
    var createdAt by IntegrationTable.createdAt
    var updatedAt by IntegrationTable.updatedAt
    val installedIntegrations by InstalledIntegrationEntity referrersOn InstalledIntegrationTable.integrationId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(IntegrationTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
