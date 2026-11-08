from sqlalchemy import Column, Dialect, Enum, ForeignKey, Index, Integer, JSON, String, Table, TypeDecorator, text
from sqlalchemy.dialects.mysql import DATETIME, MEDIUMTEXT
from sqlalchemy.orm import DeclarativeBase, Mapped, mapped_column, relationship
from typing import Any, Optional
from datetime import datetime, timezone
import uuid as uuid_mod


class UtcDateTime(TypeDecorator[datetime]):
    impl = DATETIME
    cache_ok = True

    def process_bind_param(self, value: Optional[datetime], dialect: Dialect) -> Optional[datetime]:
        if value is None or value.tzinfo is None:
            return value
        return value.astimezone(timezone.utc).replace(tzinfo=None)

    def process_result_value(self, value: Optional[datetime], dialect: Dialect) -> Optional[datetime]:
        return None if value is None else value.replace(tzinfo=timezone.utc)


def utc_now() -> datetime:
    return datetime.now(timezone.utc)


class Base(DeclarativeBase):
    type_annotation_map = {datetime: UtcDateTime(fsp=3), str: String(191)}

role_to_user = Table(
    "_RoleToUser",
    Base.metadata,
    Column("A", String(191), ForeignKey("Role.id", ondelete="CASCADE", onupdate="CASCADE", name="_RoleToUser_A_fkey"), nullable=False),
    Column("B", String(191), ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="_RoleToUser_B_fkey"), nullable=False),
    Index("_RoleToUser_AB_unique", "A", "B", unique=True),
    Index("_RoleToUser_B_index", "B"),
    mysql_charset="utf8mb4",
    mysql_collate="utf8mb4_unicode_ci",
)

user_to_user_link = Table(
    "_UserToUserLink",
    Base.metadata,
    Column("A", String(191), ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="_UserToUserLink_A_fkey"), nullable=False),
    Column("B", String(191), ForeignKey("UserLink.id", ondelete="CASCADE", onupdate="CASCADE", name="_UserToUserLink_B_fkey"), nullable=False),
    Index("_UserToUserLink_AB_unique", "A", "B", unique=True),
    Index("_UserToUserLink_B_index", "B"),
    mysql_charset="utf8mb4",
    mysql_collate="utf8mb4_unicode_ci",
)

track_to_user = Table(
    "_TrackToUser",
    Base.metadata,
    Column("A", Integer, ForeignKey("Track.id", ondelete="CASCADE", onupdate="CASCADE", name="_TrackToUser_A_fkey"), nullable=False),
    Column("B", String(191), ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="_TrackToUser_B_fkey"), nullable=False),
    Index("_TrackToUser_AB_unique", "A", "B", unique=True),
    Index("_TrackToUser_B_index", "B"),
    mysql_charset="utf8mb4",
    mysql_collate="utf8mb4_unicode_ci",
)

challenge_to_tag = Table(
    "_ChallengeToTag",
    Base.metadata,
    Column("A", Integer, ForeignKey("Challenge.id", ondelete="CASCADE", onupdate="CASCADE", name="_ChallengeToTag_A_fkey"), nullable=False),
    Column("B", String(191), ForeignKey("Tag.id", ondelete="CASCADE", onupdate="CASCADE", name="_ChallengeToTag_B_fkey"), nullable=False),
    Index("_ChallengeToTag_AB_unique", "A", "B", unique=True),
    Index("_ChallengeToTag_B_index", "B"),
    mysql_charset="utf8mb4",
    mysql_collate="utf8mb4_unicode_ci",
)


