package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class WorkspaceEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, WorkspaceEntity>(WorkspaceTable)

    var createdAt by WorkspaceTable.createdAt
    var updatedAt by WorkspaceTable.updatedAt
    var name by WorkspaceTable.name
    var deleted by WorkspaceTable.deleted
    var slug by WorkspaceTable.slug
    val workspaceAccess by WorkspaceAccessEntity referrersOn WorkspaceAccessTable.workspaceId
    val workspaceOptions by WorkspaceOptionsEntity referrersOn WorkspaceOptionsTable.workspaceId
    val configurationObject by ConfigurationObjectEntity referrersOn ConfigurationObjectTable.workspaceId
    val configurationObjectLink by ConfigurationObjectLinkEntity referrersOn ConfigurationObjectLinkTable.workspaceId
    val preferences by UserPreferencesEntity optionalReferrersOn UserPreferencesTable.workspaceId
    val workspaceUserProperties by WorkspaceUserPropertiesEntity referrersOn WorkspaceUserPropertiesTable.workspaceId
    var featuresEnabled by WorkspaceTable.featuresEnabled
    val profileBuilders by ProfileBuilderEntity referrersOn ProfileBuilderTable.workspaceId
    val oidcLoginGroups by OidcLoginGroupEntity referrersOn OidcLoginGroupTable.workspaceId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(WorkspaceTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
