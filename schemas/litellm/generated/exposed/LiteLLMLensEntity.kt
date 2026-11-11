package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class LiteLLMLensEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMLensEntity>(LiteLLMLensTable)

    val reviews by LiteLLMLensReviewEntity referrersOn LiteLLMLensReviewTable.lensId
    var version by LiteLLMLensTable.version
    var data by LiteLLMLensTable.data
    var dueAt by LiteLLMLensTable.dueAt
}