class Account(Base):
    __tablename__ = "Account"

    id: Mapped[str] = mapped_column(primary_key=True)
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="Account_userId_fkey"))
    type: Mapped[str]
    provider: Mapped[str]
    provider_account_id: Mapped[str] = mapped_column("providerAccountId")
    refresh_token: Mapped[Optional[str]]
    access_token: Mapped[Optional[str]]
    expires_at: Mapped[Optional[int]]
    token_type: Mapped[Optional[str]]
    scope: Mapped[Optional[str]]
    id_token: Mapped[Optional[str]]
    session_state: Mapped[Optional[str]]

    __table_args__ = (
        Index("Account_userId_idx", "userId"),
        Index("Account_provider_providerAccountId_key", "provider", "providerAccountId", unique=True),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    user: Mapped["User"] = relationship(back_populates="accounts")

class Session(Base):
    __tablename__ = "Session"

    id: Mapped[str] = mapped_column(primary_key=True)
    session_token: Mapped[str] = mapped_column("sessionToken")
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="CASCADE", onupdate="CASCADE", name="Session_userId_fkey"))
    expires: Mapped[datetime]

    __table_args__ = (
        Index("Session_sessionToken_key", "sessionToken", unique=True),
        Index("Session_userId_idx", "userId"),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    user: Mapped["User"] = relationship(back_populates="sessions")

class Role(Base):
    __tablename__ = "Role"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    role: Mapped[str] = mapped_column(Enum("USER", "ADMIN", "MODERATOR", "CREATOR", "SUPPORTER", "CONTRIBUTOR", name="RoleTypes"))

    __table_args__ = (
        Index("Role_role_key", "role", unique=True),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    user: Mapped[list["User"]] = relationship(secondary=role_to_user, back_populates="roles")

class UserLink(Base):
    __tablename__ = "UserLink"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    url: Mapped[str] = mapped_column(String(256))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP(3)"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    user: Mapped[list["User"]] = relationship(secondary=user_to_user_link, back_populates="user_links")

class User(Base):
    __tablename__ = "User"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    name: Mapped[str]
    email: Mapped[str]
    email_verified: Mapped[Optional[datetime]] = mapped_column("emailVerified")
    image: Mapped[Optional[str]]
    bio: Mapped[str] = mapped_column(String(1000), default="", server_default=text("''"))
    status: Mapped[str] = mapped_column(Enum("BANNED", "ACTIVE", name="UserStatus"), default="ACTIVE", server_default=text("'ACTIVE'"))
    ban_reason: Mapped[Optional[str]] = mapped_column("banReason", MEDIUMTEXT)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP(3)"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("User_email_key", "email", unique=True),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    accounts: Mapped[list["Account"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    sessions: Mapped[list["Session"]] = relationship(cascade="all, delete", passive_deletes=True, back_populates="user")
    challenge: Mapped[list["Challenge"]] = relationship(passive_deletes="all", back_populates="user")
    vote: Mapped[list["Vote"]] = relationship(passive_deletes="all", back_populates="user")
    submission: Mapped[list["Submission"]] = relationship(passive_deletes="all", back_populates="user")
    bookmark: Mapped[list["Bookmark"]] = relationship(passive_deletes="all", back_populates="user")
    shared_solution: Mapped[list["SharedSolution"]] = relationship(back_populates="user")
    comment: Mapped[list["Comment"]] = relationship(passive_deletes="all", back_populates="user")
    reports: Mapped[list["Report"]] = relationship(foreign_keys="Report.reporter_id", passive_deletes="all", back_populates="reporter")
    reports_about: Mapped[list["Report"]] = relationship(foreign_keys="Report.user_id", back_populates="user")
    moderated: Mapped[list["Report"]] = relationship(foreign_keys="Report.moderator_id", back_populates="moderator")
    image_upload: Mapped[list["ImageUpload"]] = relationship(passive_deletes="all", back_populates="user")
    beta_tokens: Mapped[list["BetaTokens"]] = relationship(back_populates="user")
    donations: Mapped[list["Donation"]] = relationship(passive_deletes="all", back_populates="user")
    short_urls: Mapped[list["ShortURL"]] = relationship(back_populates="user")
    to_user_notification: Mapped[list["Notification"]] = relationship(foreign_keys="Notification.to_user_id", passive_deletes="all", back_populates="to_user")
    from_user_notification: Mapped[list["Notification"]] = relationship(foreign_keys="Notification.from_user_id", passive_deletes="all", back_populates="from_user")
    roles: Mapped[list["Role"]] = relationship(secondary=role_to_user, back_populates="user")
    user_links: Mapped[list["UserLink"]] = relationship(secondary=user_to_user_link, back_populates="user")
    tracks: Mapped[list["Track"]] = relationship(secondary=track_to_user, back_populates="enrolled_users")

class VerificationToken(Base):
    __tablename__ = "VerificationToken"

    identifier: Mapped[str]
    token: Mapped[str] = mapped_column()
    expires: Mapped[datetime]

    __table_args__ = (
        Index("VerificationToken_token_key", "token", unique=True),
        Index("VerificationToken_identifier_token_key", "identifier", "token", unique=True),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    __mapper_args__ = {"primary_key": [token]}

class Tag(Base):
    __tablename__ = "Tag"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    tag: Mapped[str] = mapped_column(Enum("POPULAR", "NEWEST", name="Tags"))

    __table_args__ = (
        Index("Tag_tag_key", "tag", unique=True),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    challenges: Mapped[list["Challenge"]] = relationship(secondary=challenge_to_tag, back_populates="tags")

class Challenge(Base):
    __tablename__ = "Challenge"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP(3)"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    difficulty: Mapped[str] = mapped_column(Enum("BEGINNER", "EASY", "MEDIUM", "HARD", "EXTREME", "EVENT", name="Difficulty"))
    name: Mapped[str]
    slug: Mapped[str]
    description: Mapped[str] = mapped_column(MEDIUMTEXT)
    short_description: Mapped[str] = mapped_column("shortDescription")
    code: Mapped[str] = mapped_column(MEDIUMTEXT)
    tests: Mapped[str] = mapped_column(MEDIUMTEXT)
    status: Mapped[str] = mapped_column(Enum("DECLINED", "BANNED", "PENDING", "ACTIVE", name="ChallengeStatus"), default="PENDING", server_default=text("'PENDING'"))
    tsconfig: Mapped[Optional[dict[str, Any]]] = mapped_column(JSON(none_as_null=True))
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="RESTRICT", onupdate="CASCADE", name="Challenge_userId_fkey"))

    __table_args__ = (
        Index("Challenge_name_key", "name", unique=True),
        Index("Challenge_slug_key", "slug", unique=True),
        Index("Challenge_userId_idx", "userId"),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    user: Mapped["User"] = relationship(back_populates="challenge")
    vote: Mapped[list["Vote"]] = relationship(back_populates="challenge")
    submission: Mapped[list["Submission"]] = relationship(passive_deletes="all", back_populates="challenge")
    comment: Mapped[list["Comment"]] = relationship(back_populates="root_challenge")
    bookmark: Mapped[list["Bookmark"]] = relationship(back_populates="challenge")
    shared_solution: Mapped[list["SharedSolution"]] = relationship(back_populates="challenge")
    report: Mapped[list["Report"]] = relationship(back_populates="challenge")
    track_challenge: Mapped[list["TrackChallenge"]] = relationship(passive_deletes="all", back_populates="challenge")
    notification: Mapped[list["Notification"]] = relationship(back_populates="challenge")
    tags: Mapped[list["Tag"]] = relationship(secondary=challenge_to_tag, back_populates="challenges")

class Vote(Base):
    __tablename__ = "Vote"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="RESTRICT", onupdate="CASCADE", name="Vote_userId_fkey"))
    root_type: Mapped[str] = mapped_column("rootType", Enum("CHALLENGE", "COMMENT", "SUBMISSION", "SHAREDSOLUTION", name="VoteType"))
    challenge_id: Mapped[Optional[int]] = mapped_column("challengeId", ForeignKey("Challenge.id", ondelete="SET NULL", onupdate="CASCADE", name="Vote_challengeId_fkey"))
    comment_id: Mapped[Optional[int]] = mapped_column("commentId", ForeignKey("Comment.id", ondelete="SET NULL", onupdate="CASCADE", name="Vote_commentId_fkey"))
    submission_id: Mapped[Optional[int]] = mapped_column("submissionId", ForeignKey("Submission.id", ondelete="SET NULL", onupdate="CASCADE", name="Vote_submissionId_fkey"))
    shared_solution_id: Mapped[Optional[int]] = mapped_column("sharedSolutionId", ForeignKey("SharedSolution.id", ondelete="SET NULL", onupdate="CASCADE", name="Vote_sharedSolutionId_fkey"))

    __table_args__ = (
        Index("Vote_userId_idx", "userId"),
        Index("Vote_challengeId_idx", "challengeId"),
        Index("Vote_commentId_idx", "commentId"),
        Index("Vote_submissionId_idx", "submissionId"),
        Index("Vote_sharedSolutionId_idx", "sharedSolutionId"),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    user: Mapped["User"] = relationship(back_populates="vote")
    challenge: Mapped[Optional["Challenge"]] = relationship(back_populates="vote")
    comment: Mapped[Optional["Comment"]] = relationship(back_populates="vote")
    submission: Mapped[Optional["Submission"]] = relationship(back_populates="vote")
    shared_solution: Mapped[Optional["SharedSolution"]] = relationship(back_populates="vote")

class SharedSolution(Base):
    __tablename__ = "SharedSolution"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP(3)"))
    is_pinned: Mapped[bool] = mapped_column("isPinned", default=False, server_default=text("false"))
    title: Mapped[str]
    description: Mapped[str] = mapped_column(MEDIUMTEXT)
    user_id: Mapped[Optional[str]] = mapped_column("userId", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="SharedSolution_userId_fkey"))
    challenge_id: Mapped[Optional[int]] = mapped_column("challengeId", ForeignKey("Challenge.id", ondelete="SET NULL", onupdate="CASCADE", name="SharedSolution_challengeId_fkey"))

    __table_args__ = (
        Index("SharedSolution_userId_idx", "userId"),
        Index("SharedSolution_challengeId_idx", "challengeId"),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    user: Mapped[Optional["User"]] = relationship(back_populates="shared_solution")
    challenge: Mapped[Optional["Challenge"]] = relationship(back_populates="shared_solution")
    vote: Mapped[list["Vote"]] = relationship(back_populates="shared_solution")
    solution_comment: Mapped[list["Comment"]] = relationship(back_populates="root_solution")
    report: Mapped[list["Report"]] = relationship(back_populates="solution")
    notification: Mapped[list["Notification"]] = relationship(back_populates="shared_solution")

class Submission(Base):
    __tablename__ = "Submission"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    code: Mapped[str] = mapped_column(MEDIUMTEXT)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP(3)"))
    is_successful: Mapped[bool] = mapped_column("isSuccessful")
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="RESTRICT", onupdate="CASCADE", name="Submission_userId_fkey"))
    challenge_id: Mapped[int] = mapped_column("challengeId", ForeignKey("Challenge.id", ondelete="RESTRICT", onupdate="CASCADE", name="Submission_challengeId_fkey"))

    __table_args__ = (
        Index("Submission_userId_idx", "userId"),
        Index("Submission_challengeId_idx", "challengeId"),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    user: Mapped["User"] = relationship(back_populates="submission")
    challenge: Mapped["Challenge"] = relationship(back_populates="submission")
    vote: Mapped[list["Vote"]] = relationship(back_populates="submission")

class Bookmark(Base):
    __tablename__ = "Bookmark"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="RESTRICT", onupdate="CASCADE", name="Bookmark_userId_fkey"))
    challenge_id: Mapped[Optional[int]] = mapped_column("challengeId", ForeignKey("Challenge.id", ondelete="SET NULL", onupdate="CASCADE", name="Bookmark_challengeId_fkey"))
    created_at: Mapped[Optional[datetime]] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP(3)"))

    __table_args__ = (
        Index("Bookmark_userId_idx", "userId"),
        Index("Bookmark_challengeId_idx", "challengeId"),
        Index("Bookmark_userId_challengeId_key", "userId", "challengeId", unique=True),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    user: Mapped["User"] = relationship(back_populates="bookmark")
    challenge: Mapped[Optional["Challenge"]] = relationship(back_populates="bookmark")

class ReportIssue(Base):
    __tablename__ = "ReportIssue"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    report_id: Mapped[int] = mapped_column("reportId", ForeignKey("Report.id", ondelete="RESTRICT", onupdate="CASCADE", name="ReportIssue_reportId_fkey"))
    type: Mapped[str] = mapped_column(Enum("DEROGATORY", "OTHER", "UNCLEAR", "BULLYING", "SPAM", "HATESPEECH", "THREAT", name="IssueType"))

    __table_args__ = (
        Index("ReportIssue_reportId_idx", "reportId"),
        Index("ReportIssue_id_key", "id", unique=True),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    report: Mapped["Report"] = relationship(back_populates="issues")

class Report(Base):
    __tablename__ = "Report"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    text_: Mapped[str] = mapped_column("text", MEDIUMTEXT)
    type: Mapped[str] = mapped_column(Enum("CHALLENGE", "USER", "COMMENT", "SOLUTION", name="ReportType"))
    status: Mapped[str] = mapped_column(Enum("PENDING", "CLEARED", "DISMISSED", name="ReportStatus"))
    reporter_id: Mapped[str] = mapped_column("reporterId", ForeignKey("User.id", ondelete="RESTRICT", onupdate="CASCADE", name="Report_reporterId_fkey"))
    user_id: Mapped[Optional[str]] = mapped_column("userId", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="Report_userId_fkey"))
    challenge_id: Mapped[Optional[int]] = mapped_column("challengeId", ForeignKey("Challenge.id", ondelete="SET NULL", onupdate="CASCADE", name="Report_challengeId_fkey"))
    comment_id: Mapped[Optional[int]] = mapped_column("commentId", ForeignKey("Comment.id", ondelete="SET NULL", onupdate="CASCADE", name="Report_commentId_fkey"))
    solution_id: Mapped[Optional[int]] = mapped_column("solutionId", ForeignKey("SharedSolution.id", ondelete="SET NULL", onupdate="CASCADE", name="Report_solutionId_fkey"))
    moderator_id: Mapped[Optional[str]] = mapped_column("moderatorId", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="Report_moderatorId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP(3)"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("Report_commentId_idx", "commentId"),
        Index("Report_reporterId_idx", "reporterId"),
        Index("Report_userId_idx", "userId"),
        Index("Report_challengeId_idx", "challengeId"),
        Index("Report_solutionId_idx", "solutionId"),
        Index("Report_moderatorId_idx", "moderatorId"),
        Index("Report_id_key", "id", unique=True),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    reporter: Mapped["User"] = relationship(foreign_keys=[reporter_id], back_populates="reports")
    user: Mapped[Optional["User"]] = relationship(foreign_keys=[user_id], back_populates="reports_about")
    challenge: Mapped[Optional["Challenge"]] = relationship(back_populates="report")
    comment: Mapped[Optional["Comment"]] = relationship(back_populates="report")
    solution: Mapped[Optional["SharedSolution"]] = relationship(back_populates="report")
    moderator: Mapped[Optional["User"]] = relationship(foreign_keys=[moderator_id], back_populates="moderated")
    issues: Mapped[list["ReportIssue"]] = relationship(passive_deletes="all", back_populates="report")

class Comment(Base):
    __tablename__ = "Comment"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    text_: Mapped[str] = mapped_column("text", MEDIUMTEXT)
    visible: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    parent_id: Mapped[Optional[int]] = mapped_column("parentId", ForeignKey("Comment.id", ondelete="NO ACTION", onupdate="NO ACTION", name="Comment_parentId_fkey"))
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="RESTRICT", onupdate="CASCADE", name="Comment_userId_fkey"))
    root_type: Mapped[str] = mapped_column("rootType", Enum("SOLUTION", "CHALLENGE", name="CommentRoot"), default="CHALLENGE", server_default=text("'CHALLENGE'"))
    root_challenge_id: Mapped[Optional[int]] = mapped_column("rootChallengeId", ForeignKey("Challenge.id", ondelete="SET NULL", onupdate="CASCADE", name="Comment_rootChallengeId_fkey"))
    root_solution_id: Mapped[Optional[int]] = mapped_column("rootSolutionId", ForeignKey("SharedSolution.id", ondelete="SET NULL", onupdate="CASCADE", name="Comment_rootSolutionId_fkey"))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP(3)"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("Comment_rootSolutionId_idx", "rootSolutionId"),
        Index("Comment_rootChallengeId_idx", "rootChallengeId"),
        Index("Comment_parentId_idx", "parentId"),
        Index("Comment_userId_idx", "userId"),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    parent_comment: Mapped[Optional["Comment"]] = relationship(remote_side=[id], back_populates="replies")
    user: Mapped["User"] = relationship(back_populates="comment")
    root_challenge: Mapped[Optional["Challenge"]] = relationship(back_populates="comment")
    root_solution: Mapped[Optional["SharedSolution"]] = relationship(back_populates="solution_comment")
    replies: Mapped[list["Comment"]] = relationship(passive_deletes="all", back_populates="parent_comment")
    report: Mapped[list["Report"]] = relationship(back_populates="comment")
    vote: Mapped[list["Vote"]] = relationship(back_populates="comment")
    notification: Mapped[list["Notification"]] = relationship(back_populates="comment")

