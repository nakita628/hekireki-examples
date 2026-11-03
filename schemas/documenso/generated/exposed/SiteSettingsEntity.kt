package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class SiteSettingsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SiteSettingsEntity>(SiteSettingsTable)

    var enabled by SiteSettingsTable.enabled
    var data by SiteSettingsTable.data
    var lastModifiedByUserId by SiteSettingsTable.lastModifiedByUserId
    var lastModifiedAt by SiteSettingsTable.lastModifiedAt
    var lastModifiedByUser by UserEntity optionalReferencedOn SiteSettingsTable.lastModifiedByUserId
}
