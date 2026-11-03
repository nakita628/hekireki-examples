import uuid
from typing import Any, TypeVar

from django.db import models
from django.utils import timezone


_M = TypeVar("_M", bound=models.Model)


class AutoNowQuerySet(models.QuerySet[_M]):
    def update(self, **kwargs: Any) -> int:
        for field in self.model._meta.concrete_fields:
            if getattr(field, "auto_now", False):
                kwargs.setdefault(field.name, getattr(field, "stamp", timezone.now)())
        return super().update(**kwargs)


def uuid4_str() -> str:
    return str(uuid.uuid4())


class WorkspaceType(models.TextChoices):
    USER = "USER"
    TEAM = "TEAM"


class ReqType(models.TextChoices):
    REST = "REST"
    GQL = "GQL"


class TeamAccessRole(models.TextChoices):
    OWNER = "OWNER"
    VIEWER = "VIEWER"
    EDITOR = "EDITOR"


class MockServerAction(models.TextChoices):
    CREATED = "CREATED"
    DELETED = "DELETED"
    ACTIVATED = "ACTIVATED"
    DEACTIVATED = "DEACTIVATED"


class Team(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()

    class Meta:
        db_table = "Team"


class TeamMember(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    role = models.TextField(choices=TeamAccessRole.choices)
    user_uid = models.TextField(db_column="userUid")
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="members", db_column="teamID", db_index=False)

    class Meta:
        db_table = "TeamMember"
        constraints = [
            models.UniqueConstraint(fields=["team", "user_uid"], name="TeamMember_teamID_userUid_key"),
        ]


class TeamInvitation(models.Model):
    id = models.TextField(primary_key=True, default=None)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="invitations", db_column="teamID", db_index=False)
    creator_uid = models.TextField(db_column="creatorUid")
    invitee_email = models.TextField(db_column="inviteeEmail")
    invitee_role = models.TextField(choices=TeamAccessRole.choices, db_column="inviteeRole")

    class Meta:
        db_table = "TeamInvitation"
        constraints = [
            models.UniqueConstraint(fields=["team", "invitee_email"], name="TeamInvitation_teamID_inviteeEmail_key"),
        ]
        indexes = [
            models.Index(fields=["team"]),
        ]


