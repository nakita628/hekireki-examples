import uuid
from datetime import datetime
from datetime import timezone as dt_timezone
from typing import Any, TypeVar

from django.conf import settings
from django.contrib.postgres.fields import ArrayField
from django.contrib.postgres.indexes import GinIndex
from django.db import models
from django.db.backends.base.base import BaseDatabaseWrapper
from django.db.models.expressions import RawSQL
from django.utils import timezone


class UtcDateTimeField(models.DateTimeField):  # type: ignore[type-arg]
    def get_db_prep_value(self, value: Any, connection: BaseDatabaseWrapper, prepared: bool = False) -> Any:
        if not prepared:
            value = self.get_prep_value(value)
        if not isinstance(value, datetime):
            return super().get_db_prep_value(value, connection, prepared=True)
        if timezone.is_naive(value):
            value = timezone.make_aware(value, timezone.get_default_timezone())
        value = value.astimezone(dt_timezone.utc)
        if connection.vendor == "sqlite":
            return value.isoformat(timespec="milliseconds")
        return connection.ops.adapt_datetimefield_value(value.replace(tzinfo=None))

    def from_db_value(self, value: datetime | None, expression: Any, connection: BaseDatabaseWrapper) -> datetime | None:
        if value is None:
            return None
        if timezone.is_naive(value):
            value = value.replace(tzinfo=dt_timezone.utc)
        return value if settings.USE_TZ else timezone.make_naive(value, timezone.get_default_timezone())


_M = TypeVar("_M", bound=models.Model)


class AutoNowQuerySet(models.QuerySet[_M]):
    def update(self, **kwargs: Any) -> int:
        for field in self.model._meta.concrete_fields:
            if getattr(field, "auto_now", False):
                kwargs.setdefault(field.name, getattr(field, "stamp", timezone.now)())
        return super().update(**kwargs)


def uuid4_str() -> str:
    return str(uuid.uuid4())


def lite_llm_agent_identity_required_scopes_default() -> list[str]:
    return ["user_impersonation"]


class JobStatus(models.TextChoices):
    ACTIVE = "ACTIVE"
    INACTIVE = "INACTIVE"


class AccountingDeliveryState(models.TextChoices):
    pending = "pending"
    applied = "applied"
    parked = "parked"


class LiteLLMBudgetTable(models.Model):
    budget_id = models.TextField(primary_key=True, default=uuid4_str)
    max_budget = models.FloatField(null=True)
    soft_budget = models.FloatField(null=True)
    max_parallel_requests = models.IntegerField(null=True)
    tpm_limit = models.BigIntegerField(null=True)
    rpm_limit = models.BigIntegerField(null=True)
    tpd_limit = models.BigIntegerField(null=True)
    model_max_budget = models.JSONField(null=True)
    model_tpm_limit = models.JSONField(null=True)
    model_rpm_limit = models.JSONField(null=True)
    budget_duration = models.TextField(null=True)
    budget_reset_at = UtcDateTimeField(null=True)
    allowed_models = ArrayField(models.TextField(), default=list)
    temp_budget_increase = models.FloatField(null=True)
    temp_budget_expiry = UtcDateTimeField(null=True)
    created_at = UtcDateTimeField(default=timezone.now)
    created_by = models.TextField()
    updated_at = UtcDateTimeField(auto_now=True)
    updated_by = models.TextField()
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_BudgetTable"


class LiteLLMCredentialsTable(models.Model):
    credential_id = models.TextField(primary_key=True, default=uuid4_str)
    credential_name = models.TextField(unique=True)
    display_name = models.TextField(null=True)
    credential_values = models.JSONField()
    credential_info = models.JSONField(null=True)
    created_at = UtcDateTimeField(default=timezone.now)
    created_by = models.TextField()
    updated_at = UtcDateTimeField(auto_now=True)
    updated_by = models.TextField()
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_CredentialsTable"


class LiteLLMProxyModelTable(models.Model):
    model_id = models.TextField(primary_key=True, default=uuid4_str)
    model_name = models.TextField()
    litellm_params = models.JSONField()
    model_info = models.JSONField(null=True)
    blocked = models.BooleanField(default=False)
    created_at = UtcDateTimeField(default=timezone.now)
    created_by = models.TextField()
    updated_at = UtcDateTimeField(auto_now=True)
    updated_by = models.TextField()
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_ProxyModelTable"


class LiteLLMAgentsTable(models.Model):
    agent_id = models.TextField(primary_key=True, default=uuid4_str)
    agent_name = models.TextField(unique=True)
    litellm_params = models.JSONField(null=True)
    agent_card_params = models.JSONField()
    static_headers = models.JSONField(null=True, default=dict)
    extra_headers = ArrayField(models.TextField(), default=list)
    kill_switch = models.JSONField(null=True)
    agent_access_groups = ArrayField(models.TextField(), default=list)
    access_group_ids = ArrayField(models.TextField(), default=list)
    object_permission = models.ForeignKey("LiteLLMObjectPermissionTable", on_delete=models.SET_NULL, related_name="agents_table", null=True, db_index=False)
    spend = models.FloatField(default=0)
    identity_managed = models.BooleanField(default=False)
    enabled = models.BooleanField(default=True)
    execution_mode = models.TextField(default="autonomous")
    tpm_limit = models.IntegerField(null=True)
    rpm_limit = models.IntegerField(null=True)
    session_tpm_limit = models.IntegerField(null=True)
    session_rpm_limit = models.IntegerField(null=True)
    created_at = UtcDateTimeField(default=timezone.now)
    created_by = models.TextField()
    updated_at = UtcDateTimeField(auto_now=True)
    updated_by = models.TextField()
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_AgentsTable"


class LiteLLMAgentIdentity(models.Model):
    agent = models.OneToOneField("LiteLLMAgentsTable", on_delete=models.CASCADE, related_name="identity", primary_key=True)
    active = models.BooleanField(default=True)
    provider = models.TextField()
    issuer = models.TextField()
    tenant_id = models.TextField()
    client_id = models.TextField()
    service_principal_id = models.TextField(null=True)
    required_roles = ArrayField(models.TextField(), default=list)
    required_scopes = ArrayField(models.TextField(), default=lite_llm_agent_identity_required_scopes_default)
    revision = models.TextField(default=uuid4_str)
    last_authenticated_at = UtcDateTimeField(null=True)

    class Meta:
        db_table = "LiteLLM_AgentIdentity"
        constraints = [
            models.UniqueConstraint(fields=["provider", "tenant_id", "client_id"], name="LiteLLM_AgentIdentity_provider_tenant_id_client_id_key"),
            models.UniqueConstraint(fields=["issuer", "service_principal_id"], name="LiteLLM_AgentIdentity_issuer_service_principal_id_key"),
        ]


class LiteLLMRetiredAgentIdentity(models.Model):
    binding_id = models.TextField(primary_key=True, default=uuid4_str)
    agent = models.ForeignKey("LiteLLMAgentsTable", on_delete=models.SET_NULL, related_name="retired_identities", null=True, db_index=False)
    provider = models.TextField()
    issuer = models.TextField()
    tenant_id = models.TextField()
    client_id = models.TextField()

    class Meta:
        db_table = "LiteLLM_RetiredAgentIdentity"
        constraints = [
            models.UniqueConstraint(fields=["provider", "tenant_id", "client_id"], name="LiteLLM_RetiredAgentIdentity_provider_tenant_id_client_id_key"),
        ]


class LiteLLMRetiredAgent(models.Model):
    original_agent_id = models.TextField(primary_key=True, default=None)
    retired_at = UtcDateTimeField(default=timezone.now)

    class Meta:
        db_table = "LiteLLM_RetiredAgent"


class LiteLLMVerifiedSubject(models.Model):
    subject_id = models.TextField(primary_key=True, default=uuid4_str)
    issuer = models.TextField()
    tenant_id = models.TextField()
    oid = models.TextField()
    kind = models.TextField(default="human")
    user = models.ForeignKey("LiteLLMUserTable", on_delete=models.CASCADE, related_name="verified_subjects", null=True, db_index=False)
    verified_via = models.TextField(default="sso_interactive")
    verified_at = UtcDateTimeField(default=timezone.now)

    class Meta:
        db_table = "LiteLLM_VerifiedSubject"
        constraints = [
            models.UniqueConstraint(fields=["issuer", "tenant_id", "oid"], name="LiteLLM_VerifiedSubject_issuer_tenant_id_oid_key"),
        ]
        indexes = [
            models.Index(fields=["user"]),
        ]


class LiteLLMOrganizationTable(models.Model):
    organization_id = models.TextField(primary_key=True, default=uuid4_str)
    organization_alias = models.TextField()
    litellm_budget_table = models.ForeignKey("LiteLLMBudgetTable", on_delete=models.RESTRICT, related_name="organization", db_column="budget_id", db_index=False)
    metadata = models.JSONField(default=dict)
    models_field = ArrayField(models.TextField(), db_column="models")
    spend = models.FloatField(default=0)
    model_spend = models.JSONField(default=dict)
    object_permission = models.ForeignKey("LiteLLMObjectPermissionTable", on_delete=models.SET_NULL, related_name="organizations", null=True, db_index=False)
    created_at = UtcDateTimeField(default=timezone.now)
    created_by = models.TextField()
    updated_at = UtcDateTimeField(auto_now=True)
    updated_by = models.TextField()
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_OrganizationTable"


class LiteLLMModelTable(models.Model):
    id = models.AutoField(primary_key=True)
    model_aliases = models.JSONField(null=True, db_column="aliases")
    created_at = UtcDateTimeField(default=timezone.now)
    created_by = models.TextField()
    updated_at = UtcDateTimeField(auto_now=True)
    updated_by = models.TextField()
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_ModelTable"


class LiteLLMTeamTable(models.Model):
    team_id = models.TextField(primary_key=True, default=uuid4_str)
    team_alias = models.TextField(null=True)
    litellm_organization_table = models.ForeignKey("LiteLLMOrganizationTable", on_delete=models.SET_NULL, related_name="teams", db_column="organization_id", null=True, db_index=False)
    object_permission = models.ForeignKey("LiteLLMObjectPermissionTable", on_delete=models.SET_NULL, related_name="teams", null=True, db_index=False)
    admins = ArrayField(models.TextField())
    members = ArrayField(models.TextField())
    members_with_roles = models.JSONField(default=dict)
    metadata = models.JSONField(default=dict)
    max_budget = models.FloatField(null=True)
    soft_budget = models.FloatField(null=True)
    spend = models.FloatField(default=0)
    models_field = ArrayField(models.TextField(), db_column="models")
    max_parallel_requests = models.IntegerField(null=True)
    tpm_limit = models.BigIntegerField(null=True)
    rpm_limit = models.BigIntegerField(null=True)
    tpd_limit = models.BigIntegerField(null=True)
    budget_duration = models.TextField(null=True)
    budget_reset_at = UtcDateTimeField(null=True)
    blocked = models.BooleanField(default=False)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    model_spend = models.JSONField(default=dict)
    model_max_budget = models.JSONField(default=dict)
    router_settings = models.JSONField(null=True, default=dict)
    team_member_permissions = ArrayField(models.TextField(), default=list)
    access_group_ids = ArrayField(models.TextField(), default=list)
    policies = ArrayField(models.TextField(), default=list)
    default_team_member_models = ArrayField(models.TextField(), default=list)
    budget_limits = models.JSONField(null=True)
    litellm_model_table = models.OneToOneField("LiteLLMModelTable", on_delete=models.SET_NULL, related_name="team", db_column="model_id", null=True)
    allow_team_guardrail_config = models.BooleanField(default=False)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_TeamTable"
        indexes = [
            models.Index(fields=["litellm_organization_table"]),
            models.Index(fields=["team_alias"]),
            models.Index(fields=["created_at"]),
        ]


