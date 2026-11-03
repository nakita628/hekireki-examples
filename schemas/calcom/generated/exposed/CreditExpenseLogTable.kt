package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.util.UUID

object CreditExpenseLogTable : IdTable<String>("\"CreditExpenseLog\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val creditBalanceId =
        reference(
            "creditBalanceId",
            CreditBalanceTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"CreditExpenseLog_creditBalanceId_fkey\"",
        )
    val bookingUid =
        text("bookingUid")
            .references(
                BookingTable.uid,
                onDelete = ReferenceOption.CASCADE,
                onUpdate = ReferenceOption.CASCADE,
                fkName = "\"CreditExpenseLog_bookingUid_fkey\"",
            ).nullable()
    val credits = integer("credits").nullable()
    val creditType = pgEnum("creditType", "\"CreditType\"", CreditType.entries, CreditType::dbName)
    val date = pgTimestamp("date", 3)
    val smsSid = text("smsSid").nullable()
    val smsSegments = integer("smsSegments").nullable()
    val phoneNumber = text("phoneNumber").nullable()
    val email = text("email").nullable()
    val callDuration = integer("callDuration").nullable()
    val creditFor = pgEnum("creditFor", "\"CreditUsageType\"", CreditUsageType.entries, CreditUsageType::dbName).nullable()
    val externalRef = text("externalRef").nullable().uniqueIndex("\"CreditExpenseLog_externalRef_key\"")
    override val primaryKey = PrimaryKey(id, name = "\"CreditExpenseLog_pkey\"")
}
