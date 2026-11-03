package postiz.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object PayoutProblemsTable : IdTable<String>("\"PayoutProblems\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val status = text("status")
    val orderId =
        reference(
            "orderId",
            OrdersTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"PayoutProblems_orderId_fkey\"",
        )
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"PayoutProblems_userId_fkey\"",
        )
    val postId =
        optReference(
            "postId",
            PostTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"PayoutProblems_postId_fkey\"",
        )
    val amount = integer("amount")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"PayoutProblems_pkey\"")
}
