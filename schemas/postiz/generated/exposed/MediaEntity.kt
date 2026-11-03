package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class MediaEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MediaEntity>(MediaTable)

    var name by MediaTable.name
    var originalName by MediaTable.originalName
    var path by MediaTable.path
    var organizationId by MediaTable.organizationId
    var createdAt by MediaTable.createdAt
    var updatedAt by MediaTable.updatedAt
    var deletedAt by MediaTable.deletedAt
    var fileSize by MediaTable.fileSize
    var type by MediaTable.type
    var thumbnail by MediaTable.thumbnail
    var alt by MediaTable.alt
    var thumbnailTimestamp by MediaTable.thumbnailTimestamp
    var status by MediaTable.status
    var processingError by MediaTable.processingError
    var organization by OrganizationEntity referencedOn MediaTable.organizationId
    val agencies by SocialMediaAgencyEntity optionalReferrersOn SocialMediaAgencyTable.logoId
    val userPicture by UserEntity optionalReferrersOn UserTable.pictureId
    val oauthApps by OAuthAppEntity optionalReferrersOn OAuthAppTable.pictureId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(MediaTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
