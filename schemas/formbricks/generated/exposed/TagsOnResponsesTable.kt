package formbricks.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable

object TagsOnResponsesTable : CompositeIdTable("\"TagsOnResponses\"") {
    val responseId =
        reference(
            "responseId",
            ResponseTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TagsOnResponses_responseId_fkey\"",
        )
    val tagId =
        reference(
            "tagId",
            TagTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TagsOnResponses_tagId_fkey\"",
        )
    override val primaryKey = PrimaryKey(responseId, tagId, name = "\"TagsOnResponses_pkey\"")

    init {
        addIdColumn(responseId)
        addIdColumn(tagId)
    }
}
