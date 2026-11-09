package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class OidcLoginGroupEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OidcLoginGroupEntity>(OidcLoginGroupTable)

    var createdAt by OidcLoginGroupTable.createdAt
    var updatedAt by OidcLoginGroupTable.updatedAt
    var workspaceId by OidcLoginGroupTable.workspaceId
    var workspace by WorkspaceEntity referencedOn OidcLoginGroupTable.workspaceId
    var oidcProviderId by OidcLoginGroupTable.oidcProviderId
    var oidcProvider by OidcProviderEntity referencedOn OidcLoginGroupTable.oidcProviderId
    var groupValue by OidcLoginGroupTable.groupValue
    var allowAllUsers by OidcLoginGroupTable.allowAllUsers

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(OidcLoginGroupTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
