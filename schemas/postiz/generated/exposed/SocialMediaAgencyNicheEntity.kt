package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class SocialMediaAgencyNicheEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<SocialMediaAgencyNicheEntity>(SocialMediaAgencyNicheTable)

    val agencyId by SocialMediaAgencyNicheTable.agencyId
    val niche by SocialMediaAgencyNicheTable.niche
    val agency by SocialMediaAgencyEntity referencedOn SocialMediaAgencyNicheTable.agencyId
}
