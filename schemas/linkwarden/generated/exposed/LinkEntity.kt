package linkwarden.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LinkEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, LinkEntity>(LinkTable)

    var name by LinkTable.name
    var type by LinkTable.type
    var description by LinkTable.description
    val highlight by HighlightEntity referrersOn HighlightTable.linkId
    var pinnedBy by UserEntity via PinnedLinksTable
    var createdBy by UserEntity optionalReferencedOn LinkTable.createdById
    var createdById by LinkTable.createdById
    var collection by CollectionEntity referencedOn LinkTable.collectionId
    var collectionId by LinkTable.collectionId
    var tags by TagEntity via LinkToTagTable
    var icon by LinkTable.icon
    var iconWeight by LinkTable.iconWeight
    var color by LinkTable.color
    var url by LinkTable.url
    var textContent by LinkTable.textContent
    var preview by LinkTable.preview
    var image by LinkTable.image
    var pdf by LinkTable.pdf
    var readable by LinkTable.readable
    var monolith by LinkTable.monolith
    var clientSide by LinkTable.clientSide
    var aiTagged by LinkTable.aiTagged
    var metaDescription by LinkTable.metaDescription
    var indexVersion by LinkTable.indexVersion
    var lastPreserved by LinkTable.lastPreserved
    var importDate by LinkTable.importDate
    var createdAt by LinkTable.createdAt
    var updatedAt by LinkTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LinkTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
