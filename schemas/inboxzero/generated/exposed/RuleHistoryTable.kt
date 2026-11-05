package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object RuleHistoryTable : IdTable<String>("\"RuleHistory\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val ruleId =
        reference(
            "ruleId",
            RuleTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"RuleHistory_ruleId_fkey\"",
        )
    val version = integer("version")
    val triggerType = text("triggerType")
    val promptText = text("promptText").nullable()
    val name = text("name")
    val instructions = text("instructions").nullable()
    val enabled = bool("enabled")
    val automate = bool("automate")
    val runOnThreads = bool("runOnThreads")
    val conditionalOperator = text("conditionalOperator")
    val from = text("from").nullable()
    val to = text("to").nullable()
    val subject = text("subject").nullable()
    val body = text("body").nullable()
    val categoryFilterType = text("categoryFilterType").nullable()
    val systemType = text("systemType").nullable()
    val actions = jsonb<String>("actions", { it }, { it })
    val categoryFilters = jsonb<String>("categoryFilters", { it }, { it }).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"RuleHistory_pkey\"")

    init {
        index("\"RuleHistory_ruleId_createdAt_idx\"", false, ruleId, createdAt)
        uniqueIndex("\"RuleHistory_ruleId_version_key\"", ruleId, version)
    }
}
