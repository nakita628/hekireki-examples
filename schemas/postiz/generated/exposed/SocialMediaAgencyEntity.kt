package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class SocialMediaAgencyEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SocialMediaAgencyEntity>(SocialMediaAgencyTable)

    var userId by SocialMediaAgencyTable.userId
    var name by SocialMediaAgencyTable.name
    var logoId by SocialMediaAgencyTable.logoId
    var website by SocialMediaAgencyTable.website
    var slug by SocialMediaAgencyTable.slug
    var facebook by SocialMediaAgencyTable.facebook
    var instagram by SocialMediaAgencyTable.instagram
    var twitter by SocialMediaAgencyTable.twitter
    var linkedIn by SocialMediaAgencyTable.linkedIn
    var youtube by SocialMediaAgencyTable.youtube
    var tiktok by SocialMediaAgencyTable.tiktok
    var otherSocialMedia by SocialMediaAgencyTable.otherSocialMedia
    var shortDescription by SocialMediaAgencyTable.shortDescription
    var description by SocialMediaAgencyTable.description
    var approved by SocialMediaAgencyTable.approved
    var createdAt by SocialMediaAgencyTable.createdAt
    var updatedAt by SocialMediaAgencyTable.updatedAt
    var deletedAt by SocialMediaAgencyTable.deletedAt
    var logo by MediaEntity optionalReferencedOn SocialMediaAgencyTable.logoId
    var user by UserEntity referencedOn SocialMediaAgencyTable.userId
    val niches by SocialMediaAgencyNicheEntity referrersOn SocialMediaAgencyNicheTable.agencyId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SocialMediaAgencyTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
