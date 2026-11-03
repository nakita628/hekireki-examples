package typebot.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class RuntimeMediaIdCacheEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<RuntimeMediaIdCacheEntity>(RuntimeMediaIdCacheTable)

    val provider by RuntimeMediaIdCacheTable.provider
    val url by RuntimeMediaIdCacheTable.url
    var mediaId by RuntimeMediaIdCacheTable.mediaId
    var expiresAt by RuntimeMediaIdCacheTable.expiresAt
    val publicTypebot by PublicTypebotEntity referencedOn RuntimeMediaIdCacheTable.publicTypebotId
    val publicTypebotId by RuntimeMediaIdCacheTable.publicTypebotId
}
