package hoppscotch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class PublishedDocsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, PublishedDocsEntity>(PublishedDocsTable)

    var slug by PublishedDocsTable.slug
    var title by PublishedDocsTable.title
    var collectionID by PublishedDocsTable.collectionID
    var creatorUid by PublishedDocsTable.creatorUid
    var version by PublishedDocsTable.version
    var autoSync by PublishedDocsTable.autoSync
    var documentTree by PublishedDocsTable.documentTree
    var workspaceType by PublishedDocsTable.workspaceType
    var workspaceID by PublishedDocsTable.workspaceID
    var environmentID by PublishedDocsTable.environmentID
    var environmentName by PublishedDocsTable.environmentName
    var environmentVariables by PublishedDocsTable.environmentVariables
    var metadata by PublishedDocsTable.metadata
    var createdOn by PublishedDocsTable.createdOn
    var updatedOn by PublishedDocsTable.updatedOn

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(PublishedDocsTable.updatedOn)) {
                updatedOn = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
