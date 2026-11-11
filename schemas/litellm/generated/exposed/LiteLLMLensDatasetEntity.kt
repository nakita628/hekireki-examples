package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class LiteLLMLensDatasetEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<LiteLLMLensDatasetEntity>(LiteLLMLensDatasetTable)

    val id1 by LiteLLMLensDatasetTable.id1
    val revision by LiteLLMLensDatasetTable.revision
    var createdAt by LiteLLMLensDatasetTable.createdAt
    var data by LiteLLMLensDatasetTable.data
}
