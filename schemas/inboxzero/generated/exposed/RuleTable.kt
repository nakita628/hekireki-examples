package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object RuleTable : IdTable<String>("\"Rule\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val name = text("name")
    val enabled = bool("enabled").default(true)
    val automate = bool("automate").default(true)
    val runOnThreads = bool("runOnThreads").default(false)
    val emailAccountId =
        reference(
            "emailAccountId",
            EmailAccountTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Rule_emailAccountId_fkey\"",
        )
    val organizationRuleId =
        optReference(
            "organizationRuleId",
            OrganizationRuleTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Rule_organizationRuleId_fkey\"",
        )
    val organizationRuleMemberEnabled = bool("organizationRuleMemberEnabled").nullable()
    val conditionalOperator =
        pgEnum("conditionalOperator", "\"LogicalOperator\"", LogicalOperator.entries, LogicalOperator::dbName).default(LogicalOperator.AND)
    val instructions = text("instructions").nullable()
    val groupId =
        optReference(
            "groupId",
            GroupTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Rule_groupId_fkey\"",
        ).uniqueIndex("\"Rule_groupId_key\"")
    val from = text("from").nullable()
    val to = text("to").nullable()
    val subject = text("subject").nullable()
    val body = text("body").nullable()
    val categoryFilterType =
        pgEnum("categoryFilterType", "\"CategoryFilterType\"", CategoryFilterType.entries, CategoryFilterType::dbName).nullable()
    val systemType = pgEnum("systemType", "\"SystemType\"", SystemType.entries, SystemType::dbName).nullable()
    val promptText = text("promptText").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Rule_pkey\"")

    init {
        index("\"Rule_emailAccountId_enabled_idx\"", false, emailAccountId, enabled)
        index("\"Rule_organizationRuleId_idx\"", false, organizationRuleId)
        uniqueIndex("\"Rule_id_emailAccountId_key\"", id, emailAccountId)
        uniqueIndex("\"Rule_name_emailAccountId_key\"", name, emailAccountId)
        uniqueIndex("\"Rule_emailAccountId_systemType_key\"", emailAccountId, systemType)
        uniqueIndex("\"Rule_emailAccountId_organizationRuleId_key\"", emailAccountId, organizationRuleId)
    }
}
