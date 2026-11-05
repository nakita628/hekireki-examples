package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object OrganizationRuleTable : IdTable<String>("\"OrganizationRule\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val organizationId =
        reference(
            "organizationId",
            OrganizationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"OrganizationRule_organizationId_fkey\"",
        )
    val enabled = bool("enabled").default(true)
    val name = text("name")
    val instructions = text("instructions").nullable()
    val runOnThreads = bool("runOnThreads").default(false)
    val conditionalOperator =
        pgEnum("conditionalOperator", "\"LogicalOperator\"", LogicalOperator.entries, LogicalOperator::dbName).default(LogicalOperator.AND)
    val from = text("from").nullable()
    val to = text("to").nullable()
    val subject = text("subject").nullable()
    val body = text("body").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"OrganizationRule_pkey\"")

    init {
        index("\"OrganizationRule_organizationId_enabled_idx\"", false, organizationId, enabled)
        uniqueIndex("\"OrganizationRule_organizationId_name_key\"", organizationId, name)
    }
}
