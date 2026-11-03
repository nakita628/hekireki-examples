package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object UserTable : IdTable<String>("\"User\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val name = text("name").nullable()
    val email = text("email").nullable().uniqueIndex("\"User_email_key\"")
    val emailVerified = pgTimestamp("emailVerified", 3).nullable()
    val image = text("image").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val contactId = text("contactId").nullable()
    val plan = text("plan").default("free")
    val stripeId = text("stripeId").nullable().uniqueIndex("\"User_stripeId_key\"")
    val subscriptionId = text("subscriptionId").nullable().uniqueIndex("\"User_subscriptionId_key\"")
    val startsAt = pgTimestamp("startsAt", 3).nullable()
    val endsAt = pgTimestamp("endsAt", 3).nullable()
    val referralTrack = pgEnum("referralTrack", "\"ReferralTrack\"", ReferralTrack.entries, ReferralTrack::dbName).nullable()
    val referralTrackSelectedAt = pgTimestamp("referralTrackSelectedAt", 3).nullable()
    val referralProfile = jsonb<String>("referralProfile", { it }, { it }).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"User_pkey\"")
}
