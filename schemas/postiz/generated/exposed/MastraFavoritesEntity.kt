package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class MastraFavoritesEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<MastraFavoritesEntity>(MastraFavoritesTable)

    val userId by MastraFavoritesTable.userId
    val entityType by MastraFavoritesTable.entityType
    val entityId by MastraFavoritesTable.entityId
    var createdAt by MastraFavoritesTable.createdAt
    var createdAtZ by MastraFavoritesTable.createdAtZ
}