class ImageUpload(Base):
    __tablename__ = "ImageUpload"

    id: Mapped[str] = mapped_column(primary_key=True, default=lambda: str(uuid_mod.uuid4()))
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="RESTRICT", onupdate="CASCADE", name="ImageUpload_userId_fkey"))
    url: Mapped[str] = mapped_column(String(256))
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP(3)"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)

    __table_args__ = (
        Index("ImageUpload_userId_idx", "userId"),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    user: Mapped["User"] = relationship(back_populates="image_upload")

class TrackChallenge(Base):
    __tablename__ = "TrackChallenge"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    order_id: Mapped[int] = mapped_column("orderId")
    challenge_id: Mapped[int] = mapped_column("challengeId", ForeignKey("Challenge.id", ondelete="RESTRICT", onupdate="CASCADE", name="TrackChallenge_challengeId_fkey"))
    track_id: Mapped[Optional[int]] = mapped_column("trackId", ForeignKey("Track.id", ondelete="SET NULL", onupdate="CASCADE", name="TrackChallenge_trackId_fkey"))

    __table_args__ = (
        Index("TrackChallenge_challengeId_idx", "challengeId"),
        Index("TrackChallenge_trackId_idx", "trackId"),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    challenge: Mapped["Challenge"] = relationship(back_populates="track_challenge")
    track: Mapped[Optional["Track"]] = relationship(back_populates="track_challenges")

class Track(Base):
    __tablename__ = "Track"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    name: Mapped[str]
    slug: Mapped[str]
    description: Mapped[str] = mapped_column(MEDIUMTEXT)
    visible: Mapped[bool] = mapped_column(default=True, server_default=text("true"))
    is_coming_soon: Mapped[bool] = mapped_column("isComingSoon", default=False, server_default=text("false"))

    __table_args__ = (
        Index("Track_name_key", "name", unique=True),
        Index("Track_slug_key", "slug", unique=True),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    track_challenges: Mapped[list["TrackChallenge"]] = relationship(back_populates="track")
    enrolled_users: Mapped[list["User"]] = relationship(secondary=track_to_user, back_populates="tracks")

class BetaTokens(Base):
    __tablename__ = "BetaTokens"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    token: Mapped[str]
    user_id: Mapped[Optional[str]] = mapped_column("userId", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="BetaTokens_userId_fkey"))

    __table_args__ = (
        Index("BetaTokens_token_key", "token", unique=True),
        Index("BetaTokens_userId_idx", "userId"),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    user: Mapped[Optional["User"]] = relationship(back_populates="beta_tokens")

class Donation(Base):
    __tablename__ = "Donation"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    user_id: Mapped[str] = mapped_column("userId", ForeignKey("User.id", ondelete="RESTRICT", onupdate="CASCADE", name="Donation_userId_fkey"))
    amount: Mapped[int]
    stripe_checkout_session_id: Mapped[str] = mapped_column("stripeCheckoutSessionId")
    created_at: Mapped[Optional[datetime]] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP(3)"))

    __table_args__ = (
        Index("Donation_stripeCheckoutSessionId_key", "stripeCheckoutSessionId", unique=True),
        Index("Donation_userId_idx", "userId"),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    user: Mapped["User"] = relationship(back_populates="donations")

class Notification(Base):
    __tablename__ = "Notification"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP(3)"))
    updated_at: Mapped[datetime] = mapped_column("updatedAt", default=utc_now, onupdate=utc_now)
    is_read: Mapped[bool] = mapped_column("isRead", default=False, server_default=text("false"))
    type: Mapped[str] = mapped_column(Enum("LIKE", "REPLY", "MENTION", "MISC", "SYSTEM", name="NotificationType"))
    to_user_id: Mapped[str] = mapped_column("toUserId", ForeignKey("User.id", ondelete="RESTRICT", onupdate="CASCADE", name="Notification_toUserId_fkey"))
    from_user_id: Mapped[str] = mapped_column("fromUserId", ForeignKey("User.id", ondelete="RESTRICT", onupdate="CASCADE", name="Notification_fromUserId_fkey"))
    comment_id: Mapped[Optional[int]] = mapped_column("commentId", ForeignKey("Comment.id", ondelete="SET NULL", onupdate="CASCADE", name="Notification_commentId_fkey"))
    content: Mapped[Optional[str]]
    blurb: Mapped[Optional[str]]
    url: Mapped[str]
    challenge_id: Mapped[Optional[int]] = mapped_column("challengeId", ForeignKey("Challenge.id", ondelete="SET NULL", onupdate="CASCADE", name="Notification_challengeId_fkey"))
    shared_solution_id: Mapped[Optional[int]] = mapped_column("sharedSolutionId", ForeignKey("SharedSolution.id", ondelete="SET NULL", onupdate="CASCADE", name="Notification_sharedSolutionId_fkey"))

    __table_args__ = (
        Index("Notification_toUserId_idx", "toUserId"),
        Index("Notification_fromUserId_idx", "fromUserId"),
        Index("Notification_commentId_idx", "commentId"),
        Index("Notification_challengeId_idx", "challengeId"),
        Index("Notification_sharedSolutionId_idx", "sharedSolutionId"),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    to_user: Mapped["User"] = relationship(foreign_keys=[to_user_id], back_populates="to_user_notification")
    from_user: Mapped["User"] = relationship(foreign_keys=[from_user_id], back_populates="from_user_notification")
    comment: Mapped[Optional["Comment"]] = relationship(back_populates="notification")
    challenge: Mapped[Optional["Challenge"]] = relationship(back_populates="notification")
    shared_solution: Mapped[Optional["SharedSolution"]] = relationship(back_populates="notification")

class ShortURL(Base):
    __tablename__ = "ShortURL"

    id: Mapped[int] = mapped_column(primary_key=True, autoincrement=True)
    original_url: Mapped[str] = mapped_column("originalUrl", MEDIUMTEXT)
    short_url_slug: Mapped[str] = mapped_column("shortUrlSlug")
    created_at: Mapped[datetime] = mapped_column("createdAt", default=utc_now, server_default=text("CURRENT_TIMESTAMP(3)"))
    expires_at: Mapped[Optional[datetime]] = mapped_column("expiresAt")
    clicks: Mapped[int] = mapped_column(default=0, server_default=text("'0'"))
    user_id: Mapped[Optional[str]] = mapped_column("userId", ForeignKey("User.id", ondelete="SET NULL", onupdate="CASCADE", name="ShortURL_userId_fkey"))

    __table_args__ = (
        Index("ShortURL_shortUrlSlug_key", "shortUrlSlug", unique=True),
        Index("ShortURL_userId_idx", "userId"),
        Index("ShortURL_shortUrlSlug_idx", "shortUrlSlug"),
        {"mysql_charset": "utf8mb4", "mysql_collate": "utf8mb4_unicode_ci"},
    )

    user: Mapped[Optional["User"]] = relationship(back_populates="short_urls")
