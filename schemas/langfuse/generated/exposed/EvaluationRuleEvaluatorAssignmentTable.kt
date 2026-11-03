package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object EvaluationRuleEvaluatorAssignmentTable : IdTable<String>("evaluation_rule_evaluator_assignments") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val projectId =
        reference(
            "project_id",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "evaluation_rule_evaluator_assignments_project_id_fkey",
        )
    val evaluationRuleId =
        reference(
            "evaluation_rule_id",
            EvaluationRuleTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "evaluation_rule_evaluator_assignments_evaluation_rule_id_fkey",
        )
    val evaluatorId =
        reference(
            "evaluator_id",
            EvaluatorTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "evaluation_rule_evaluator_assignments_evaluator_id_fkey",
        )
    val variableMapping = jsonb<String>("variable_mapping", { it }, { it }).nullable()
    override val primaryKey = PrimaryKey(id, name = "evaluation_rule_evaluator_assignments_pkey")

    init {
        index("evaluation_rule_assignments_project_id_idx", false, projectId)
        index("evaluation_rule_assignments_evaluator_id_idx", false, evaluatorId)
        uniqueIndex("evaluation_rule_assignments_rule_evaluator_key", evaluationRuleId, evaluatorId)
    }
}