class TeamCollection(models.Model):
    id = models.TextField(primary_key=True, default=None)
    parent = models.ForeignKey("self", on_delete=models.CASCADE, related_name="children", db_column="parentID", null=True, db_index=False)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="collections", db_column="teamID", db_index=False)
    title = models.TextField()
    order_index = models.IntegerField(db_column="orderIndex")
    data = models.JSONField(null=True)
    created_on = models.DateTimeField(db_column="createdOn", default=timezone.now)
    updated_on = models.DateTimeField(db_column="updatedOn", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "TeamCollection"
        constraints = [
            models.UniqueConstraint(fields=["team", "parent", "order_index"], name="TeamCollection_teamID_parentID_orderIndex_key"),
        ]


class TeamRequest(models.Model):
    id = models.TextField(primary_key=True, default=None)
    collection = models.ForeignKey("TeamCollection", on_delete=models.CASCADE, related_name="requests", db_column="collectionID", db_index=False)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="requests", db_column="teamID", db_index=False)
    title = models.TextField()
    request = models.JSONField()
    mock_examples = models.JSONField(null=True, db_column="mockExamples")
    order_index = models.IntegerField(db_column="orderIndex")
    created_on = models.DateTimeField(db_column="createdOn", default=timezone.now)
    updated_on = models.DateTimeField(db_column="updatedOn", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "TeamRequest"
        constraints = [
            models.UniqueConstraint(fields=["team", "collection", "order_index"], name="TeamRequest_teamID_collectionID_orderIndex_key"),
        ]


class TeamEnvironment(models.Model):
    id = models.TextField(primary_key=True, default=None)
    team = models.ForeignKey("Team", on_delete=models.CASCADE, related_name="environments", db_column="teamID", db_index=False)
    name = models.TextField()
    variables = models.JSONField()

    class Meta:
        db_table = "TeamEnvironment"


class User(models.Model):
    uid = models.TextField(primary_key=True, default=None)
    display_name = models.TextField(null=True, db_column="displayName")
    email = models.TextField(null=True, unique=True)
    photo_url = models.TextField(null=True, db_column="photoURL")
    is_admin = models.BooleanField(db_column="isAdmin", default=False)
    refresh_token = models.TextField(null=True, db_column="refreshToken")
    current_restsession = models.JSONField(null=True, db_column="currentRESTSession")
    current_gqlsession = models.JSONField(null=True, db_column="currentGQLSession")
    created_on = models.DateTimeField(db_column="createdOn", default=timezone.now)
    last_logged_on = models.DateTimeField(null=True, db_column="lastLoggedOn")
    last_active_on = models.DateTimeField(null=True, db_column="lastActiveOn")

    class Meta:
        db_table = "User"


class Account(models.Model):
    id = models.TextField(primary_key=True, default=None)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="provider_accounts", db_column="userId", db_index=False)
    provider = models.TextField()
    provider_account_id = models.TextField(db_column="providerAccountId")
    provider_refresh_token = models.TextField(null=True, db_column="providerRefreshToken")
    provider_access_token = models.TextField(null=True, db_column="providerAccessToken")
    provider_scope = models.TextField(null=True, db_column="providerScope")
    logged_in = models.DateTimeField(db_column="loggedIn", default=timezone.now)

    class Meta:
        db_table = "Account"
        constraints = [
            models.UniqueConstraint(fields=["provider", "provider_account_id"], name="Account_provider_providerAccountId_key"),
        ]


class VerificationToken(models.Model):
    device_identifier = models.TextField(db_column="deviceIdentifier")
    token = models.TextField(primary_key=True, default=None)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="verification_tokens", db_column="userUid", db_index=False)
    expires_on = models.DateTimeField(db_column="expiresOn")

    class Meta:
        db_table = "VerificationToken"
        constraints = [
            models.UniqueConstraint(fields=["device_identifier", "token"], name="VerificationToken_deviceIdentifier_token_key"),
        ]


class UserSettings(models.Model):
    id = models.TextField(primary_key=True, default=None)
    user = models.OneToOneField("User", on_delete=models.CASCADE, related_name="settings", db_column="userUid")
    properties = models.JSONField()
    updated_on = models.DateTimeField(db_column="updatedOn", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "UserSettings"


class UserHistory(models.Model):
    id = models.TextField(primary_key=True, default=None)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="history", db_column="userUid", db_index=False)
    req_type = models.TextField(choices=ReqType.choices, db_column="reqType")
    request = models.JSONField()
    response_metadata = models.JSONField(db_column="responseMetadata")
    is_starred = models.BooleanField(db_column="isStarred")
    executed_on = models.DateTimeField(db_column="executedOn", default=timezone.now)

    class Meta:
        db_table = "UserHistory"


class UserEnvironment(models.Model):
    id = models.TextField(primary_key=True, default=None)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="environments", db_column="userUid", db_index=False)
    name = models.TextField(null=True)
    variables = models.JSONField()
    is_global = models.BooleanField(db_column="isGlobal")

    class Meta:
        db_table = "UserEnvironment"


class InvitedUsers(models.Model):
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="invited_users", db_column="adminUid", db_index=False)
    admin_email = models.TextField(db_column="adminEmail")
    invitee_email = models.TextField(primary_key=True, db_column="inviteeEmail", default=None)
    invited_on = models.DateTimeField(db_column="invitedOn", default=timezone.now)

    class Meta:
        db_table = "InvitedUsers"