class LiteLLMProjectTable(models.Model):
    project_id = models.TextField(primary_key=True, default=uuid4_str)
    project_alias = models.TextField(null=True)
    description = models.TextField(null=True)
    litellm_team_table = models.ForeignKey("LiteLLMTeamTable", on_delete=models.SET_NULL, related_name="projects", db_column="team_id", null=True, db_index=False)
    litellm_budget_table = models.ForeignKey("LiteLLMBudgetTable", on_delete=models.SET_NULL, related_name="projects", db_column="budget_id", null=True, db_index=False)
    metadata = models.JSONField(default=dict)
    models_field = ArrayField(models.TextField(), db_column="models")
    spend = models.FloatField(default=0)
    model_spend = models.JSONField(default=dict)
    model_rpm_limit = models.JSONField(default=dict)
    model_tpm_limit = models.JSONField(default=dict)
    blocked = models.BooleanField(default=False)
    object_permission = models.ForeignKey("LiteLLMObjectPermissionTable", on_delete=models.SET_NULL, related_name="projects", null=True, db_index=False)
    created_at = UtcDateTimeField(default=timezone.now)
    created_by = models.TextField()
    updated_at = UtcDateTimeField(auto_now=True)
    updated_by = models.TextField()
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_ProjectTable"


class LiteLLMDeletedTeamTable(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    team_id = models.TextField()
    team_alias = models.TextField(null=True)
    organization_id = models.TextField(null=True)
    object_permission_id = models.TextField(null=True)
    admins = ArrayField(models.TextField())
    members = ArrayField(models.TextField())
    members_with_roles = models.JSONField(default=dict)
    metadata = models.JSONField(default=dict)
    max_budget = models.FloatField(null=True)
    soft_budget = models.FloatField(null=True)
    spend = models.FloatField(default=0)
    models_field = ArrayField(models.TextField(), db_column="models")
    max_parallel_requests = models.IntegerField(null=True)
    tpm_limit = models.BigIntegerField(null=True)
    rpm_limit = models.BigIntegerField(null=True)
    tpd_limit = models.BigIntegerField(null=True)
    budget_duration = models.TextField(null=True)
    budget_reset_at = UtcDateTimeField(null=True)
    blocked = models.BooleanField(default=False)
    model_spend = models.JSONField(default=dict)
    model_max_budget = models.JSONField(default=dict)
    router_settings = models.JSONField(null=True, default=dict)
    team_member_permissions = ArrayField(models.TextField(), default=list)
    access_group_ids = ArrayField(models.TextField(), default=list)
    policies = ArrayField(models.TextField(), default=list)
    model_id = models.IntegerField(null=True)
    allow_team_guardrail_config = models.BooleanField(default=False)
    created_at = UtcDateTimeField(null=True)
    updated_at = UtcDateTimeField(null=True)
    deleted_at = UtcDateTimeField(default=timezone.now)
    deleted_by = models.TextField(null=True)
    deleted_by_api_key = models.TextField(null=True)
    litellm_changed_by = models.TextField(null=True)

    class Meta:
        db_table = "LiteLLM_DeletedTeamTable"
        indexes = [
            models.Index(fields=["team_id"]),
            models.Index(fields=["deleted_at"]),
            models.Index(fields=["organization_id"]),
            models.Index(fields=["team_alias"]),
            models.Index(fields=["created_at"]),
        ]


class LiteLLMUserTable(models.Model):
    user_id = models.TextField(primary_key=True, default=None)
    user_alias = models.TextField(null=True)
    team_id = models.TextField(null=True)
    sso_user_id = models.TextField(null=True, unique=True)
    litellm_organization_table = models.ForeignKey("LiteLLMOrganizationTable", on_delete=models.SET_NULL, related_name="users", db_column="organization_id", null=True, db_index=False)
    object_permission = models.ForeignKey("LiteLLMObjectPermissionTable", on_delete=models.SET_NULL, related_name="users", null=True, db_index=False)
    password = models.TextField(null=True)
    password_reset_required = models.BooleanField(null=True)
    last_breach_check_at = UtcDateTimeField(null=True)
    teams = ArrayField(models.TextField(), default=list)
    user_role = models.TextField(null=True)
    max_budget = models.FloatField(null=True)
    spend = models.FloatField(default=0)
    user_email = models.TextField(null=True)
    models_field = ArrayField(models.TextField(), db_column="models")
    metadata = models.JSONField(default=dict)
    max_parallel_requests = models.IntegerField(null=True)
    tpm_limit = models.BigIntegerField(null=True)
    rpm_limit = models.BigIntegerField(null=True)
    budget_duration = models.TextField(null=True)
    budget_reset_at = UtcDateTimeField(null=True)
    allowed_cache_controls = ArrayField(models.TextField(), default=list)
    policies = ArrayField(models.TextField(), default=list)
    model_spend = models.JSONField(default=dict)
    model_max_budget = models.JSONField(default=dict)
    created_at = UtcDateTimeField(null=True, default=timezone.now)
    updated_at = UtcDateTimeField(null=True, auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_UserTable"


class LiteLLMObjectPermissionTable(models.Model):
    object_permission_id = models.TextField(primary_key=True, default=uuid4_str)
    mcp_servers = ArrayField(models.TextField(), default=list)
    mcp_access_groups = ArrayField(models.TextField(), default=list)
    mcp_tool_permissions = models.JSONField(null=True)
    vector_stores = ArrayField(models.TextField(), default=list)
    agents = ArrayField(models.TextField(), default=list)
    agent_access_groups = ArrayField(models.TextField(), default=list)
    models_field = ArrayField(models.TextField(), db_column="models", default=list)
    blocked_tools = ArrayField(models.TextField(), default=list)
    mcp_toolsets = ArrayField(models.TextField(), default=list)
    search_tools = ArrayField(models.TextField(), default=list)
    mcp_tool_search_enabled = models.BooleanField(null=True)
    skills = ArrayField(models.TextField(), default=list)

    class Meta:
        db_table = "LiteLLM_ObjectPermissionTable"


class LiteLLMMCPServerTable(models.Model):
    server_id = models.TextField(primary_key=True, default=uuid4_str)
    server_name = models.TextField(null=True)
    alias = models.TextField(null=True)
    description = models.TextField(null=True)
    instructions = models.TextField(null=True)
    url = models.TextField(null=True)
    spec_path = models.TextField(null=True)
    transport = models.TextField(default="sse")
    auth_type = models.TextField(null=True)
    credentials = models.JSONField(null=True, default=dict)
    created_at = UtcDateTimeField(null=True, default=timezone.now)
    created_by = models.TextField(null=True)
    updated_at = UtcDateTimeField(null=True, auto_now=True)
    updated_by = models.TextField(null=True)
    mcp_info = models.JSONField(null=True, default=dict)
    mcp_access_groups = ArrayField(models.TextField())
    allowed_tools = ArrayField(models.TextField(), default=list)
    tool_name_to_display_name = models.JSONField(null=True, default=dict)
    tool_name_to_description = models.JSONField(null=True, default=dict)
    pinned_tools = models.JSONField(null=True, default=dict)
    extra_headers = ArrayField(models.TextField(), default=list)
    static_headers = models.JSONField(null=True, default=dict)
    env_vars = models.JSONField(null=True, default=list)
    status = models.TextField(null=True, default="unknown")
    last_health_check = UtcDateTimeField(null=True)
    health_check_error = models.TextField(null=True)
    command = models.TextField(null=True)
    args = ArrayField(models.TextField(), default=list)
    env = models.JSONField(null=True, default=dict)
    issuer = models.TextField(null=True)
    authorization_url = models.TextField(null=True)
    token_url = models.TextField(null=True)
    registration_url = models.TextField(null=True)
    oauth2_flow = models.TextField(null=True)
    token_exchange_endpoint = models.TextField(null=True)
    audience = models.TextField(null=True)
    subject_token_type = models.TextField(null=True)
    token_exchange_profile = models.TextField(null=True)
    allow_all_keys = models.BooleanField(default=False)
    available_on_public_internet = models.BooleanField(default=True)
    delegate_auth_to_upstream = models.BooleanField(default=False)
    oauth_passthrough = models.BooleanField(default=False)
    dcr_bridge = models.BooleanField(null=True)
    per_server_oauth_discovery = models.BooleanField(default=False)
    is_byok = models.BooleanField(default=False)
    byok_description = ArrayField(models.TextField(), default=list)
    byok_api_key_help_url = models.TextField(null=True)
    source_url = models.TextField(null=True)
    timeout = models.FloatField(null=True)
    max_concurrent_requests = models.IntegerField(null=True)
    rpm = models.IntegerField(null=True)
    approval_status = models.TextField(null=True, default="active")
    submitted_by = models.TextField(null=True)
    submitted_at = UtcDateTimeField(null=True)
    reviewed_at = UtcDateTimeField(null=True)
    review_notes = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_MCPServerTable"
        indexes = [
            models.Index(fields=["approval_status"]),
        ]


class LiteLLMMCPToolsetTable(models.Model):
    toolset_id = models.TextField(primary_key=True, default=uuid4_str)
    toolset_name = models.TextField(unique=True)
    description = models.TextField(null=True)
    tools = models.JSONField(default=list)
    created_at = UtcDateTimeField(default=timezone.now)
    created_by = models.TextField(null=True)
    updated_at = UtcDateTimeField(auto_now=True)
    updated_by = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_MCPToolsetTable"


class LiteLLMMCPUserCredentials(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    user_id = models.TextField()
    server_id = models.TextField()
    credential_b64 = models.TextField()
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_MCPUserCredentials"
        constraints = [
            models.UniqueConstraint(fields=["user_id", "server_id"], name="LiteLLM_MCPUserCredentials_user_id_server_id_key"),
        ]


class LiteLLMUserProviderCredentials(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    user_id = models.TextField()
    credential_name = models.TextField()
    provider = models.TextField()
    credential_b64 = models.TextField()
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_UserProviderCredentials"
        constraints = [
            models.UniqueConstraint(fields=["user_id", "credential_name"], name="LiteLLM_UserProviderCredentials_user_id_credential_name_key"),
        ]
        indexes = [
            models.Index(fields=["credential_name"]),
        ]


class LiteLLMMCPUserEnvVars(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    user_id = models.TextField()
    server_id = models.TextField()
    values_b64 = models.TextField()
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_MCPUserEnvVars"
        constraints = [
            models.UniqueConstraint(fields=["user_id", "server_id"], name="LiteLLM_MCPUserEnvVars_user_id_server_id_key"),
        ]
        indexes = [
            models.Index(fields=["user_id"]),
            models.Index(fields=["server_id"]),
        ]


class LiteLLMMCPServerOAuthClient(models.Model):
    server_id = models.TextField(primary_key=True, default=None)
    credentials = models.JSONField(null=True)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_MCPServerOAuthClient"


class LiteLLMSSOIdentityAssertion(models.Model):
    user_id = models.TextField(primary_key=True, default=None)
    assertion_b64 = models.TextField()
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_SSOIdentityAssertion"


class LiteLLMVerificationToken(models.Model):
    token = models.TextField(primary_key=True, default=None)
    key_name = models.TextField(null=True)
    key_alias = models.TextField(null=True)
    soft_budget_cooldown = models.BooleanField(default=False)
    spend = models.FloatField(default=0)
    total_spend = models.FloatField(default=0)
    expires = UtcDateTimeField(null=True)
    models_field = ArrayField(models.TextField(), db_column="models")
    aliases = models.JSONField(default=dict)
    config = models.JSONField(default=dict)
    router_settings = models.JSONField(null=True, default=dict)
    user_id = models.TextField(null=True)
    team_id = models.TextField(null=True)
    agent_id = models.TextField(null=True)
    litellm_project_table = models.ForeignKey("LiteLLMProjectTable", on_delete=models.SET_NULL, related_name="keys", db_column="project_id", null=True, db_index=False)
    permissions = models.JSONField(default=dict)
    max_parallel_requests = models.IntegerField(null=True)
    metadata = models.JSONField(default=dict)
    blocked = models.BooleanField(null=True)
    tpm_limit = models.BigIntegerField(null=True)
    rpm_limit = models.BigIntegerField(null=True)
    tpd_limit = models.BigIntegerField(null=True)
    max_budget = models.FloatField(null=True)
    budget_duration = models.TextField(null=True)
    budget_reset_at = UtcDateTimeField(null=True)
    allowed_cache_controls = ArrayField(models.TextField(), default=list)
    allowed_routes = ArrayField(models.TextField(), default=list)
    key_type = models.TextField(null=True)
    policies = ArrayField(models.TextField(), default=list)
    access_group_ids = ArrayField(models.TextField(), default=list)
    model_spend = models.JSONField(default=dict)
    model_max_budget = models.JSONField(default=dict)
    budget_fallbacks = models.JSONField(default=dict)
    litellm_budget_table = models.ForeignKey("LiteLLMBudgetTable", on_delete=models.SET_NULL, related_name="keys", db_column="budget_id", null=True, db_index=False)
    litellm_organization_table = models.ForeignKey("LiteLLMOrganizationTable", on_delete=models.SET_NULL, related_name="keys", db_column="organization_id", null=True, db_index=False)
    object_permission = models.ForeignKey("LiteLLMObjectPermissionTable", on_delete=models.SET_NULL, related_name="verification_tokens", null=True, db_index=False)
    created_at = UtcDateTimeField(null=True, default=timezone.now)
    created_by = models.TextField(null=True)
    updated_at = UtcDateTimeField(null=True, auto_now=True)
    updated_by = models.TextField(null=True)
    settings_updated_at = UtcDateTimeField(null=True)
    last_active = UtcDateTimeField(null=True)
    rotation_count = models.IntegerField(null=True, default=0)
    auto_rotate = models.BooleanField(null=True, default=False)
    rotation_interval = models.TextField(null=True)
    last_rotation_at = UtcDateTimeField(null=True)
    key_rotation_at = UtcDateTimeField(null=True)
    budget_limits = models.JSONField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_VerificationToken"
        indexes = [
            models.Index(fields=["user_id", "team_id"]),
            models.Index(fields=["team_id"]),
            models.Index(fields=["budget_reset_at", "expires"]),
        ]


class LiteLLMJWTKeyMapping(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    jwt_issuer = models.TextField(default="")
    jwt_claim_name = models.TextField()
    jwt_claim_value = models.TextField()
    litellm_verification_token = models.ForeignKey("LiteLLMVerificationToken", on_delete=models.CASCADE, related_name="jwt_key_mappings", db_column="token", db_index=False)
    description = models.TextField(null=True)
    is_active = models.BooleanField(default=True)
    created_at = UtcDateTimeField(default=timezone.now)
    created_by = models.TextField(null=True)
    updated_at = UtcDateTimeField(auto_now=True)
    updated_by = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_JWTKeyMapping"
        constraints = [
            models.UniqueConstraint(fields=["jwt_issuer", "jwt_claim_name", "jwt_claim_value"], name="LiteLLM_JWTKeyMapping_jwt_issuer_jwt_claim_name_jwt_claim_value_key"),
        ]
        indexes = [
            models.Index(fields=["jwt_issuer", "jwt_claim_name", "jwt_claim_value", "is_active"]),
        ]


class LiteLLMDeprecatedVerificationToken(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    token = models.TextField(unique=True)
    active_token_id = models.TextField()
    revoke_at = UtcDateTimeField()
    created_at = UtcDateTimeField(default=timezone.now)

    class Meta:
        db_table = "LiteLLM_DeprecatedVerificationToken"
        constraints = [
            models.UniqueConstraint(fields=["token"], name="LiteLLM_DeprecatedVerificationToken_token_key"),
        ]
        indexes = [
            models.Index(fields=["token", "revoke_at"]),
            models.Index(fields=["revoke_at"]),
        ]


class LiteLLMDeletedVerificationToken(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    token = models.TextField()
    key_name = models.TextField(null=True)
    key_alias = models.TextField(null=True)
    soft_budget_cooldown = models.BooleanField(default=False)
    spend = models.FloatField(default=0)
    total_spend = models.FloatField(default=0)
    expires = UtcDateTimeField(null=True)
    models_field = ArrayField(models.TextField(), db_column="models")
    aliases = models.JSONField(default=dict)
    config = models.JSONField(default=dict)
    user_id = models.TextField(null=True)
    team_id = models.TextField(null=True)
    agent_id = models.TextField(null=True)
    project_id = models.TextField(null=True)
    permissions = models.JSONField(default=dict)
    max_parallel_requests = models.IntegerField(null=True)
    metadata = models.JSONField(default=dict)
    blocked = models.BooleanField(null=True)
    tpm_limit = models.BigIntegerField(null=True)
    rpm_limit = models.BigIntegerField(null=True)
    tpd_limit = models.BigIntegerField(null=True)
    max_budget = models.FloatField(null=True)
    budget_duration = models.TextField(null=True)
    budget_reset_at = UtcDateTimeField(null=True)
    allowed_cache_controls = ArrayField(models.TextField(), default=list)
    allowed_routes = ArrayField(models.TextField(), default=list)
    key_type = models.TextField(null=True)
    policies = ArrayField(models.TextField(), default=list)
    access_group_ids = ArrayField(models.TextField(), default=list)
    model_spend = models.JSONField(default=dict)
    model_max_budget = models.JSONField(default=dict)
    budget_fallbacks = models.JSONField(default=dict)
    router_settings = models.JSONField(null=True, default=dict)
    budget_id = models.TextField(null=True)
    organization_id = models.TextField(null=True)
    object_permission_id = models.TextField(null=True)
    created_at = UtcDateTimeField(null=True)
    created_by = models.TextField(null=True)
    updated_at = UtcDateTimeField(null=True)
    updated_by = models.TextField(null=True)
    settings_updated_at = UtcDateTimeField(null=True)
    last_active = UtcDateTimeField(null=True)
    rotation_count = models.IntegerField(null=True, default=0)
    auto_rotate = models.BooleanField(null=True, default=False)
    rotation_interval = models.TextField(null=True)
    last_rotation_at = UtcDateTimeField(null=True)
    key_rotation_at = UtcDateTimeField(null=True)
    deleted_at = UtcDateTimeField(default=timezone.now)
    deleted_by = models.TextField(null=True)
    deleted_by_api_key = models.TextField(null=True)
    litellm_changed_by = models.TextField(null=True)

    class Meta:
        db_table = "LiteLLM_DeletedVerificationToken"
        indexes = [
            models.Index(fields=["token"]),
            models.Index(fields=["deleted_at"]),
            models.Index(fields=["user_id"]),
            models.Index(fields=["team_id"]),
            models.Index(fields=["organization_id"]),
            models.Index(fields=["key_alias"]),
            models.Index(fields=["created_at"]),
        ]


class LiteLLMEndUserTable(models.Model):
    user_id = models.TextField(primary_key=True, default=None)
    alias = models.TextField(null=True)
    spend = models.FloatField(default=0)
    allowed_model_region = models.TextField(null=True)
    default_model = models.TextField(null=True)
    models_field = ArrayField(models.TextField(), db_column="models", default=list)
    litellm_budget_table = models.ForeignKey("LiteLLMBudgetTable", on_delete=models.SET_NULL, related_name="end_users", db_column="budget_id", null=True, db_index=False)
    object_permission = models.ForeignKey("LiteLLMObjectPermissionTable", on_delete=models.SET_NULL, related_name="end_users", null=True, db_index=False)
    blocked = models.BooleanField(default=False)

    class Meta:
        db_table = "LiteLLM_EndUserTable"


class LiteLLMModelAccessGroupBudgetTable(models.Model):
    access_group_name = models.TextField(primary_key=True, default=None)
    spend = models.FloatField(default=0)
    litellm_budget_table = models.ForeignKey("LiteLLMBudgetTable", on_delete=models.SET_NULL, related_name="model_access_groups", db_column="budget_id", null=True, db_index=False)
    created_at = UtcDateTimeField(default=timezone.now)
    created_by = models.TextField(null=True)
    updated_at = UtcDateTimeField(auto_now=True)
    updated_by = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_ModelAccessGroupBudgetTable"


class LiteLLMTagTable(models.Model):
    tag_name = models.TextField(primary_key=True, default=None)
    description = models.TextField(null=True)
    models_field = ArrayField(models.TextField(), db_column="models")
    model_info = models.JSONField(null=True)
    spend = models.FloatField(default=0)
    litellm_budget_table = models.ForeignKey("LiteLLMBudgetTable", on_delete=models.SET_NULL, related_name="tags", db_column="budget_id", null=True, db_index=False)
    created_at = UtcDateTimeField(default=timezone.now)
    created_by = models.TextField(null=True)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_TagTable"


class LiteLLMConfig(models.Model):
    param_name = models.TextField(primary_key=True, default=None)
    param_value = models.JSONField(null=True)
    last_run_at = UtcDateTimeField(null=True)
    reload_revision = models.BigIntegerField(default=0)

    class Meta:
        db_table = "LiteLLM_Config"


class LiteLLMSpendLogs(models.Model):
    request_id = models.TextField(primary_key=True, default=None)
    call_type = models.TextField()
    api_key = models.TextField(default="")
    spend = models.FloatField(default=0)
    total_tokens = models.IntegerField(default=0)
    prompt_tokens = models.IntegerField(default=0)
    completion_tokens = models.IntegerField(default=0)
    start_time = UtcDateTimeField(db_column="startTime")
    end_time = UtcDateTimeField(db_column="endTime")
    request_duration_ms = models.IntegerField(null=True)
    completion_start_time = UtcDateTimeField(null=True, db_column="completionStartTime")
    model = models.TextField(default="")
    model_id = models.TextField(null=True, default="")
    model_group = models.TextField(null=True, default="")
    custom_llm_provider = models.TextField(null=True, default="")
    api_base = models.TextField(null=True, default="")
    user = models.TextField(null=True, default="")
    metadata = models.JSONField(null=True, default=dict)
    cache_hit = models.TextField(null=True, default="")
    cache_key = models.TextField(null=True, default="")
    request_tags = models.JSONField(null=True, default=list)
    team_id = models.TextField(null=True)
    organization_id = models.TextField(null=True)
    end_user = models.TextField(null=True)
    requester_ip_address = models.TextField(null=True)
    messages = models.JSONField(null=True, default=dict)
    response = models.JSONField(null=True, default=dict)
    session_id = models.TextField(null=True)
    status = models.TextField(null=True)
    mcp_namespaced_tool_name = models.TextField(null=True)
    billing_agent_id = models.TextField(null=True)
    agent_id = models.TextField(null=True)
    proxy_server_request = models.JSONField(null=True, default=dict)
    litellm_call_id = models.TextField(null=True)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_SpendLogs"
        indexes = [
            models.Index(fields=["start_time"]),
            models.Index(fields=["start_time", "request_id"]),
            models.Index(fields=["end_user"]),
            models.Index(fields=["session_id"]),
            models.Index(fields=["litellm_call_id"]),
            models.Index(fields=["api_key", "start_time"]),
        ]


class LiteLLMBudgetWindowSpend(models.Model):
    pk = models.CompositePrimaryKey("entity_type", "entity_id", "window_duration")
    entity_type = models.TextField()
    entity_id = models.TextField()
    window_duration = models.TextField()
    window_start = UtcDateTimeField()
    spend = models.FloatField(default=0)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_BudgetWindowSpend"


class LiteLLMErrorLogs(models.Model):
    request_id = models.TextField(primary_key=True, default=uuid4_str)
    start_time = UtcDateTimeField(db_column="startTime")
    end_time = UtcDateTimeField(db_column="endTime")
    api_base = models.TextField(default="")
    model_group = models.TextField(default="")
    litellm_model_name = models.TextField(default="")
    model_id = models.TextField(default="")
    request_kwargs = models.JSONField(default=dict)
    exception_type = models.TextField(default="")
    exception_string = models.TextField(default="")
    status_code = models.TextField(default="")

    class Meta:
        db_table = "LiteLLM_ErrorLogs"


class LiteLLMUserNotifications(models.Model):
    request_id = models.TextField(primary_key=True, default=None)
    user_id = models.TextField()
    models_field = ArrayField(models.TextField(), db_column="models")
    justification = models.TextField()
    status = models.TextField()

    class Meta:
        db_table = "LiteLLM_UserNotifications"


class LiteLLMTeamMembership(models.Model):
    pk = models.CompositePrimaryKey("user_id", "team_id")
    user_id = models.TextField()
    team_id = models.TextField()
    spend = models.FloatField(default=0)
    total_spend = models.FloatField(default=0)
    litellm_budget_table = models.ForeignKey("LiteLLMBudgetTable", on_delete=models.SET_NULL, related_name="team_membership", db_column="budget_id", null=True, db_index=False)

    class Meta:
        db_table = "LiteLLM_TeamMembership"


class LiteLLMOrganizationMembership(models.Model):
    pk = models.CompositePrimaryKey("user_id", "organization_id")
    user = models.ForeignKey("LiteLLMUserTable", on_delete=models.RESTRICT, related_name="organization_memberships", db_index=False)
    organization = models.ForeignKey("LiteLLMOrganizationTable", on_delete=models.RESTRICT, related_name="members", db_index=False)
    user_role = models.TextField(null=True)
    spend = models.FloatField(null=True, default=0)
    litellm_budget_table = models.ForeignKey("LiteLLMBudgetTable", on_delete=models.SET_NULL, related_name="organization_membership", db_column="budget_id", null=True, db_index=False)
    created_at = UtcDateTimeField(null=True, default=timezone.now)
    updated_at = UtcDateTimeField(null=True, auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_OrganizationMembership"
        constraints = [
            models.UniqueConstraint(fields=["user", "organization"], name="LiteLLM_OrganizationMembership_user_id_organization_id_key"),
        ]


class LiteLLMInvitationLink(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    lite_llm_user_table_user = models.ForeignKey("LiteLLMUserTable", on_delete=models.RESTRICT, related_name="invitations_user", db_column="user_id", db_index=False)
    is_accepted = models.BooleanField(default=False)
    accepted_at = UtcDateTimeField(null=True)
    expires_at = UtcDateTimeField()
    created_at = UtcDateTimeField()
    lite_llm_user_table_created = models.ForeignKey("LiteLLMUserTable", on_delete=models.RESTRICT, related_name="invitations_created", db_column="created_by", db_index=False)
    updated_at = UtcDateTimeField()
    lite_llm_user_table_updated = models.ForeignKey("LiteLLMUserTable", on_delete=models.RESTRICT, related_name="invitations_updated", db_column="updated_by", db_index=False)

    class Meta:
        db_table = "LiteLLM_InvitationLink"


class LiteLLMAuditLog(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    updated_at = UtcDateTimeField(default=timezone.now)
    changed_by = models.TextField(default="")
    changed_by_api_key = models.TextField(default="")
    action = models.TextField()
    table_name = models.TextField()
    object_id = models.TextField()
    before_value = models.JSONField(null=True)
    updated_values = models.JSONField(null=True)

    class Meta:
        db_table = "LiteLLM_AuditLog"


class LiteLLMDailyUserSpend(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    user_id = models.TextField(null=True)
    date = models.TextField()
    api_key = models.TextField()
    model = models.TextField(null=True)
    model_group = models.TextField(null=True)
    custom_llm_provider = models.TextField(null=True)
    mcp_namespaced_tool_name = models.TextField(null=True)
    endpoint = models.TextField(null=True)
    prompt_tokens = models.BigIntegerField(default=0)
    completion_tokens = models.BigIntegerField(default=0)
    cache_read_input_tokens = models.BigIntegerField(default=0)
    cache_creation_input_tokens = models.BigIntegerField(default=0)
    compression_saved_tokens = models.BigIntegerField(default=0)
    compression_savings_spend = models.FloatField(default=0)
    prompt_caching_savings_spend = models.FloatField(default=0)
    gateway_injected_caching_savings_spend = models.FloatField(default=0)
    autorouter_savings_spend = models.FloatField(default=0)
    guardrail_spend = models.FloatField(default=0)
    spend = models.FloatField(default=0)
    api_requests = models.BigIntegerField(default=0)
    successful_requests = models.BigIntegerField(default=0)
    failed_requests = models.BigIntegerField(default=0)
    total_response_time_ms = models.BigIntegerField(default=0)
    timed_requests = models.BigIntegerField(default=0)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_DailyUserSpend"
        constraints = [
            models.UniqueConstraint(fields=["user_id", "date", "api_key", "model", "custom_llm_provider", "mcp_namespaced_tool_name", "endpoint"], name="LiteLLM_DailyUserSpend_user_id_date_api_key_model_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key"),
        ]
        indexes = [
            models.Index(fields=["date"]),
            models.Index(fields=["user_id", "date"]),
            models.Index(fields=["api_key"]),
            models.Index(fields=["model"]),
            models.Index(fields=["mcp_namespaced_tool_name"]),
            models.Index(fields=["endpoint"]),
        ]


class LiteLLMDailyGlobalSpend(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    date = models.TextField()
    model = models.TextField(null=True)
    model_group = models.TextField(null=True)
    custom_llm_provider = models.TextField(null=True)
    mcp_namespaced_tool_name = models.TextField(null=True)
    endpoint = models.TextField(null=True)
    prompt_tokens = models.BigIntegerField(default=0)
    completion_tokens = models.BigIntegerField(default=0)
    cache_read_input_tokens = models.BigIntegerField(default=0)
    cache_creation_input_tokens = models.BigIntegerField(default=0)
    compression_saved_tokens = models.BigIntegerField(default=0)
    compression_savings_spend = models.FloatField(default=0)
    prompt_caching_savings_spend = models.FloatField(default=0)
    gateway_injected_caching_savings_spend = models.FloatField(default=0)
    autorouter_savings_spend = models.FloatField(default=0)
    guardrail_spend = models.FloatField(default=0)
    spend = models.FloatField(default=0)
    api_requests = models.BigIntegerField(default=0)
    successful_requests = models.BigIntegerField(default=0)
    failed_requests = models.BigIntegerField(default=0)
    total_response_time_ms = models.BigIntegerField(default=0)
    timed_requests = models.BigIntegerField(default=0)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_DailyGlobalSpend"
        constraints = [
            models.UniqueConstraint(fields=["date", "model", "model_group", "custom_llm_provider", "mcp_namespaced_tool_name", "endpoint"], name="LiteLLM_DailyGlobalSpend_date_model_model_group_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key"),
        ]
        indexes = [
            models.Index(fields=["date"]),
        ]


class LiteLLMDailyOrganizationSpend(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    organization_id = models.TextField(null=True)
    date = models.TextField()
    api_key = models.TextField()
    model = models.TextField(null=True)
    model_group = models.TextField(null=True)
    custom_llm_provider = models.TextField(null=True)
    mcp_namespaced_tool_name = models.TextField(null=True)
    endpoint = models.TextField(null=True)
    prompt_tokens = models.BigIntegerField(default=0)
    completion_tokens = models.BigIntegerField(default=0)
    cache_read_input_tokens = models.BigIntegerField(default=0)
    cache_creation_input_tokens = models.BigIntegerField(default=0)
    compression_saved_tokens = models.BigIntegerField(default=0)
    compression_savings_spend = models.FloatField(default=0)
    prompt_caching_savings_spend = models.FloatField(default=0)
    gateway_injected_caching_savings_spend = models.FloatField(default=0)
    autorouter_savings_spend = models.FloatField(default=0)
    guardrail_spend = models.FloatField(default=0)
    spend = models.FloatField(default=0)
    api_requests = models.BigIntegerField(default=0)
    successful_requests = models.BigIntegerField(default=0)
    failed_requests = models.BigIntegerField(default=0)
    total_response_time_ms = models.BigIntegerField(default=0)
    timed_requests = models.BigIntegerField(default=0)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_DailyOrganizationSpend"
        constraints = [
            models.UniqueConstraint(fields=["organization_id", "date", "api_key", "model", "custom_llm_provider", "mcp_namespaced_tool_name", "endpoint"], name="LiteLLM_DailyOrganizationSpend_organization_id_date_api_key_model_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key"),
        ]
        indexes = [
            models.Index(fields=["date"]),
            models.Index(fields=["organization_id", "date"]),
            models.Index(fields=["api_key"]),
            models.Index(fields=["model"]),
            models.Index(fields=["mcp_namespaced_tool_name"]),
            models.Index(fields=["endpoint"]),
        ]


class LiteLLMDailyEndUserSpend(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    end_user_id = models.TextField(null=True)
    date = models.TextField()
    api_key = models.TextField()
    model = models.TextField(null=True)
    model_group = models.TextField(null=True)
    custom_llm_provider = models.TextField(null=True)
    mcp_namespaced_tool_name = models.TextField(null=True)
    endpoint = models.TextField(null=True)
    prompt_tokens = models.BigIntegerField(default=0)
    completion_tokens = models.BigIntegerField(default=0)
    cache_read_input_tokens = models.BigIntegerField(default=0)
    cache_creation_input_tokens = models.BigIntegerField(default=0)
    compression_saved_tokens = models.BigIntegerField(default=0)
    compression_savings_spend = models.FloatField(default=0)
    prompt_caching_savings_spend = models.FloatField(default=0)
    gateway_injected_caching_savings_spend = models.FloatField(default=0)
    autorouter_savings_spend = models.FloatField(default=0)
    guardrail_spend = models.FloatField(default=0)
    spend = models.FloatField(default=0)
    api_requests = models.BigIntegerField(default=0)
    successful_requests = models.BigIntegerField(default=0)
    failed_requests = models.BigIntegerField(default=0)
    total_response_time_ms = models.BigIntegerField(default=0)
    timed_requests = models.BigIntegerField(default=0)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_DailyEndUserSpend"
        constraints = [
            models.UniqueConstraint(fields=["end_user_id", "date", "api_key", "model", "custom_llm_provider", "mcp_namespaced_tool_name", "endpoint"], name="LiteLLM_DailyEndUserSpend_end_user_id_date_api_key_model_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key"),
        ]
        indexes = [
            models.Index(fields=["date"]),
            models.Index(fields=["end_user_id", "date"]),
            models.Index(fields=["api_key"]),
            models.Index(fields=["model"]),
            models.Index(fields=["mcp_namespaced_tool_name"]),
            models.Index(fields=["endpoint"]),
        ]


class LiteLLMDailyAgentSpend(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    agent_id = models.TextField(null=True)
    date = models.TextField()
    api_key = models.TextField()
    model = models.TextField(null=True)
    model_group = models.TextField(null=True)
    custom_llm_provider = models.TextField(null=True)
    mcp_namespaced_tool_name = models.TextField(null=True)
    endpoint = models.TextField(null=True)
    prompt_tokens = models.BigIntegerField(default=0)
    completion_tokens = models.BigIntegerField(default=0)
    cache_read_input_tokens = models.BigIntegerField(default=0)
    cache_creation_input_tokens = models.BigIntegerField(default=0)
    compression_saved_tokens = models.BigIntegerField(default=0)
    compression_savings_spend = models.FloatField(default=0)
    prompt_caching_savings_spend = models.FloatField(default=0)
    gateway_injected_caching_savings_spend = models.FloatField(default=0)
    autorouter_savings_spend = models.FloatField(default=0)
    guardrail_spend = models.FloatField(default=0)
    spend = models.FloatField(default=0)
    api_requests = models.BigIntegerField(default=0)
    successful_requests = models.BigIntegerField(default=0)
    failed_requests = models.BigIntegerField(default=0)
    total_response_time_ms = models.BigIntegerField(default=0)
    timed_requests = models.BigIntegerField(default=0)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_DailyAgentSpend"
        constraints = [
            models.UniqueConstraint(fields=["agent_id", "date", "api_key", "model", "custom_llm_provider", "mcp_namespaced_tool_name", "endpoint"], name="LiteLLM_DailyAgentSpend_agent_id_date_api_key_model_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key"),
        ]
        indexes = [
            models.Index(fields=["date"]),
            models.Index(fields=["agent_id", "date"]),
            models.Index(fields=["api_key"]),
            models.Index(fields=["model"]),
            models.Index(fields=["mcp_namespaced_tool_name"]),
            models.Index(fields=["endpoint"]),
        ]


class LiteLLMDailyTeamSpend(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    team_id = models.TextField(null=True)
    date = models.TextField()
    api_key = models.TextField()
    model = models.TextField(null=True)
    model_group = models.TextField(null=True)
    custom_llm_provider = models.TextField(null=True)
    mcp_namespaced_tool_name = models.TextField(null=True)
    endpoint = models.TextField(null=True)
    prompt_tokens = models.BigIntegerField(default=0)
    completion_tokens = models.BigIntegerField(default=0)
    cache_read_input_tokens = models.BigIntegerField(default=0)
    cache_creation_input_tokens = models.BigIntegerField(default=0)
    compression_saved_tokens = models.BigIntegerField(default=0)
    compression_savings_spend = models.FloatField(default=0)
    prompt_caching_savings_spend = models.FloatField(default=0)
    gateway_injected_caching_savings_spend = models.FloatField(default=0)
    autorouter_savings_spend = models.FloatField(default=0)
    guardrail_spend = models.FloatField(default=0)
    spend = models.FloatField(default=0)
    api_requests = models.BigIntegerField(default=0)
    successful_requests = models.BigIntegerField(default=0)
    failed_requests = models.BigIntegerField(default=0)
    total_response_time_ms = models.BigIntegerField(default=0)
    timed_requests = models.BigIntegerField(default=0)
    ptu_flat_cost = models.FloatField(default=0)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_DailyTeamSpend"
        constraints = [
            models.UniqueConstraint(fields=["team_id", "date", "api_key", "model", "custom_llm_provider", "mcp_namespaced_tool_name", "endpoint"], name="LiteLLM_DailyTeamSpend_team_id_date_api_key_model_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key"),
        ]
        indexes = [
            models.Index(fields=["date"]),
            models.Index(fields=["team_id", "date"]),
            models.Index(fields=["api_key"]),
            models.Index(fields=["model"]),
            models.Index(fields=["mcp_namespaced_tool_name"]),
            models.Index(fields=["endpoint"]),
        ]


class LiteLLMDailyTagSpend(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    request_id = models.TextField(null=True)
    tag = models.TextField(null=True)
    date = models.TextField()
    api_key = models.TextField()
    model = models.TextField(null=True)
    model_group = models.TextField(null=True)
    custom_llm_provider = models.TextField(null=True)
    mcp_namespaced_tool_name = models.TextField(null=True)
    endpoint = models.TextField(null=True)
    prompt_tokens = models.BigIntegerField(default=0)
    completion_tokens = models.BigIntegerField(default=0)
    cache_read_input_tokens = models.BigIntegerField(default=0)
    cache_creation_input_tokens = models.BigIntegerField(default=0)
    compression_saved_tokens = models.BigIntegerField(default=0)
    compression_savings_spend = models.FloatField(default=0)
    prompt_caching_savings_spend = models.FloatField(default=0)
    gateway_injected_caching_savings_spend = models.FloatField(default=0)
    autorouter_savings_spend = models.FloatField(default=0)
    guardrail_spend = models.FloatField(default=0)
    spend = models.FloatField(default=0)
    api_requests = models.BigIntegerField(default=0)
    successful_requests = models.BigIntegerField(default=0)
    failed_requests = models.BigIntegerField(default=0)
    total_response_time_ms = models.BigIntegerField(default=0)
    timed_requests = models.BigIntegerField(default=0)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_DailyTagSpend"
        constraints = [
            models.UniqueConstraint(fields=["tag", "date", "api_key", "model", "custom_llm_provider", "mcp_namespaced_tool_name", "endpoint"], name="LiteLLM_DailyTagSpend_tag_date_api_key_model_custom_llm_provider_mcp_namespaced_tool_name_endpoint_key"),
        ]
        indexes = [
            models.Index(fields=["date"]),
            models.Index(fields=["tag", "date"]),
            models.Index(fields=["api_key"]),
            models.Index(fields=["model"]),
            models.Index(fields=["mcp_namespaced_tool_name"]),
            models.Index(fields=["endpoint"]),
        ]


class LiteLLMProxyWorkerHeartbeat(models.Model):
    worker_id = models.TextField(primary_key=True, default=None)
    hostname = models.TextField()
    started_at = UtcDateTimeField(default=timezone.now)
    last_heartbeat_at = UtcDateTimeField(default=timezone.now)

    class Meta:
        db_table = "LiteLLM_ProxyWorkerHeartbeat"


class LiteLLMCronJob(models.Model):
    cronjob_id = models.TextField(primary_key=True, default=None)
    pod_id = models.TextField()
    status = models.TextField(choices=JobStatus.choices, default=JobStatus.INACTIVE)
    last_updated = UtcDateTimeField(default=timezone.now)
    ttl = UtcDateTimeField()

    class Meta:
        db_table = "LiteLLM_CronJob"


class LiteLLMManagedFileTable(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    unified_file_id = models.TextField(unique=True)
    file_object = models.JSONField(null=True)
    model_mappings = models.JSONField()
    flat_model_file_ids = ArrayField(models.TextField(), default=list)
    storage_backend = models.TextField(null=True)
    storage_url = models.TextField(null=True)
    created_at = UtcDateTimeField(default=timezone.now)
    created_by = models.TextField(null=True)
    team_id = models.TextField(null=True)
    updated_at = UtcDateTimeField(auto_now=True)
    updated_by = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_ManagedFileTable"
        indexes = [
            models.Index(fields=["unified_file_id"]),
            GinIndex(fields=["flat_model_file_ids"]),
            models.Index(fields=["team_id", "-created_at"]),
            models.Index(fields=["created_by", "-created_at"]),
        ]


class LiteLLMManagedObjectTable(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    unified_object_id = models.TextField(unique=True)
    model_object_id = models.TextField(unique=True)
    file_object = models.JSONField()
    file_purpose = models.TextField()
    status = models.TextField(null=True)
    batch_processed = models.BooleanField(default=False)
    created_at = UtcDateTimeField(default=timezone.now)
    created_by = models.TextField(null=True)
    team_id = models.TextField(null=True)
    org_id = models.TextField(null=True)
    api_key = models.TextField(null=True)
    request_tags = models.JSONField(null=True, default=list)
    end_user_id = models.TextField(null=True)
    updated_at = UtcDateTimeField(auto_now=True)
    updated_by = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_ManagedObjectTable"
        indexes = [
            models.Index(fields=["unified_object_id"]),
            models.Index(fields=["model_object_id"]),
            models.Index(fields=["team_id", "-created_at"]),
        ]


class LiteLLMManagedFileContentTable(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    content = models.BinaryField()
    created_at = UtcDateTimeField(default=timezone.now)

    class Meta:
        db_table = "LiteLLM_ManagedFileContentTable"


class LiteLLMManagedVectorStoreTable(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    unified_resource_id = models.TextField(unique=True)
    resource_object = models.JSONField(null=True)
    model_mappings = models.JSONField()
    flat_model_resource_ids = ArrayField(models.TextField(), default=list)
    storage_backend = models.TextField(null=True)
    storage_url = models.TextField(null=True)
    created_at = UtcDateTimeField(default=timezone.now)
    created_by = models.TextField(null=True)
    team_id = models.TextField(null=True)
    updated_at = UtcDateTimeField(auto_now=True)
    updated_by = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_ManagedVectorStoreTable"
        indexes = [
            models.Index(fields=["unified_resource_id"]),
            models.Index(fields=["team_id", "-created_at"]),
        ]


class LiteLLMManagedVectorStoresTable(models.Model):
    vector_store_id = models.TextField(primary_key=True, default=None)
    custom_llm_provider = models.TextField()
    vector_store_name = models.TextField(null=True)
    vector_store_description = models.TextField(null=True)
    vector_store_metadata = models.JSONField(null=True)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    litellm_credential_name = models.TextField(null=True)
    litellm_params = models.JSONField(null=True)
    team_id = models.TextField(null=True)
    user_id = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_ManagedVectorStoresTable"
        indexes = [
            models.Index(fields=["team_id"]),
            models.Index(fields=["user_id"]),
        ]


class LiteLLMGuardrailsTable(models.Model):
    guardrail_id = models.TextField(primary_key=True, default=uuid4_str)
    guardrail_name = models.TextField(unique=True)
    litellm_params = models.JSONField()
    guardrail_info = models.JSONField(null=True)
    team_id = models.TextField(null=True)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    status = models.TextField(default="active")
    submitted_at = UtcDateTimeField(null=True)
    reviewed_at = UtcDateTimeField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_GuardrailsTable"
        indexes = [
            models.Index(fields=["status"]),
        ]


class LiteLLMDailyGuardrailMetrics(models.Model):
    pk = models.CompositePrimaryKey("guardrail_id", "date")
    guardrail_id = models.TextField()
    date = models.TextField()
    requests_evaluated = models.BigIntegerField(default=0)
    passed_count = models.BigIntegerField(default=0)
    blocked_count = models.BigIntegerField(default=0)
    flagged_count = models.BigIntegerField(default=0)
    avg_score = models.FloatField(null=True)
    avg_latency_ms = models.FloatField(null=True)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_DailyGuardrailMetrics"
        indexes = [
            models.Index(fields=["date"]),
            models.Index(fields=["guardrail_id"]),
        ]


class LiteLLMDailyGuardrailUsageUnits(models.Model):
    pk = models.CompositePrimaryKey("guardrail_id", "date", "team_id", "api_key", "usage_unit")
    guardrail_id = models.TextField()
    date = models.TextField()
    team_id = models.TextField()
    api_key = models.TextField()
    usage_unit = models.TextField()
    units = models.BigIntegerField(default=0)
    cost = models.FloatField(null=True)
    untracked_units = models.BigIntegerField(default=0)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_DailyGuardrailUsageUnits"
        indexes = [
            models.Index(fields=["date"]),
        ]


class LiteLLMDailyPolicyMetrics(models.Model):
    pk = models.CompositePrimaryKey("policy_id", "date")
    policy_id = models.TextField()
    date = models.TextField()
    requests_evaluated = models.BigIntegerField(default=0)
    passed_count = models.BigIntegerField(default=0)
    blocked_count = models.BigIntegerField(default=0)
    flagged_count = models.BigIntegerField(default=0)
    avg_score = models.FloatField(null=True)
    avg_latency_ms = models.FloatField(null=True)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_DailyPolicyMetrics"
        indexes = [
            models.Index(fields=["date"]),
            models.Index(fields=["policy_id"]),
        ]


class LiteLLMSpendLogGuardrailIndex(models.Model):
    pk = models.CompositePrimaryKey("request_id", "guardrail_id")
    request_id = models.TextField()
    guardrail_id = models.TextField()
    policy_id = models.TextField(null=True)
    start_time = UtcDateTimeField()

    class Meta:
        db_table = "LiteLLM_SpendLogGuardrailIndex"
        indexes = [
            models.Index(fields=["guardrail_id", "start_time"]),
            models.Index(fields=["policy_id", "start_time"]),
        ]


class LiteLLMSpendLogToolIndex(models.Model):
    pk = models.CompositePrimaryKey("request_id", "tool_name")
    request_id = models.TextField()
    tool_name = models.TextField()
    start_time = UtcDateTimeField()

    class Meta:
        db_table = "LiteLLM_SpendLogToolIndex"
        indexes = [
            models.Index(fields=["tool_name", "start_time"]),
            models.Index(fields=["start_time"]),
        ]


class LiteLLMDailyToolSpend(models.Model):
    pk = models.CompositePrimaryKey("date", "tool_name")
    date = models.TextField()
    tool_name = models.TextField()
    spend = models.FloatField(default=0)
    total_tokens = models.BigIntegerField(default=0)
    request_count = models.BigIntegerField(default=0)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_DailyToolSpend"


class LiteLLMDailyModelUsage(models.Model):
    pk = models.CompositePrimaryKey("date", "model_group", "model", "custom_llm_provider", "task_type")
    date = models.TextField()
    model_group = models.TextField()
    model = models.TextField()
    custom_llm_provider = models.TextField()
    task_type = models.TextField()
    spend = models.FloatField(default=0)
    prompt_tokens = models.BigIntegerField(default=0)
    completion_tokens = models.BigIntegerField(default=0)
    request_count = models.BigIntegerField(default=0)
    successful_requests = models.BigIntegerField(default=0)
    failed_requests = models.BigIntegerField(default=0)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_DailyModelUsage"
        indexes = [
            models.Index(fields=["date"]),
            models.Index(fields=["model_group"]),
        ]


class LiteLLMDailyGatewayRequests(models.Model):
    pk = models.CompositePrimaryKey("date", "category", "route")
    date = models.TextField()
    category = models.TextField()
    route = models.TextField()
    successful_requests = models.BigIntegerField(default=0)
    failed_requests = models.BigIntegerField(default=0)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_DailyGatewayRequests"
        indexes = [
            models.Index(fields=["date"]),
        ]


class LiteLLMDailyGatewayFailedRequests(models.Model):
    pk = models.CompositePrimaryKey("date", "category", "route", "status_code")
    date = models.TextField()
    category = models.TextField()
    route = models.TextField()
    status_code = models.IntegerField()
    failed_requests = models.BigIntegerField(default=0)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_DailyGatewayFailedRequests"
        indexes = [
            models.Index(fields=["date"]),
        ]


class LiteLLMDailyRequestErrors(models.Model):
    pk = models.CompositePrimaryKey("date", "api_key", "team_id", "user_id", "model_group", "status_code")
    date = models.TextField()
    api_key = models.TextField()
    team_id = models.TextField(default="")
    user_id = models.TextField(default="")
    model_group = models.TextField(default="")
    status_code = models.IntegerField()
    failed_requests = models.BigIntegerField(default=0)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_DailyRequestErrors"
        indexes = [
            models.Index(fields=["date"]),
        ]


class LiteLLMPromptTable(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    prompt_id = models.TextField()
    version = models.IntegerField(default=1)
    environment = models.TextField(default="development")
    created_by = models.TextField(null=True)
    litellm_params = models.JSONField()
    prompt_info = models.JSONField(null=True)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_PromptTable"
        constraints = [
            models.UniqueConstraint(fields=["prompt_id", "version", "environment"], name="LiteLLM_PromptTable_prompt_id_version_environment_key"),
        ]
        indexes = [
            models.Index(fields=["prompt_id", "environment"]),
            models.Index(fields=["prompt_id"]),
        ]


class LiteLLMHealthCheckTable(models.Model):
    health_check_id = models.TextField(primary_key=True, default=uuid4_str)
    model_name = models.TextField()
    model_id = models.TextField(null=True)
    status = models.TextField()
    healthy_count = models.IntegerField(default=0)
    unhealthy_count = models.IntegerField(default=0)
    error_message = models.TextField(null=True)
    response_time_ms = models.FloatField(null=True)
    details = models.JSONField(null=True)
    checked_by = models.TextField(null=True)
    checked_at = UtcDateTimeField(default=timezone.now)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_HealthCheckTable"
        indexes = [
            models.Index(fields=["model_name"]),
            models.Index(fields=["checked_at"]),
            models.Index(fields=["status"]),
            models.Index(fields=["model_id", "model_name", "-checked_at"]),
        ]


class LiteLLMSearchToolsTable(models.Model):
    search_tool_id = models.TextField(primary_key=True, default=uuid4_str)
    search_tool_name = models.TextField(unique=True)
    litellm_params = models.JSONField()
    search_tool_info = models.JSONField(null=True)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_SearchToolsTable"


class LiteLLMSSOConfig(models.Model):
    id = models.TextField(primary_key=True, default=None)
    sso_settings = models.JSONField()
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_SSOConfig"


class LiteLLMManagedVectorStoreIndexTable(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    index_name = models.TextField(unique=True)
    litellm_params = models.JSONField()
    index_info = models.JSONField(null=True)
    created_at = UtcDateTimeField(default=timezone.now)
    created_by = models.TextField(null=True)
    updated_at = UtcDateTimeField(auto_now=True)
    updated_by = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_ManagedVectorStoreIndexTable"


class LiteLLMCacheConfig(models.Model):
    id = models.TextField(primary_key=True, default=None)
    cache_settings = models.JSONField()
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_CacheConfig"


class LiteLLMUISettings(models.Model):
    id = models.TextField(primary_key=True, default=None)
    ui_settings = models.JSONField()
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_UISettings"


class LiteLLMConfigOverrides(models.Model):
    config_type = models.TextField(primary_key=True, default=None)
    config_value = models.JSONField()
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_ConfigOverrides"


class LiteLLMSkillsTable(models.Model):
    skill_id = models.TextField(primary_key=True, default=uuid4_str)
    display_title = models.TextField(null=True)
    description = models.TextField(null=True)
    instructions = models.TextField(null=True)
    source = models.TextField(default="custom")
    latest_version = models.TextField(null=True)
    file_content = models.BinaryField(null=True)
    file_name = models.TextField(null=True)
    file_type = models.TextField(null=True)
    metadata = models.JSONField(null=True, default=dict)
    created_at = UtcDateTimeField(default=timezone.now)
    created_by = models.TextField(null=True)
    updated_at = UtcDateTimeField(auto_now=True)
    updated_by = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_SkillsTable"


class LiteLLMPolicyTable(models.Model):
    policy_id = models.TextField(primary_key=True, default=uuid4_str)
    policy_name = models.TextField()
    version_number = models.IntegerField(default=1)
    version_status = models.TextField(default="production")
    parent_version_id = models.TextField(null=True)
    is_latest = models.BooleanField(default=True)
    published_at = UtcDateTimeField(null=True)
    production_at = UtcDateTimeField(null=True)
    inherit = models.TextField(null=True)
    description = models.TextField(null=True)
    guardrails_add = ArrayField(models.TextField(), default=list)
    guardrails_remove = ArrayField(models.TextField(), default=list)
    condition = models.JSONField(null=True, default=dict)
    pipeline = models.JSONField(null=True)
    created_at = UtcDateTimeField(default=timezone.now)
    created_by = models.TextField(null=True)
    updated_at = UtcDateTimeField(auto_now=True)
    updated_by = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_PolicyTable"
        constraints = [
            models.UniqueConstraint(fields=["policy_name", "version_number"], name="LiteLLM_PolicyTable_policy_name_version_number_key"),
        ]
        indexes = [
            models.Index(fields=["policy_name", "version_status"]),
        ]


class LiteLLMPolicyAttachmentTable(models.Model):
    attachment_id = models.TextField(primary_key=True, default=uuid4_str)
    policy_name = models.TextField()
    scope = models.TextField(null=True)
    teams = ArrayField(models.TextField(), default=list)
    keys = ArrayField(models.TextField(), default=list)
    models_field = ArrayField(models.TextField(), db_column="models", default=list)
    tags = ArrayField(models.TextField(), default=list)
    priority = models.IntegerField(null=True)
    is_default = models.BooleanField(default=False)
    created_at = UtcDateTimeField(default=timezone.now)
    created_by = models.TextField(null=True)
    updated_at = UtcDateTimeField(auto_now=True)
    updated_by = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_PolicyAttachmentTable"


class LiteLLMToolTable(models.Model):
    tool_id = models.TextField(primary_key=True, default=uuid4_str)
    tool_name = models.TextField(unique=True)
    origin = models.TextField(null=True)
    input_policy = models.TextField(default="untrusted")
    output_policy = models.TextField(default="untrusted")
    call_count = models.IntegerField(default=0)
    assignments = models.JSONField(null=True, default=dict)
    key_hash = models.TextField(null=True)
    team_id = models.TextField(null=True)
    key_alias = models.TextField(null=True)
    user_agent = models.TextField(null=True)
    last_used_at = UtcDateTimeField(null=True)
    created_at = UtcDateTimeField(default=timezone.now)
    created_by = models.TextField(null=True)
    updated_at = UtcDateTimeField(auto_now=True)
    updated_by = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_ToolTable"
        indexes = [
            models.Index(fields=["input_policy"]),
            models.Index(fields=["output_policy"]),
            models.Index(fields=["team_id"]),
        ]


class LiteLLMAccessGroupTable(models.Model):
    access_group_id = models.TextField(primary_key=True, default=uuid4_str)
    access_group_name = models.TextField(unique=True)
    description = models.TextField(null=True)
    access_model_names = ArrayField(models.TextField(), default=list)
    access_mcp_server_ids = ArrayField(models.TextField(), default=list)
    mcp_tool_permissions = models.JSONField(null=True)
    access_agent_ids = ArrayField(models.TextField(), default=list)
    access_skill_names = ArrayField(models.TextField(), default=list)
    assigned_team_ids = ArrayField(models.TextField(), default=list)
    assigned_key_ids = ArrayField(models.TextField(), default=list)
    created_at = UtcDateTimeField(default=timezone.now)
    created_by = models.TextField(null=True)
    updated_at = UtcDateTimeField(auto_now=True)
    updated_by = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_AccessGroupTable"


class LiteLLMClaudeCodePluginTable(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    name = models.TextField(unique=True)
    version = models.TextField(null=True)
    description = models.TextField(null=True)
    manifest_json = models.TextField(null=True)
    files_json = models.TextField(null=True, default="{}")
    enabled = models.BooleanField(default=True)
    created_at = UtcDateTimeField(null=True, default=timezone.now)
    updated_at = UtcDateTimeField(null=True, auto_now=True)
    created_by = models.TextField(null=True)
    anonymous_download_count = models.BigIntegerField(default=0)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_ClaudeCodePluginTable"


class LiteLLMClaudeCodePluginVersion(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    plugin = models.ForeignKey("LiteLLMClaudeCodePluginTable", on_delete=models.CASCADE, related_name="versions", db_index=False)
    version = models.TextField(null=True)
    description = models.TextField(null=True)
    manifest_json = models.TextField(null=True)
    published_at = UtcDateTimeField(null=True)
    published_by = models.TextField(null=True)
    archive_available = models.BooleanField(default=False)
    content = models.BinaryField(null=True)

    class Meta:
        db_table = "LiteLLM_ClaudeCodePluginVersion"
        constraints = [
            models.UniqueConstraint(fields=["plugin", "version"], name="LiteLLM_ClaudeCodePluginVersion_plugin_id_version_key"),
        ]
        indexes = [
            models.Index(fields=["plugin", "published_at"]),
        ]


class LiteLLMSkillSubmission(models.Model):
    id = models.TextField(primary_key=True, default=uuid4_str)
    plugin_name = models.TextField()
    operation = models.TextField()
    status = models.TextField(default="pending_review")
    proposal_json = models.TextField()
    archive_content = models.BinaryField(null=True)
    submitted_by = models.TextField()
    created_at = UtcDateTimeField(default=timezone.now)
    base_plugin_id = models.TextField(null=True)
    base_updated_at = UtcDateTimeField(null=True)
    reviewed_by = models.TextField(null=True)
    reviewed_at = UtcDateTimeField(null=True)
    rejection_reason = models.TextField(null=True)
    scan_json = models.TextField(null=True)

    class Meta:
        db_table = "LiteLLM_SkillSubmission"
        indexes = [
            models.Index(fields=["submitted_by", "-created_at", "-id"]),
            models.Index(fields=["submitted_by", "status", "-created_at", "-id"]),
            models.Index(fields=["status", "-created_at", "-id"]),
            models.Index(fields=["-created_at", "-id"]),
        ]


class LiteLLMClaudeCodePluginDownload(models.Model):
    id = models.BigAutoField(primary_key=True)
    plugin = models.ForeignKey("LiteLLMClaudeCodePluginTable", on_delete=models.CASCADE, related_name="downloads", db_index=False)
    downloaded_at = UtcDateTimeField(default=timezone.now)
    version = models.TextField(null=True)
    digest = models.TextField()
    authenticated = models.BooleanField()
    user_id = models.TextField(null=True)
    user_email = models.TextField(null=True)
    key_alias = models.TextField(null=True)
    team_id = models.TextField(null=True)
    team_alias = models.TextField(null=True)

    class Meta:
        db_table = "LiteLLM_ClaudeCodePluginDownload"
        indexes = [
            models.Index(fields=["plugin", "id"]),
        ]


class LiteLLMClaudeCodePluginUpload(models.Model):
    plugin = models.OneToOneField("LiteLLMClaudeCodePluginTable", on_delete=models.CASCADE, related_name="uploaded_archive", to_field="name", primary_key=True, db_column="plugin_name")
    content = models.BinaryField()

    class Meta:
        db_table = "LiteLLM_ClaudeCodePluginUpload"


class LiteLLMMemoryTable(models.Model):
    memory_id = models.TextField(primary_key=True, default=uuid4_str)
    key = models.TextField(unique=True)
    value = models.TextField()
    metadata = models.JSONField(null=True)
    user_id = models.TextField(null=True)
    team_id = models.TextField(null=True)
    created_at = UtcDateTimeField(default=timezone.now)
    created_by = models.TextField(null=True)
    updated_at = UtcDateTimeField(auto_now=True)
    updated_by = models.TextField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_MemoryTable"
        indexes = [
            models.Index(fields=["user_id"]),
            models.Index(fields=["team_id"]),
        ]


class LiteLLMAdaptiveRouterState(models.Model):
    pk = models.CompositePrimaryKey("router_name", "request_type", "model_name")
    router_name = models.TextField()
    request_type = models.TextField()
    model_name = models.TextField()
    alpha = models.FloatField()
    beta = models.FloatField()
    total_samples = models.IntegerField(default=0)
    last_updated_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_AdaptiveRouterState"


class LiteLLMAdaptiveRouterSession(models.Model):
    pk = models.CompositePrimaryKey("session_id", "router_name", "model_name")
    session_id = models.TextField()
    router_name = models.TextField()
    model_name = models.TextField()
    classified_type = models.TextField()
    misalignment_count = models.IntegerField(default=0)
    stagnation_count = models.IntegerField(default=0)
    disengagement_count = models.IntegerField(default=0)
    satisfaction_count = models.IntegerField(default=0)
    failure_count = models.IntegerField(default=0)
    loop_count = models.IntegerField(default=0)
    exhaustion_count = models.IntegerField(default=0)
    last_user_content = models.TextField(null=True)
    last_assistant_content = models.TextField(null=True)
    tool_call_history = models.JSONField(default=list)
    pending_tool_calls = models.JSONField(default=dict)
    turn_count = models.IntegerField(default=0)
    last_processed_turn = models.IntegerField(default=-1)
    clean_credit_awarded = models.BooleanField(default=False)
    terminal_status = models.IntegerField(null=True)
    last_activity_at = UtcDateTimeField(auto_now=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_AdaptiveRouterSession"
        indexes = [
            models.Index(fields=["last_activity_at"]),
        ]


class LiteLLMAutoRouterBaselineComparison(models.Model):
    scope = models.TextField(primary_key=True, default=None)
    api_key = models.TextField()
    session_id = models.TextField()
    router_name = models.TextField()
    initial_equivalent = models.BooleanField()
    revision = models.BigIntegerField(default=0)
    published_revision = models.BigIntegerField(default=0)
    history = models.TextField(null=True)
    attempted_at = UtcDateTimeField(null=True)
    retired = models.BooleanField(default=False)
    updated_at = UtcDateTimeField(default=timezone.now)

    class Meta:
        db_table = "LiteLLM_AutoRouterBaselineComparison"
        indexes = [
            models.Index(fields=["api_key", "session_id", "router_name"], name="idx_autorouter_baseline_scope"),
            models.Index(fields=["updated_at"]),
        ]


class LiteLLMAutoRouterBaselineObservation(models.Model):
    request_id = models.TextField(primary_key=True, default=None)
    scope = models.TextField()
    started_at = models.FloatField()
    revision = models.BigIntegerField()
    data = models.TextField()
    publication = models.TextField(null=True)
    conflicted = models.BooleanField(default=False)

    class Meta:
        db_table = "LiteLLM_AutoRouterBaselineObservation"
        indexes = [
            models.Index(fields=["scope", "started_at", "request_id"]),
            models.Index(fields=["scope", "revision", "started_at"]),
        ]


class LiteLLMAutoRouterSession(models.Model):
    pk = models.CompositePrimaryKey("api_key", "session_id", "router_name")
    api_key = models.TextField()
    session_id = models.TextField()
    router_name = models.TextField()
    router_type = models.TextField()
    first_turn_at = UtcDateTimeField()
    last_turn_at = UtcDateTimeField()
    last_model = models.TextField()
    models_field = models.JSONField(db_column="models", default=dict)
    turns = models.IntegerField(default=0)
    unordered_turns = models.IntegerField(default=0)
    covered_turns = models.IntegerField(default=0)
    cache_hits = models.IntegerField(default=0)
    same_model_turns = models.IntegerField(default=0)
    same_model_hits = models.IntegerField(default=0)
    first_visit_turns = models.IntegerField(default=0)
    first_visit_hits = models.IntegerField(default=0)
    return_turns = models.IntegerField(default=0)
    return_hits = models.IntegerField(default=0)
    return_expired_misses = models.IntegerField(default=0)
    return_within_ttl_misses = models.IntegerField(default=0)
    ttl_5m_turns = models.IntegerField(default=0)
    ttl_1h_turns = models.IntegerField(default=0)
    total_tokens = models.BigIntegerField(default=0)
    spend = models.FloatField(default=0)
    saved_spend = models.FloatField(default=0)
    savings_estimated_turns = models.IntegerField(default=0)
    savings_estimated_actual_spend = models.FloatField(default=0)
    savings_estimated_saved_spend = models.FloatField(default=0)
    savings_estimated_baseline_models = models.JSONField(default=dict)
    classifier_cost = models.FloatField(default=0)
    classifier_cost_recorded_turns = models.IntegerField(default=0)
    tier_turns = models.JSONField(default=dict)
    baseline_models = models.JSONField(default=dict)

    class Meta:
        db_table = "LiteLLM_AutoRouterSession"
        indexes = [
            models.Index(fields=["last_turn_at"]),
        ]


class LiteLLMAutoRouterUserSession(models.Model):
    pk = models.CompositePrimaryKey("user_id", "api_key", "session_id", "router_name")
    user_id = models.TextField()
    api_key = models.TextField()
    session_id = models.TextField()
    router_name = models.TextField()
    router_type = models.TextField()
    first_turn_at = UtcDateTimeField()
    last_turn_at = UtcDateTimeField()
    last_model = models.TextField()
    models_field = models.JSONField(db_column="models", default=dict)
    turns = models.IntegerField(default=0)
    unordered_turns = models.IntegerField(default=0)
    covered_turns = models.IntegerField(default=0)
    cache_hits = models.IntegerField(default=0)
    same_model_turns = models.IntegerField(default=0)
    same_model_hits = models.IntegerField(default=0)
    first_visit_turns = models.IntegerField(default=0)
    first_visit_hits = models.IntegerField(default=0)
    return_turns = models.IntegerField(default=0)
    return_hits = models.IntegerField(default=0)
    return_expired_misses = models.IntegerField(default=0)
    return_within_ttl_misses = models.IntegerField(default=0)
    ttl_5m_turns = models.IntegerField(default=0)
    ttl_1h_turns = models.IntegerField(default=0)
    total_tokens = models.BigIntegerField(default=0)
    spend = models.FloatField(default=0)
    saved_spend = models.FloatField(default=0)
    savings_estimated_turns = models.IntegerField(default=0)
    savings_estimated_actual_spend = models.FloatField(default=0)
    savings_estimated_saved_spend = models.FloatField(default=0)
    savings_estimated_baseline_models = models.JSONField(default=dict)
    classifier_cost = models.FloatField(default=0)
    classifier_cost_recorded_turns = models.IntegerField(default=0)
    tier_turns = models.JSONField(default=dict)
    baseline_models = models.JSONField(default=dict)

    class Meta:
        db_table = "LiteLLM_AutoRouterUserSession"
        indexes = [
            models.Index(fields=["last_turn_at"]),
            models.Index(fields=["user_id", "last_turn_at"]),
        ]


class LiteLLMAutoRouterDailySpend(models.Model):
    pk = models.CompositePrimaryKey("date", "api_key", "user_id", "router_name", "router_type")
    date = models.TextField()
    api_key = models.TextField()
    user_id = models.TextField()
    router_name = models.TextField()
    router_type = models.TextField()
    turns = models.IntegerField(default=0)
    total_tokens = models.BigIntegerField(default=0)
    token_recorded_turns = models.IntegerField(default=0)
    spend = models.FloatField(default=0)
    saved_spend = models.FloatField(default=0)
    savings_estimated_turns = models.IntegerField(default=0)
    savings_estimated_actual_spend = models.FloatField(default=0)
    savings_estimated_saved_spend = models.FloatField(default=0)
    classifier_cost = models.FloatField(default=0)
    classifier_cost_recorded_turns = models.IntegerField(default=0)

    class Meta:
        db_table = "LiteLLM_AutoRouterDailySpend"


class LiteLLMShadowEvalJob(models.Model):
    id = models.TextField(primary_key=True, default=None)
    group_id = models.TextField()
    target_type = models.TextField(default="key")
    target_id = models.TextField()
    router_name = models.TextField()
    router_names = ArrayField(models.TextField(), default=list)
    models_field = ArrayField(models.TextField(), db_column="models", default=list)
    direction = models.TextField(default="forward")
    baseline_model = models.TextField(null=True)
    judge_model = models.TextField()
    shadow_percentage = models.FloatField()
    max_turns = models.IntegerField()
    max_budget = models.FloatField(null=True)
    created_at = UtcDateTimeField(default=timezone.now)
    created_by = models.TextField(null=True)
    ends_at = UtcDateTimeField()
    stopped_at = UtcDateTimeField(null=True)
    stopped_by = models.TextField(null=True)

    class Meta:
        db_table = "LiteLLM_ShadowEvalJob"
        indexes = [
            models.Index(fields=["group_id"]),
            models.Index(fields=["target_type", "target_id"]),
            models.Index(fields=["created_at"]),
        ]


class LiteLLMShadowEvalAttempt(models.Model):
    id = models.TextField(primary_key=True, default=None)
    job_id = models.TextField()
    request_id = models.TextField()
    outcome = models.TextField()
    router_name = models.TextField(null=True)
    tier = models.TextField(null=True)
    real_model = models.TextField(null=True)
    shadow_model = models.TextField(null=True)
    confidence = models.FloatField(null=True)
    judge_cost = models.FloatField(default=0)
    shadow_cost = models.FloatField(default=0)
    real_cost = models.FloatField(null=True)
    real_classifier_cost = models.FloatField(default=0)
    shadow_classifier_cost = models.FloatField(default=0)
    real_cache_hit = models.BooleanField(default=False)
    error = models.TextField(null=True)
    created_at = UtcDateTimeField(default=timezone.now)

    class Meta:
        db_table = "LiteLLM_ShadowEvalAttempt"
        indexes = [
            models.Index(fields=["job_id"]),
        ]


class LiteLLMShadowEvalFunnel(models.Model):
    job_id = models.TextField(primary_key=True, default=None)
    not_sampled = models.IntegerField(default=0)
    unjudgeable = models.IntegerField(default=0)
    shed = models.IntegerField(default=0)
    withheld = models.IntegerField(default=0)

    class Meta:
        db_table = "LiteLLM_ShadowEvalFunnel"


class LiteLLMWorkflowRun(models.Model):
    run_id = models.TextField(primary_key=True, default=uuid4_str)
    session_id = models.TextField(unique=True, default=uuid4_str)
    workflow_type = models.TextField()
    status = models.TextField(default="pending")
    created_by = models.TextField(null=True)
    created_at = UtcDateTimeField(default=timezone.now)
    updated_at = UtcDateTimeField(auto_now=True)
    input = models.JSONField(null=True)
    output = models.JSONField(null=True)
    metadata = models.JSONField(null=True)
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_WorkflowRun"
        indexes = [
            models.Index(fields=["workflow_type", "status"]),
            models.Index(fields=["session_id"]),
            models.Index(fields=["created_at"]),
            models.Index(fields=["created_by"]),
        ]


class LiteLLMWorkflowEvent(models.Model):
    event_id = models.TextField(primary_key=True, default=uuid4_str)
    run = models.ForeignKey("LiteLLMWorkflowRun", on_delete=models.RESTRICT, related_name="events", db_index=False)
    event_type = models.TextField()
    step_name = models.TextField()
    sequence_number = models.IntegerField()
    data = models.JSONField(null=True)
    created_at = UtcDateTimeField(default=timezone.now)

    class Meta:
        db_table = "LiteLLM_WorkflowEvent"
        constraints = [
            models.UniqueConstraint(fields=["run", "sequence_number"], name="LiteLLM_WorkflowEvent_run_id_sequence_number_key"),
        ]
        indexes = [
            models.Index(fields=["run"]),
        ]


class LiteLLMWorkflowMessage(models.Model):
    message_id = models.TextField(primary_key=True, default=uuid4_str)
    run = models.ForeignKey("LiteLLMWorkflowRun", on_delete=models.RESTRICT, related_name="messages", db_index=False)
    role = models.TextField()
    content = models.TextField()
    sequence_number = models.IntegerField()
    session_id = models.TextField(null=True)
    created_at = UtcDateTimeField(default=timezone.now)

    class Meta:
        db_table = "LiteLLM_WorkflowMessage"
        constraints = [
            models.UniqueConstraint(fields=["run", "sequence_number"], name="LiteLLM_WorkflowMessage_run_id_sequence_number_key"),
        ]
        indexes = [
            models.Index(fields=["run"]),
        ]


class LiteLLMBackgroundInteractionSettlement(models.Model):
    interaction_id = models.TextField(primary_key=True, default=None)
    custom_llm_provider = models.TextField()
    create_context = models.JSONField()
    created_at = UtcDateTimeField(default=timezone.now)
    claimed_at = UtcDateTimeField(null=True)
    claimed_by = models.TextField(null=True)
    settled_at = UtcDateTimeField(null=True)
    outcome = models.TextField(null=True)

    class Meta:
        db_table = "LiteLLM_BackgroundInteractionSettlement"
        indexes = [
            models.Index(fields=["claimed_at"]),
            models.Index(fields=["settled_at"]),
        ]


class LiteLLMBackgroundResponseSettlement(models.Model):
    response_id = models.TextField(primary_key=True, default=None)
    settled_at = UtcDateTimeField(default=timezone.now)

    class Meta:
        db_table = "LiteLLM_BackgroundResponseSettlement"


class LiteLLMLens(models.Model):
    id = models.TextField(primary_key=True, default=None)
    version = models.IntegerField(default=0)
    data = models.JSONField()
    due_at = UtcDateTimeField(null=True, db_default=RawSQL("'1970-01-01 00:00:00'::timestamp without time zone", []))

    class Meta:
        db_table = "LiteLLM_Lens"
        indexes = [
            models.Index(fields=["due_at"], name="LiteLLM_Lens_due_at_idx"),
        ]


class LiteLLMLensRun(models.Model):
    id = models.TextField(primary_key=True, default=None)
    lens_id = models.TextField()
    created_at = UtcDateTimeField()
    data = models.JSONField()

    class Meta:
        db_table = "LiteLLM_LensRun"
        indexes = [
            models.Index(fields=["lens_id", "created_at"]),
        ]


class LiteLLMLensReview(models.Model):
    pk = models.CompositePrimaryKey("lens_id", "criteria_key", "execution_id")
    lens = models.ForeignKey("LiteLLMLens", on_delete=models.CASCADE, related_name="reviews", db_index=False)
    criteria_key = models.TextField()
    execution_id = models.TextField()
    data = models.JSONField()

    class Meta:
        db_table = "LiteLLM_LensReview"


class LiteLLMLensWorker(models.Model):
    id = models.TextField(primary_key=True, default=None)
    token_hash = models.TextField(unique=True)
    data = models.JSONField()

    class Meta:
        db_table = "LiteLLM_LensWorker"


class LiteLLMLensIngestionKey(models.Model):
    id = models.TextField(primary_key=True, default=None)
    data = models.JSONField()

    class Meta:
        db_table = "LiteLLM_LensIngestionKey"


class LiteLLMLensDataset(models.Model):
    pk = models.CompositePrimaryKey("id", "revision")
    id = models.TextField()
    revision = models.IntegerField()
    created_at = UtcDateTimeField()
    data = models.JSONField()

    class Meta:
        db_table = "LiteLLM_LensDataset"


class LiteLLMLensSignalConfig(models.Model):
    id = models.TextField(primary_key=True, default=None)
    data = models.JSONField()

    class Meta:
        db_table = "LiteLLM_LensSignalConfig"


class LiteLLMLensTraceSignal(models.Model):
    pk = models.CompositePrimaryKey("trace_id", "trace_ref")
    trace_id = models.TextField()
    trace_ref = models.TextField(default="")
    config_key = models.TextField()
    span_count = models.IntegerField()
    claimed_until = UtcDateTimeField(null=True)
    classified_at = UtcDateTimeField(null=True)
    data = models.JSONField()

    class Meta:
        db_table = "LiteLLM_LensTraceSignal"


class LiteLLMTelemetryReport(models.Model):
    id = models.TextField(primary_key=True, default=None)
    window_start = models.FloatField()
    window_end = models.FloatField()
    report = models.JSONField()
    created_at = UtcDateTimeField(default=timezone.now)

    class Meta:
        db_table = "LiteLLM_TelemetryReport"
        indexes = [
            models.Index(fields=["window_end"]),
            models.Index(fields=["created_at"]),
        ]


class LiteLLMSpendLogDelivery(models.Model):
    delivery_id = models.TextField(primary_key=True, db_default=RawSQL("(gen_random_uuid())::text", []))
    request_id = models.TextField()
    state = models.TextField(choices=AccountingDeliveryState.choices, default=AccountingDeliveryState.pending)
    accounting_payload = models.JSONField()
    is_batch = models.BooleanField()
    attempts = models.IntegerField(default=0)
    next_attempt_at = UtcDateTimeField(default=timezone.now)
    applied_at = UtcDateTimeField(null=True)
    last_error = models.TextField(null=True)
    created_at = UtcDateTimeField(default=timezone.now)

    class Meta:
        db_table = "LiteLLM_SpendLogDelivery"
        indexes = [
            models.Index(fields=["state", "next_attempt_at"]),
            models.Index(fields=["state", "applied_at"]),
            models.Index(fields=["request_id"]),
        ]


class LiteLLMSubscriptionAccountTable(models.Model):
    subscription_account_id = models.TextField(primary_key=True, default=uuid4_str)
    custom_llm_provider = models.TextField()
    account_id = models.TextField()
    label = models.TextField(null=True)
    monthly_fee = models.FloatField()
    currency = models.TextField()
    billing_period_start = models.TextField()
    created_at = UtcDateTimeField(default=timezone.now)
    created_by = models.TextField()
    updated_at = UtcDateTimeField(auto_now=True)
    updated_by = models.TextField()
    objects = AutoNowQuerySet.as_manager()

    class Meta:
        db_table = "LiteLLM_SubscriptionAccountTable"
        constraints = [
            models.UniqueConstraint(fields=["custom_llm_provider", "account_id"], name="LiteLLM_SubscriptionAccountTable_provider_account_key"),
        ]
