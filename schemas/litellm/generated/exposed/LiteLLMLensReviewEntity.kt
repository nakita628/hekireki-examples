package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class LiteLLMLensReviewEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<LiteLLMLensReviewEntity>(LiteLLMLensReviewTable)

    val lensId by LiteLLMLensReviewTable.lensId
    val criteriaKey by LiteLLMLensReviewTable.criteriaKey
    val executionId by LiteLLMLensReviewTable.executionId
    var data by LiteLLMLensReviewTable.data
    val lens by LiteLLMLensEntity referencedOn LiteLLMLensReviewTable.lensId
}