class UserRequest(models.Model):
    id = models.TextField(primary_key=True, default=None)
    user_collection = models.ForeignKey("UserCollection", on_delete=models.CASCADE, related_name="requests", db_column="collectionID", db_index=False)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="user_requests", db_column="userUid", db_index=False)
    title = models.TextField()
    request = models.JSONField()
    type = models.TextField(choices=ReqType.choices)
    mock_examples = models.JSONField(null=True, db_column="mockExamples")
    order_index = models.IntegerField(db_column="orderIndex")
    created_on = models.DateTimeField(db_column="createdOn", default=timezone.now)
    updated_on = models.DateTimeField(db_column="updatedOn", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "UserRequest"
        constraints = [
            models.UniqueConstraint(fields=["user", "user_collection", "order_index"], name="UserRequest_userUid_collectionID_orderIndex_key"),
        ]


class UserCollection(models.Model):
    id = models.TextField(primary_key=True, default=None)
    parent = models.ForeignKey("self", on_delete=models.CASCADE, related_name="children", db_column="parentID", null=True, db_index=False)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="user_collections", db_column="userUid", db_index=False)
    title = models.TextField()
    type = models.TextField(choices=ReqType.choices)
    order_index = models.IntegerField(db_column="orderIndex")
    data = models.JSONField(null=True)
    created_on = models.DateTimeField(db_column="createdOn", default=timezone.now)
    updated_on = models.DateTimeField(db_column="updatedOn", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "UserCollection"
        constraints = [
            models.UniqueConstraint(fields=["user", "parent", "order_index"], name="UserCollection_userUid_parentID_orderIndex_key"),
        ]


class MockServer(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField()
    subdomain = models.TextField(unique=True)
    user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="mock_servers", db_column="creatorUid", null=True, db_index=False)
    collection_id = models.TextField(db_column="collectionID")
    workspace_type = models.TextField(choices=WorkspaceType.choices, db_column="workspaceType")
    workspace_id = models.TextField(db_column="workspaceID")
    delay_in_ms = models.IntegerField(db_column="delayInMs", default=0)
    is_public = models.BooleanField(db_column="isPublic", default=True)
    is_active = models.BooleanField(db_column="isActive", default=True)
    hit_count = models.IntegerField(db_column="hitCount", default=0)
    last_hit_at = models.DateTimeField(null=True, db_column="lastHitAt")
    created_on = models.DateTimeField(db_column="createdOn", default=timezone.now)
    updated_on = models.DateTimeField(db_column="updatedOn", auto_now=True)
    deleted_at = models.DateTimeField(null=True, db_column="deletedAt")
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "MockServer"


class MockServerLog(models.Model):
    id = models.TextField(primary_key=True, default=None)
    mock_server = models.ForeignKey("MockServer", on_delete=models.CASCADE, related_name="request_logs", db_column="mockServerID", db_index=False)
    request_method = models.TextField(db_column="requestMethod")
    request_path = models.TextField(db_column="requestPath")
    request_headers = models.JSONField(db_column="requestHeaders")
    request_body = models.JSONField(null=True, db_column="requestBody")
    request_query = models.JSONField(null=True, db_column="requestQuery")
    response_status = models.IntegerField(db_column="responseStatus")
    response_headers = models.JSONField(db_column="responseHeaders")
    response_body = models.JSONField(null=True, db_column="responseBody")
    response_time = models.IntegerField(db_column="responseTime")
    ip_address = models.TextField(null=True, db_column="ipAddress")
    user_agent = models.TextField(null=True, db_column="userAgent")
    executed_at = models.DateTimeField(db_column="executedAt", default=timezone.now)

    class Meta:
        db_table = "MockServerLog"
        indexes = [
            models.Index(fields=["mock_server"]),
            models.Index(fields=["mock_server", "executed_at"]),
        ]


class MockServerActivity(models.Model):
    id = models.TextField(primary_key=True, default=None)
    mock_server = models.ForeignKey("MockServer", on_delete=models.CASCADE, related_name="activity_history", db_column="mockServerID", db_index=False)
    action = models.TextField(choices=MockServerAction.choices)
    performed_by = models.TextField(null=True, db_column="performedBy")
    performed_at = models.DateTimeField(db_column="performedAt", default=timezone.now)

    class Meta:
        db_table = "MockServerActivity"
        indexes = [
            models.Index(fields=["mock_server"]),
        ]


class Shortcode(models.Model):
    id = models.TextField(primary_key=True, default=None)
    request = models.JSONField()
    user = models.ForeignKey("User", on_delete=models.SET_NULL, related_name="shortcodes", db_column="creatorUid", null=True, db_index=False)
    embed_properties = models.JSONField(null=True, db_column="embedProperties")
    created_on = models.DateTimeField(db_column="createdOn", default=timezone.now)
    updated_on = models.DateTimeField(db_column="updatedOn", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "Shortcode"
        constraints = [
            models.UniqueConstraint(fields=["id", "user"], name="Shortcode_id_creatorUid_key"),
        ]


class InfraConfig(models.Model):
    id = models.TextField(primary_key=True, default=None)
    name = models.TextField(unique=True)
    value = models.TextField(null=True)
    is_encrypted = models.BooleanField(db_column="isEncrypted", default=False)
    last_synced_env_file_value = models.TextField(null=True, db_column="lastSyncedEnvFileValue")
    created_on = models.DateTimeField(db_column="createdOn", default=timezone.now)
    updated_on = models.DateTimeField(db_column="updatedOn", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "InfraConfig"


class PersonalAccessToken(models.Model):
    id = models.TextField(primary_key=True, default=None)
    user = models.ForeignKey("User", on_delete=models.CASCADE, related_name="personal_access_tokens", db_column="userUid", db_index=False)
    label = models.TextField()
    token = models.TextField(unique=True, default=uuid4_str)
    expires_on = models.DateTimeField(null=True, db_column="expiresOn")
    created_on = models.DateTimeField(db_column="createdOn", default=timezone.now)
    updated_on = models.DateTimeField(db_column="updatedOn", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "PersonalAccessToken"


class InfraToken(models.Model):
    id = models.TextField(primary_key=True, default=None)
    creator_uid = models.TextField(db_column="creatorUid")
    label = models.TextField()
    token = models.TextField(unique=True, default=uuid4_str)
    expires_on = models.DateTimeField(null=True, db_column="expiresOn")
    created_on = models.DateTimeField(db_column="createdOn", default=timezone.now)
    updated_on = models.DateTimeField(db_column="updatedOn", default=timezone.now)

    class Meta:
        db_table = "InfraToken"


class PublishedDocs(models.Model):
    id = models.TextField(primary_key=True, default=None)
    slug = models.TextField()
    title = models.TextField()
    collection_id = models.TextField(db_column="collectionID")
    creator_uid = models.TextField(db_column="creatorUid")
    version = models.TextField()
    auto_sync = models.BooleanField(db_column="autoSync")
    document_tree = models.JSONField(null=True, db_column="documentTree")
    workspace_type = models.TextField(choices=WorkspaceType.choices, db_column="workspaceType")
    workspace_id = models.TextField(db_column="workspaceID")
    environment_id = models.TextField(null=True, db_column="environmentID")
    environment_name = models.TextField(null=True, db_column="environmentName")
    environment_variables = models.JSONField(null=True, db_column="environmentVariables")
    metadata = models.JSONField(null=True)
    created_on = models.DateTimeField(db_column="createdOn", default=timezone.now)
    updated_on = models.DateTimeField(db_column="updatedOn", auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "PublishedDocs"
        constraints = [
            models.UniqueConstraint(fields=["slug", "version"], name="PublishedDocs_slug_version_key"),
        ]
        indexes = [
            models.Index(fields=["collection_id"]),
        ]
