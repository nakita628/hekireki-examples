package litellm.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import org.jetbrains.exposed.v1.json.jsonb

object LiteLLMLensReviewTable : CompositeIdTable("\"LiteLLM_LensReview\"") {
    val lensId =
        reference(
            "lens_id",
            LiteLLMLensTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"LiteLLM_LensReview_lens_id_fkey\"",
        )
    val criteriaKey = text("criteria_key").entityId()
    val executionId = text("execution_id").entityId()
    val data = jsonb<String>("data", { it }, { it })
    override val primaryKey = PrimaryKey(lensId, criteriaKey, executionId, name = "\"LiteLLM_LensReview_pkey\"")

    init {
        addIdColumn(lensId)
    }
}
