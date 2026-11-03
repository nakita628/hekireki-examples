package documenso.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object OrganisationMonthlyStatTable : IdTable<String>("\"OrganisationMonthlyStat\"") {
    override val id = text("id").entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val organisationId =
        reference(
            "organisationId",
            OrganisationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"OrganisationMonthlyStat_organisationId_fkey\"",
        )
    val period = text("period")
    val documentCount = integer("documentCount").default(0)
    val emailCount = integer("emailCount").default(0)
    val apiCount = integer("apiCount").default(0)
    val emailReports = integer("emailReports").default(0)
    override val primaryKey = PrimaryKey(id, name = "\"OrganisationMonthlyStat_pkey\"")

    init {
        index("\"OrganisationMonthlyStat_organisationId_idx\"", false, organisationId)
        uniqueIndex("\"OrganisationMonthlyStat_organisationId_period_key\"", organisationId, period)
    }
}
