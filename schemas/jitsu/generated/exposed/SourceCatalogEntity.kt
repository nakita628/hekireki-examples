package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class SourceCatalogEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<SourceCatalogEntity>(SourceCatalogTable)

    val `package` by SourceCatalogTable.`package`
    val version by SourceCatalogTable.version
    val key by SourceCatalogTable.key
    var catalog by SourceCatalogTable.catalog
    var timestamp by SourceCatalogTable.timestamp
    var status by SourceCatalogTable.status
    var description by SourceCatalogTable.description
}
