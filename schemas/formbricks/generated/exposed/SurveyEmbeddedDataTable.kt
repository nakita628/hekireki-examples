package formbricks.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object SurveyEmbeddedDataTable : IdTable<String>("\"SurveyEmbeddedData\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val workspaceId = text("workspaceId")
    val surveyId = text("surveyId")
    val embeddedDataId = text("embeddedDataId")
    val storageKey = text("storageKey")
    val order = integer("order")
    override val primaryKey = PrimaryKey(id, name = "\"SurveyEmbeddedData_pkey\"")

    init {
        index("\"SurveyEmbeddedData_embeddedDataId_idx\"", false, embeddedDataId)
        index("\"SurveyEmbeddedData_surveyId_order_idx\"", false, surveyId, order)
        uniqueIndex("\"SurveyEmbeddedData_surveyId_embeddedDataId_key\"", surveyId, embeddedDataId)
        uniqueIndex("\"SurveyEmbeddedData_surveyId_storageKey_key\"", surveyId, storageKey)
        foreignKey(
            surveyId to SurveyTable.id,
            workspaceId to SurveyTable.workspaceId,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            name = "\"SurveyEmbeddedData_surveyId_workspaceId_fkey\"",
        )
        foreignKey(
            embeddedDataId to EmbeddedDataTable.id,
            workspaceId to EmbeddedDataTable.workspaceId,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            name = "\"SurveyEmbeddedData_embeddedDataId_workspaceId_fkey\"",
        )
    }
}
