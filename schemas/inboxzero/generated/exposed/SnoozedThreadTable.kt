package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object SnoozedThreadTable : IdTable<String>("\"SnoozedThread\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val threadId = text("threadId")
    val scheduledFor = pgTimestamp("scheduledFor", 3)
    val status =
        pgEnum("status", "\"SnoozedThreadStatus\"", SnoozedThreadStatus.entries, SnoozedThreadStatus::dbName).default(
            SnoozedThreadStatus.PENDING,
        )
    val clientMutationId = text("clientMutationId").nullable()
    val emailAccountId =
        reference(
            "emailAccountId",
            EmailAccountTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"SnoozedThread_emailAccountId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"SnoozedThread_pkey\"")

    init {
        index("\"SnoozedThread_status_scheduledFor_idx\"", false, status, scheduledFor)
        index("\"SnoozedThread_emailAccountId_threadId_status_idx\"", false, emailAccountId, threadId, status)
        uniqueIndex("\"SnoozedThread_emailAccountId_clientMutationId_key\"", emailAccountId, clientMutationId)
    }
}
