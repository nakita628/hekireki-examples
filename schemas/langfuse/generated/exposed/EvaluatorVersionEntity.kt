package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class EvaluatorVersionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, EvaluatorVersionEntity>(EvaluatorVersionTable)

    var createdAt by EvaluatorVersionTable.createdAt
    var evaluatorId by EvaluatorVersionTable.evaluatorId
    var evaluator by EvaluatorEntity referencedOn EvaluatorVersionTable.evaluatorId
    var version by EvaluatorVersionTable.version
    var createdByUserId by EvaluatorVersionTable.createdByUserId
    var createdByUser by UserEntity optionalReferencedOn EvaluatorVersionTable.createdByUserId
    var prompt by EvaluatorVersionTable.prompt
    var promptMessages by EvaluatorVersionTable.promptMessages
    var partner by EvaluatorVersionTable.partner
    var model by EvaluatorVersionTable.model
    var provider by EvaluatorVersionTable.provider
    var modelParams by EvaluatorVersionTable.modelParams
    var vars by EvaluatorVersionTable.vars
    var variableMapping by EvaluatorVersionTable.variableMapping
    var outputDefinition by EvaluatorVersionTable.outputDefinition
    var sourceCode by EvaluatorVersionTable.sourceCode
    var sourceCodeLanguage by EvaluatorVersionTable.sourceCodeLanguage
    var questions by EvaluatorVersionTable.questions
}
