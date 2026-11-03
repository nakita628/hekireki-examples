package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class EvalTemplateEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, EvalTemplateEntity>(EvalTemplateTable)

    var createdAt by EvalTemplateTable.createdAt
    var updatedAt by EvalTemplateTable.updatedAt
    var projectId by EvalTemplateTable.projectId
    var project by ProjectEntity optionalReferencedOn EvalTemplateTable.projectId
    var name by EvalTemplateTable.name
    var version by EvalTemplateTable.version
    var prompt by EvalTemplateTable.prompt
    var type by EvalTemplateTable.type
    var partner by EvalTemplateTable.partner
    var model by EvalTemplateTable.model
    var provider by EvalTemplateTable.provider
    var modelParams by EvalTemplateTable.modelParams
    var vars by EvalTemplateTable.vars
    var outputDefinition by EvalTemplateTable.outputDefinition
    var sourceCode by EvalTemplateTable.sourceCode
    var sourceCodeLanguage by EvalTemplateTable.sourceCodeLanguage
    val jobConfiguration by JobConfigurationEntity optionalReferrersOn JobConfigurationTable.evalTemplateId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(EvalTemplateTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
